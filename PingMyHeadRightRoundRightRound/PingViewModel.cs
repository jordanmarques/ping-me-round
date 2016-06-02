using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using PingMyHeadRightRoundRightRound.Annotations;

namespace PingMyHeadRightRoundRightRound
{
    public class PingViewModel : INotifyPropertyChanged
    {
        private bool _googleRadioBtn;

        public bool GoogleRadioBtn
        {
            get
            {
                return _googleRadioBtn;
            }
            set
            {
                _googleRadioBtn = value;
                OnPropertyChanged("GoogleRadioBtn");
            }
        }

        public bool HostRadioBtn { get; set; }
        public string Host { get; set; }
        public Task PingTask { get; set; }
        public PingModel MyPingModel { get; set; }
        public long Value { get; set; }
        public CancellationTokenSource MyCancellationTokenSource { get; set; }

        private Brush _lblColor;
        public Brush LblColor {
            get { return _lblColor;}
            set
            {
                _lblColor = value;
                OnPropertyChanged("LblColor");
            } }

        public PingViewModel()
        {
            GoogleRadioBtn = true;
            MyPingModel = new PingModel();
            LblColor = Brushes.Black;
        }

        public void StartPing()
        {
            string host;
            if (HostRadioBtn)
            {
                host = Host ?? "";
                Regex regex = new Regex(".*([\\w-]+\\.)+[a-z]{2,5}(/[\\w-]+)*");
                Match match = regex.Match(host);
                if (!match.Success)
                {
                    return;
                }
            }
            else
            {
                host = "www.google.com";
            }


            Ping pingSender = new Ping();
            MyCancellationTokenSource = new CancellationTokenSource();

            PingTask = Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    if (MyCancellationTokenSource.Token.IsCancellationRequested)
                    {
                        break;
                    }
                    long pingResult = pingSender.Send(host).RoundtripTime;
                    ManageLblColors(pingResult);
                    MyPingModel.Value = pingResult;
                    await Task.Delay(1000);
                }
            }, MyCancellationTokenSource.Token);

        }


        public void StopPing()
        {
            MyCancellationTokenSource.Cancel();
            MyPingModel.Value = 0;
        }

        private void ManageLblColors(long value)
        {
            if (value >= 0 && value <= 100)
            {
                LblColor = Brushes.DarkGreen;
            }
            else if (value > 100 && value <= 150)
            {
                LblColor = Brushes.DarkGoldenrod;
            }
            else
            {
                LblColor = LblColor = Brushes.DarkRed;
            }
        
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
