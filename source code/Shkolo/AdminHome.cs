using MySql.Data.MySqlClient;
using Shkolo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shkolo
{
    public partial class AdminHome : Form
    {
        private readonly string connectionString = BuildConnectionString();
        private Person admin;
        public AdminHome(Person person)
        {
            InitializeComponent();
            this.ControlBox = false;

            eventsPanel.Visible = false;
            CreateClassPanel.Visible = false;
            profilePanel.Visible = true;
            PrivateEventsMenuPanel.Visible = false;


            dateTimePicker.MinDate = DateTime.Today;
            SelectedDateLabel.Text = "";

            FullNameLabel.Text = person.FullName;
            AgeLabel.Text += person.Age;
            BirthDateLabel.Text += person.BirthDate;
            EmailLabel.Text = person.Email;

            ovalPictureBox1.Image = Image.FromStream(new MemoryStream(person.Image));

            FullNameLabel.Location = new Point(ovalPictureBox1.Location.X - (FullNameLabel.Width / 2 - ovalPictureBox1.Width / 2), FullNameLabel.Location.Y);
            EmailLabel.Location = new Point(ovalPictureBox1.Location.X - (EmailLabel.Width / 2 - ovalPictureBox1.Width / 2), EmailLabel.Location.Y);

            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand getPersonID = new MySqlCommand($@"SELECT id FROM administration WHERE email = '{person.Email}';", conn);
            MySqlCommand getColor = new MySqlCommand($@"SELECT profile_color FROM administration WHERE email = '{person.Email}';", conn);

            conn.Open();

            int[] rgbValues = getColor.ExecuteScalar().ToString().Split(',').Select(x => x.Trim()).Select(int.Parse).ToArray();
            ColorPickerButton.FillColor = Color.FromArgb(rgbValues[0], rgbValues[1], rgbValues[2]);
            MainBackgroundInProfilePanel.BackColor = Color.FromArgb(rgbValues[0], rgbValues[1], rgbValues[2]);
            ChangeProfilePicButton.ForeColor = Color.FromArgb(rgbValues[0], rgbValues[1], rgbValues[2]);
            FullNameLabel.BackColor = Color.FromArgb(rgbValues[0], rgbValues[1], rgbValues[2]);
            EmailLabel.BackColor = Color.FromArgb(rgbValues[0], rgbValues[1], rgbValues[2]);


            person.ID = long.Parse(getPersonID.ExecuteScalar().ToString());

            conn.Close();

            admin = person;

            EventTimeStart.Text = GetCurrentTime();              

            /// initialize people types
            foreach (string file in Directory.GetFiles(@"D:\Programming\C# desktop app\Shkolo\Shkolo\Models\"))
            {
                string item = Path.GetFileName(file).Remove(Path.GetFileName(file).Length - 3);
                if (item != nameof(Person))
                {
                    PeopleTypesComboBox.Items.Add(item);
                }
            }

            PeopleTypesComboBox.SelectedItem = PeopleTypesComboBox.Items[0];

            InitializeClasses();
            InitializeNamesInAddPanel();
            InitializeEvents();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ProfileMenuItem_Click(object sender, EventArgs e)
        {
            PrivateEventsMenuPanel.Visible = false;
            CreateClassPanel.Visible = false; 
            eventsPanel.Visible = false;
            profilePanel.Visible = true;
        }

        private void EventsMenuItem_Click(object sender, EventArgs e)
        {
            MenuPanel.Visible = true;
           
            eventsPanel.Visible = true;
            profilePanel.Visible = false;
            RemoveEventPanel.Visible = false;
            CreatePanel.Visible = false;
            AddPanel.Visible = false;
            PrivateEventsPanel.Visible = false;
            CreateClassPanel.Visible = false;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            CreatePanel.Visible = false;
            MenuPanel.Visible = true;
            AddPanel.Visible = false;
            RemoveEventPanel.Visible = false;
        }

        private void CreateBtn_Click(object sender, EventArgs e)
        {
            MenuPanel.Visible = false;
            CreatePanel.Visible = true;
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            MenuPanel.Visible = false;
            MenuPanel.Visible = false;
            AddPanel.Visible = true;

            
        }

        private void OfflineRadioButton_Click(object sender, EventArgs e)
        {
            OnlineRadioButton.Checked = false;
            OfflineRadioButton.Checked = true;
        }
        private void OnlineRadioButton_Click(object sender, EventArgs e)
        {
            OnlineRadioButton.Checked = true;
            OfflineRadioButton.Checked = false;
        }
        private void CreateEventBtn_Click(object sender, EventArgs e)
        {
            bool validTitle = ValidateTitle();
            bool validDate = ValidateDate();
            bool validTime = ValidateTime();
            bool validDuration = ValidateDuration();
            bool validDescription = ValidateDescription();

            if (!validTitle)
            {
                TitleErrorLabel.Visible = true;
            }
            else 
            {
                TitleErrorLabel.Visible = false;

            }

            if (!validDate)
            {
                DateErrorLabel.Visible = true;
            }
            else
            {
                DateErrorLabel.Visible = false;

            }

            if (!validTime)
            {
                TimeErrorLabel.Visible = true;
            }
            else
            {
                TimeErrorLabel.Visible = false;

            }

            if (!validDuration)
            {
                DurationErrorLabel.Visible = true;
            }
            else
            {
                DurationErrorLabel.Visible = false;

            }

            if (!validDescription)
            {
                DescriptionErrorLabel.Visible = true;
            }
            else
            {
                DescriptionErrorLabel.Visible = false;

            }

            if (validTitle && validDate && validTime && validDuration && validDescription)
            {
                Event @event = new Event(admin.ID, TitleTextBox.Text, dateTimePicker.Value.ToString().Split(' ')[0] + " " + EventTimeStart.Text, OnlineRadioButton.Checked ? "Online" : "Offline", DescriptionTextBox.Text, int.Parse(DurationTextBox.Text));

                if (ValidateAppropriateEventDateTime(@event.Date))
                {
                    Random rand = new Random();
                    int eventID = rand.Next(1, 999999999);

                    while (!ValidateEventId(eventID))
                    {
                        eventID = rand.Next(1, 999999999);
                    }
                    @event.ID = eventID;

                    MySqlConnection conn = new MySqlConnection(connectionString);
                    MySqlCommand command = new MySqlCommand($@"INSERT INTO shkolo.eventss(id,organiser_id, title, datee, duration,online_offline, descriptionn) VALUES ({@event.ID},{@event.OrganiserID},'{@event.Title}', '{@event.Date}', {@event.Duration},'{@event.OnlineOffline}','{@event.Description}');", conn);

                    conn.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("New event created successful!");
                    conn.Close();  
                }
                else
                {
                    MessageBox.Show("At that time you have another event. Change date and/or time!");

                }
            }

        }

        private bool ValidateAppropriateEventDateTime(string date)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand command = new MySqlCommand($@"SELECT * FROM eventss WHERE organiser_id = {admin.ID} AND datee = '{date}'", conn);

            conn.Open();
            if (command.ExecuteScalar() != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool ValidateEventId(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand command = new MySqlCommand($@"SELECT id FROM administration WHERE administration.id = {id};", conn);
                if (command.ExecuteScalar() != null)
                {
                    conn.Close();
                    return false;
                }
                else
                {
                    conn.Close();
                    return true;
                }
               
            }
        }
        private bool ValidateTitle()
        {
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text))
            {
                TitleErrorLabel.Text = "Invalid empty title!";
                return false;
            }
            else if (TitleTextBox.Text.Length > 60)
            {
                TitleErrorLabel.Text = "Too long title!";
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool ValidateDate()
        {
            return !string.IsNullOrEmpty(SelectedDateLabel.Text);
        }
        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            SelectedDateLabel.Text = dateTimePicker.Value.ToString().Split(' ')[0];
        }
        private bool ValidateTime()
        {
            if (string.IsNullOrWhiteSpace(EventTimeStart.Text.Split(':')[0]) || string.IsNullOrWhiteSpace(EventTimeStart.Text.Split(':')[1]))
            {
                TimeErrorLabel.Text = "No inputed time!";

                return false;
            }

            if (Regex.IsMatch(EventTimeStart.Text.Split(':')[0], @"[0-9]{2}") && Regex.IsMatch(EventTimeStart.Text.Split(':')[1], @"[0-9]{2}"))
            {
                if (int.Parse(EventTimeStart.Text.Split(':')[0]) < 10 && EventTimeStart.Text.Split(':')[0][0] != '0')
                {
                    TimeErrorLabel.Text = "Invalid hours! format[HH:mm]";
                    return false;
                }
                if (EventTimeStart.Text.Split(':')[1].Length != 2)
                {
                    TimeErrorLabel.Text = "Invalid minutes! format[HH:mm]";
                    return false;
                }

                int[] parameters = EventTimeStart.Text.Split(':').Select(int.Parse).ToArray();

                if (parameters[0] >= 0 && parameters[0] <= 24 && parameters[1] >= 0 && parameters[1] <= 59)
                {
                    return true;
                }
                else
                {
                    TimeErrorLabel.Text = "Invalid time format[hour:minute]";
                    return false;
                }
            }
            else
            {
                TimeErrorLabel.Text = "Invalid symbols! Only numbers are allowed!";
                return false;
            }
        }
        private bool ValidateDuration()
        {
            if (string.IsNullOrWhiteSpace(DurationTextBox.Text))
            {
                DurationErrorLabel.Text = "Empty duration!";

                return false;
            }
            if (int.TryParse(DurationTextBox.Text, out int value) && value > 0)
            {
                if (value < 1)
                {
                    DurationErrorLabel.Text = "Negative duration is invalid!";
                    return false;
                }
                return true;
            }
            else
            {
                DurationErrorLabel.Text = "Invalid symbols! Write duration of an event with numbers!";
                return false;
            }
        }
        private bool ValidateDescription()
        {
            if (string.IsNullOrWhiteSpace(DescriptionTextBox.Text))
            {
                DescriptionErrorLabel.Text = "Empty description! Give details!";

                return false;
            }
            else if (DescriptionTextBox.Text.Length > 1000)
            {
                DescriptionErrorLabel.Text = "Description too long!";
                return false;
            }
            else
            {
                return true;
            }

        }
        private string GetCurrentTime()
        {
            if(DateTime.Now.Hour < 10 && DateTime.Now.Minute < 10)
                return "0" + DateTime.Now.Hour.ToString() + "0" + DateTime.Now.Minute.ToString();
            if (DateTime.Now.Hour < 10)
                return "0" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString();

            if (DateTime.Now.Minute < 10)
                return DateTime.Now.Hour.ToString() + "0" + DateTime.Now.Minute.ToString();

            else
                return DateTime.Now.Hour.ToString()+ DateTime.Now.Minute.ToString();
        }
        private int ClassIdByName()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand command = new MySqlCommand($@"SELECT class_id FROM classes WHERE class_name = '{ClassesComboBox.SelectedItem}';", conn);

            conn.Open();

            return int.Parse(command.ExecuteScalar().ToString());

            conn.Close();
        }
        private void PeopleTypesComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            classLabelAddPanel.Visible = false;
            if (ClassesComboBox.Items.Count > 0)
            {
                ClassesComboBox.SelectedItem = ClassesComboBox.Items[0]; 
            }
            ClassesComboBox.Visible = false;
           

            NamesLabel.Visible = false;
            if (NamesComboBox.Items.Count > 0)
            {
                NamesComboBox.SelectedItem = NamesComboBox.Items[0];
            }
            NamesComboBox.Visible = false;

            AddToLabel.Visible = false;

            RefreshButton.Visible = false;
            RefreshLabel.Visible = false;
            EventLabelAddPanel.Visible = false;
            EventsComboBox.Visible = false;

            if (EventsComboBox.Items.Count > 0)
            {
                EventsComboBox.SelectedItem = EventsComboBox.Items[0];
            }

            if (PeopleTypesComboBox.SelectedItem.ToString() == nameof(Student))
            {
                classLabelAddPanel.Visible = true;
                ClassesComboBox.Visible = true;
                InitializeClasses();
            }
            else
            {
                NamesLabel.Visible = true;
                NamesComboBox.Visible = true;
                AddToLabel.Visible = true;
                InitializeNamesInAddPanel();
            }
        }
        private void ClassesComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            NamesLabel.Visible = false;
            NamesComboBox.Visible = false;

            if (NamesComboBox.Items.Count > 0)
            {
                NamesComboBox.SelectedItem = NamesComboBox.Items[0]; 
            }

            RefreshButton.Visible = false;
            RefreshLabel.Visible = false;
            EventLabelAddPanel.Visible = false;
            if (EventsComboBox.Items.Count > 0)
            {
                EventsComboBox.SelectedItem = EventsComboBox.Items[0]; 
            }
            EventsComboBox.Visible = false;

            AddToLabel.Visible = true;

            NamesLabel.Visible = true;
            NamesComboBox.Visible = true;
            InitializeNamesInAddPanel();
        }
        private void NamesComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            EventLabelAddPanel.Visible = true;
            EventsComboBox.Visible = true;
            RefreshButton.Visible = true;
            RefreshLabel.Visible = true;
            InitializeEvents();
        }
        private void InitializeNamesInAddPanel()
        {
            NamesComboBox.Items.Clear();
            NamesComboBox.Text = "";
            MySqlConnection conn = new MySqlConnection(connectionString);

            if (PeopleTypesComboBox.SelectedItem.ToString() == nameof(Student))
            {
                MySqlCommand command = new MySqlCommand($@"SELECT full_name, student_id FROM students WHERE class_id = {ClassIdByName()}", conn);

                conn.Open();
                MySqlDataReader readerForStudentNames = command.ExecuteReader();

                while (readerForStudentNames.Read())
                {
                    NamesComboBox.Items.Add(readerForStudentNames[0].ToString() + " - " + readerForStudentNames[1].ToString());
                }
                conn.Close();
            }
            else if (PeopleTypesComboBox.SelectedItem.ToString() == nameof(Teacher))
            {
                MySqlCommand command = new MySqlCommand($@"SELECT teacher_name, teacher_id FROM teachers;", conn);

                conn.Open();
                MySqlDataReader readerForTeachersNames = command.ExecuteReader();

                while (readerForTeachersNames.Read())
                {
                    NamesComboBox.Items.Add(readerForTeachersNames[0].ToString() + " - " + readerForTeachersNames[1].ToString());
                }
                conn.Close();
            }
            else if (PeopleTypesComboBox.SelectedItem.ToString() == nameof(Administrator))
            {
                MySqlCommand command = new MySqlCommand($@"SELECT namee, id FROM administration WHERE namee != '{admin.FullName}';", conn);

                conn.Open();
                MySqlDataReader readerForAdminsNames = command.ExecuteReader();

                while (readerForAdminsNames.Read())
                {
                    NamesComboBox.Items.Add(readerForAdminsNames[0].ToString() + " - " + readerForAdminsNames[1].ToString());
                }
                conn.Close();
            }


        }
        private void InitializeEvents()
        {
            EventsComboBox.Items.Clear();
            RemoveEventPicker.Items.Clear();
            MySqlConnection conn = new MySqlConnection(connectionString);

            MySqlCommand getEvents = new MySqlCommand($@"SELECT title, datee FROM eventss WHERE organiser_id = {admin.ID}", conn);

            conn.Open();
            MySqlDataReader getEventsReader = getEvents.ExecuteReader();

            while (getEventsReader.Read())
            {
                EventsComboBox.Items.Add(getEventsReader[0].ToString() + " - " + getEventsReader[1].ToString());
                RemoveEventPicker.Items.Add(getEventsReader[0].ToString() + " - " + getEventsReader[1].ToString());
            }
            conn.Close();

            if (EventsComboBox.Items.Count > 0)
                EventsComboBox.SelectedItem = EventsComboBox.Items[0];
        }
        private void InitializeClasses()
        {
            ClassesComboBox.Items.Clear();
            MySqlConnection conn = new MySqlConnection(connectionString);

            MySqlCommand getClasses = new MySqlCommand(@"SELECT class_name FROM classes;", conn);

            conn.Open();

            MySqlDataReader reader = getClasses.ExecuteReader();

            while (reader.Read())
            {
                ClassesComboBox.Items.Add(reader[0].ToString());
            }
            conn.Close();

            if (ClassesComboBox.Items.Count > 0)
            {
                ClassesComboBox.SelectedItem = ClassesComboBox.Items[0];
            }
        }
        
        private bool ValidateEventInviteId(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand command = new MySqlCommand($@"SELECT event_invite_id FROM event_invites WHERE event_invite_id = {id};", conn);
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
                
        private void AddButtonInAddPanel_Click(object sender, EventArgs e)
        {
            if (EventsComboBox.SelectedItem != null)
            {
                Random rand = new Random();
                int inviteID = rand.Next(1, 999999999);

                while (!ValidateEventInviteId(inviteID))
                {
                    inviteID = rand.Next(1, 999999999);
                }

                MySqlConnection conn = new MySqlConnection(connectionString);

                MySqlCommand insertInvite = new MySqlCommand($@"INSERT INTO event_invites(event_invite_id, organiser_id,event_id,person_id) VALUES ({inviteID},{admin.ID},{FindEventIdByDateTimeInAddPanel()},{int.Parse(NamesComboBox.SelectedItem.ToString().Split('-')[1].Trim())});", conn);

                conn.Open();
                if (!InviteExists())
                {
                    insertInvite.ExecuteNonQuery();
                    MessageBox.Show("Invite successful!");
                }
                else
                {
                    MessageBox.Show("Invite is on hold! That action is a spam!");
                }
                conn.Close();

            }
            else
            {
                MessageBox.Show("Not selected event!");
            }
        }

        private int FindEventIdByDateTimeInAddPanel()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand command = new MySqlCommand($@"SELECT id FROM eventss WHERE datee = '{EventsComboBox.SelectedItem.ToString().Split('-')[1].Trim()}'", conn);

            conn.Open();

            return int.Parse(command.ExecuteScalar().ToString());
        }
        
        private int FindEventIdByDateTimeInRemovePanel()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand command = new MySqlCommand($@"SELECT id FROM eventss WHERE datee = '{RemoveEventPicker.SelectedItem.ToString().Split('-')[1].Trim()}'", conn);

            conn.Open();

            return int.Parse(command.ExecuteScalar().ToString());
        }

        private bool InviteExists()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            MySqlCommand check = new MySqlCommand($@"SELECT organiser_id, event_id,person_id FROM event_invites WHERE organiser_id = {admin.ID} AND event_id = {FindEventIdByDateTimeInAddPanel()} AND person_id = {int.Parse(NamesComboBox.SelectedItem.ToString().Split('-')[1].Trim())};", conn);
            conn.Open();
            return check.ExecuteScalar() != null;
            conn.Close();

        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            InitializeEvents();
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (RemoveEventPicker.SelectedItem != null)
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                MySqlCommand cmd = new MySqlCommand($@"DELETE FROM event_invites WHERE event_id = {FindEventIdByDateTimeInRemovePanel()}", conn);
                MySqlCommand cmd2 = new MySqlCommand($@"DELETE FROM eventss WHERE id = {FindEventIdByDateTimeInRemovePanel()}", conn);
                conn.Open();

                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();

                conn.Close();

            }
            else
            {
                MessageBox.Show("Not selected event!");
            }
        }

        private void RemoveBtn_Click(object sender, EventArgs e)
        {
            RemoveEventPanel.Visible = true;
            MenuPanel.Visible = false;
        }

        private void YourEventsListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateClassPanel.Visible = false;
            eventsPanel.Visible = false;
            profilePanel.Visible = false;
            PrivateEventsPanel.Visible = true;
            NewInvitesPanel.Visible = false;
            AcceptedInvitesPanel.Visible = false;
            PrivateEventsMenuPanel.Visible = true;
        }


        private void BackButtonInNewinvitesPanel_Click(object sender, EventArgs e)
        {
            PrivateEventsMenuPanel.Visible = true;
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
            MySqlCommand getInvites = new MySqlCommand($@"SELECT event_id FROM event_invites WHERE statuss = 'hold' AND person_id = {admin.ID};", conn);

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
                eventsPanels[count].Width = eventsFlowPanelInNewInvitesPanel.Width - 6;
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
            MySqlCommand getInvites = new MySqlCommand($@"SELECT event_id FROM event_invites WHERE statuss = 'accepted' AND person_id = {admin.ID};", conn);

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
                eventsPanels[count].Width = eventsFlowPanelInAcceptedInvitesPanel.Width - 6;
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
            PrivateEventsMenuPanel.Visible = false;
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
            PrivateEventsMenuPanel.Visible = false;
            AcceptedInvitesPanel.Visible = true;
            RenderInvitesInAcceptedInvites();
        }

        private void BackButtonInAcceptedInvites_Click(object sender, EventArgs e)
        {
            PrivateEventsMenuPanel.Visible = true;
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
            MySqlCommand cmd = new MySqlCommand($@"UPDATE administration SET profile_color = '{colorQuery}' WHERE id = {admin.ID}", conn);
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

        private void AddClassButton_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            long classID = rand.Next(1, 999999999);

            while (!ValidateClassId(classID))
            {
                classID = rand.Next(1, 999999999);
            }

            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand add = new MySqlCommand($@"INSERT INTO classes(class_id, number_of_students, average_grade, class_name) VALUES ({classID},0,0.00, '{ClassNameTextBox.Text}');", conn);

            conn.Open();
            if (ValidateClassName())
            {
                add.ExecuteNonQuery();
                MessageBox.Show("Class is creared successfully!");

            }
            else
            {
                MessageBox.Show("Class name already exists!");
            }
            conn.Close();
        }
        private bool ValidateClassName()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand command = new MySqlCommand($@"SELECT class_name FROM classes WHERE class_name = '{ClassNameTextBox.Text}';", conn);
                if (command.ExecuteScalar() != null)
                {
                    conn.Close();
                    return false;
                }
                else
                {
                    conn.Close();
                    return true;
                }

            }
        }
        private bool ValidateClassId(long id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand command = new MySqlCommand($@"SELECT class_id FROM classes WHERE class_id = {id};", conn);
                if (command.ExecuteScalar() != null)
                {
                    conn.Close();
                    return false;
                }
                else
                {
                    conn.Close();
                    return true;
                }

            }
        }

        private void classesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateClassPanel.Visible = true;
            profilePanel.Visible = false;
            PrivateEventsMenuPanel.Visible = false;
            eventsPanel.Visible = false;
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
