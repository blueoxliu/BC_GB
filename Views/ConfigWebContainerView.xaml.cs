using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Reflection;

namespace GatewayBrowser.Views
{
    /// <summary>
    /// ConfigWebContainerView.xaml 的交互逻辑
    /// </summary>
    public partial class ConfigWebContainerView : UserControl
    {
        public ConfigWebContainerView()
        {
            InitializeComponent();

            this.Webbw_GatewayWebConfig.Navigated += (a,b)=>{ HideScriptErrors(Webbw_GatewayWebConfig, true); }; 
        }



       public void HideScriptErrors(WebBrowser wb, bool hide) 
       {
            var fiComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fiComWebBrowser == null) return;
            var objComWebBrowser = fiComWebBrowser.GetValue(wb);
            if (objComWebBrowser == null) {
                wb.Loaded += (o, s) => HideScriptErrors(wb, hide); //In case we are to early
                return;
            }
            objComWebBrowser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, objComWebBrowser, new object[] { hide });
        }



    }
}
