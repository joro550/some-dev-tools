using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace DevTools.Server.Controllers;

[ApiController, Route("api/cryptography/sha256")]
public class Sha256Controller : ControllerBase
{
    [HttpGet]
    public string GetHash(string input)
    {
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        var sb = new StringBuilder();

        foreach (var b in hash)
            // can be "x2" if you want lowercase
            sb.Append(b.ToString("X2"));

        return sb.ToString();
    }
}