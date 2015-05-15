using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Net.NetworkInformation;

using GatewayBrowser.Presenters;
using GatewayBrowser.Model;

namespace GatewayBrowser.Help
{

    public class BeaconDiscoveryService
    {
        // This is the Listen Port for the Beacon Discovery Service
        private const int beaconDiscoveryListenPort = 9999;

        // This is the Listen Port for the Beacon Discovery Service
        private const int beaconDiscoverySendPort = 9999;

        // Ping timeout for Used IP's search process
        private const int PING_TIMEOUT = 200;

        // Maximum number of IPs to search for when looking
        // for an unused IP address
        private const int MAX_IPS_TO_SEARCH = 20;

        // listener socket that will receive responses from the slave devices
        private List<UdpClient> listenerSockets;

        private BecaonDevice deviceConfig;

        // This is the event that signals that a new device 
        // has been found
        public delegate void discoveredDevice(BecaonDevice newDevice, bool IPChanged);
        public event discoveredDevice newDiscoveredDevice;

        // Save the ApplicationPresenter instance
        private readonly ApplicationPresenter _applicationPresenter;

        // This class keeps the state of the Object in between
        // UDP messages
        class UdpState
        {
            public UdpClient listenerSocket;
            public IPEndPoint endPoint;
            public BeaconDiscoveryService discoveryService;
        }

        // Constructor of the class. This constructor will start the listening thread (callback)
        public BeaconDiscoveryService(ApplicationPresenter applicationPresenter)
        {
            _applicationPresenter = applicationPresenter;
            deviceConfig = null;
            // Create the Listening Socket 
            // Create the socket using 0 as the port so we get
            // an available free socket port
            listenerSockets = new List<UdpClient>();


            //try
            //{
            //    IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, beaconDiscoveryListenPort);
            //    listenerSocket = new UdpClient(endPoint);

            //    // Create the state. The state will be used to transfer data to event functions
            //    UdpState state = new UdpState();
            //    state.endPoint = endPoint;
            //    state.listenerSocket = listenerSocket;
            //    state.discoveryService = this;

            //    // Debugging code to show the UDP port that we are bind to
            //    System.Diagnostics.Debug.Print("Local UDP Port: {0}", ((IPEndPoint)(listenerSocket.Client.LocalEndPoint)).Port);

            //    // Start the listening process as a callback
            //    listenerSocket.BeginReceive(new AsyncCallback(UDPReceiveCallback), state);
            //}
            //catch (Exception ex)
            //{
            //    applicationPresenter.ShowErrorMessage("Can not start the browser service!");
            //    System.Diagnostics.Debug.Print("Start listenerSocket meet exception {0}", ex.ToString());
            //}
           
        }

        private void CreateListenerSockets()
        {
            IPAddress ipAddr = IPAddress.Any;
            listenerSockets.Clear();

            foreach (NetworkInterface netif in NetworkInterface.GetAllNetworkInterfaces())
            {
                IPInterfaceProperties properties = netif.GetIPProperties();

                foreach (IPAddressInformation unicast in properties.UnicastAddresses)
                {
                    ipAddr = unicast.Address;

                    // We're only interested in IPv4 addresses for now
                    if (ipAddr.AddressFamily != AddressFamily.InterNetwork)
                        continue;

                    // Ignore loopback addresses (e.g., 127.0.0.1)
                    if (IPAddress.IsLoopback(ipAddr))
                        continue;

                    // Create the Listening Socket 
                    // Create the socket using 0 as the port so we get
                    // an available free socket port
                    IPEndPoint endPoint = new IPEndPoint(ipAddr, 0);
                    UdpClient newSocket = null;
                    try
                    {
                        newSocket = new UdpClient(endPoint);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        continue;
                    }

                    listenerSockets.Add(newSocket);

                    // Create the state. The state will be used to transfer data to event functions
                    UdpState state = new UdpState();
                    state.endPoint = endPoint;
                    state.listenerSocket = newSocket;
                    state.discoveryService = this;

                    // Debugging code to show the UDP port that we are bind to
                    System.Diagnostics.Debug.Print("Local UDP Port: {0}", ((IPEndPoint)(newSocket.Client.LocalEndPoint)).Port);

                    // Start the listening process as a callback
                    newSocket.BeginReceive(new AsyncCallback(UDPReceiveCallback), state);
                }
            }
        }

