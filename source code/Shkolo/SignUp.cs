using Shkolo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using Konscious.Security.Cryptography;

namespace Shkolo
{
    public partial class SignUp : Form
    {
        private readonly string connectionString = BuildConnectionString();
        
        public SignUp()
        {
            InitializeComponent();
            ClassIDErrorLabel.Text = "Choose class!";

            foreach (string file in Directory.GetFiles(@"D:\Programming\C# desktop app\Shkolo\Shkolo\Models\"))
            {
                string item = Path.GetFileName(file).Remove(Path.GetFileName(file).Length - 3);
                if (item != nameof(Person))
                {
                    RoleChooser.Items.Add(item);
                }
            }

            RoleChooser.SelectedItem = RoleChooser.Items[0];

            FullNameErrorLable.Text = "Invalid full name!";
            FileDialog1.FileName = @"D:\Programming\C# desktop app\Shkolo\Shkolo\Images\defaultUserImg.jpg";

            Password.UseSystemPasswordChar = true;
            PasswordEye.BackgroundImage = System.Drawing.Image.FromFile(@"D:\Programming\C# desktop app\Shkolo\Shkolo\Images\ImagesForControls\OpenedEye.png");

            DateTimePicker.Value = DateTimePicker.MinDate;

            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand command = new MySqlCommand(@"SELECT subject_name FROM shkolo.grade_types;", conn);

            conn.Open();
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                SubjectCombobox.Items.Add(reader[0].ToString());
            }
            reader.Dispose();


            MySqlCommand getClasses = new MySqlCommand(@"SELECT class_name FROM classes;", conn);

            reader = getClasses.ExecuteReader();

            while (reader.Read())
            {
                ClassesComboBox.Items.Add(reader[0].ToString());
            }

            conn.Close();

        }

        private void PasswordEye_Click(object sender, EventArgs e)
        {
            if (Password.UseSystemPasswordChar)
            {
                Password.UseSystemPasswordChar = false;
                PasswordEye.BackgroundImage = System.Drawing.Image.FromFile(@"D:\Programming\C# desktop app\Shkolo\Shkolo\Images\ImagesForControls\ClosedEye.png");

            }
            else
            {
                Password.UseSystemPasswordChar = true;
                PasswordEye.BackgroundImage = System.Drawing.Image.FromFile(@"D:\Programming\C# desktop app\Shkolo\Shkolo\Images\ImagesForControls\OpenedEye.png");

            }
        }
        private void SignUpButton_Click(object sender, EventArgs e)
        { 
            BirthDateErrorLabel.Visible = false;
            ClassIDErrorLabel.Visible = false;

            bool validEmail = ValidateEmail(Email.Text);
            bool validPasssword = ValidatePassword(Password.Text);
            bool validFullName = ValidateFullName(FullName.Text);
            bool validBirthDate = DateTimePicker.Value != DateTimePicker.MinDate;
            bool validImage = ValidateImage(FileDialog1.FileName);

            int age = DateTime.Now.Year - DateTimePicker.Value.Year - (DateTime.Now.DayOfYear < DateTimePicker.Value.DayOfYear ? 1 : 0);
            bool validAge = ValidateAge(age);

            if (!validFullName)
            {
                FullNameErrorLable.Visible = true;

            }
            
            if (!validPasssword)
            {
                PasswordErrorLabel.Visible = true;

            }
            if (!validAge)
            {
                BirthDateErrorLabel.Visible = true;

            }

            if (!validEmail)
            {
                EmailErrorLabel.Visible = true;
            }

            if (!validBirthDate)
            {
                BirthDateErrorLabel.Visible = true;
            }

            if (!validImage)
            {
                InvalidImageErrorLabel.Visible = true;
            }

            if (SubjectCombobox.SelectedItem == null && RoleChooser.SelectedItem.ToString() == nameof(Teacher))
            {
                SubjectErrorLabel.Visible = true;
            }

            if (validEmail && validPasssword && validAge && validFullName && validImage)
            {
                Person person = null;

                /// 
                var salt = CreateSalt();
                byte[] hash = HidePass(salt);
                ///


                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    if (RoleChooser.SelectedItem.ToString() == nameof(Student))
                    {
                        Random rand = new Random();
                        int studentID = rand.Next(1, 999999999);

                        while (!ValidateStudentId(studentID))
                        {
                            studentID = rand.Next(1, 999999999);
                        }

                        if (ClassesComboBox.SelectedItem != null)
                        {
                            person = new Student(FullName.Text, age, DateTimePicker.Value.ToString().Split(' ')[0], Email.Text, hash, ConvertImage(FileDialog1.FileName), studentID, FindClassIdByName());
                            MySqlCommand command = new MySqlCommand($@"INSERT INTO students(student_id, class_id, full_name, age, birth_date, email, passwordd, profile_image, salt,profile_color) VALUES ({((Student)person).StudentID}, {((Student)person).ClassID}, '{person.FullName}', {person.Age}, '{person.BirthDate}', '{person.Email}', @pass, @img, @salt, '{"255, 162, 0"}');", conn);
                            MySqlCommand getStudentsCount = new MySqlCommand($@"SELECT * FROM students WHERE class_id = {((Student)person).ClassID};", conn);


                            command.Parameters.Add(new MySqlParameter("@img", person.Image));
                            command.Parameters.Add(new MySqlParameter("@salt", salt));
                            command.Parameters.Add(new MySqlParameter("@pass", person.Password));

                            command.ExecuteNonQuery();
                            int count = 0;

                            MySqlDataReader reader = getStudentsCount.ExecuteReader();

                            while (reader.Read())
                            {
                                count++;
                            }

                            MySqlCommand incrementStuentsCountInClass = new MySqlCommand($@"UPDATE classes SET classes.number_of_students = {count}  WHERE class_id = {((Student)person).ClassID};", conn);
                            reader.Close();


                            incrementStuentsCountInClass.ExecuteNonQuery();

                            this.Hide();
                            StudentHome home = new StudentHome(person);
                            home.Show();
                        }
                        else
                        {
                            ClassIDErrorLabel.Visible = true;
                        }

                        
                    }
                    else if (RoleChooser.SelectedItem.ToString() == nameof(Teacher))
                    {
                        Random rand = new Random();
                        int teacherID = rand.Next(1, 999999999);

                        while (!ValidateTeacherId(teacherID))
                        {
                            teacherID = rand.Next(1, 999999999);
                        }

                        bool validSubject = SubjectCombobox.SelectedItem != null;

                        if (validSubject)
                        {

                            SubjectErrorLabel.Visible = false;
                            if (ClassesComboBox.SelectedItem != null)
                            {
                                person = new Teacher(FullName.Text, age, DateTimePicker.Value.ToString().Split(' ')[0], Email.Text, hash, ConvertImage(FileDialog1.FileName), teacherID, ConvertSubjectNameToSubjectId(), FindClassIdByName());

                                MySqlCommand command = new MySqlCommand($@"INSERT INTO teachers(teacher_id, teacher_name, subject_id, age, birth_date, email, passwordd, profile_image, class_id, salt, profile_color) VALUES ({teacherID},'{person.FullName}', '{((Teacher)person).Subject}', {person.Age}, '{person.BirthDate}', '{person.Email}', @pass, @img, {((Teacher)person).Class_Id}, @salt, '{"255, 162, 0"}');", conn);
                                command.Parameters.Add(new MySqlParameter("@img", person.Image));
                                command.Parameters.Add(new MySqlParameter("@pass", person.Password));
                                command.Parameters.Add(new MySqlParameter("@salt", salt));



                                command.ExecuteNonQuery();
                            }
                            else
                            {
                                person = new Teacher(FullName.Text, age, DateTimePicker.Value.ToString().Split(' ')[0], Email.Text, hash, ConvertImage(FileDialog1.FileName), teacherID, ConvertSubjectNameToSubjectId());

                                MySqlCommand command = new MySqlCommand($@"INSERT INTO teachers(teacher_id, teacher_name, subject_id, age, birth_date, email, passwordd, profile_image,salt, profile_color) VALUES ({teacherID},'{person.FullName}', '{((Teacher)person).Subject}', {person.Age}, '{person.BirthDate}', '{person.Email}', @pass, @img, @salt, '{"255, 162, 0"}');", conn);
                                command.Parameters.Add(new MySqlParameter("@img", person.Image));
                                command.Parameters.Add(new MySqlParameter("@salt", salt));
                                command.Parameters.Add(new MySqlParameter("@pass", person.Password));

                                command.ExecuteNonQuery();
                            }

                            this.Hide();
                            TeacherHome home = new TeacherHome(person);
                            home.Show();

                        }
                        else
                            SubjectErrorLabel.Visible = true;
                    }
                    else if (RoleChooser.SelectedItem.ToString() == nameof(Administrator))
                    {
                        Random rand = new Random();
                        int adminID = rand.Next(1, 999999999);

                        while (!ValidateAdminId(adminID))
                        {
                            adminID = rand.Next(1, 999999999);
                        }

                        person = new Administrator(FullName.Text, age, DateTimePicker.Value.ToString().Split(' ')[0], Email.Text, hash, ConvertImage(FileDialog1.FileName));
                        MySqlCommand command = new MySqlCommand($@"INSERT INTO administration(id, namee, age, birth_date, email, passwordd, profile_image,salt, profile_color) VALUES ({adminID},'{person.FullName}', {person.Age}, '{person.BirthDate}', '{person.Email}', @pass, @img, @salt, '{"255, 162, 0"}');", conn);
                        command.Parameters.Add(new MySqlParameter("@img", person.Image));
                        command.Parameters.Add(new MySqlParameter("@salt", salt));
                        command.Parameters.Add(new MySqlParameter("@pass", person.Password));

                        command.ExecuteNonQuery();

                        this.Hide();
                        AdminHome home = new AdminHome(person);
                        home.Show();
                    }
                    conn.Close();
                }
                

            }
        }

