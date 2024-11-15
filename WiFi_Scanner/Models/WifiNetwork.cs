namespace WiFi_Scanner.Models
{
    public class WifiNetwork
    {
        public Guid Id { get; set; }
        public string Ssid { get; set; }
        public int SignalStrength { get; set; }
    }
}