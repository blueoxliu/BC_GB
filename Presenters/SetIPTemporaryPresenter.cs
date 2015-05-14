using GatewayBrowser.Model;
using GatewayBrowser.Views;
using GatewayBrowser.Help;
using System.Windows.Controls;
using System.Net;
using System.Net.Sockets;

namespace GatewayBrowser.Presenters
{
    public class SetIPTemporaryPresenter : PresenterBase<SetIPTemporaryView>
    {
        private readonly ApplicationPresenter _applicationPresenter;
        private BecaonDevice _gateway;
        private BeaconDiscoveryService discoveryService;


        public SetIPTemporaryPresenter(ApplicationPresenter applicationPresenter, SetIPTemporaryView view)
            : base(view, "Gateway.LookupName")
        {
            _applicationPresenter = applicationPresenter;
            
        }

        public BecaonDevice Gateway
        {
            get { return _gateway; }
        }

        public void SendTempIPConfig(BecaonDevice beaconDevice)
        {

            discoveryService.changeTemporaryIPAddr(beaconDevice, View.txt_TempIPAddress.getIP(), View.txt_TempNetworkMask.getIP()); 
        }
       
        public override bool Equals(object obj)
        {
            SetIPTemporaryPresenter presenter = obj as SetIPTemporaryPresenter;
            return presenter != null && presenter.Gateway.Equals(Gateway);
        }
    }
}