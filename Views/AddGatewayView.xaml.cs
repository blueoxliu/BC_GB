using System.Windows;
using System.Windows.Controls;
using GatewayBrowser.Presenters;
using Microsoft.Win32;

namespace GatewayBrowser.Views
{
    public partial class AddGatewayView : Window
    {
        public string selectDeviceType;

        public AddGatewayView()
        {
            InitializeComponent();
            // Set the default item
            cmb_DeviceType.SelectedIndex = 0;
        }

        public AddGatewayPresenter Presenter
        {
            get { return DataContext as AddGatewayPresenter; }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Presenter.Save();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmb_DeviceType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (sender as ComboBox).SelectedItem as ComboBoxItem;
            if (item != null)
            {
                //MessageBox.Show(item.Content.ToString());
                selectDeviceType = item.Content.ToString();
            }

        }

    }
}