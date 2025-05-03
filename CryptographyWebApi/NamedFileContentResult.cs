using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace CryptographyWebApi;

public class NamedFileContentResult :FileContentResult
{
    private readonly string _fileName;

    public NamedFileContentResult(byte[] fileContent, string contentType, string fileName):base(fileContent, contentType)
    {
        _fileName = fileName;
    }

    public override Task ExecuteResultAsync(ActionContext context)
    {
        var disposition = new ContentDispositionHeaderValue("inline");
        disposition.SetHttpFileName(_fileName);
        context.HttpContext.Response.Headers.Add(HeaderNames.ContentDisposition, disposition.ToString());
        
        return base.ExecuteResultAsync(context);
    }
}