        private static void UDPReceiveCallback(IAsyncResult ar)
        {
            UdpState state = (UdpState)(ar.AsyncState);
            // Get the UdpClient Object that generated the event
            UdpClient udpClient = state.listenerSocket;
            IPEndPoint endPoint = state.endPoint;

            try
            {
                Byte[] receivedBytes = udpClient.EndReceive(ar, ref endPoint);
                string receivedString = Encoding.ASCII.GetString(receivedBytes);

                System.Diagnostics.Debug.Print("Received from: {0}:{1} data: {2}", endPoint.Address.ToString(), endPoint.Port.ToString(), receivedString);

                XMLParser xmlParser = new XMLParser();
                XMLNode xn = xmlParser.Parse(receivedString);
                string responseSerialNumber = xn.GetValue("DiscoveryResponse>0>SerialNumber>0>_text");

                // Abort if not received data or response is not Discover Service
                // Make sure the received message length is right
                if (responseSerialNumber!= null && responseSerialNumber.Length > 2)
                {
                    BecaonDevice newBecaonDevice;
                    string secIFCName = xn.GetValue("DiscoveryResponse>0>NetworkConfig>0>Interface>1>@Name");
                    string mainTempMACAddress = xn.GetValue("DiscoveryResponse>0>NetworkConfig>0>Interface>0>MACAddress>0>_text");
                    // If the secIFCName name have value,should create device with two port
                    if (secIFCName.Length > 1)
                    {
                        newBecaonDevice = new BecaonDevice(2, mainTempMACAddress);

                        newBecaonDevice.SecIfcName = secIFCName;
                        newBecaonDevice.SecIpAddress = xn.GetValue("DiscoveryResponse>0>NetworkConfig>0>Interface>1>IPAddress>0>_text");
                        newBecaonDevice.SecIpNetmask = xn.GetValue("DiscoveryResponse>0>NetworkConfig>0>Interface>1>Netmask>0>_text");
                        newBecaonDevice.SecMACAddress = xn.GetValue("DiscoveryResponse>0>NetworkConfig>0>Interface>1>MACAddress>0>_text");
                    }
                    else
                    {
                        newBecaonDevice = new BecaonDevice(1, mainTempMACAddress);
                    }
                    newBecaonDevice.SerialNumber = xn.GetValue("DiscoveryResponse>0>SerialNumber>0>_text");
                    newBecaonDevice.ProductType = xn.GetValue("DiscoveryResponse>0>ProductType>0>_text");
                    newBecaonDevice.ProductName = xn.GetValue("DiscoveryResponse>0>ProductName>0>_text");
                    newBecaonDevice.ProductCode = xn.GetValue("DiscoveryResponse>0>ProductCode>0>_text");
                    newBecaonDevice.FirmwareVersion = xn.GetValue("DiscoveryResponse>0>FirmwareVersion>0>_text");
                    newBecaonDevice.FirmwareDate = xn.GetValue("DiscoveryResponse>0>FirmwareDate>0>_text");

                    newBecaonDevice.IfcName = xn.GetValue("DiscoveryResponse>0>NetworkConfig>0>Interface>0>@Name");
                    newBecaonDevice.MainIpAddress = xn.GetValue("DiscoveryResponse>0>NetworkConfig>0>Interface>0>IPAddress>0>_text");
                    newBecaonDevice.MainIpNetmask = xn.GetValue("DiscoveryResponse>0>NetworkConfig>0>Interface>0>Netmask>0>_text");
                    newBecaonDevice.MainMACAddress = xn.GetValue("DiscoveryResponse>0>NetworkConfig>0>Interface>0>MACAddress>0>_text");

                    // If the BecaonDevice object was created succesfully call the callback event
                    if (state.discoveryService.newDiscoveredDevice != null)
                    { 
                        state.discoveryService.newDiscoveredDevice(newBecaonDevice, false);
                    }
                }
                else
                {
                    System.Diagnostics.Debug.Print("Discover Service Invalid Length Received");
                }

                // Schedule aother message reception
                udpClient.BeginReceive(new AsyncCallback(UDPReceiveCallback), (UdpState)(ar.AsyncState));
            }
            catch (Exception)
            {
                // Schedule Another message reception
                udpClient.BeginReceive(new AsyncCallback(UDPReceiveCallback), (UdpState)(ar.AsyncState));
            }
        }