        private bool ValidateTeacherId(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand command = new MySqlCommand($@"SELECT teacher_id fROM teachers WHERE teachers.teacher_id = {id};", conn);
                if (command.ExecuteScalar() != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
                conn.Close();
            }
        }
        private bool ValidateAdminId(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand command = new MySqlCommand($@"SELECT id FROM administration WHERE administration.id = {id};", conn);
                if (command.ExecuteScalar() != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
                conn.Close();
            }
        }
        private bool ValidateStudentId(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand command = new MySqlCommand($@"SELECT student_id fROM students WHERE students.student_id = {id};", conn);
                if (command.ExecuteScalar() != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
                conn.Close();
            }
        }
        private bool ValidateFullName(string fullname)
        {
            if (!string.IsNullOrWhiteSpace(fullname) && fullname.Split().Length == 3
                && fullname.Split(' ')[2].Length > 1 && char.IsUpper(fullname.Split(' ')[2][0])
                && fullname.Split(' ')[1].Length > 1 && char.IsUpper(fullname.Split(' ')[1][0])
                && fullname.Split(' ')[0].Length > 1 && char.IsUpper(FullName.Text.Split(' ')[0][0]))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        private bool ValidateEmail(string email)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand command = new MySqlCommand($@"SELECT email FROM students WHERE email='{email}';", conn);
                if (command.ExecuteScalar() != null)
                {
                    EmailErrorLabel.Text = "This email address is already used!";
                    EmailErrorLabel.Visible = true;

                    return false;
                }

                conn.Close();
            }
            

