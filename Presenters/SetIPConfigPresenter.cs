
using GatewayBrowser.Model;
using GatewayBrowser.Views;
using GatewayBrowser.Help;


namespace GatewayBrowser.Presenters
{
    public class SetIPConfigPresenter : PresenterBase<SetIPConfigView>
    {
        private readonly ApplicationPresenter _applicationPresenter;
        private BecaonDevice _gateway;
        private BeaconDiscoveryService discoveryService;

        public SetIPConfigPresenter(ApplicationPresenter applicationPresenter, SetIPConfigView view)
            : base(view, "Gateway.LookupName")
        {
            _applicationPresenter = applicationPresenter;
            discoveryService = _applicationPresenter.BeaconDiscoveryService;
            
        }

        public BecaonDevice Gateway
        {
            get { return _gateway; }
        }

        public void SendIPConfig(BecaonDevice beaconDevice)
        {

            discoveryService.changePermanentIPAddrs(beaconDevice);
        }

        public override bool Equals(object obj)
        {
            SetIPConfigPresenter presenter = obj as SetIPConfigPresenter;
            return presenter != null && presenter.Gateway.Equals(Gateway);
        }
    }
}