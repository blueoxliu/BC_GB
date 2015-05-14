using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace GatewayBrowser.Help
{

    public class XMLNodeList : ArrayList
    {
        public XMLNode Pop()
        {
            XMLNode item = null;

            item = (XMLNode)this[this.Count - 1];
            this.Remove(item);

            return item;
        }

        public int Push(XMLNode item)
        {
            Add(item);

            return this.Count;
        }
    }
}
