using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace GatewayBrowser.Model
{
    public class DeviceRepository
    {
        private List<BecaonDevice> _gatewayStore;
        private readonly string _stateFile;

        public DeviceRepository()
        {
            _stateFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GatewayBrowser.state" );
            try
            {
                Deserialize();
            }
            catch (System.Runtime.Serialization.SerializationException EXDes)
            {
                Console.Write("Deserialize meet problem:" + EXDes.ToString());
            }
            
        }

        public void Save(BecaonDevice gateway)
        {
            lock (this)
            {
                if (gateway.Id == Guid.Empty)
                { 
                    gateway.Id = Guid.NewGuid(); 
                }
                    
                _gatewayStore.Add(gateway);

                Serialize();
            }
        }

        public void Delete(BecaonDevice gateway)
        {
            lock (this)
            {
                _gatewayStore.Remove(gateway);
                Serialize();
            }
            
        }

        private void Serialize()
        {
            using (FileStream stream =File.Open(_stateFile, FileMode.OpenOrCreate))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, _gatewayStore);
            }
        }

        private void Deserialize()
        {
            if (File.Exists(_stateFile))
            {
                using (FileStream stream = File.Open(_stateFile, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();

                    _gatewayStore = (List<BecaonDevice>)formatter.Deserialize(stream);
                }
            }
            else
            {
                _gatewayStore = new List<BecaonDevice>(); 
            }
                
        }

        public List<BecaonDevice> FindByLookup(string lookupName)
        {
            IEnumerable<BecaonDevice> found =
                from c in _gatewayStore
                where c.LookupName.StartsWith
                (
                    lookupName,
                    StringComparison.OrdinalIgnoreCase
                )
                select c;

            return found.ToList();
        }

        public List<BecaonDevice> FindAll()
        {
            return new List<BecaonDevice>(_gatewayStore);
        }
    }
}