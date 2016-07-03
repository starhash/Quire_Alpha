using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using System.IO.IsolatedStorage;

namespace Quire
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        private MobileServiceCollection<TodoItem, TodoItem> personalItems;
        private MobileServiceCollection<TodoItem, TodoItem> officeItems;
        private MobileServiceCollection<TodoItem, TodoItem> indoorItems;
        private MobileServiceCollection<TodoItem, TodoItem> outdoorItems;
        private IMobileServiceTable<TodoItem> todoTable = App.QuireService.GetTable<TodoItem>();
        public static string[] pages = {"personal", "office", "indoor", "outdoor"};
        public static string page = "personal";

        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
        }


        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            NavigationService.RemoveBackEntry();

            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
            await Refresh();
        }

        async Task Refresh()
        {
            var setting = IsolatedStorageSettings.ApplicationSettings;
            string u = setting["userName"].ToString();
            try
            {
                personalItems = await todoTable.Where(x => x.Type == "personal" && x.UserName == (u)).ToCollectionAsync();
                personalList.ItemsSource = personalItems;
                officeItems = await todoTable.Where(x => x.Type == "office" && x.UserName == (u)).ToCollectionAsync();
                officeList.ItemsSource = officeItems;
                indoorItems = await todoTable.Where(x => x.Type == "indoor" && x.UserName == (u)).ToCollectionAsync();
                indoorList.ItemsSource = indoorItems;
                outdoorItems = await todoTable.Where(x => x.Type == "outdoor" && x.UserName == (u)).ToCollectionAsync();
                outdoorList.ItemsSource = outdoorItems;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void SetProgressIndicator(bool value)
        {
            SystemTray.ProgressIndicator.IsIndeterminate = value;
            SystemTray.ProgressIndicator.IsVisible = value;
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {

        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/NewEvent.Xaml", UriKind.Relative));
        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ApplicationBarMenuItem_Click_1(object sender, EventArgs e)
        {

            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));

        }

        private void personalList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Panorama_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            var setting = IsolatedStorageSettings.ApplicationSettings;
            string u = setting["userName"].ToString();
            panoramaSelector.Title = u;
        }

        private void officeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void indoorList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void outdoorList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}