namespace Shkolo
{
    partial class SignUp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SignUp));
            this.SignUpButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.FullName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Email = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.TextBox();
            this.PasswordEye = new System.Windows.Forms.Button();
            this.EmailErrorLabel = new System.Windows.Forms.Label();
            this.PasswordErrorLabel = new System.Windows.Forms.Label();
            this.FullNameErrorLable = new System.Windows.Forms.Label();
            this.LogInButton = new System.Windows.Forms.Button();
            this.ClassIDLabel = new System.Windows.Forms.Label();
            this.ClassIDErrorLabel = new System.Windows.Forms.Label();
            this.DateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.DateTimeLabel = new System.Windows.Forms.Label();
            this.BirthDateErrorLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.FileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ChooseImageButton = new System.Windows.Forms.Button();
            this.InvalidImageErrorLabel = new System.Windows.Forms.Label();
            this.ImagePreview = new System.Windows.Forms.PictureBox();
            this.LeaveEmptyImageButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.SubjectCombobox = new System.Windows.Forms.ComboBox();
            this.SubjectLabel = new System.Windows.Forms.Label();
            this.SubjectErrorLabel = new System.Windows.Forms.Label();
            this.RoleChooser = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ClassesComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePreview)).BeginInit();
            this.SuspendLayout();
            // 
            // SignUpButton
            // 
            this.SignUpButton.BackColor = System.Drawing.Color.White;
            this.SignUpButton.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.SignUpButton.FlatAppearance.BorderSize = 3;
            this.SignUpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SignUpButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.SignUpButton.ForeColor = System.Drawing.Color.RoyalBlue;
            this.SignUpButton.Location = new System.Drawing.Point(278, 371);
            this.SignUpButton.Name = "SignUpButton";
            this.SignUpButton.Size = new System.Drawing.Size(104, 35);
            this.SignUpButton.TabIndex = 6;
            this.SignUpButton.Text = "SignUp";
            this.SignUpButton.UseVisualStyleBackColor = false;
            this.SignUpButton.Click += new System.EventHandler(this.SignUpButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-7, 44);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(678, 565);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 29);
            this.label1.TabIndex = 8;
            this.label1.Text = "Full name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(13, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 29);
            this.label2.TabIndex = 9;
            this.label2.Text = "Birth date:";
            // 
            // FullName
            // 
            this.FullName.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.FullName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.FullName.Location = new System.Drawing.Point(152, 12);
            this.FullName.Name = "FullName";
            this.FullName.Size = new System.Drawing.Size(415, 31);
            this.FullName.TabIndex = 12;
            this.FullName.TextChanged += new System.EventHandler(this.FullName_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(298, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 29);
            this.label4.TabIndex = 14;
            this.label4.Text = "Email:";
            // 
            // Email
            // 
            this.Email.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Email.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.Email.Location = new System.Drawing.Point(388, 57);
            this.Email.Name = "Email";
            this.Email.Size = new System.Drawing.Size(262, 31);
            this.Email.TabIndex = 15;
            this.Email.TextChanged += new System.EventHandler(this.Email_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(249, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 29);
            this.label5.TabIndex = 16;
            this.label5.Text = "Password:";
            // 
            // Password
            // 
            this.Password.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.Password.Location = new System.Drawing.Point(387, 114);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(227, 31);
            this.Password.TabIndex = 17;
            this.Password.TextChanged += new System.EventHandler(this.Password_TextChanged);
            // 
            // PasswordEye
            // 
            this.PasswordEye.BackColor = System.Drawing.Color.White;
            this.PasswordEye.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PasswordEye.Location = new System.Drawing.Point(615, 114);
            this.PasswordEye.Name = "PasswordEye";
            this.PasswordEye.Size = new System.Drawing.Size(44, 31);
            this.PasswordEye.TabIndex = 18;
            this.PasswordEye.UseVisualStyleBackColor = false;
            this.PasswordEye.Click += new System.EventHandler(this.PasswordEye_Click);
            // 
            // EmailErrorLabel
            // 
            this.EmailErrorLabel.AutoSize = true;
            this.EmailErrorLabel.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold);
            this.EmailErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.EmailErrorLabel.Location = new System.Drawing.Point(371, 95);
            this.EmailErrorLabel.Name = "EmailErrorLabel";
            this.EmailErrorLabel.Size = new System.Drawing.Size(45, 16);
            this.EmailErrorLabel.TabIndex = 19;
            this.EmailErrorLabel.Text = "label6";
            this.EmailErrorLabel.Visible = false;
            // 
            // PasswordErrorLabel
            // 
            this.PasswordErrorLabel.AutoSize = true;
            this.PasswordErrorLabel.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold);
            this.PasswordErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.PasswordErrorLabel.Location = new System.Drawing.Point(371, 148);
            this.PasswordErrorLabel.Name = "PasswordErrorLabel";
            this.PasswordErrorLabel.Size = new System.Drawing.Size(45, 16);
            this.PasswordErrorLabel.TabIndex = 20;
            this.PasswordErrorLabel.Text = "label7";
            this.PasswordErrorLabel.Visible = false;
            // 
            // FullNameErrorLable
            // 
            this.FullNameErrorLable.AutoSize = true;
            this.FullNameErrorLable.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold);
            this.FullNameErrorLable.ForeColor = System.Drawing.Color.Red;
            this.FullNameErrorLable.Location = new System.Drawing.Point(138, 46);
            this.FullNameErrorLable.Name = "FullNameErrorLable";
            this.FullNameErrorLable.Size = new System.Drawing.Size(45, 16);
            this.FullNameErrorLable.TabIndex = 22;
            this.FullNameErrorLable.Text = "label7";
            this.FullNameErrorLable.Visible = false;
            // 
            // LogInButton
            // 
            this.LogInButton.BackColor = System.Drawing.Color.White;
            this.LogInButton.FlatAppearance.BorderSize = 0;
            this.LogInButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LogInButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LogInButton.ForeColor = System.Drawing.Color.Blue;
            this.LogInButton.Location = new System.Drawing.Point(585, 378);
            this.LogInButton.Name = "LogInButton";
            this.LogInButton.Size = new System.Drawing.Size(69, 28);
            this.LogInButton.TabIndex = 24;
            this.LogInButton.Text = "Log In";
            this.LogInButton.UseVisualStyleBackColor = false;
            this.LogInButton.Click += new System.EventHandler(this.LogInButton_Click);
            // 
            // ClassIDLabel
            // 
            this.ClassIDLabel.AutoSize = true;
            this.ClassIDLabel.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.ClassIDLabel.Location = new System.Drawing.Point(13, 129);
            this.ClassIDLabel.Name = "ClassIDLabel";
            this.ClassIDLabel.Size = new System.Drawing.Size(112, 29);
            this.ClassIDLabel.TabIndex = 25;
            this.ClassIDLabel.Text = "Class Id:";
            // 
            // ClassIDErrorLabel
            // 
            this.ClassIDErrorLabel.AutoSize = true;
            this.ClassIDErrorLabel.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold);
            this.ClassIDErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.ClassIDErrorLabel.Location = new System.Drawing.Point(15, 163);
            this.ClassIDErrorLabel.Name = "ClassIDErrorLabel";
            this.ClassIDErrorLabel.Size = new System.Drawing.Size(52, 16);
            this.ClassIDErrorLabel.TabIndex = 27;
            this.ClassIDErrorLabel.Text = "label11";
            this.ClassIDErrorLabel.Visible = false;
            // 
            // DateTimePicker
            // 
            this.DateTimePicker.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateTimePicker.Location = new System.Drawing.Point(152, 70);
            this.DateTimePicker.MinDate = new System.DateTime(1950, 1, 1, 0, 0, 0, 0);
            this.DateTimePicker.Name = "DateTimePicker";
            this.DateTimePicker.Size = new System.Drawing.Size(18, 31);
            this.DateTimePicker.TabIndex = 28;
            this.DateTimePicker.ValueChanged += new System.EventHandler(this.DateTimePicker_ValueChanged);
            // 
            // DateTimeLabel
            // 
            this.DateTimeLabel.AutoSize = true;
            this.DateTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateTimeLabel.Location = new System.Drawing.Point(176, 74);
            this.DateTimeLabel.Name = "DateTimeLabel";
            this.DateTimeLabel.Size = new System.Drawing.Size(0, 24);
            this.DateTimeLabel.TabIndex = 29;
            // 
            // BirthDateErrorLabel
            // 
            this.BirthDateErrorLabel.AutoSize = true;
            this.BirthDateErrorLabel.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold);
            this.BirthDateErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.BirthDateErrorLabel.Location = new System.Drawing.Point(49, 104);
            this.BirthDateErrorLabel.Name = "BirthDateErrorLabel";
            this.BirthDateErrorLabel.Size = new System.Drawing.Size(127, 16);
            this.BirthDateErrorLabel.TabIndex = 30;
            this.BirthDateErrorLabel.Text = "Input date of birth!";
            this.BirthDateErrorLabel.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(51, 211);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(161, 29);
            this.label7.TabIndex = 31;
            this.label7.Text = "Insert Image:";
            // 
            // FileDialog1
            // 
            this.FileDialog1.FileName = "openFileDialog1";
            this.FileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.FileDialog_FileOk);
            // 
            // ChooseImageButton
            // 
            this.ChooseImageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChooseImageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChooseImageButton.Location = new System.Drawing.Point(218, 211);
            this.ChooseImageButton.Name = "ChooseImageButton";
            this.ChooseImageButton.Size = new System.Drawing.Size(80, 29);
            this.ChooseImageButton.TabIndex = 32;
            this.ChooseImageButton.Text = "Choose";
            this.ChooseImageButton.UseVisualStyleBackColor = true;
            this.ChooseImageButton.Click += new System.EventHandler(this.ChooseImageButton_Click);
            // 
            // InvalidImageErrorLabel
            // 
            this.InvalidImageErrorLabel.AutoSize = true;
            this.InvalidImageErrorLabel.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold);
            this.InvalidImageErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.InvalidImageErrorLabel.Location = new System.Drawing.Point(215, 246);
            this.InvalidImageErrorLabel.Name = "InvalidImageErrorLabel";
            this.InvalidImageErrorLabel.Size = new System.Drawing.Size(96, 16);
            this.InvalidImageErrorLabel.TabIndex = 33;
            this.InvalidImageErrorLabel.Text = "Invalid Image!";
            this.InvalidImageErrorLabel.Visible = false;
            // 
            // ImagePreview
            // 
            this.ImagePreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ImagePreview.Image = ((System.Drawing.Image)(resources.GetObject("ImagePreview.Image")));
            this.ImagePreview.Location = new System.Drawing.Point(323, 200);
            this.ImagePreview.Name = "ImagePreview";
            this.ImagePreview.Size = new System.Drawing.Size(59, 62);
            this.ImagePreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImagePreview.TabIndex = 34;
            this.ImagePreview.TabStop = false;
            // 
            // LeaveEmptyImageButton
            // 
            this.LeaveEmptyImageButton.FlatAppearance.BorderSize = 0;
            this.LeaveEmptyImageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LeaveEmptyImageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LeaveEmptyImageButton.ForeColor = System.Drawing.Color.Blue;
            this.LeaveEmptyImageButton.Location = new System.Drawing.Point(388, 211);
            this.LeaveEmptyImageButton.Name = "LeaveEmptyImageButton";
            this.LeaveEmptyImageButton.Size = new System.Drawing.Size(49, 36);
            this.LeaveEmptyImageButton.TabIndex = 36;
            this.LeaveEmptyImageButton.Text = "Leave empty";
            this.LeaveEmptyImageButton.UseVisualStyleBackColor = true;
            this.LeaveEmptyImageButton.Click += new System.EventHandler(this.LeaveEmptyImageButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.BackColor = System.Drawing.Color.Red;
            this.ExitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitButton.ForeColor = System.Drawing.Color.White;
            this.ExitButton.Location = new System.Drawing.Point(629, 3);
            this.ExitButton.Margin = new System.Windows.Forms.Padding(0);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(41, 38);
            this.ExitButton.TabIndex = 37;
            this.ExitButton.Text = "X";
            this.ExitButton.UseVisualStyleBackColor = false;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // SubjectCombobox
            // 
            this.SubjectCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SubjectCombobox.FormattingEnabled = true;
            this.SubjectCombobox.Location = new System.Drawing.Point(470, 226);
            this.SubjectCombobox.Name = "SubjectCombobox";
            this.SubjectCombobox.Size = new System.Drawing.Size(183, 21);
            this.SubjectCombobox.TabIndex = 40;
            this.SubjectCombobox.Visible = false;
            this.SubjectCombobox.SelectedValueChanged += new System.EventHandler(this.SubjectCombobox_SelectedValueChanged);
            // 
            // SubjectLabel
            // 
            this.SubjectLabel.AutoSize = true;
            this.SubjectLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubjectLabel.Location = new System.Drawing.Point(557, 207);
            this.SubjectLabel.Name = "SubjectLabel";
            this.SubjectLabel.Size = new System.Drawing.Size(97, 16);
            this.SubjectLabel.TabIndex = 41;
            this.SubjectLabel.Text = "Primal Subject:";
            this.SubjectLabel.Visible = false;
            // 
            // SubjectErrorLabel
            // 
            this.SubjectErrorLabel.AutoSize = true;
            this.SubjectErrorLabel.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold);
            this.SubjectErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.SubjectErrorLabel.Location = new System.Drawing.Point(509, 254);
            this.SubjectErrorLabel.Name = "SubjectErrorLabel";
            this.SubjectErrorLabel.Size = new System.Drawing.Size(105, 16);
            this.SubjectErrorLabel.TabIndex = 42;
            this.SubjectErrorLabel.Text = "Choose subject!";
            this.SubjectErrorLabel.Visible = false;
            // 
            // RoleChooser
            // 
            this.RoleChooser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RoleChooser.FormattingEnabled = true;
            this.RoleChooser.Location = new System.Drawing.Point(420, 186);
            this.RoleChooser.Name = "RoleChooser";
            this.RoleChooser.Size = new System.Drawing.Size(131, 28);
            this.RoleChooser.TabIndex = 43;
            this.RoleChooser.SelectedValueChanged += new System.EventHandler(this.RoleChooser_SelectedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(416, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 22);
            this.label3.TabIndex = 44;
            this.label3.Text = "Choose Role:";
            // 
            // ClassesComboBox
            // 
            this.ClassesComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClassesComboBox.FormattingEnabled = true;
            this.ClassesComboBox.Location = new System.Drawing.Point(122, 125);
            this.ClassesComboBox.Name = "ClassesComboBox";
            this.ClassesComboBox.Size = new System.Drawing.Size(121, 32);
            this.ClassesComboBox.TabIndex = 45;
            // 
            // SignUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(673, 418);
            this.Controls.Add(this.ClassesComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.RoleChooser);
            this.Controls.Add(this.SubjectErrorLabel);
            this.Controls.Add(this.SubjectLabel);
            this.Controls.Add(this.SubjectCombobox);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.LeaveEmptyImageButton);
            this.Controls.Add(this.ImagePreview);
            this.Controls.Add(this.InvalidImageErrorLabel);
            this.Controls.Add(this.ChooseImageButton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.BirthDateErrorLabel);
            this.Controls.Add(this.DateTimeLabel);
            this.Controls.Add(this.DateTimePicker);
            this.Controls.Add(this.ClassIDErrorLabel);
            this.Controls.Add(this.ClassIDLabel);
            this.Controls.Add(this.LogInButton);
            this.Controls.Add(this.FullNameErrorLable);
            this.Controls.Add(this.PasswordErrorLabel);
            this.Controls.Add(this.EmailErrorLabel);
            this.Controls.Add(this.PasswordEye);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Email);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.FullName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SignUpButton);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SignUp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SignUp";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SignUpButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox FullName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Email;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Button PasswordEye;
        private System.Windows.Forms.Label EmailErrorLabel;
        private System.Windows.Forms.Label PasswordErrorLabel;
        private System.Windows.Forms.Label FullNameErrorLable;
        private System.Windows.Forms.Button LogInButton;
        private System.Windows.Forms.Label ClassIDLabel;
        private System.Windows.Forms.Label ClassIDErrorLabel;
        private System.Windows.Forms.DateTimePicker DateTimePicker;
        private System.Windows.Forms.Label DateTimeLabel;
        private System.Windows.Forms.Label BirthDateErrorLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.OpenFileDialog FileDialog1;
        private System.Windows.Forms.Button ChooseImageButton;
        private System.Windows.Forms.Label InvalidImageErrorLabel;
        private System.Windows.Forms.PictureBox ImagePreview;
        private System.Windows.Forms.Button LeaveEmptyImageButton;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.ComboBox SubjectCombobox;
        private System.Windows.Forms.Label SubjectLabel;
        private System.Windows.Forms.Label SubjectErrorLabel;
        private System.Windows.Forms.ComboBox RoleChooser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ClassesComboBox;
    }
}