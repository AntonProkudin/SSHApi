namespace SSHApi.Controllers.RemoteVirt.Response;

public class VCLStatus
{
    public string Name { get; set; }
    public string State { get; set; }
}
public class VCLStatusResponse
{
    public List<VCLStatus> status { get; set; }
}