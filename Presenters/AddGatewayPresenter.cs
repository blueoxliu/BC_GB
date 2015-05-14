using GatewayBrowser.Model;
using GatewayBrowser.Views;
using GatewayBrowser.Help;
using System.Windows.Controls;

namespace GatewayBrowser.Presenters
{
    public class AddGatewayPresenter : PresenterBase<AddGatewayView>
    {
        private readonly ApplicationPresenter _applicationPresenter;
        private BecaonDevice _gateway;


        public AddGatewayPresenter(ApplicationPresenter applicationPresenter, AddGatewayView view)
            : base(view, "Gateway.LookupName")
        {
            _applicationPresenter = applicationPresenter;
            
        }

        public BecaonDevice Gateway
        {
            get { return _gateway; }
        }

        public void Save()
        {
            // Save the offline device.
            _gateway = new BecaonDevice(1, "00:00:00:00:00:00");;
            _gateway.ProductName = View.cmb_DeviceType.SelectedItem.ToString();
            _gateway.MainIpAddress = "127.0.0.1";
            _gateway.DeviceStatus = GatewayStatus.Offline;
            _applicationPresenter.SaveGateway(_gateway);

            _applicationPresenter.StatusText = string.Format("Offline Beacon Device '{0}' was added.", _gateway.LookupName);
        }

        public override bool Equals(object obj)
        {
            AddGatewayPresenter presenter = obj as AddGatewayPresenter;
            return presenter != null && presenter.Gateway.Equals(Gateway);
        }
    }
}