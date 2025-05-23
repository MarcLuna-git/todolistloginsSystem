using System;
using System.Windows.Forms;
using ToDoListProcess.DL; // Your data logic namespace

public partial class LoginForm : Form
{
    private IUserAccess userAccess;

    public LoginForm()
    {
        InitializeComponent();
        userAccess = new TextFileUserAccess();  // Use your text file user access
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        string username = txtUsername.Text.Trim();
        string password = txtPassword.Text.Trim();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            lblMessage.Text = "Please enter both username and password.";
            return;
        }

        bool isValid = userAccess.ValidateCredentials(username, password);
        if (isValid)
        {
            MessageBox.Show($"Welcome {username}!", "Login Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            this.Hide();
          
        }
        else
        {
            lblMessage.Text = "Invalid username or password.";
        }
    }
}
