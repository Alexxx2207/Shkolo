using Shkolo.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Linq;
using Guna.UI2.WinForms;

namespace Shkolo
{
    public partial class StudentHome : Form
    {
        private Person person = null;
        private string connectionString = @"server=192.168.100.53;username=root;password=qazwsxedcrfvtgbyhnujm;database=shkolo";

        public StudentHome(Person temp)
        {
            InitializeComponent();
            this.ControlBox = false;

            person = temp;

            profilePanel.Visible = true;
            eventsPanel.Visible = false;
            gradesPanel.Visible = false;

            //Profile Initialization
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand command = new MySqlCommand($@"SELECT class_name FROM classes WHERE class_id = {((Student)person).ClassID};" , conn);
            MySqlCommand getPersonID = new MySqlCommand($@"SELECT student_id FROM students WHERE email = '{person.Email}';", conn);
            MySqlCommand getColor = new MySqlCommand($@"SELECT profile_color FROM students WHERE email = '{person.Email}';", conn);

            conn.Open();
            ClassLabel.Text = command.ExecuteScalar().ToString();
            person.ID = long.Parse(getPersonID.ExecuteScalar().ToString());

            FullNameLabel.Text = person.FullName;
            AgeLabel.Text += person.Age.ToString();
            BirthDateLabel.Text += person.BirthDate;
            EmailLabel.Text = person.Email;

            pictureBox1.Image = Image.FromStream(new MemoryStream(person.Image));

            FullNameLabel.Location = new Point( pictureBox1.Location.X - (FullNameLabel.Width/2 - pictureBox1.Width/2) ,FullNameLabel.Location.Y);
            EmailLabel.Location = new Point( pictureBox1.Location.X - (EmailLabel.Width/2 - pictureBox1.Width/2) , EmailLabel.Location.Y);
            ClassDrawingBorder.Width = ClassLabel.Width + 5;

            int[] rgbValues = getColor.ExecuteScalar().ToString().Split(',').Select(x => x.Trim()).Select(int.Parse).ToArray();
            ColorPickerButton.FillColor = Color.FromArgb(rgbValues[0],rgbValues[1],rgbValues[2]);
            MainBackgroundInProfilePanel.BackColor = Color.FromArgb(rgbValues[0], rgbValues[1], rgbValues[2]);
            ChangeProfilePicButton.ForeColor = Color.FromArgb(rgbValues[0], rgbValues[1], rgbValues[2]);
            FullNameLabel.BackColor = Color.FromArgb(rgbValues[0], rgbValues[1], rgbValues[2]);
            EmailLabel.BackColor = Color.FromArgb(rgbValues[0], rgbValues[1], rgbValues[2]);
            
            ChangeBorderByPickedColor(ColorPickerButton);

            conn.Close();
            //Grades Initialization
            MySqlCommand getGrades = new MySqlCommand($@"SELECT grade_types.subject_name,grade FROM shkolo.grades JOIN grade_types ON grades.grade_type = grade_types.grade_id WHERE student_id = {((Student)person).StudentID};", conn);

            conn.Open();

            MySqlDataReader reader = getGrades.ExecuteReader();

            while (reader.Read())
            {
                string subject = reader[0].ToString();
                if (! ( (Student) person ).Grades.ContainsKey(subject))
                {
                    ( (Student) person ).Grades[subject] = new List<double>();
                }

                ( (Student) person ).Grades[subject].Add(double.Parse(reader[1].ToString()));

            }
            conn.Close();

            int spaceBetweenButtons = 8;
            foreach (var subject in ((Student)person).Grades)
            {
                switch (subject.Key)
                {
                    case "Български език и литература":
                        int count1 = 0;
                        foreach (var grade in ((Student)person).Grades[subject.Key])
                        {
                            Button button = BuildButtonForAGrade((int)grade);
                            BulgarianGradesDisplay.Controls.Add(button);
                            button.Location = new Point(spaceBetweenButtons*count1 + button.Width * count1, 2);
                            count1++;
                        }
                        break;

                    case "Математика":
                        int count2 = 0;
                        foreach (var grade in ((Student)person).Grades[subject.Key])
                        {
                            Button button = BuildButtonForAGrade((int)grade);
                            MathGradesDisplay.Controls.Add(button);
                            button.Location = new Point(spaceBetweenButtons*count2 + button.Width * count2 + 4, 2);
                            count2++;
                        }
                        break;

                    case "Английски език":
                        int count3 = 0;
                        foreach (var grade in ((Student)person).Grades[subject.Key])
                        {
                            Button button = BuildButtonForAGrade((int)grade);
                            EnglishGradesDisplay.Controls.Add(button);
                            button.Location = new Point(spaceBetweenButtons * count3 + button.Width * count3 + 4, 2);
                            count3++;
                        }
                        break;

                    case "Биология и здравно образование":
                        int count4 = 0;
                        foreach (var grade in ((Student)person).Grades[subject.Key])
                        {
                            Button button = BuildButtonForAGrade((int)grade);
                            BiologyGradesDisplay.Controls.Add(button);
                            button.Location = new Point(spaceBetweenButtons * count4 + button.Width * count4 + 4, 2);
                            count4++;
                        }
                        break;

                    case "Химия и ООС":
                        int count5 = 0;          
                        foreach (var grade in ((Student)person).Grades[subject.Key])
                        {
                            Button button = BuildButtonForAGrade((int)grade);
                            ChemistryGradesDisplay.Controls.Add(button);
                            button.Location = new Point(spaceBetweenButtons * count5 + button.Width * count5 + 4, 2);
                            count5++;
                        }
                        break;

                    case "История и цивилизация":
                        int count6 = 0;
                        foreach (var grade in ((Student)person).Grades[subject.Key])
                        {
                            Button button = BuildButtonForAGrade((int)grade);
                            HistoryGradesDisplay.Controls.Add(button);
                            button.Location = new Point(spaceBetweenButtons * count6 + button.Width * count6 + 4, 2);
                            count6++;
                        }
                        break;

                    case "Физическо възпитание и спорт":
                        int count7 = 0;
                        foreach (var grade in ((Student)person).Grades[subject.Key])
                        {

                            Button button = BuildButtonForAGrade((int)grade);
                            PEGradesDisplay.Controls.Add(button);
                            button.Location = new Point(spaceBetweenButtons * count7 + button.Width * count7 + 4, 2);
                            count7++;
                        }
                        break;

                    case "География и икономика":
                        int count8 = 0;
                        foreach (var grade in ((Student)person).Grades[subject.Key])
                        {
                            Button button = BuildButtonForAGrade((int)grade);
                            GeographyGradesDisplay.Controls.Add(button);
                            button.Location = new Point(spaceBetweenButtons * count8 + button.Width*count8 + 4, 2);
                            count8++;
                        }
                        break;
                    default:
                        break;
                }
            }
            //Grades Initialization-end

            //Average Grade
            double averageGrade = 0;
            int gradesCount = 0;
            getGrades = new MySqlCommand($@"SELECT grade FROM shkolo.grades WHERE student_id = {((Student)person).StudentID};", conn);

            conn.Open();

            reader = getGrades.ExecuteReader();

            while (reader.Read())
            {
                averageGrade += reader.GetDouble(0);
                gradesCount++;
            }
            reader.Close();

            AverageDisplay.Text = $"{(averageGrade / gradesCount):f2}"; 
            AverageGradeLabelInProfile.Text += $"{(averageGrade / gradesCount):f2}";
            if (AverageDisplay.Text == "NaN")
            {
                AverageDisplay.Text = $"{0:f2}";
            }
            
            if (AverageGradeLabelInProfile.Text == "NaN")
            {
                AverageGradeLabelInProfile.Text = $"{0:f2}";
            }

            MySqlCommand getAverageGrade = new MySqlCommand($@"SELECT average_grade FROM classes WHERE class_id = {((Student)person).ClassID};", conn);
            ClassAverageGradeLabel.Text += getAverageGrade.ExecuteScalar().ToString();
            
            conn.Close();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ProfileMenuItem_Click(object sender, EventArgs e)
        {
            profilePanel.Visible = true;
            gradesPanel.Visible = false;
            eventsPanel.Visible = false;
        }

        private void ChangeProfilePicButton_Click(object sender, EventArgs e)
        {
            FileDialog1.ShowDialog();

            if (ValidateImage(FileDialog1.FileName))
            {
                byte[] newImageByted = ConvertImage(FileDialog1.FileName);

                MySqlConnection conn = new MySqlConnection(connectionString);
                MySqlCommand command = new MySqlCommand($@"UPDATE students SET profile_image = @img WHERE student_id = {((Student)person).StudentID};", conn);
                command.Parameters.Add((new MySqlParameter("@img", newImageByted)));

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();

                pictureBox1.Image = new Bitmap(FileDialog1.FileName);
            }
            else
            {
                MessageBox.Show("Invalid image file!");
            }
        }

        private byte[] ConvertImage(string fileName)
        {

            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            byte[] img = br.ReadBytes((int)fs.Length);

            return img;
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

        private void GradesMenuItem_Click(object sender, EventArgs e)
        {
            profilePanel.Visible = false;
            gradesPanel.Visible = true;
            eventsPanel.Visible = false;
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
            MySqlCommand getInvites = new MySqlCommand($@"SELECT event_id FROM event_invites WHERE statuss = 'hold' AND person_id = {person.ID};", conn);

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
                eventsPanels[count].Width = eventsFlowPanelInNewInvitesPanel.Width-6;
                eventsPanels[count].Height = 160;
                eventsPanels[count].Controls.Add(title);
                eventsPanels[count].Controls.Add(duration);
                eventsPanels[count].Controls.Add(organiser);
                eventsPanels[count].Controls.Add(online_onfline);
                eventsPanels[count].Controls.Add(description);
                eventsPanels[count].Controls.Add(acceptButton);
                eventsPanels[count].Controls.Add(rejectButton);

                title.Location = new Point(3, 3);

                acceptButton.Location = new Point(eventsPanels[count].Width - 96, 2);
                rejectButton.Location = new Point(eventsPanels[count].Width - 51, 2);

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
            MySqlCommand getInvites = new MySqlCommand($@"SELECT event_id FROM event_invites WHERE statuss = 'accepted' AND person_id = {person.ID};", conn);

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
                eventsPanels[count].Width = eventsFlowPanelInAcceptedInvitesPanel.Width-6;
                eventsPanels[count].Height = 160;
                eventsPanels[count].Controls.Add(title);
                eventsPanels[count].Controls.Add(duration);
                eventsPanels[count].Controls.Add(organiser);
                eventsPanels[count].Controls.Add(online_onfline);
                eventsPanels[count].Controls.Add(description);
                eventsPanels[count].Controls.Add(rejectButton);

                title.Location = new Point(3, 3);

                rejectButton.Location = new Point(eventsPanels[count].Width - 51, 2);

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
            FullNameLabel.BackColor  = ((Guna.UI2.WinForms.Guna2Button)(sender)).FillColor; 
            EmailLabel.BackColor  = ((Guna.UI2.WinForms.Guna2Button)(sender)).FillColor;
            ChangeBorderByPickedColor(sender);

            string colorQuery = ((Guna.UI2.WinForms.Guna2Button)(sender)).FillColor.R + ", " + ((Guna.UI2.WinForms.Guna2Button)(sender)).FillColor.G + ", " + ((Guna.UI2.WinForms.Guna2Button)(sender)).FillColor.B;

            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand($@"UPDATE students SET profile_color = '{colorQuery}' WHERE student_id = {person.ID}", conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void ColorDialogPicker_MouseHover(object sender, EventArgs e)
        {
            ColorDialogPicker.Visible = true;
        }
        private void Widgets_MouseHover(object sender, EventArgs e)
        {
            ColorDialogPicker.Visible = false;
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
    }
}
