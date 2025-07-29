using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace _2048WindowsFormsApp
{
    public partial class ResultsForm : Form
    {
        private List<User> users;
        public ResultsForm()
        {
            InitializeComponent();
        }

        private void ResultsForm_Load(object sender, EventArgs e)
        {
            users = UserStorage.GetUsersResults();

            ShowUsersResults(users);
        }

        private void ShowUsersResults(List<User> users)
        {
            foreach (var user in users)
            {
                resultsDataGridView.Rows.Add(user.Name, user.Score);
            }
        }
    }
}
