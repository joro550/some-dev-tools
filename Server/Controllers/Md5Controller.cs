using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace DevTools.Server.Controllers;

[ApiController, Route("api/cryptography/md5")]
public class Md5Controller : ControllerBase
{
    [HttpGet]
    public string GetHash(string input)
    {
        var hashBytes = MD5.HashData(Encoding.ASCII.GetBytes(input));
        var sb = new StringBuilder();
        foreach (var t in hashBytes)
            sb.Append(t.ToString("X2"));
        return sb.ToString();
    }

}