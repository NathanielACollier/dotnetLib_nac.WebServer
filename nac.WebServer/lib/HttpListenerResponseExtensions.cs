using System.Net;
using System.Text;

namespace nac.WebServer.lib;

public static class HttpListenerResponseExtensions
{
    public static void WriteTextResponse(this HttpListenerResponse response, string contentType, string text)
    {
        var buf = Encoding.UTF8.GetBytes(text);
        response.ContentType = contentType;
        response.ContentLength64 = buf.Length;
        response.OutputStream.Write(buf, 0, buf.Length);
    }

    public static void WriteBinaryResponse(this HttpListenerResponse response, string contentType, byte[] data)
    {
        response.ContentType = contentType;
        response.ContentLength64 = data.Length;
        response.OutputStream.Write(data, 0, data.Length);
    }


    public static void WriteHtmlResponse(this HttpListenerResponse response, string html)
    {
        WriteTextResponse(response, "text/html", html);
    }
}