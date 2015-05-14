using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GatewayBrowser.Help
{
    public class StateToNullableBoolConverter:IValueConverter  
    {  
  
       public object Convert(object value, Type targetType, object parameters, System.Globalization.CultureInfo culture)  
       {
          GatewayStatus state = (GatewayStatus)value;  
           switch (state)  
            {
                case GatewayStatus.Active:  
                    return true;
                case GatewayStatus.Exception:  
                    return false;
                case GatewayStatus.Unknown:
                    return false;  
                      
                default:  
                    return null;  
            }  
        }  
  
       public object ConvertBack(object value, Type targetType, object parameters, System.Globalization.CultureInfo culture)  
        {  
            bool? nb = (bool?)value;  
            switch (nb)  
           {  
                case true:
                   return GatewayStatus.Active;  
                case false:
                   return GatewayStatus.Exception;  
                default:
                   return GatewayStatus.Unknown;  
                      
            }  
        }  
    }  

}
