using Microsoft.VisualStudio.TestTools.UnitTesting;
using nac.WebServer.lib;

namespace Tests;

[TestClass]
public class GeneralTests
{
    [TestMethod]
    public async Task GetHTMLHelloWorld()
    {
        var webMan = new nac.WebServer.WebServerManager();
        var promiseQuit = new System.Threading.Tasks.TaskCompletionSource<bool>();

        webMan.OnNewRequest += (_s, _e) =>
        {
            if(_e.Request.Url.LocalPath == "/"){
                _e.Response.WriteTextResponse( "text/html", @"
                    <div style='color:green;'>Hello World!</div>

                    <button type='button' onclick='onClick_Quit()'>Quit</button>

                    <script type='text/javascript'>
                        function onClick_Quit(){{
                            window.location.href = '/quit';
                        }}
                    </script>
                ");
            }

            if (string.Equals(_e.Request.Url.LocalPath, "/quit"))
            {
                promiseQuit.SetResult(true);
            }
        };
        webMan.Start();

        var proc = lib.Browser.OpenBrowser(webMan.Url);

        await promiseQuit.Task;
    }
}