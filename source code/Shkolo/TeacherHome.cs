using Shkolo.Models;
using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Shkolo
{
    public partial class TeacherHome : Form
    {
        private Teacher teacher = null;
        private string connectionString = @"server=192.168.100.53;username=root;password=qazwsxedcrfvtgbyhnujm;database=shkolo";

        private int gradeSelected = -1;
        public TeacherHome(Person person)
        {
            InitializeComponent();
            this.ControlBox = false;

            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand getPersonID = new MySqlCommand($@"SELECT teacher_id FROM teachers WHERE email = '{person.Email}';", conn);
            conn.Open();
            person.ID = long.Parse(getPersonID.ExecuteScalar().ToString());
            conn.Close();

            teacher = (Teacher)person;

            //Profile Inialization
            MySqlCommand command = new MySqlCommand($@"SELECT class_name FROM classes WHERE class_id = {teacher.Class_Id};", conn);
            MySqlCommand getColor = new MySqlCommand($@"SELECT profile_color FROM teachers WHERE email = '{person.Email}';", conn);

            conn.Open();

            if (command.ExecuteScalar() != null)
            {
                ClassLabel.Text = command.ExecuteScalar().ToString();
            }
            else 
            {
                ClassLabel.Text = "None";
            }


            FullNameLabel.Text = person.FullName;
            AgeLabel.Text += person.Age;
            BirthDateLabel.Text += person.BirthDate;
            EmailLabel.Text = person.Email;
            SubjectLabel.Text += ConvertSubjectIdToSubjectName();

            ovalPictureBox1.Image = Image.FromStream(new MemoryStream(person.Image));


            FullNameLabel.Location = new Point(ovalPictureBox1.Location.X - (FullNameLabel.Width / 2 - ovalPictureBox1.Width / 2), FullNameLabel.Location.Y);
            EmailLabel.Location = new Point(ovalPictureBox1.Location.X - (EmailLabel.Width / 2 - ovalPictureBox1.Width / 2), EmailLabel.Location.Y);
            ClassDrawingBorder.Width = ClassLabel.Width + 5;

            int[] rgbValues = getColor.ExecuteScalar().ToString().Split(',').Select(x => x.Trim()).Select(int.Parse).ToArray();
            ColorPickerButton.FillColor = Color.FromArgb(rgbValues[0], rgbValues[1], rgbValues[2]);
            MainBackgroundInProfilePanel.BackColor = Color.FromArgb(rgbValues[0], rgbValues[1], rgbValues[2]);
            ChangeProfilePicButton.ForeColor = Color.FromArgb(rgbValues[0], rgbValues[1], rgbValues[2]);
            FullNameLabel.BackColor = Color.FromArgb(rgbValues[0], rgbValues[1], rgbValues[2]); 
            EmailLabel.BackColor = Color.FromArgb(rgbValues[0], rgbValues[1], rgbValues[2]);

            ChangeBorderByPickedColor(ColorPickerButton);

            conn.Close();

            command = new MySqlCommand(@"SELECT class_name FROM classes;",conn);

            conn.Open();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                classesComboBox.Items.Add(reader[0].ToString());
            }
            reader.Close();


            if (ClassLabel.Text != "None")
            { 
                MySqlCommand getAverageGrade = new MySqlCommand($@"SELECT average_grade FROM classes WHERE class_id = {teacher.Class_Id};", conn);
                ClassAverageGradeLabel.Text += getAverageGrade.ExecuteScalar().ToString();
            }
            else
            {
                ClassAverageGradeLabel.Text += "No class!";
            }
            conn.Close();
        }
        private void GradesMenuItem_Click(object sender, EventArgs e)
        {
            profilePanel.Visible = false;
            gradesPanel.Visible = true;
            eventsPanel.Visible = false;
        }
        private void ProfileMenuItem_Click(object sender, EventArgs e)
        {
            profilePanel.Visible = true;
            gradesPanel.Visible = false;
            eventsPanel.Visible = false;
        }
        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private int ClassIdByName()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand command = new MySqlCommand($@"SELECT class_id FROM classes WHERE class_name = '{classesComboBox.SelectedItem}';", conn);

            conn.Open();

            return int.Parse(command.ExecuteScalar().ToString());

            conn.Close();
        }

        private void Grade2Button_Click(object sender, EventArgs e)
        {
            gradeSelected = 2;
            GradeSelected.Text = "2";
            AddButton.Visible = true;
            RemoveButton.Visible = true;

        }

        private void Grade3Button_Click(object sender, EventArgs e)
        {
            gradeSelected = 3;
            GradeSelected.Text = "3";
            AddButton.Visible = true;
            RemoveButton.Visible = true;

        }

        private void Grade4Button_Click(object sender, EventArgs e)
        {
            gradeSelected = 4;
            GradeSelected.Text = "4";
            AddButton.Visible = true;
            RemoveButton.Visible = true;

        }

        private void Grade5Button_Click(object sender, EventArgs e)
        {
            gradeSelected = 5;
            GradeSelected.Text = "5";
            AddButton.Visible = true;
            RemoveButton.Visible = true;

        }

        private void Grade6Button_Click(object sender, EventArgs e)
        {
            gradeSelected = 6;
            GradeSelected.Text = "6";
            AddButton.Visible = true;
            RemoveButton.Visible = true;

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            int gradeID = rand.Next(1, 999999999);

            while (!ValidateGradeId(gradeID))
            {
                gradeID = rand.Next(1, 999999999);
            }

            List<int> students = new List<int>();
            double average = 0;
            int count = 0;

            MySqlConnection conn = new MySqlConnection(connectionString);

            MySqlCommand command = new MySqlCommand($@"INSERT INTO grades(grade_id,student_id, grade_type, grade) VALUES ({gradeID},{int.Parse(StudentsComboBox.SelectedItem.ToString().Split('-')[1].Trim())}, {teacher.Subject}, {gradeSelected});", conn);
            conn.Open();
            command.ExecuteNonQuery();

            MySqlCommand getStudentsFromTheClass = new MySqlCommand($@"SELECT student_id FROM students WHERE class_id = {FindClassIdByName()};", conn);
            MySqlDataReader reader = getStudentsFromTheClass.ExecuteReader();

            while (reader.Read())
            {
                students.Add(int.Parse(reader[0].ToString()));
            }
            reader.Close();

            foreach (var id in students)
            {
                MySqlCommand getGradesFromCurrentStudent = new MySqlCommand($@"SELECT grade FROM grades WHERE student_id = {id};", conn);
                reader = getGradesFromCurrentStudent.ExecuteReader();

                while (reader.Read())
                {
                    average += double.Parse(reader[0].ToString());
                    count++;
                }
                reader.Close();

            }

            average /= count;

            MySqlCommand updateClassesTable = new MySqlCommand($@"UPDATE classes SET average_grade = {average} WHERE class_id = {FindClassIdByName()}", conn);

            updateClassesTable.ExecuteNonQuery();

            conn.Close();
            RenderGrades();

        }

        private bool ValidateGradeId(int gradeID)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand command = new MySqlCommand($@"SELECT grade_id FROM grades WHERE grade_id = {gradeID};", conn);
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

        private void classesComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            StudentGrades.Text = "";

            AddButton.Visible = false;
            RemoveButton.Visible = false;

            StudentsComboBox.Items.Clear();
            if (classesComboBox.SelectedItem != null)
            {

                studentsNameLabel.Visible = true;
                StudentsComboBox.Visible = true;
                StudentsComboBox.SelectedItem = null;
                StudentsComboBox.Text = null;
                GradeSelected.Text = ".";
                gradeSelected = -1;

                selectedGradeLabel.Visible = false;
                GradeSelected.Visible = false;
                GradeLabel.Visible = false;
                Grade2Button.Visible = false;
                Grade3Button.Visible = false;
                Grade4Button.Visible = false;
                Grade5Button.Visible = false;
                Grade6Button.Visible = false;

                MySqlConnection conn = new MySqlConnection(connectionString);

                MySqlCommand command = new MySqlCommand($@"SELECT full_name, student_id FROM students WHERE class_id = {ClassIdByName()}", conn);

                conn.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    StudentsComboBox.Items.Add(reader[0].ToString() + " - " + reader[1].ToString());
                }
                conn.Close();

            }
            else
            {
                StudentsComboBox.SelectedItem = null;
                studentsNameLabel.Visible = false;
                StudentsComboBox.Visible = false;

                
            }
        }

        private void StudentsComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            RenderGrades();

            AddButton.Visible = false;
            RemoveButton.Visible = false;

            GradeSelected.Text = ".";
            gradeSelected = -1;
            if (StudentsComboBox.SelectedItem == null)
            {
                selectedGradeLabel.Visible = false;
                GradeSelected.Visible = false;
                GradeLabel.Visible = false;
                Grade2Button.Visible = false;
                Grade3Button.Visible = false;
                Grade4Button.Visible = false;
                Grade5Button.Visible = false;
                Grade6Button.Visible = false;
            }
            else
            {
                selectedGradeLabel.Visible = true;
                GradeSelected.Visible = true;
                GradeLabel.Visible = true;
                Grade2Button.Visible = true;
                Grade3Button.Visible = true;
                Grade4Button.Visible = true;
                Grade5Button.Visible = true;
                Grade6Button.Visible = true;
            }
        }

        private string ConvertSubjectIdToSubjectName()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand command = new MySqlCommand(@"SELECT * FROM shkolo.grade_types;", conn);

            conn.Open();

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader[0].ToString() == teacher.Subject.ToString())
                {
                    return reader[1].ToString();
                }
            }

            conn.Close();

            return "";
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            int grade_id = FindFirstGradeByGradeTypeAndStudentId();
            conn.Open();
            if (grade_id != -1)
            {
                MySqlCommand command = new MySqlCommand($@"DELETE FROM grades WHERE grade_id = {grade_id}", conn);

                command.ExecuteNonQuery();

                RenderGrades();
            }
            else
            {
                MessageBox.Show($"The student doesn't have {gradeSelected}!");
            }
            conn.Close();

            
        }

        private int FindFirstGradeByGradeTypeAndStudentId()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand($@"SELECT grade_id FROM grades WHERE student_id = { int.Parse(StudentsComboBox.SelectedItem.ToString().Split('-')[1].Trim()) } AND grade = {gradeSelected} AND grade_type = {teacher.Subject};", conn);

                MySqlDataReader reader = command.ExecuteReader();

                int result = -1;

                while (reader.Read())
                {
                    result =  int.Parse(reader[0].ToString());
                }

                conn.Close();
                return result;
            }

        }
        private void RenderGrades()
        {
            StudentGrades.Controls.Clear();

            MySqlConnection conn = new MySqlConnection(connectionString);

            MySqlCommand getGrades = new MySqlCommand($@"SELECT grade FROM shkolo.grades WHERE student_id = {int.Parse(StudentsComboBox.SelectedItem.ToString().Split('-')[1].Trim())} AND grade_type = {teacher.Subject};", conn);

            conn.Open();

            MySqlDataReader reader = getGrades.ExecuteReader();

            List<int> grades = new List<int>();

            int count = 0;
            int spaceBetweenButtons = 8;

            while (reader.Read())
            {
                grades.Add(int.Parse(reader[0].ToString()));
            }
            conn.Close();

            grades.Sort();

            foreach (var grade in grades)
            {
                Button button = BuildButtonForAGrade(grade);
                StudentGrades.Controls.Add(button);
                button.Location = new Point(spaceBetweenButtons * count + button.Width * count, 2);
                count++;
            }
        }
        private void EventsMenuItem_Click(object sender, EventArgs e)
        {
            profilePanel.Visible = false;
            gradesPanel.Visible = false;
            eventsPanel.Visible = true;

            NewInvitesPanel.Visible = false;
            AcceptedInvitesPanel.Visible = false;
            MenuPanel.Visible = true;

        }

        private void BackButtonInNewinvitesPanel_Click(object sender, EventArgs e)
        {
            MenuPanel.Visible = true;
            NewInvitesPanel.Visible = false;

        }

        private string getOrganiserByID(long Id)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand($@"SELECT namee FROM administration WHERE id = {Id}", conn);

            conn.Open();
            return cmd.ExecuteScalar().ToString();
        }

        private void RenderInvitesInNewInvites()
        {
            List<int> eventInvites = new List<int>();
            List<Panel> eventsPanels = new List<Panel>();



            MySqlConnection conn = new MySqlConnection(connectionString);

            conn.Open();
            MySqlCommand getInvites = new MySqlCommand($@"SELECT event_id FROM event_invites WHERE statuss = 'hold' AND person_id = {teacher.ID};", conn);

            MySqlDataReader readInvites = getInvites.ExecuteReader();

            int count = 0;
            while (readInvites.Read())
            {
                eventInvites.Add(int.Parse(readInvites[0].ToString()));
            }


            readInvites.Close();

            for (int i = 0; i < eventInvites.Count; i++)
            {
                MySqlCommand getEvent = new MySqlCommand($@"SELECT * FROM eventss WHERE id={eventInvites[i]}", conn);
                MySqlDataReader readEvent = getEvent.ExecuteReader();

                readEvent.Read();

                FontFamily fontFamily = new FontFamily("Arial Rounded MT Bold");

                Label title = new Label
                {
                    Text = readEvent[2].ToString() + " " + readEvent[3].ToString(),
                    Font = new Font(fontFamily, 16f, FontStyle.Regular),
                };

                Label duration = new Label
                {
                    Text = "Duration:  " + readEvent[4].ToString() + " min",
                    Font = new Font(fontFamily, 11.25f, FontStyle.Regular)
                };

                Label organiser = new Label
                {
                    Text = "Organiser:  " + getOrganiserByID(long.Parse(readEvent[1].ToString())),
                    Font = new Font(fontFamily, 11.25f, FontStyle.Regular)
                };

                Label online_onfline = new Label
                {
                    Text = readEvent[5].ToString(),
                    Font = new Font(fontFamily, 11.25f, FontStyle.Regular)
                };

                RichTextBox description = new RichTextBox
                {
                    Text = readEvent[6].ToString(),
                    Font = new Font(fontFamily, 11.25f, FontStyle.Regular),
                    WordWrap = true,
                    BackColor = Color.FromArgb(204, 204, 204),
                    BorderStyle = BorderStyle.None,
                    ReadOnly = true,
                };

                PictureBox acceptButton = new PictureBox
                {
                    Image = Image.FromFile(@"D:\Programming\C# desktop app\Shkolo\Shkolo\Images\ImagesForControls\TickButton.png"),
                    Width = 35,
                    Height = 35,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Cursor = Cursors.Hand,
                    Name = "s" + readEvent[0].ToString()
                };
                PictureBox rejectButton = new PictureBox
                {
                    Image = Image.FromFile(@"D:\Programming\C# desktop app\Shkolo\Shkolo\Images\ImagesForControls\CrossButton.png"),
                    Width = 35,
                    Height = 35,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Cursor = Cursors.Hand,
                    Name = "n" + readEvent[0].ToString()
                };

                acceptButton.MouseClick += new MouseEventHandler(AcceptInvitationButton_Click);
                rejectButton.MouseClick += new MouseEventHandler(RejectInvitationButton_Click);

                eventsPanels.Add(new Panel());
                eventsFlowPanelInNewInvitesPanel.Controls.Add(eventsPanels[count]);
                eventsPanels[count].Location = new Point(3, 100 * count + 3 * (count + 1));
                eventsPanels[count].Width = 781;
                eventsPanels[count].Height = 160;
                eventsPanels[count].Controls.Add(title);
                eventsPanels[count].Controls.Add(duration);
                eventsPanels[count].Controls.Add(organiser);
                eventsPanels[count].Controls.Add(online_onfline);
                eventsPanels[count].Controls.Add(description);
                eventsPanels[count].Controls.Add(acceptButton);
                eventsPanels[count].Controls.Add(rejectButton);

                title.Location = new Point(3, 3);

                acceptButton.Location = new Point(685, 2);
                rejectButton.Location = new Point(730, 2);

                description.Location = new Point(16, 40);
                description.Width = 748;

                duration.Location = new Point(3, 140);
                duration.Width = duration.Text.Length * 9;

                organiser.Location = new Point(duration.Width + 40, 140);
                organiser.Width = organiser.Text.Length * 9;

                online_onfline.Width = online_onfline.Text.Length * 10;
                online_onfline.Location = new Point(eventsPanels[count].Size.Width - online_onfline.Width - 5, 140);

                eventsPanels[count].BackColor = Color.FromArgb(204, 204, 204);

                count++;
                readEvent.Close();

            }
        }

        private void RenderInvitesInAcceptedInvites()
        {
            List<int> eventInvites = new List<int>();
            List<Panel> eventsPanels = new List<Panel>();

            MySqlConnection conn = new MySqlConnection(connectionString);

            conn.Open();
            MySqlCommand getInvites = new MySqlCommand($@"SELECT event_id FROM event_invites WHERE statuss = 'accepted' AND person_id = {teacher.ID};", conn);

            MySqlDataReader readInvites = getInvites.ExecuteReader();

            int count = 0;
            while (readInvites.Read())
            {
                eventInvites.Add(int.Parse(readInvites[0].ToString()));
            }


            readInvites.Close();

            for (int i = 0; i < eventInvites.Count; i++)
            {
                MySqlCommand getEvent = new MySqlCommand($@"SELECT * FROM eventss WHERE id={eventInvites[i]}", conn);
                MySqlDataReader readEvent = getEvent.ExecuteReader();

                readEvent.Read();

                FontFamily fontFamily = new FontFamily("Arial Rounded MT Bold");

                Label title = new Label
                {
                    Text = readEvent[2].ToString() + " " + readEvent[3].ToString(),
                    Font = new Font(fontFamily, 16f, FontStyle.Regular),
                };

                Label duration = new Label
                {
                    Text = "Duration:  " + readEvent[4].ToString() + " min",
                    Font = new Font(fontFamily, 11.25f, FontStyle.Regular)
                };

                Label organiser = new Label
                {
                    Text = "Organiser:  " + getOrganiserByID(long.Parse(readEvent[1].ToString())),
                    Font = new Font(fontFamily, 11.25f, FontStyle.Regular)
                };

                Label online_onfline = new Label
                {
                    Text = readEvent[5].ToString(),
                    Font = new Font(fontFamily, 11.25f, FontStyle.Regular)
                };

                RichTextBox description = new RichTextBox
                {
                    Text = readEvent[6].ToString(),
                    Font = new Font(fontFamily, 11.25f, FontStyle.Regular),
                    WordWrap = true,
                    BackColor = Color.FromArgb(204, 204, 204),
                    BorderStyle = BorderStyle.None,
                    ReadOnly = true,
                };

                PictureBox rejectButton = new PictureBox
                {
                    Image = Image.FromFile(@"D:\Programming\C# desktop app\Shkolo\Shkolo\Images\ImagesForControls\CrossButton.png"),
                    Width = 35,
                    Height = 35,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Cursor = Cursors.Hand,
                    Name = "a" + readEvent[0].ToString()
                };

                rejectButton.MouseClick += new MouseEventHandler(RejectInvitationButton_Click);

                eventsPanels.Add(new Panel());
                eventsFlowPanelInAcceptedInvitesPanel.Controls.Add(eventsPanels[count]);
                eventsPanels[count].Location = new Point(3, 100 * count + 3 * (count + 1));
                eventsPanels[count].Width = 781;
                eventsPanels[count].Height = 160;
                eventsPanels[count].Controls.Add(title);
                eventsPanels[count].Controls.Add(duration);
                eventsPanels[count].Controls.Add(organiser);
                eventsPanels[count].Controls.Add(online_onfline);
                eventsPanels[count].Controls.Add(description);
                eventsPanels[count].Controls.Add(rejectButton);

                title.Location = new Point(3, 3);

                rejectButton.Location = new Point(730, 2);

                description.Location = new Point(16, 40);
                description.Width = 748;

                duration.Location = new Point(3, 140);
                duration.Width = duration.Text.Length * 9;

                organiser.Location = new Point(duration.Width + 40, 140);
                organiser.Width = organiser.Text.Length * 9;

                online_onfline.Width = online_onfline.Text.Length * 10;
                online_onfline.Location = new Point(eventsPanels[count].Size.Width - online_onfline.Width - 5, 140);

                eventsPanels[count].BackColor = Color.FromArgb(204, 204, 204);

                count++;
                readEvent.Close();

            }
        }


        private void NewInvitesButton_Click(object sender, EventArgs e)
        {

            eventsFlowPanelInNewInvitesPanel.Controls.Clear();
            MenuPanel.Visible = false;
            NewInvitesPanel.Visible = true;
            RenderInvitesInNewInvites();
        }

        private void AcceptInvitationButton_Click(object sender, EventArgs e)
        {
            eventsFlowPanelInNewInvitesPanel.Controls.Clear();

            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand($@"UPDATE event_invites SET statuss = 'accepted' WHERE event_id = {int.Parse(((PictureBox)sender).Name.Substring(1))}", conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            RenderInvitesInNewInvites();

        }
        private void RejectInvitationButton_Click(object sender, EventArgs e)
        {


            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand($@"UPDATE event_invites SET statuss = 'rejected' WHERE event_id = {int.Parse(((PictureBox)sender).Name.Substring(1))}", conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            if (((PictureBox)sender).Name[0] == 'n')
            {
                eventsFlowPanelInNewInvitesPanel.Controls.Clear();
                RenderInvitesInNewInvites();
            }
            else
            {
                eventsFlowPanelInAcceptedInvitesPanel.Controls.Clear();
                RenderInvitesInAcceptedInvites();
            }
        }

        private void AcceptedInvitesButton_Click(object sender, EventArgs e)
        {
            eventsFlowPanelInAcceptedInvitesPanel.Controls.Clear();
            MenuPanel.Visible = false;
            AcceptedInvitesPanel.Visible = true;
            RenderInvitesInAcceptedInvites();
        }

        private void BackButtonInAcceptedInvites_Click(object sender, EventArgs e)
        {
            MenuPanel.Visible = true;
            AcceptedInvitesPanel.Visible = false;

        }

        private void RefreshButtonInNewPanel_Click(object sender, EventArgs e)
        {
            eventsFlowPanelInNewInvitesPanel.Controls.Clear();
            RenderInvitesInNewInvites();
        }

        private void RefreshButtonInAcceptedPanel_Click(object sender, EventArgs e)
        {
            eventsFlowPanelInAcceptedInvitesPanel.Controls.Clear();
            RenderInvitesInAcceptedInvites();
        }

        private void ColorPickerButton_MouseHover(object sender, EventArgs e)
        {
            ColorDialogPicker.Visible = true;
        }

        private void ChangeColorButtonClicked_Click(object sender, EventArgs e)
        {
            ColorPickerButton.FillColor = ((Guna.UI2.WinForms.Guna2Button)(sender)).FillColor;
            MainBackgroundInProfilePanel.BackColor = ((Guna.UI2.WinForms.Guna2Button)(sender)).FillColor;
            ChangeProfilePicButton.ForeColor = ((Guna.UI2.WinForms.Guna2Button)(sender)).FillColor;
            FullNameLabel.BackColor = ((Guna.UI2.WinForms.Guna2Button)(sender)).FillColor;
            EmailLabel.BackColor = ((Guna.UI2.WinForms.Guna2Button)(sender)).FillColor;
            ChangeBorderByPickedColor(sender);

            string colorQuery = ((Guna.UI2.WinForms.Guna2Button)(sender)).FillColor.R + ", " + ((Guna.UI2.WinForms.Guna2Button)(sender)).FillColor.G + ", " + ((Guna.UI2.WinForms.Guna2Button)(sender)).FillColor.B;

            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand($@"UPDATE teachers SET profile_color = '{colorQuery}' WHERE teacher_id = {teacher.ID}", conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void ColorDialogPicker_MouseHover(object sender, EventArgs e)
        {
            ColorDialogPicker.Visible = true;
        }
        private void ChangeBorderByPickedColor(object sender)
        {
            if (((Guna.UI2.WinForms.Guna2Button)(sender)).FillColor == Color.FromArgb(25, 25, 25) ||
                ((Guna.UI2.WinForms.Guna2Button)(sender)).FillColor == Color.FromArgb(170, 0, 255) ||
                ((Guna.UI2.WinForms.Guna2Button)(sender)).FillColor == Color.Red)

            {
                ColorPickerButton.BorderColor = Color.White;
            }
            else
            {
                ColorPickerButton.BorderColor = Color.Black;
            }

            if (((Guna.UI2.WinForms.Guna2Button)(sender)).FillColor == Color.FromArgb(255, 255, 128) ||
                ((Guna.UI2.WinForms.Guna2Button)(sender)).FillColor == Color.FromArgb(0, 255, 110) ||
                ((Guna.UI2.WinForms.Guna2Button)(sender)).FillColor == Color.FromArgb(0, 192, 255) ||
                ((Guna.UI2.WinForms.Guna2Button)(sender)).FillColor == Color.FromArgb(0, 192, 0) ||
                ((Guna.UI2.WinForms.Guna2Button)(sender)).FillColor == Color.FromArgb(225, 199, 153))
            {
                FullNameLabel.ForeColor = Color.Black;
                EmailLabel.ForeColor = Color.Black;
                EmailDrawingBorder.BackColor = Color.Black;


            }
            else
            {
                FullNameLabel.ForeColor = Color.White;
                EmailDrawingBorder.BackColor = Color.White;
                EmailLabel.ForeColor = Color.White;
            }

            if (((Guna.UI2.WinForms.Guna2Button)(sender)).FillColor == Color.FromArgb(255, 255, 128) ||
                ((Guna.UI2.WinForms.Guna2Button)(sender)).FillColor == Color.FromArgb(0, 255, 110) ||
                ((Guna.UI2.WinForms.Guna2Button)(sender)).FillColor == Color.FromArgb(225, 199, 153))
            {
                ChangeProfilePicButton.ForeColor = Color.Black;
            }
        }

        private void Widgets_MouseHover(object sender, EventArgs e)
        {
            ColorDialogPicker.Visible = false;
        }
        private Button BuildButtonForAGrade(int grade)
        {
            Button button = new Button
            {
                ForeColor = Color.White,
                Font = new Font("Times New Roman", 14, FontStyle.Regular),
                Text = grade.ToString(),
                Width = 32,
                Height = 32,
                FlatStyle = FlatStyle.Flat,
            };
            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.BorderColor = Color.Black;
            if (grade == 6)
            {
                button.BackColor = Color.FromArgb(4, 140, 1);
            }
            else if (grade == 5)
            {
                button.BackColor = Color.FromArgb(0, 128, 196);
            }
            else if (grade == 4)
            {
                button.BackColor = Color.FromArgb(227, 196, 5);
            }
            else if (grade == 3)
            {
                button.BackColor = Color.FromArgb(227, 146, 5);
            }
            else if (grade == 2)
            {
                button.BackColor = Color.FromArgb(245, 37, 0);
            }

            return button;
        }

        private int FindClassIdByName()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand find = new MySqlCommand($@"SELECT class_id FROM classes WHERE class_name = '{classesComboBox.SelectedItem.ToString()}'", conn);
            conn.Open();

            string result = find.ExecuteScalar().ToString();

            conn.Close();

            return int.Parse(result);
        }
    }
}
