using System.Threading.Tasks;

namespace todo_Desktop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txtUsername_Click(object sender, EventArgs e)
        {

        }

        private void txtpassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblpassword_Click(object sender, EventArgs e)
        {

        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            TextFileUserAccess userAccess = new TextFileUserAccess();

            if (userAccess.ValidateCredentials(username, password))
            {
                lblMessage.Text = "";

                // Floating welcome message
                var messageForm = new FloatingMessageForm($"Welcome, {username}!");
                messageForm.Show();

                this.Hide();
                await Task.Delay(2000);

                MainForm mainForm = new MainForm();
                mainForm.Show();
            }
            else
            {
                lblMessage.Text = "Invalid username or password!";
                lblMessage.ForeColor = Color.Red;
            }
        }

    }
}
