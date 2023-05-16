using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace nac.WebServer.lib;

public static class HttpHelper
{
    public static void WriteTextResponse(HttpListenerResponse response, string contentType, string text)
    {
        var buf = Encoding.UTF8.GetBytes(text);
        response.ContentType = contentType;
        response.ContentLength64 = buf.Length;
        response.OutputStream.Write(buf, 0, buf.Length);
    }

    public static void WriteBinaryResponse(HttpListenerResponse response, string contentType, byte[] data)
    {
        response.ContentType = contentType;
        response.ContentLength64 = data.Length;
        response.OutputStream.Write(data, 0, data.Length);
    }


    public static models.FileServeData getFileServeInfo(string extension)
    {
        var info = new models.FileServeData();
        info.contentType = MimeTypes.MimeTypeMap.GetMimeType(extension);

        info.IsBinary = !info.contentType.StartsWith("text/", StringComparison.OrdinalIgnoreCase);

        return info;
    }


}