        // Function that sends the Discover Request message
        public void sendDiscoverRequest()
        {
            deviceConfig = null;

            CreateListenerSockets();

            // Set the Broadcast address as an end point. Then the browse commands will be
            // sent to this end point
            IPEndPoint sending_end_point;
            //sending_end_point = new IPEndPoint(IPAddress.Parse("192.168.0.255"), beaconDiscoveryListenPort);
            sending_end_point = new IPEndPoint(IPAddress.Parse("255.255.255.255"), beaconDiscoveryListenPort);

            string strSendText = "<?xml version='1.0' encoding='UTF-8'?>" +
                "<DiscoveryRequest>" +
                "<GetConfig/>" +
                "</DiscoveryRequest>";
            byte[] byteData = System.Text.Encoding.UTF8.GetBytes(strSendText);

            try
            {
                foreach (UdpClient socket in listenerSockets)
                {
                    socket.Send(byteData, byteData.Length, sending_end_point);
                }
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.Print("Problem transmitting data, probably no ethernet adapters are connected.");
            }

            return;
        }

        public void changeTemporaryIPAddr(BecaonDevice device, String newTemporaryIP, String newTemporaryNetMask)
        {
            deviceConfig = null;

            CreateListenerSockets();

            // Set the Broadcast address as an end point. Then the browse commands will be
            // sent to this end point
            IPEndPoint sending_end_point;
            sending_end_point = new IPEndPoint(IPAddress.Parse("255.255.255.255"), beaconDiscoveryListenPort);

            StringBuilder msg = new StringBuilder();
            msg.Append("<?xml version='1.0' encoding='UTF-8'?>\n");
            msg.Append("<DiscoveryRequest>\n");
            msg.Append("<ChangeTempIP>\n");
            msg.Append("<SerialNumber>");
            msg.Append(device.SerialNumber);
            msg.Append("</SerialNumber>\n");
            msg.Append("<MACAddress>");
            msg.Append(device.TempMACAddress);
            msg.Append("</MACAddress>\n");
            msg.Append("<IPAddress>");
            msg.Append(newTemporaryIP);
            msg.Append("</IPAddress>\n");
            msg.Append("<Netmask>");
            msg.Append(newTemporaryNetMask);
            msg.Append("</Netmask>\n");
            msg.Append("</ChangeTempIP>\n");
            msg.Append("</DiscoveryRequest>");

            string strSendText = msg.ToString();
            byte[] byteData = System.Text.Encoding.UTF8.GetBytes(strSendText);
            foreach (UdpClient socket in listenerSockets)
            {
                socket.Send(byteData, byteData.Length, sending_end_point);
            }

            return;

        }

        public void removeTemporaryIPAddr(BecaonDevice device)
        {
            deviceConfig = null;
            CreateListenerSockets();

            // Set the Broadcast address as an end point. Then the browse commands will be
            // sent to this end point
            IPEndPoint sending_end_point;
            sending_end_point = new IPEndPoint(IPAddress.Parse("255.255.255.255"), beaconDiscoveryListenPort);

            StringBuilder msg = new StringBuilder();
            msg.Append("<?xml version='1.0' encoding='UTF-8'?>\n");
            msg.Append("<DiscoveryRequest>\n");
            msg.Append("<RemoveTempIP>\n");
            msg.Append("<SerialNumber>");
            msg.Append(device.SerialNumber);
            msg.Append("</SerialNumber>\n");
            msg.Append("<MACAddress>");
            msg.Append(device.TempMACAddress);
            msg.Append("</MACAddress>\n");
            msg.Append("</RemoveTempIP>\n");
            msg.Append("</DiscoveryRequest>");
            string strSendText = msg.ToString();

            byte[] byteData = System.Text.Encoding.UTF8.GetBytes(strSendText);

            foreach (UdpClient socket in listenerSockets)
            {
                socket.Send(byteData, byteData.Length, sending_end_point);
            }

            // Set the status bar message
            _applicationPresenter.StatusText = string.Format("Becaon Device Temporary IP '{0}' was removed.", device.LookupName);
            return;

        }

