using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

using GatewayBrowser.Help;


namespace GatewayBrowser.Model
{
    [Serializable]
    public struct DeviceIpInfo
    {
        public String portName; //E1, E2
        public String IfcName;
        public String MACAddress;
        public String IpAddress;
        public String IpNetmask;
    }

    // Class that describes a Beacon Device
    [Serializable]
    public class BecaonDevice:Notifier
    {
        private const String IFC_E1 = "eth0";
        private const String IFC_E2 = "eth1";
        private const String TEMP_IP_IFC = "eth0:9";
        private const String PORT_E1 = "E1";
        private const String PORT_E2 = "E2";

        //Owen add for save use
        private Guid _id = Guid.Empty;
        private GatewayStatus deviceStatus;
        private string deviceDescription;

        private String subProductType;   // SubProduct Type Code: 
        private String productType;   // Product Type Code: 
        private String productName;   // Full Name of the Product: 
        private String serialNumber;  //
        private String moduleName;    // Name Asigned by the user
        private String productCode; // Beacon name Code: 
        private String firmwareVersion; // firmware.Rev 
        private String firmwareDate; // firmware.Data
        private DeviceIpInfo m_tempIp; // temp IP
        private DeviceIpInfo[] m_deviceIp; // eth0, eth1
        private int m_IfcCount; // support Interface count
        private int m_protocolVersion = 1; // 1 - Legacy, 2 - new

        // support 1 IP port
        public BecaonDevice(String ProductType,
                             String ProductName,
                             String SerialNumber,
                             String macAddress,
                             String ModuleName,
                             String ProductNameCode,
                             String ProductRevision,
                             String SoftwareRevisionLevel,
                             String OsRevisionLevel,
                             String MainIpAddress,
                             String MainIpNetmask,
                             String TempIpAddress,
                             String TempIpNetmask)
        {
            m_IfcCount = 1;
            m_deviceIp = new DeviceIpInfo[1]; 

            subProductType = ProductType;
            productType = ProductType;
            productName = ProductName;
            serialNumber = SerialNumber;
            moduleName = ModuleName;
            productCode = ProductCode;
            firmwareVersion = FirmwareVersion;
            firmwareDate = FirmwareDate;
            m_deviceIp[0].portName = PORT_E1;
            m_deviceIp[0].MACAddress = macAddress;
            m_deviceIp[0].IpAddress = MainIpAddress;
            m_deviceIp[0].IpNetmask = MainIpNetmask;
            m_tempIp.MACAddress = macAddress;
            m_tempIp.IpAddress = TempIpAddress;
            m_tempIp.IpNetmask = TempIpNetmask;
        }

        // support more than 1 IP port
        public BecaonDevice(String[] deviceInfo)
        {
            m_protocolVersion = 2;

            // Initialize temp IP info
            m_tempIp.portName = String.Empty;
            m_tempIp.IfcName = TEMP_IP_IFC;
            m_tempIp.MACAddress = String.Empty;
            m_tempIp.IpAddress = String.Empty;
            m_tempIp.IpNetmask = String.Empty;

            productType = deviceInfo[1];
            subProductType = productType;
            productName = deviceInfo[2];
            serialNumber = deviceInfo[3];
            moduleName = deviceInfo[4];
            productCode = deviceInfo[5];
            firmwareVersion = deviceInfo[6];
            firmwareDate = deviceInfo[7];

            int n = (deviceInfo.Length - 9) / 4; // exclude basic info
            m_deviceIp = new DeviceIpInfo[n];

            m_IfcCount = n;

            int startIndex = 9;
            int k = 0;
            for (int i = 0; i < n; i++, startIndex += 4)
            {
                if (deviceInfo[startIndex].IndexOf(':') != -1)
                {
                    m_IfcCount--;

                    // found temp ip
                    if (deviceInfo[startIndex] == m_tempIp.IfcName)
                    {
                        //m_tempIp.IfcName = deviceInfo[startIndex];
                        m_tempIp.MACAddress = deviceInfo[startIndex + 1];
                        m_tempIp.IpAddress = deviceInfo[startIndex + 2];
                        m_tempIp.IpNetmask = deviceInfo[startIndex + 3];
                    }
                    continue;
                }
                else if (deviceInfo[startIndex] == IFC_E1)
                {
                    k = 0;
                    m_deviceIp[k].portName = PORT_E1;
                }
                else if (deviceInfo[startIndex] == IFC_E2)
                {
                    k = 1;
                    m_deviceIp[k].portName = PORT_E2;
                }
                else
                {
                    // Ignore third or more ports 
                    continue;
                }

                m_deviceIp[k].IfcName = deviceInfo[startIndex];
                m_deviceIp[k].MACAddress = deviceInfo[startIndex + 1];
                m_deviceIp[k].IpAddress = deviceInfo[startIndex + 2];
                m_deviceIp[k].IpNetmask = deviceInfo[startIndex + 3];
            }

            // Set default to the MAC address of temp IP so that we can change the temp IP
            if (m_tempIp.MACAddress.Length == 0)
            {
                m_tempIp.MACAddress = MainMACAddress;
            }

        }

