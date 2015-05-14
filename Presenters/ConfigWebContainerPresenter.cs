using GatewayBrowser.Model;
using GatewayBrowser.Views;
using GatewayBrowser.Help;

namespace GatewayBrowser.Presenters
{
    public class ConfigWebContainerPresenter : PresenterBase<ConfigWebContainerView>
    {
        private readonly BecaonDevice _gateway;

        public ConfigWebContainerPresenter(ApplicationPresenter applicationPresenter, ConfigWebContainerView view, BecaonDevice gateway)
            : base(view, "Gateway.LookupName")     
        {
            // Build the web browser address
            string frameBrowserIP = string.Empty;
            if(gateway.DeviceStatus != GatewayStatus.Active)
            {
                frameBrowserIP = gateway.MainIpAddress + ":8080";
            }
            else
            {
                frameBrowserIP = gateway.MainIpAddress;
            }

            System.Uri gatewaywebaddress = new System.Uri("http://" + frameBrowserIP);
            view.Webbw_GatewayWebConfig.Source = gatewaywebaddress;
            _gateway = gateway;
        }

        public BecaonDevice Gateway
        {
            get { return _gateway; }
        }

        // Overwirte the equal method to compare the presenter
        public override bool Equals(object obj)
        {
            ConfigWebContainerPresenter presenter = obj as ConfigWebContainerPresenter;
            return presenter != null && presenter.Gateway.Equals(Gateway);
        }
    }
}