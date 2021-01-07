namespace WpfNetCoreMvvm.Models
{
    public class AppSettings
    {
        public string BaseUrl { get; set; }
        public string HubRoute { get; set; }

        public string HubUrl { get => $"{BaseUrl}{HubRoute}"; }
    }
}