        public BecaonDevice(int devicePort, string mainMACAddress)
        {
            m_deviceIp = new DeviceIpInfo[devicePort];
            // Set the m_tempIp MACaddress
            m_tempIp.MACAddress = mainMACAddress;
            
        }


#region Public Propopty
        public Guid Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }
        public string LookupName
        {
            get { return string.Format("{0}-({1})", ProductName, MainIpAddress); }
        }
        public GatewayStatus DeviceStatus
        {
            get { return deviceStatus; }
            set
            {
                deviceStatus = value;
                OnPropertyChanged("DeviceStatus");
            }
        }
        public string DeviceDescription
        {
            get { return deviceDescription; }
            set
            {
                deviceDescription = value;
                OnPropertyChanged("DeviceDescription");
            }
        }
        public string ImagePath
        {
            get
            {
                if (DeviceStatus == GatewayStatus.Active)
                {
                    return "/GatewayBrowser;component/Resources/Images/online.png";
                }
                else
                {
                    return "/GatewayBrowser;component/Resources/Images/offline.png";
                }
            }
        }

        public String SubProductType
        { 
            get { return subProductType; }
            set
            {
                subProductType = value;
                OnPropertyChanged("SubProductType");
            }
        }
        public String ProductType
        {
            get { return productType; }
            set
            {
                productType = value;
                OnPropertyChanged("ProductType");
            }
        }
        public String ProductName
        {
            get { return productName; }
            set
            {
                productName = value;
                OnPropertyChanged("ProductName");
            }
        }
        public String SerialNumber
        {
            get { return serialNumber; }
            set
            {
                serialNumber = value;
                OnPropertyChanged("SerialNumber");
            }
        }
        public String ModuleName
        {
            get { return moduleName; }
            set
            {
                moduleName = value;
                OnPropertyChanged("ModuleName");
            }
        }
        public String ProductCode
        {
            get { return productCode; }
            set
            {
                productCode = value;
                OnPropertyChanged("ProductCode");
            }
        }
        public String FirmwareVersion
        {
            get { return firmwareVersion; }
            set
            {
                firmwareVersion = value;
                OnPropertyChanged("FirmwareVersion");
            }
        }
        public String FirmwareDate
        {
            get { return firmwareDate; }
            set
            {
                firmwareDate = value;
                OnPropertyChanged("FirmwareDate");
            }
        }  

