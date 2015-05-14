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
    public partial class SetIPTemporaryView : Window
    {
        
        private String suggestedIP = null;
        private String suggestedNetMask = null;
        private BecaonDevice beaconDevice;

        // Ping timeout for Used IP's search process
        private const int PING_TIMEOUT = 200;
        // Maximum number of IPs to search for when looking
        // for an unused IP address
        private const int MAX_IPS_TO_SEARCH = 20;

        public SetIPTemporaryView(BecaonDevice gateway)
        {
            InitializeComponent();
            beaconDevice = gateway;

            getSuggestedIpAddressAndNetmask();
            txt_TempIPAddress.setIP(suggestedIP);
            txt_TempNetworkMask.setIP(suggestedNetMask);
            //txt_TempIPAddress.setIP("192.168.1.1");
            //txt_TempNetworkMask.setIP("255.255.255.0");
        }

        public SetIPTemporaryPresenter Presenter
        {
            get { return DataContext as SetIPTemporaryPresenter; }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (!IsAddressValid(txt_TempIPAddress.getIP()))
            {
                StringBuilder msg = new StringBuilder();
                msg.Append("IP ");
                msg.Append(txt_TempIPAddress.getIP());
                msg.Append(" is not valid. Please enter a different IP.");
                MessageBox.Show(msg.ToString(), "Beacon Gateway Browse Tool", MessageBoxButton.OK);
                return;
            }

            if (ipIsReachable(txt_TempIPAddress.getIP()))
            {
                StringBuilder msg = new StringBuilder();
                msg.Append("IP ");
                msg.Append(txt_TempIPAddress.getIP());
                msg.Append(" is already in use. Please enter a different IP.");
                MessageBox.Show(msg.ToString(), "Beacon Gateway Browse Tool", MessageBoxButton.OK);
                return;
            }
            Presenter.SendTempIPConfig(beaconDevice);
        }
        
        private bool ipIsReachable(string ipAddr)
        {
            Ping pingSender = new Ping();
            
            // The timeout was hardcoded to 100 mSec
            PingReply reply = pingSender.Send(ipAddr,PING_TIMEOUT);

            if (reply.Status == IPStatus.Success) return true;
            return false;
        }

        private bool IsAddressValid(string addrString)
        {
            IPAddress address;
            return IPAddress.TryParse(addrString, out address);
        }
 
        private IPAddress findUnusedIP(IPAddress ipAddr, IPAddress netmask)
        {
            byte[] ipBytes = ipAddr.GetAddressBytes();
            //reverse the bytes due to little endian 
            Array.Reverse(ipBytes);
            UInt32 ipNumAddr = BitConverter.ToUInt32(ipBytes, 0); 

            byte[] netmaskBytes = netmask.GetAddressBytes();
            Array.Reverse(netmaskBytes);
            UInt32 netmaskNumAddr = BitConverter.ToUInt32(netmaskBytes, 0); 
            

            // This is the first IP address of the network
            UInt32 networkStart = (ipNumAddr & netmaskNumAddr) +1;
            // This is the last IP address of the network
            UInt32 networkEnd = (ipNumAddr | ~netmaskNumAddr) - 1;

            UInt32 currentAddr;


            byte[] tempbytes;
            Ping pingSender = new Ping();

            PingReply reply;

            int ipSearchCount = 0;

            for (currentAddr = networkEnd; currentAddr >= networkStart; currentAddr--)
            {
                tempbytes = BitConverter.GetBytes(currentAddr); 
                Array.Reverse(tempbytes); 
                IPAddress unusedIp = new IPAddress(tempbytes);
                // Try to ping the address
                reply = pingSender.Send(unusedIp, PING_TIMEOUT);
                // If ping is unsuccessfull return it as an unused IP Address
                if (reply.Status != IPStatus.Success)
                {
                    return unusedIp;
                }
                if (ipSearchCount >= MAX_IPS_TO_SEARCH) return null;
                ipSearchCount++;
            }

            return null;
        }

        // Function that determines a siggested Network Mask and IP Address
        private void getSuggestedIpAddressAndNetmask() 
        {
            // Check all Network interfaces and select the one that looks like
            // an active one
            foreach (NetworkInterface f in NetworkInterface.GetAllNetworkInterfaces())
            {
                //Console.WriteLine("New interface discovered: {0}", f.ToString());
                //Console.WriteLine(f.Name);
                IPInterfaceProperties ipInterface = f.GetIPProperties();
                foreach (UnicastIPAddressInformation unicastAddress in ipInterface.UnicastAddresses)
                {
                    IPAddress ipAddr = unicastAddress.Address;
                    IPAddress netMask = unicastAddress.IPv4Mask;

                    if ((ipAddr != null) && (netMask != null))
                    {
                        // Found a valid interface. Only an IP is suggested for the 
                        // first valid interface
                        suggestedNetMask = netMask.ToString();
                        
                        byte[] nmBin = netMask.GetAddressBytes();
                        byte[] ipBin = ipAddr.GetAddressBytes();

                        IPAddress unusedIP = findUnusedIP(ipAddr, netMask);
                        if (unusedIP != null)
                        {
                            suggestedIP = unusedIP.ToString();
                        }
                        else
                        {
                            suggestedIP = null;
                        }
                    }
                }
            }  
        }


    }
}