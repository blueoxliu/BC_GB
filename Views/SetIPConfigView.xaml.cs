using System.Windows;
using System.Windows.Controls;
using GatewayBrowser.Presenters;
using Microsoft.Win32;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Net.NetworkInformation;

using GatewayBrowser.Model;
using GatewayBrowser.Help;

namespace GatewayBrowser.Views
{
    public partial class SetIPConfigView : Window
    {
        
        private BecaonDevice beaconDevice;

        // Ping timeout for Used IP's search process
        private const int PING_TIMEOUT = 200;

        private string m_originalIp;
        private string m_originalIp2;

        public SetIPConfigView(BecaonDevice gateway)
        {
            InitializeComponent();

            beaconDevice = gateway;
            InitDialog(gateway);
        }

        public SetIPConfigPresenter Presenter
        {
            get { return DataContext as SetIPConfigPresenter; }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (!IsAddressValid(this.txt_IPAddress_p1.getIP()))
            {
                StringBuilder msg = new StringBuilder();
                msg.Append("IP ");
                msg.Append(this.txt_IPAddress_p1.getIP());
                msg.Append(" is not valid. Please enter a different IP.");
                MessageBox.Show(msg.ToString(), "Beacon Gateway Browse Tool", MessageBoxButton.OK);
                return;
            }

            if (m_originalIp.CompareTo(this.txt_IPAddress_p1.getIP()) != 0 && ipIsReachable(this.txt_IPAddress_p1.getIP()))
            {
                StringBuilder msg = new StringBuilder();
                msg.Append("IP ");
                msg.Append(this.txt_IPAddress_p1.getIP());
                msg.Append(" is already in use. Please enter a different IP.");
                MessageBox.Show(msg.ToString(), "Beacon Gateway Browse Tool", MessageBoxButton.OK);
                return;
            }

            if (beaconDevice.GetIfcCount() > 1)
            {
                if (!IsAddressValid(this.txt_IPAddress_p2.getIP()))
                {
                    StringBuilder msg = new StringBuilder();
                    msg.Append("IP ");
                    msg.Append(this.txt_IPAddress_p2.getIP());
                    msg.Append(" is not valid. Please enter a different IP.");
                    MessageBox.Show(msg.ToString(), "Beacon Gateway Browse Tool", MessageBoxButton.OK);
                    return;
                }

                if (m_originalIp2.CompareTo(this.txt_IPAddress_p2.ToString()) != 0 && ipIsReachable(this.txt_IPAddress_p2.getIP()))
                {
                    StringBuilder msg = new StringBuilder();
                    msg.Append("IP ");
                    msg.Append(this.txt_IPAddress_p2.getIP());
                    msg.Append(" is already in use. Please enter a different IP.");
                    MessageBox.Show(msg.ToString(), "Beacon Gateway Browse Tool", MessageBoxButton.OK);
                    return;
                }

                IPAddress ipAddrNew = IPAddress.Parse(this.txt_IPAddress_p1.getIP());
                IPAddress ipNetmask = IPAddress.Parse(this.txt_NetworkMask_p1.getIP());

                IPAddress ipAddrNew2 = IPAddress.Parse(this.txt_IPAddress_p2.getIP());
                IPAddress ipNetmask2 = IPAddress.Parse(this.txt_NetworkMask_p2.getIP());

                UInt32 subnetAddr = BitConverter.ToUInt32(ipAddrNew.GetAddressBytes(), 0) & BitConverter.ToUInt32(ipNetmask.GetAddressBytes(), 0);
                UInt32 subnetAddr2 = BitConverter.ToUInt32(ipAddrNew2.GetAddressBytes(), 0) & BitConverter.ToUInt32(ipNetmask2.GetAddressBytes(), 0);

                if (subnetAddr == subnetAddr2)
                {
                    StringBuilder msg = new StringBuilder();
                    msg.AppendLine("The IP addresses must be in different networks.");
                    msg.Append("Please enter valid IP addresses.");
                    MessageBox.Show(msg.ToString(), "Beacon Gateway Browse Tool", MessageBoxButton.OK);
                    return;
                }
            }

            beaconDevice.MainIpAddress = this.txt_IPAddress_p1.getIP();
            beaconDevice.MainIpNetmask = this.txt_NetworkMask_p1.getIP();

            if (beaconDevice.GetIfcCount() > 1)
            {
                beaconDevice.SecIpAddress = this.txt_IPAddress_p2.getIP();
                beaconDevice.SecIpNetmask = this.txt_NetworkMask_p2.getIP();
            }

            Presenter.SendIPConfig(beaconDevice);
            
        }

        public void InitDialog(BecaonDevice device)
        {
            m_originalIp = device.MainIpAddress;
            this.lbl_IfcName_p1.Content = device.IfcName;
            this.txt_IPAddress_p1.setIP(device.MainIpAddress);
            this.txt_NetworkMask_p1.setIP(device.MainIpNetmask);
            this.txt_MAC_p1.Content = device.MainMACAddress;

            if (device.GetIfcCount() == 1)
            {
                // Hide port2
                this.grd_Port1.Visibility = Visibility.Collapsed;
                this.Height -= this.grd_Port2.Height;
            }
            else
            {
                m_originalIp2 = device.SecIpAddress;
                this.lbl_IfcName_p2.Content = device.SecIfcName;
                this.txt_IPAddress_p2.setIP(device.SecIpAddress);
                this.txt_NetworkMask_p2.setIP(device.SecIpNetmask);
                this.txt_MAC_p2.Content = device.SecMACAddress;
            }

        }

        private bool ipIsReachable(string ipAddr)
        {
            Ping pingSender = new Ping();

            // The timeout was hardcoded to 100 mSec
            PingReply reply = pingSender.Send(ipAddr, PING_TIMEOUT);

            if (reply.Status == IPStatus.Success) return true;
            return false;
        }

        private bool IsAddressValid(string addrString)
        {
            IPAddress address;
            return IPAddress.TryParse(addrString, out address);
        }

    }
}