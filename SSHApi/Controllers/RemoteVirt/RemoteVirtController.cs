using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Renci.SshNet;
using SSHApi.Controllers.RemoteVirt.Response;

namespace SSHApi.Controllers.RemoteVirt;

[ApiController]
[Route("[controller]")]
public class RemoteVirtController : ControllerBase
{
    string username = "vatnik1488";
    string password = "root";
    string host = "192.168.1.107";

    [Authorize]
    [HttpGet("StatusVCL")]
    public async Task<IActionResult> StatusVCL()
    {
        string response = "";
        using (var client = new SshClient(host, 22, username, password))
        {
            client.Connect();
            // ѕример выполнени€ команды на удаленном сервере
            var command = client.RunCommand("cd /var/lib/libvirt/boot/");
            //response += command.Result;
            command = client.RunCommand("virsh -c qemu:///system list --all");
            response += command.Result;
            client.Disconnect();
        }

       
        string[] lines = response.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        List<VCLStatus> models = new List<VCLStatus>();
        for (int i = 2; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 3) parts[2] +=" " +  parts[3];
            VCLStatus model = new VCLStatus
            {
                Name = parts[1],
                State = parts[2],
            };
            models.Add(model);
        } 
        return Ok(models);
    }
    [Authorize]
    [HttpGet("StatusLXC")]
    public async Task<IActionResult> StatusLXC()
    {
        string response = "";
        using (var client = new SshClient(host, 22, username, password))
        {
            client.Connect();
            // ѕример выполнени€ команды на удаленном сервере
            var command = client.RunCommand("cd /var/lib/libvirt/boot/");
            //response += command.Result;
            command = client.RunCommand("lxc list");
            response += command.Result;
            client.Disconnect();
        }

        string[] lines = response.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        string[] header = lines[1].Split('|', StringSplitOptions.RemoveEmptyEntries);
        string[] values = lines[3].Split('|', StringSplitOptions.RemoveEmptyEntries);

        ResponseLXC resource = new ResponseLXC
        {
            Name = values[0].Trim(),
            State = values[1].Trim(),
            IPv4 = values[2].Trim(),
            IPv6 = values[3].Trim(),
            Type = values[4].Trim(),
            Snapshots = int.Parse(values[5].Trim()),
            Location = values[6].Trim()
        };


        return Ok(resource);
    }
    [Authorize]
    [HttpGet("StatusDocker")]
    public async Task<IActionResult> S1()
    {
        return Ok();
    }
    [Authorize]
    [HttpPut("ChangeHost")]
    public async Task<IActionResult> ChangeHost([FromBody] string _host)
    {
        host = _host;
        return Ok("New host is " + host);
    }
    [Authorize]
    [HttpPut("ChangePassword")]
    public async Task<IActionResult> ChangePassword([FromBody] string _password)
    {
        password = _password;
        return Ok("New password is " + password);
    }
    [Authorize]
    [HttpPut("ChangeUsername")]
    public async Task<IActionResult> ChangeUsername([FromBody] string _username)
    {
        username = _username;
        return Ok("New username is " + username);
    }
    [Authorize]
    [HttpPost("StartVCL")]
    public async Task<IActionResult> StartVCL([FromBody] Request nameVCL)
    {
        string response = "";
        using (var client = new SshClient(host, 22, username, password))
        {
            client.Connect();
            var command = client.RunCommand("cd /var/lib/libvirt/boot/");
            command = client.RunCommand($"virsh -c qemu:///system start {nameVCL.Name}");
            response += command.Result;
            client.Disconnect();
        }
        return Ok(response);
    }
    [Authorize]
    [HttpPost("StartLXC")]
    public async Task<IActionResult> StartLXC([FromBody] Request nameVCL)
    {
        string response = "";
        using (var client = new SshClient(host, 22, username, password))
        {
            client.Connect();
            var command = client.RunCommand("cd /var/lib/libvirt/boot/");
            command = client.RunCommand($"lxc start {nameVCL.Name}");
            response += command.Result;
            client.Disconnect();
        }
        return Ok(response);
    }
    [Authorize]
    [HttpPost("StartDocker")]
    public async Task<IActionResult> S2()
    {
        return Ok();
    }
    [Authorize]
    [HttpPost("DestroyVCL")]
    public async Task<IActionResult> DestroyVCL([FromBody] Request nameVCL)
    {
        string response = "";
        using (var client = new SshClient(host, 22, username, password))
        {
            client.Connect();
            var command = client.RunCommand("cd /var/lib/libvirt/boot/");
            command = client.RunCommand($"virsh -c qemu:///system destroy {nameVCL.Name}");
            response += command.Result;
            client.Disconnect();
        }
        return Ok(response);
    }
    [Authorize]
    [HttpPost("DestroyLXC")]
    public async Task<IActionResult> DestroyLXC([FromBody] Request nameVCL)
    {
        string response = "";
        using (var client = new SshClient(host, 22, username, password))
        {
            client.Connect();
            var command = client.RunCommand("cd /var/lib/libvirt/boot/");
            command = client.RunCommand($"lxc pause {nameVCL.Name}");
            response += command.Result;
            client.Disconnect();
        }
        return Ok(response);
    }
    [Authorize]
    [HttpPost("DestroyDocker")]
    public async Task<IActionResult> S3()
    {
        return Ok();
    }
    [Authorize]
    [HttpPost("CreateVCL")]
    public async Task<IActionResult> S4()
    {
        return Ok();
    }
};