        public void changePermanentIPAddrs(BecaonDevice device)
        {
            CreateListenerSockets();

            // Set the Broadcast address as an end point. Then the browse commands will be
            // sent to this end point
            IPEndPoint sending_end_point;
            sending_end_point = new IPEndPoint(IPAddress.Parse("255.255.255.255"), beaconDiscoveryListenPort);

            StringBuilder msg = new StringBuilder();
            msg.Append("<?xml version='1.0' encoding='UTF-8'?>\n");
            msg.Append("<DiscoveryRequest>\n");
            msg.Append("<SetIPConfig>\n");
            msg.Append("<SerialNumber>");
            msg.Append(device.SerialNumber);
            msg.Append("</SerialNumber>\n");
            msg.Append("<MACAddress>");
            msg.Append(device.TempMACAddress);
            msg.Append("</MACAddress>\n");
            msg.Append("<Network>\n");
            //msg.Append("<Name>");
            //msg.Append(device.IfcName);
            //msg.Append("</Name>\n");
            msg.Append("<IPAddress>");
            msg.Append(device.MainIpAddress);
            msg.Append("</IPAddress>\n");
            msg.Append("<Netmask>");
            msg.Append(device.MainIpNetmask);
            msg.Append("</Netmask>\n");
            //msg.Append("<Gateway>");
            //msg.Append("");
            //msg.Append("</Gateway>\n");
            msg.Append("</Network>\n");

            if (device.GetIfcCount() > 1)
            {
                msg.Append("<Network>\n");
                //msg.Append("<Name>");
                //msg.Append(device.SecIfcName);
                //msg.Append("</Name>\n");
                msg.Append("<IPAddress>");
                msg.Append(device.SecIpAddress);
                msg.Append("</IPAddress>\n");
                msg.Append("<Netmask>");
                msg.Append(device.SecIpNetmask);
                msg.Append("</Netmask>\n");
                //msg.Append("<Gateway>");
                //msg.Append("");
                //msg.Append("</Gateway>\n");
                msg.Append("</Network>\n");
            }

            msg.Append("</SetIPConfig>\n");
            msg.Append("</DiscoveryRequest>");

            byte[] byteData = Encoding.ASCII.GetBytes(msg.ToString());
            foreach (UdpClient socket in listenerSockets)
            {
                socket.Send(byteData, byteData.Length, sending_end_point);
            }

            deviceConfig = device;

            return;

        }

        public void RebootModule(BecaonDevice device)
        {
            deviceConfig = null;
            CreateListenerSockets();

            // Set the Broadcast address as an end point. Then the browse commands will be
            // sent to this end point
            IPEndPoint sending_end_point;
            sending_end_point = new IPEndPoint(IPAddress.Parse("255.255.255.255"), beaconDiscoveryListenPort);

            StringBuilder msg = new StringBuilder();
            msg.Append("<?xml version='1.0' encoding='UTF-8'?>\n");
            msg.Append("<DiscoveryRequest>\n");
            msg.Append("<RebootModule>\n");
            msg.Append("<SerialNumber>");
            msg.Append(device.SerialNumber);
            msg.Append("</SerialNumber>\n");
            msg.Append("<MACAddress>");
            msg.Append(device.TempMACAddress);
            msg.Append("</MACAddress>\n");
            msg.Append("</RebootModule>\n");
            msg.Append("</DiscoveryRequest>");

            string strSendText = msg.ToString();
            byte[] byteData = System.Text.Encoding.UTF8.GetBytes(strSendText);
            foreach (UdpClient socket in listenerSockets)
            {
                socket.Send(byteData, byteData.Length, sending_end_point);
            }

            _applicationPresenter.StatusText = string.Format("Becaon Device '{0}' was rebooted.", device.LookupName);
        }

        private bool ipIsReachable(string ipAddr)
        {
            Ping pingSender = new Ping();

            // The timeout was hardcoded to 200 mSec
            PingReply reply = pingSender.Send(ipAddr, PING_TIMEOUT);

            if (reply.Status == IPStatus.Success) return true;
            return false;
        }

        public void StopScan()
        {
            // do we need close the connection here?
            //listenerSocket.
        }
    }

}