            if (!Regex.IsMatch(email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
            {
                EmailErrorLabel.Text = "Invalid email address!";
                return false;

            }
            else
            {
                EmailErrorLabel.Visible = false;
                return true;
            }
        }
        private bool ValidatePassword(string password)
        {
            if (password.Length < 5 || password.Length > 40)
            {
                PasswordErrorLabel.Text = "Password must be between 5 and 40 symbols!";
                return false;

            }
            else
            {
                return true;

            }
        }
        private bool ValidateAge(int age)
        {
            if (age < 0)
            {
                BirthDateErrorLabel.Text = "Invalid year!";
                BirthDateErrorLabel.Left = 180;
                return false;
            }
            else if (RoleChooser.SelectedItem.ToString() == nameof(Student))
            {
                if (age < 7 || age > 18)
                {
                    BirthDateErrorLabel.Text = "Age must be from 7 to 18! [for students]";
                    BirthDateErrorLabel.Left = 50;

                    return false;

                }
                else
                {
                    return true;

                }
            }
            else
            {
                if (age < 18 || age > 70)
                {
                    if (RoleChooser.SelectedItem.ToString() == nameof(Teacher))
                    {
                        BirthDateErrorLabel.Text = "Age must be from 18 to 70! [for teachers]";
                    }
                    else
                    { 
                        BirthDateErrorLabel.Text = "Age must be from 18 to 70! [for Admins]";

                    }
                    BirthDateErrorLabel.Left = 50;

                    return false;

                }
                else
                {
                    return true;

                }
            }
        }
        private bool ValidateImage(string fileName)
        {
            try
            {
                Bitmap bmp = new Bitmap(fileName);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private void LogInButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            LogIn logIn = new LogIn();
            logIn.Show();
        }

        private void DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            DateTimeLabel.Text = DateTimePicker.Value.ToString().Split(' ')[0];
            ValidateAge(DateTime.Now.Year - DateTimePicker.Value.Year - (DateTime.Now.DayOfYear < DateTimePicker.Value.DayOfYear ? 1 : 0));
        }

        private void FullName_TextChanged(object sender, EventArgs e)
        {
            if (ValidateFullName(FullName.Text))
            {
                FullNameErrorLable.Visible = false;

            }
            else
            {
                FullNameErrorLable.Visible = true;

            }
        }


        private void Password_TextChanged(object sender, EventArgs e)
        {
            if (ValidatePassword(Password.Text))
            {
                PasswordErrorLabel.Visible = false;
            }
            else
            {
                PasswordErrorLabel.Visible = true;
            }
        }

        private void Email_TextChanged(object sender, EventArgs e)
        {
            if (ValidateEmail(Email.Text))
            {
                EmailErrorLabel.Visible = false;
            }
            else
            {
                EmailErrorLabel.Visible = true;
            }
        }

        private byte[] ConvertImage(string fileName)
        { 

            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            
            byte[] img = br.ReadBytes((int)fs.Length);

            return img;
        }

        private void ChooseImageButton_Click(object sender, EventArgs e)
        {
            FileDialog1.ShowDialog();
        }

        private void FileDialog_FileOk(object sender, CancelEventArgs e)
        {
            if (ValidateImage(FileDialog1.FileName))
            {
                InvalidImageErrorLabel.Visible = false;
                ImagePreview.Image = new Bitmap(FileDialog1.FileName);
            }
            else
            {
                InvalidImageErrorLabel.Visible = true;
                ImagePreview.Image = null;

            }
        }

        private void LeaveEmptyImageButton_Click(object sender, EventArgs e)
        {
            FileDialog1.FileName = @"D:\Programming\C# desktop app\Shkolo\Shkolo\Images\defaultUserImg.jpg";
            ImagePreview.Image = new Bitmap(FileDialog1.FileName);
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SubjectCombobox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (SubjectCombobox.SelectedItem != null)
                SubjectErrorLabel.Visible = false;
            else
                SubjectErrorLabel.Visible = true;
        }

        private int ConvertSubjectNameToSubjectId()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand command = new MySqlCommand(@"SELECT * FROM shkolo.grade_types;", conn);

            conn.Open();

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader[1].ToString() == SubjectCombobox.SelectedItem.ToString())
                { 
                    return int.Parse(reader[0].ToString());
                }
            }

