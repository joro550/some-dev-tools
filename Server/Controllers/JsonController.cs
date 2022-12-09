using CsvHelper;
using System.Text;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace DevTools.Server.Controllers;

[ApiController, Route("api/generator/json")]
public class JsonController : ControllerBase
{
    [HttpPost]
    public IActionResult GetHash([FromQuery]string source, [FromBody] string fileData)
    {
        if (source != "csv") 
            return BadRequest();
        
        var bytes = Encoding.UTF8.GetBytes(fileData);
        using var stream = new MemoryStream(bytes);
        using var reader = new StreamReader(stream);
        using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csvReader.GetRecords<dynamic>()
            .ToList();
        
        return new JsonResult(records);
    }
}