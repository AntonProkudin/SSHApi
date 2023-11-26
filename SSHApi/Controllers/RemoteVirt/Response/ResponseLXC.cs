namespace SSHApi.Controllers.RemoteVirt.Response;

public class ResponseLXC
{
        public string Name { get; set; }
        public string State { get; set; }
        public string IPv4 { get; set; }
        public string IPv6 { get; set; }
        public string Type { get; set; }
        public int Snapshots { get; set; }
        public string Location { get; set; }

}
