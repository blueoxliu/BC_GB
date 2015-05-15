using System.Collections.ObjectModel;
using GatewayBrowser.Model;
using GatewayBrowser.Views;
using System.Globalization;
using System.Windows.Input;
using System.Net;
using System.Collections.Generic;
using System.Windows;
using System.Diagnostics;

using GatewayBrowser.Properties;
using GatewayBrowser.Help;

using System.Waf.Applications;
using System.Waf.Presentation.Services;
using System.Waf.Applications.Services;

namespace GatewayBrowser.Presenters
{
    public class ApplicationPresenter : PresenterBase<Shell>
    {
        private readonly DeviceRepository _gatewayRepository;
        private AsyncObservableCollection<BecaonDevice> _currentGateways;
        private string _statusText;

        private readonly DelegateCommand aboutCommand;
        private readonly DelegateCommand scanCommand;
        private readonly DelegateCommand manuallyAddCommand;
        private BeaconDiscoveryService beaconDiscoveryService;

        private IMessageService messageService = new MessageService();
        public ICommand AboutCommand { get { return aboutCommand; } }
        public ICommand ManuallyAddCommand { get { return manuallyAddCommand; } }
        public ICommand ScanCommand { get { return scanCommand; } }

        public BeaconDiscoveryService BeaconDiscoveryService
        {
            get { return beaconDiscoveryService; }
            set { }
        }

        public ApplicationPresenter(Shell view, DeviceRepository gatewayRepository): base(view)
        {
            _gatewayRepository = gatewayRepository;
            _currentGateways = new AsyncObservableCollection<BecaonDevice>(_gatewayRepository.FindAll());

            this.aboutCommand = new DelegateCommand(ShowAboutMessage);
            this.scanCommand = new DelegateCommand(StartScan);
            this.manuallyAddCommand = new DelegateCommand(ShwoAddGatewayWindow);

            // Create the only one 
            beaconDiscoveryService = new BeaconDiscoveryService(this);

            // Add this object to the DiscoveryService event list
            beaconDiscoveryService.newDiscoveredDevice += new BeaconDiscoveryService.discoveredDevice(deviceBrowser_newDiscoveredDevice);
            // After startup, do a browse so the screen will be populated
            //beaconDiscoveryService.sendDiscoverRequest();

            // Set all the device to offline when startup
            SetAllDeviceOffline();

        }

        public AsyncObservableCollection<BecaonDevice> CurrentGateways
        {
            get { return _currentGateways; }
            set
            {
                _currentGateways = value;
                OnPropertyChanged("CurrentGateways");
            }
        }

        public string StatusText
        {
            get { return _statusText; }
            set
            {
                _statusText = value;
                OnPropertyChanged("StatusText");
            }
        }

        public void SaveGateway(BecaonDevice gateway)
        {
            StatusText = string.Format("Start save device");
            if (!CurrentGateways.Contains(gateway))
            {
                CurrentGateways.Add(gateway);
                _gatewayRepository.Save(gateway);
                // StatusText = string.Format("Gateway '{0}' was saved.", gateway.LookupName);
            }
        }

        public void DeleteGateway(BecaonDevice gateway)
        {
            if (CurrentGateways.Contains(gateway))
            {
                CurrentGateways.Remove(gateway);
            }
            _gatewayRepository.Delete(gateway);
            StatusText = string.Format("Becaon Device '{0}' was deleted.",gateway.LookupName );
        }

        public void OpenWebGatewayConfigTab(BecaonDevice gateway)
        {
            if (gateway == null) return;
            View.AddTab(new ConfigWebContainerPresenter(this, new ConfigWebContainerView(), gateway));
            
        }

        public void ShowSetIPWindow(BecaonDevice gateway)
        {
            if (gateway == null) return;
            View.ShowModuleWindow(new SetIPConfigPresenter(this, new SetIPConfigView(gateway)));

        }

        public void ShowSetIPTemporaryWindow(BecaonDevice gateway)
        {
            if (gateway == null) return;
            View.ShowModuleWindow(new SetIPTemporaryPresenter(this, new SetIPTemporaryView(gateway)));

        }

        public void SendContextMenuCommand(ContextMenuCommand cmdType, BecaonDevice gateway)
        {
            switch (cmdType)
            {
                case ContextMenuCommand.Reboot:
                    //Debug.WriteLine("Case 1");
                    this.beaconDiscoveryService.RebootModule(gateway);
                    break;
                case ContextMenuCommand.RemoveTempIP:
                    //Debug.WriteLine("Case 2");
                    this.beaconDiscoveryService.removeTemporaryIPAddr(gateway);
                    break;
                default:
                    //Debug.WriteLine("Default case");
                    break;
            }
        }

        public void ShowErrorMessage(string errorString)
        {
            string strErrorString = errorString;
            messageService.ShowMessage("", strErrorString);
        }

        public void StopScan()
        {
            beaconDiscoveryService.StopScan();
        }

        #region private function
        private void ShowAboutMessage()
        {
            string strHelpString = string.Format(CultureInfo.CurrentCulture, Resources.AboutText, ApplicationInfo.ProductName, ApplicationInfo.Version);
            messageService.ShowMessage("", strHelpString);
        }

        private void ShwoAddGatewayWindow()
        {
            View.ShowModuleWindow(new AddGatewayPresenter(this, new AddGatewayView()));
        }

        // Event received when a new unit is discovered
        private void deviceBrowser_newDiscoveredDevice(BecaonDevice newDevice, bool IPChanged)
        {

            if (!CurrentGateways.Contains(newDevice))
            {
                newDevice.DeviceStatus = GatewayStatus.Active;
                SaveGateway(newDevice);
                StatusText = string.Format("New Beacon device '{0}' was found.", newDevice.LookupName);
            }
            else
            {
                DeleteGateway(newDevice);
                newDevice.DeviceStatus = GatewayStatus.Active;
                System.Threading.Thread.Sleep(200);
                SaveGateway(newDevice);
                StatusText = string.Format("Offline Beacon device '{0}' was online now.", newDevice.LookupName);
            }
            if (IPChanged) // for protocol version 2
            {
                beaconDiscoveryService.RebootModule(newDevice);
            }

        }

        private void SetAllDeviceOffline()
        {
            foreach (BecaonDevice item in CurrentGateways)
            {
                item.DeviceStatus = GatewayStatus.Offline;
            }
        }

        private void DeleteOnlineDeviceBeforeScan()
        {
            foreach (BecaonDevice item in CurrentGateways)
            {
                if (item.DeviceStatus == GatewayStatus.Active)
                {
                    DeleteGateway(item);
                }
            }
        }

        // Button that starts the network browsing.
        // Actually the browsing is almost inmediate but for customer feedback purposes
        // a progress bar is shown in the status strip
        private void StartScan()
        {
            DeleteOnlineDeviceBeforeScan();

            beaconDiscoveryService.sendDiscoverRequest();

            StatusText = string.Format("Start browsing Beacon gateway...");
            // Show progess bar when start scan.
            //ProgessBarView progessBarView = new ProgessBarView(this);
            //progessBarView.Owner = this.View;
            //progessBarView.ShowDialog();
        }

#endregion
    }
}