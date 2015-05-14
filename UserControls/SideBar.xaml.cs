using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using GatewayBrowser.Model;
using GatewayBrowser.Presenters;
using GatewayBrowser.Help;

namespace GatewayBrowser.UserControls
{
    public partial class SideBar : UserControl
    {
        public SideBar()
        {
            InitializeComponent();
        }

        public ApplicationPresenter Presenter
        {
            get { return DataContext as ApplicationPresenter; }
        }

        private void OpenWebGateway_Click(object sender, RoutedEventArgs e)
        {
            Button button = e.OriginalSource as Button;

            if (button != null)
            {
                Presenter.OpenWebGatewayConfigTab(button.DataContext as BecaonDevice);
            }

        }

        private void ContextMenu_delete_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement obj = e.OriginalSource as FrameworkElement;

            if (obj != null)
             {
                 Presenter.DeleteGateway(obj.DataContext as BecaonDevice);
             }
        }

        // Fileter the online and offline for the source
        private void Online_Filter(object sender, FilterEventArgs e)
        {
            if (e.Item != null)
            {

                e.Accepted = ((BecaonDevice)e.Item).DeviceStatus.Equals(GatewayStatus.Active);

            }
        }

        private void Offline_Filter(object sender, FilterEventArgs e)
        {
            if (e.Item != null)
            {
                
                e.Accepted = (((BecaonDevice)e.Item).DeviceStatus.Equals(GatewayStatus.Offline));
                
            }

        }

        private void ContextMenu_ChangeTempIP_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement sourceItem = e.OriginalSource as FrameworkElement;

            if (sourceItem != null)
            {
                Presenter.ShowSetIPTemporaryWindow(sourceItem.DataContext as BecaonDevice);
            }
        }

        private void ContextMenu_SetIP_Click(object sender, RoutedEventArgs e)
        {

            FrameworkElement sourceItem = e.OriginalSource as FrameworkElement;

            if (sourceItem != null)
            {
                Presenter.ShowSetIPWindow(sourceItem.DataContext as BecaonDevice);
            }
        }

        private void ContextMenu_RemoveTempIP_Click(object sender, RoutedEventArgs e)
        {
             FrameworkElement sourceItem = e.OriginalSource as FrameworkElement;

             if (sourceItem != null)
             {
                 Presenter.SendContextMenuCommand(ContextMenuCommand.RemoveTempIP, sourceItem.DataContext as BecaonDevice);
             }
        }

        private void ContextMenu_RebootModule_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement sourceItem = e.OriginalSource as FrameworkElement;

            if (sourceItem != null)
            {
                Presenter.SendContextMenuCommand(ContextMenuCommand.Reboot, sourceItem.DataContext as BecaonDevice);
            }
        }


    }
}