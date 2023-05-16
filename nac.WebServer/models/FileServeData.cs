using System;
using System.Collections.Generic;
using System.Text;

namespace nac.WebServer.models;

public class FileServeData
{
    public bool IsBinary { get; set; }
    public string contentType { get; set; }
}
