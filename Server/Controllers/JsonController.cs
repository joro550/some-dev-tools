using CsvHelper;
using System.Text;
using System.Globalization;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace DevTools.Server.Controllers;

/// <summary>
/// Controller to convert data to json
/// </summary>
[ApiController, Route("api/generator/json")]
public class JsonController : ControllerBase
{
    /// <summary>
    /// Currently only works with source=csv and a plain text body of csv content
    /// </summary>
    /// <remarks>
    /// Sample value of message:
    /// ```
    ///album,year,US_peak_chart_post
    ///The White Stripes,1999,-
    ///De Stijl,2000,-
    ///White Blood Cells,2001,61
    ///Elephant,2003,6
    ///Get Behind Me Satan,2005,3
    ///Icky Thump,2007,2
    ///Under Great White Northern Lights,2010,11
    ///Live in Mississippi,2011 -
    ///Live at the Gold Dollar,2012,-
    ///Nine Miles from the White City,2013,-
    /// ```
    /// </remarks>
    /// <param name="source">csv</param>
    /// <param name="fileData">csv data</param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult ConvertToJson([FromQuery]string source, [FromBody] string fileData)
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