namespace todo_Desktop
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtUsername = new Label();
            txtUname = new TextBox();
            lblpassword = new Label();
            txtpassword = new TextBox();
            btnlogin = new Button();
            SuspendLayout();
            // 
            // txtUsername
            // 
            txtUsername.AutoSize = true;
            txtUsername.Location = new Point(244, 105);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(78, 20);
            txtUsername.TabIndex = 0;
            txtUsername.Text = "Username:";
            txtUsername.Click += txtUsername_Click;
            // 
            // txtUname
            // 
            txtUname.Location = new Point(337, 98);
            txtUname.Name = "txtUname";
            txtUname.Size = new Size(172, 27);
            txtUname.TabIndex = 1;
            // 
            // lblpassword
            // 
            lblpassword.AutoSize = true;
            lblpassword.Location = new Point(244, 170);
            lblpassword.Name = "lblpassword";
            lblpassword.Size = new Size(73, 20);
            lblpassword.TabIndex = 2;
            lblpassword.Text = "Password:";
            lblpassword.Click += lblpassword_Click;
            // 
            // txtpassword
            // 
            txtpassword.BackColor = SystemColors.HighlightText;
            txtpassword.Location = new Point(337, 163);
            txtpassword.Name = "txtpassword";
            txtpassword.PasswordChar = '*';
            txtpassword.Size = new Size(172, 27);
            txtpassword.TabIndex = 3;
            txtpassword.TextChanged += txtpassword_TextChanged;
            // 
            // btnlogin
            // 
            btnlogin.Location = new Point(337, 223);
            btnlogin.Name = "btnlogin";
            btnlogin.Size = new Size(172, 29);
            btnlogin.TabIndex = 4;
            btnlogin.Text = "LOGIN";
            btnlogin.UseVisualStyleBackColor = true;
            btnlogin.Click += btnlogin_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnlogin);
            Controls.Add(txtpassword);
            Controls.Add(lblpassword);
            Controls.Add(txtUname);
            Controls.Add(txtUsername);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label txtUsername;
        private TextBox txtUname;
        private Label lblpassword;
        private TextBox txtpassword;
        private Button btnlogin;
    }
}