            conn.Close();

            return -1;
        }

        private void RoleChooser_SelectedChanged(object sender, EventArgs e)
        {
            if (RoleChooser.SelectedItem.ToString() == nameof(Teacher))
            {
                SubjectLabel.Visible = true;
                SubjectCombobox.Visible = true;
                SubjectErrorLabel.Visible = true;
                ClassIDLabel.Visible = true;
                ClassesComboBox.Visible = true;

            }
            else if(RoleChooser.SelectedItem.ToString() == nameof(Administrator))
            {
                SubjectLabel.Visible = false;
                SubjectCombobox.Visible = false;
                SubjectCombobox.SelectedItem = null;
                SubjectCombobox.Text = "";
                SubjectErrorLabel.Visible = false;
                ClassIDErrorLabel.Visible = false;
                ClassIDLabel.Visible = false;
                ClassesComboBox.Visible = false;
                
            }
            else if (RoleChooser.SelectedItem.ToString() == nameof(Student))
            {
                SubjectLabel.Visible = false;
                SubjectCombobox.Visible = false;
                SubjectCombobox.SelectedItem = null;
                SubjectCombobox.Text = "";
                SubjectErrorLabel.Visible = false;
                ClassIDLabel.Visible = true;
                ClassesComboBox.Visible = true;

            }
        }

        private byte[] CreateSalt()
        {
            var salt = new byte[32];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(salt);

            return salt;
        }

        private byte[] HidePass(byte[] salt)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(Password.Text, salt, 100000);
            return pbkdf2.GetBytes(32);
        }

        private int FindClassIdByName()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand find = new MySqlCommand($@"SELECT class_id FROM classes WHERE class_name = '{ClassesComboBox.SelectedItem.ToString()}'", conn);
            conn.Open();

            string result = find.ExecuteScalar().ToString();

            conn.Close();

            return int.Parse(result);
        }
        private static string BuildConnectionString()
        {
            StringBuilder sb = new StringBuilder();
            StreamReader stream = new StreamReader(@"..\..\..\ExportedDB\connectionStringParameters.ini");

            while (!stream.EndOfStream)
            {
                sb.Append(stream.ReadLine() + ";");
            }

            return sb.ToString();
        }
    }
}
