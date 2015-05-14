using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using GatewayBrowser.Model;
using GatewayBrowser.Presenters;
using GatewayBrowser.UserControls;
using System.Globalization;

using GatewayBrowser.Views;

namespace GatewayBrowser
{
    public partial class Shell : Window
    {
      
        public Shell()
        {
            InitializeComponent();
            DataContext = new ApplicationPresenter(this, new DeviceRepository());

            // Add a route event to handle the close action
            this.AddHandler(CloseableTabItem.CloseTabEvent, new RoutedEventHandler(this.CloseTab));

        }

        public void AddTab<T>(PresenterBase<T> presenter)
        {
            CloseableTabItem newTab = null;

            for (int i = 0; i < tabs.Items.Count; i++)
            {
                CloseableTabItem existingTab = (CloseableTabItem)tabs.Items[i];

                if (existingTab.DataContext.Equals(presenter))
                {
                    tabs.Items.Remove(existingTab);
                    newTab = existingTab;
                    break;
                }
            }

            if (newTab == null)
            {
                newTab = new CloseableTabItem();
                Binding headerBinding = new Binding(presenter.TabHeaderPath);
                BindingOperations.SetBinding(newTab, CloseableTabItem.HeaderProperty, headerBinding);
                newTab.DataContext = presenter;
                newTab.Content = presenter.View;
            }

            tabs.Items.Insert(0, newTab);
            newTab.Focus();
        }

        private void CloseTab(object source, RoutedEventArgs args)
        {
            TabItem tabItem = args.Source as TabItem;
            if (tabItem != null)
            {
                TabControl tabControl = tabItem.Parent as TabControl;
                if (tabControl != null)
                    tabControl.Items.Remove(tabItem);
            }
        }

        public void ShowModuleWindow<T>(PresenterBase<T> presenter)
        {

            Window addGatewayView = presenter.View as Window;
            addGatewayView.Owner = this;
            addGatewayView.DataContext = presenter;
            addGatewayView.ShowDialog();

        }

    }
}