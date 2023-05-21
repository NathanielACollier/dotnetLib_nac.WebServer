using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace nac.WebServer.lib;

public static class HttpHelper
{
    
    public static models.FileServeData getFileServeInfo(string extension)
    {
        var info = new models.FileServeData();
        info.contentType = MimeTypes.MimeTypeMap.GetMimeType(extension);

        info.IsBinary = !info.contentType.StartsWith("text/", StringComparison.OrdinalIgnoreCase);

        return info;
    }


}