        // Temp Interface Property
        public String TempIpAddress
        {
            get { return m_tempIp.IpAddress; }
            set
            {
                m_tempIp.IpAddress = value;
                OnPropertyChanged("TempIpAddress");
            }
        }
        public String TempIpNetmask
        {
            get { return m_tempIp.IpNetmask; }
            set
            {
                m_tempIp.IpNetmask = value;
                OnPropertyChanged("TempIpNetmask");
            }
        }
        public String TempMACAddress
        {
            get { return m_tempIp.MACAddress; }
            set
            {
                m_tempIp.MACAddress = value;
                OnPropertyChanged("TempMACAddress");
            }
        }
        // Main Interface Property
        public String IfcName
        {
            get { return m_deviceIp[0].IfcName; }
            set
            {
                m_deviceIp[0].IfcName = value;
                OnPropertyChanged("IfcName");
            }
        }
        public String MainPortName
        {
            get { return m_deviceIp[0].portName; }
            set
            {
                m_deviceIp[0].portName = value;
                OnPropertyChanged("MainPortName");
            }
        }
        public String MainIpAddress
        {
            get { return m_deviceIp[0].IpAddress; }
            set
            {
                m_deviceIp[0].IpAddress = value;
                OnPropertyChanged("MainIpAddress");
            }
        }
        public String MainIpNetmask
        {
            get { return m_deviceIp[0].IpNetmask; }
            set
            {
                m_deviceIp[0].IpNetmask = value;
                OnPropertyChanged("MainIpNetmask");
            }
        }
        public String MainMACAddress
        {
            get { return m_deviceIp[0].MACAddress; }
            set
            {
                m_deviceIp[0].MACAddress = value;
                OnPropertyChanged("MainMACAddress");
            }
        }
        // Sec Interface Property
        public String SecIfcName
        {
            get { return m_deviceIp[1].IfcName; }
            set
            {
                m_deviceIp[1].IfcName = value;
                OnPropertyChanged("SecIfcName");
            }
        }
        public String SecPortName
        {
            get { return m_deviceIp[1].portName; }
            set
            {
                m_deviceIp[1].portName = value;
                OnPropertyChanged("SecPortName");
            }
        }
        public String SecIpAddress
        {
            get { return m_deviceIp[1].IpAddress; }
            set
            {
                m_deviceIp[1].IpAddress = value;
                OnPropertyChanged("SecIpAddress");
            }
        }
        public String SecIpNetmask
        {
            get { return m_deviceIp[1].IpNetmask; }
            set
            {
                m_deviceIp[1].IpNetmask = value;
                OnPropertyChanged("SecIpNetmask");
            }
        }
        public String SecMACAddress
        {
            get { return m_deviceIp[1].MACAddress; }
            set
            {
                m_deviceIp[1].MACAddress = value;
                OnPropertyChanged("SecMACAddress");
            }
        }
#endregion

        public int GetIfcCount() { return m_IfcCount; }
        public int GetProtocolVersion() { return m_protocolVersion; }

        private bool IsAddressValid(string addrString)
        {
            IPAddress address;
            return IPAddress.TryParse(addrString, out address);
        }

        public String getReachableIP()
        {
            Ping pingSender = new Ping();
            // The timeout was hardcoded to 100 mSec
            PingReply reply;
            if (IsAddressValid(MainIpAddress))
            {
                try
                {
                    reply = pingSender.Send(MainIpAddress, 100);
                    if (reply.Status == IPStatus.Success) return MainIpAddress;
                }
                catch (Exception)
                {
                }
            }

            if (GetIfcCount() > 1 && IsAddressValid(SecIpAddress))
            {
                try
                {
                    reply = pingSender.Send(SecIpAddress, 100);
                    if (reply.Status == IPStatus.Success) return SecIpAddress;
                }
                catch (Exception)
                {
                }
            }

            if (IsAddressValid(TempIpAddress))
            {
                try
                {
                    reply = pingSender.Send(TempIpAddress, 100);
                    if (reply.Status == IPStatus.Success) return TempIpAddress;
                }
                catch (Exception)
                {
                }
            }
            return null;
        }

        public override string ToString()
        {
            return LookupName;
        }

        public override bool Equals(object obj)
        {
            BecaonDevice other = obj as BecaonDevice;
            return other != null && other.MainIpAddress == MainIpAddress && other.ProductName == ProductName;
        }
    }
}
