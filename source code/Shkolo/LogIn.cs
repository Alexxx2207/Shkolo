using Shkolo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using System.Security.Cryptography;

namespace Shkolo
{
    public partial class LogIn : Form
    {
        private string connectionString = @"server=192.168.100.53;user=root;password=qazwsxedcrfvtgbyhnujm;database=shkolo";
        public LogIn()
        {
            InitializeComponent();
            label1.Parent = pictureBox1;    
            label2.Parent = pictureBox1;

            PasswordTextBox.UseSystemPasswordChar = true;
            PasswordEye.BackgroundImage = System.Drawing.Image.FromFile(@"D:\Programming\C# desktop app\Shkolo\Shkolo\Images\ImagesForControls\OpenedEye.png");

            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand command = new MySqlCommand(@"SELECT subject_name FROM shkolo.grade_types;", conn);

            foreach (string file in Directory.GetFiles(@"D:\Programming\C# desktop app\Shkolo\Shkolo\Models\"))
            {
                string item = Path.GetFileName(file).Remove(Path.GetFileName(file).Length - 3);
                if (item != nameof(Person))
                {
                    RoleChooser.Items.Add(item);
                }
            }

            RoleChooser.SelectedItem = RoleChooser.Items[0];

        }
        private void LogInButton_Click(object sender, EventArgs e)
        {
            bool validEmail = ValidateEmail(emailTextBox.Text);

            bool validPasssword = ValidatePassword(PasswordTextBox.Text);

            if (validEmail && validPasssword)
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    if (RoleChooser.SelectedItem.ToString() == nameof(Student))
                    {
                        MySqlCommand command = new MySqlCommand($@"SELECT * FROM students WHERE students.email = '{emailTextBox.Text}'", conn);
                        MySqlDataReader getPassword = command.ExecuteReader();

                        if (getPassword.Read() && Encoding.Default.GetString( HashPassword(GetSalt()) ) == Encoding.Default.GetString( (byte[])getPassword[6] ) ) 
                        {
                            getPassword.Close();

                            Person person = null;
                            var sqlReader = command.ExecuteReader();

                            sqlReader.Read();
                            person = new Student(sqlReader[2].ToString(), int.Parse(sqlReader[3].ToString()), sqlReader[4].ToString(), sqlReader[5].ToString(), (byte[])sqlReader[6], (byte[])sqlReader[7], int.Parse(sqlReader[0].ToString()), int.Parse(sqlReader[1].ToString()));


                            StudentHome home = new StudentHome(person);

                            home.Show();
                            Hide();
                        }
                        else
                        {
                            MessageBox.Show("Student not found!");

                        } 
                    }
                    else if (RoleChooser.SelectedItem.ToString() == nameof(Teacher))
                    {
                        MySqlCommand command = new MySqlCommand($@"SELECT * FROM teachers WHERE teachers.email = '{emailTextBox.Text}';", conn);
                        MySqlDataReader getPassword = command.ExecuteReader();

                        if (getPassword.Read() && Encoding.Default.GetString(HashPassword(GetSalt())) == Encoding.Default.GetString((byte[])getPassword[6]))
                        {
                            getPassword.Close();

                            Person person = null;
                            var sqlReader = command.ExecuteReader();

                            sqlReader.Read();
                            if (!string.IsNullOrEmpty(sqlReader[9].ToString()))
                            {
                                person = new Teacher(sqlReader[1].ToString(), int.Parse(sqlReader[3].ToString()), sqlReader[4].ToString(), sqlReader[5].ToString(), (byte[])sqlReader[6], (byte[])sqlReader[7], int.Parse(sqlReader[0].ToString()), int.Parse(sqlReader[2].ToString()), int.Parse(sqlReader[9].ToString())); 
                            }
                            else
                            {
                                person = new Teacher(sqlReader[1].ToString(), int.Parse(sqlReader[3].ToString()), sqlReader[4].ToString(), sqlReader[5].ToString(), (byte[])sqlReader[6], (byte[])sqlReader[7], int.Parse(sqlReader[0].ToString()), int.Parse(sqlReader[2].ToString()));

                            }

                            TeacherHome home = new TeacherHome(person);

                            home.Show();
                            Hide();
                        }
                        else
                        {
                            MessageBox.Show("Teacher not found!");

                        }
                    }
                    else if (RoleChooser.SelectedItem.ToString() == nameof(Administrator))
                    {
                        MySqlCommand command = new MySqlCommand($@"SELECT * FROM administration WHERE administration.email = '{emailTextBox.Text}';", conn);
                        MySqlDataReader getPassword = command.ExecuteReader();

                       

                        if (getPassword.Read() && Encoding.Default.GetString(HashPassword(GetSalt())) == Encoding.Default.GetString((byte[])getPassword[5]))
                        {
                            getPassword.Close();
                            Person person = null;
                            var sqlReader = command.ExecuteReader();

                            sqlReader.Read();
                            person = new Administrator(sqlReader[1].ToString(), int.Parse(sqlReader[2].ToString()), sqlReader[3].ToString(), sqlReader[4].ToString(), (byte[])sqlReader[5], (byte[])sqlReader[6]);

                            AdminHome home = new AdminHome(person);

                            home.Show();
                            Hide();
                        }
                        else
                        {
                            MessageBox.Show("Administrator not found!");

                        }
                    }
                    conn.Close();
                }
            }
            
            
        }
        private bool ValidateEmail(string email)
        {
           if(!Regex.IsMatch(email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
            {
                emailErrorLabel.Text = "Invalid email address!";
                emailErrorLabel.Visible = true;
                return false;

            }
            else
            {
                emailErrorLabel.Visible = false;
                return true;
            }
        }
        private bool ValidatePassword(string password)
        {
            if (password.Length < 5 || password.Length > 40)
            {
                PasswordErrorLabel.Text = "Password must be between 5 and 40 symbols!";
                PasswordErrorLabel.Visible = true;
                return false;

            }
            else
            {
                PasswordErrorLabel.Visible = false;
                return true;

            }
        }

        private void PasswordEye_Click(object sender, EventArgs e)
        {
            if (PasswordTextBox.UseSystemPasswordChar)
            {
                PasswordTextBox.UseSystemPasswordChar = false;
                PasswordEye.BackgroundImage = System.Drawing.Image.FromFile(@"D:\Programming\C# desktop app\Shkolo\Shkolo\Images\ImagesForControls\ClosedEye.png");

            }
            else
            {
                PasswordTextBox.UseSystemPasswordChar = true;
                PasswordEye.BackgroundImage = System.Drawing.Image.FromFile(@"D:\Programming\C# desktop app\Shkolo\Shkolo\Images\ImagesForControls\OpenedEye.png");

            }
        }

        private void SignUpButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            SignUp signUp = new SignUp();
            signUp.Show();
        }

        private void emailTextBox_TextChanged(object sender, EventArgs e)
        {
            ValidateEmail(emailTextBox.Text);
        }

        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            ValidatePassword(PasswordTextBox.Text);
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private byte[] GetSalt()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            MySqlCommand command;

            if(RoleChooser.SelectedItem.ToString() == nameof(Student))
                command = new MySqlCommand($@"SELECT salt FROM students WHERE students.email = '{emailTextBox.Text}';", conn);
            else if(RoleChooser.SelectedItem.ToString() == nameof(Teacher))
                command = new MySqlCommand($@"SELECT salt FROM teachers WHERE email = '{emailTextBox.Text}';", conn);
            else
                command = new MySqlCommand($@"SELECT salt FROM administration WHERE email = '{emailTextBox.Text}';", conn);

            conn.Open();
            return (byte[])command.ExecuteScalar();
        }

        private byte[] HashPassword(byte[] salt)
        {
            if (salt != null)
            {
                Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(PasswordTextBox.Text, salt, 100000);
                return pbkdf2.GetBytes(32); 
            }
            else
            {
                return new byte[0];
            }
        }

    }
}
