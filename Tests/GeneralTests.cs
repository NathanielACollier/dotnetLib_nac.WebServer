using Microsoft.VisualStudio.TestTools.UnitTesting;
using nac.WebServer.lib;

namespace Tests;

[TestClass]
public class GeneralTests
{
    [TestMethod]
    public void GetHTMLHelloWorld()
    {
        var webMan = new nac.WebServer.WebServerManager();

        webMan.OnNewRequest += (_s, _e) =>
        {
            if(_e.Request.Url.LocalPath == "/"){
                _e.Response.WriteTextResponse( "text/html", @"
                    <div style='color:green;'>Hello World!</div>
                ");
            }
        };
        webMan.Start();
    }
}