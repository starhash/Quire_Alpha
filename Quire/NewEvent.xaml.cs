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
    public partial class Page2 : PhoneApplicationPage
    {
        private MobileServiceCollection<TodoItem, TodoItem> todos;
        private IMobileServiceTable<TodoItem> todoTable = App.QuireService.GetTable<TodoItem>();
        
        public Page2()
        {
            InitializeComponent();
        }

        private async void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            try
            {
                IsolatedStorageSettings setting = IsolatedStorageSettings.ApplicationSettings;
                newProgress.IsIndeterminate = true;
                    TodoItem t = new TodoItem();
                    t.Header = eventHeader.Text;
                    t.UserName = setting["userName"].ToString();
                    t.Password = "";
                    t.Time = DateTime.Now;
                    t.Description = eventDescription.Text;
                    t.Checked = false;
                    t.Type = MainPage.page;
                    t.Visible = true;
                    await todoTable.InsertAsync(t);
                    await Refresh();
                newProgress.IsIndeterminate = false;
                NavigationService.Navigate(new Uri("/HomePage.xaml", UriKind.Relative));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                newProgress.IsIndeterminate = false;
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

        private void eventHeader_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}