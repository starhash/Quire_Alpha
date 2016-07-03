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
    public partial class Signin : PhoneApplicationPage
    {
        private MobileServiceCollection<TodoItem, TodoItem> todos;
        private IMobileServiceTable<TodoItem> todoTable = App.QuireService.GetTable<TodoItem>();
        
        
        public Signin()
        {
            InitializeComponent();
              DataContext = App.ViewModel;
        }
        
        private async void SignIn_Click(object sender, RoutedEventArgs e)
        {
            await Refresh();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            NavigationService.RemoveBackEntry();

            var setting = IsolatedStorageSettings.ApplicationSettings;

            if (setting.Contains("userName"))
            {
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));

            }
        }

        async Task Refresh()
        {
            try
            {
                signInProgress.IsIndeterminate = true;
                todos = await todoTable.Where(x => x.UserName == (userName.Text)).ToCollectionAsync();
                if(todos.Count==0)
                {
                    throw new Exception("Incorrect Username/Password : " + userName.Text);
                }
                TodoItem t = todos.ElementAt<TodoItem>(0);
                if(userPassword.Password.Equals(t.Password))
                {
                    var setting = IsolatedStorageSettings.ApplicationSettings;
                    if (setting.Contains("userName"))
                        setting.Remove("userName");
                    setting.Add("userName", t.UserName);
                }
                signInProgress.IsIndeterminate = false;
                NavigationService.Navigate(new Uri("/HomePage.xaml", UriKind.Relative));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                signInProgress.IsIndeterminate = false;
            }

        }

        private void signup_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Signup.xaml", UriKind.Relative));
        }
    }
}