using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.WindowsAzure.MobileServices;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;

namespace Quire
{
    public partial class Page1 : PhoneApplicationPage
    {
        private MobileServiceCollection<TodoItem, TodoItem> todos;
        private IMobileServiceTable<TodoItem> todoTable = App.QuireService.GetTable<TodoItem>();
        
        public Page1()
        {
            InitializeComponent();
        }

        private async void signUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                signUpProgress.IsIndeterminate = true;
                todos = await todoTable.Where(x => x.UserName == (newUserName.Text)).ToCollectionAsync();
                if (todos.Count != 0)
                {
                    throw new Exception("User Name Already Exists! : " + newUserName.Text);
                }
                if (newUserPassword.Password.Equals(repeatUserPassword.Password))
                {
                    IsolatedStorageSettings setting = IsolatedStorageSettings.ApplicationSettings;
                    TodoItem t = new TodoItem();
                    t.Header = "@signup";
                    t.UserName = newUserName.Text;
                    t.Password = newUserPassword.Password;
                    t.Time = DateTime.Now;
                    t.Description = "User Signed Up";
                    t.Checked = true;
                    t.Type = "system";
                    t.Visible = false;
                    if (setting.Contains("userName"))
                        setting.Remove("userName");
                    setting.Add("userName", t.UserName);
                    await todoTable.InsertAsync(t);
                    await Refresh();    
                }
                signUpProgress.IsIndeterminate = false;
                NavigationService.Navigate(new Uri("/HomePage.xaml", UriKind.Relative));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                signUpProgress.IsIndeterminate = false;
            }
        }

        async Task Refresh()
        {
            var setting = IsolatedStorageSettings.ApplicationSettings;
            string u = setting["userName"].ToString();
            try
            {
                todos = await todoTable.Where(x => x.UserName == (u)).ToCollectionAsync();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured. Check your internet connection\n" + ex.Message);
            }

        }
    }
}