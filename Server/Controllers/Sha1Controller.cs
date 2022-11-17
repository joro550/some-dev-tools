using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace DevTools.Server.Controllers;

[ApiController, Route("api/cryptography/sha1")]
public class Sha1Controller : ControllerBase
{
    [HttpGet]
    public string GetHash(string input)
    {
        using var sha1 = SHA1.Create();
        var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
        var sb = new StringBuilder(hash.Length * 2);

        foreach (var b in hash)
            // can be "x2" if you want lowercase
            sb.Append(b.ToString("X2"));

        return sb.ToString();
    }
}