using System;
using System.Windows;
using System.Windows.Controls;
using GatewayBrowser.Presenters;
using System.Windows.Threading;


namespace GatewayBrowser.Views
{
    public partial class ProgessBarView : Window
    {
        private readonly ApplicationPresenter _applicationPresenter;

        // Ping timeout for Used IP's search process
        private const int MAX_BROWSER_TIME = 15;
        private DispatcherTimer progessTimer;

        public ProgessBarView(ApplicationPresenter applicationPresenter)
        {
            InitializeComponent();
            StartProgessBar();
            _applicationPresenter = applicationPresenter;
        }

        private void StartProgessBar()
        {
            progessTimer = new DispatcherTimer();
            progessTimer.Tick += progessTimer_Tick;
            progessTimer.Interval = TimeSpan.FromMilliseconds(1000);
            pgb_browserProgess.Minimum = 0;
            pgb_browserProgess.Maximum = MAX_BROWSER_TIME;
            pgb_browserProgess.Value = 0;

            progessTimer.Start();
        }

        private void progessTimer_Tick(object sender, System.EventArgs e)
        {
            pgb_browserProgess.Value++;

            // Update the progess status text.
            double percent = (pgb_browserProgess.Value / MAX_BROWSER_TIME) * 100;
            int intPercent = (int)percent;
            lbl_progessText.Content = "Browseing " + intPercent + " %";

            if(pgb_browserProgess.Value > MAX_BROWSER_TIME -1 )
            {
                progessTimer.Stop();
                _applicationPresenter.StopScan();
                this.Close();
            }
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            pgb_browserProgess.Value = 0;
            progessTimer.Stop();
            this.Close();
        }

    }
}