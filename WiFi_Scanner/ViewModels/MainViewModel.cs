using System.Collections.ObjectModel;
using System.Windows.Input;
using WiFi_Scanner.Commands;
using WiFi_Scanner.Data;
using WiFi_Scanner.Models;
using NativeWifi;
using System.Text;

namespace WiFi_Scanner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly WifiDbContext _context;
        public ObservableCollection<WifiNetwork> Networks { get; set; }
        private string _bestNetwork;

        public string BestNetwork
        {
            get => _bestNetwork;
            set
            {
                _bestNetwork = value;
                OnPropertyChanged(nameof(BestNetwork));
            }
        }

        public ICommand ScanCommand { get; }
        public ICommand SaveCommand { get; }

        public MainViewModel()
        {
            _context = new WifiDbContext();
            Networks = new ObservableCollection<WifiNetwork>();
            ScanCommand = new RelayCommand(ScanNetworks);
            SaveCommand = new RelayCommand(SaveNetworks);
        }

        private void ScanNetworks()
        {
            Networks.Clear();

            var wlanClient = new WlanClient();

            foreach (var wlan in wlanClient.Interfaces)
            {
                var networks = wlan.GetNetworkBssList();

                foreach (var network in networks)
                {
                    Networks.Add(new WifiNetwork
                    {
                        Ssid = Encoding.UTF8.GetString(network.dot11Ssid.SSID, 0, (int)network.dot11Ssid.SSIDLength),
                        SignalStrength = (int)network.linkQuality
                    });
                }
            }

            var bestNetwork = Networks.OrderByDescending(n => n.SignalStrength).FirstOrDefault();
            BestNetwork = bestNetwork?.Ssid;
        }

        private void SaveNetworks()
        {
            _context.WifiNetworks.AddRange(Networks);
            _context.SaveChanges();
        }
    }
}