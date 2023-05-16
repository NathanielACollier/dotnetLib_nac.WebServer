using nac.WebServer.lib;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;

namespace nac.WebServer;

public class WebServerManager
{
    private static lib.Log log = new();
    private string _url;
    public string Url { get { return _url; } }
    private lib.WebServer server;

    public bool IsRunning { get; set; }

    public void Stop()
    {
        this.server.Stop();
        this.IsRunning = false;
    }


    public class OnNewRequestEventArgs
    {
        public HttpListenerResponse Response { get; set; }
        public HttpListenerRequest Request { get; set; }
    }

    public event EventHandler<OnNewRequestEventArgs> OnNewRequest;

    public void Start()
    {
        if (this.IsRunning)
        {
            throw new Exception("Allready running");
        }
        int retryLimit = 5;

        do
        {
            try
            {
                int port = TCPUtillity.FreeTcpPort();
                this._url = $"http://localhost:{port}/";

                this.server = new lib.WebServer((request, response) => {

                    var newRequestArgs = new OnNewRequestEventArgs
                    {
                        Response = response,
                        Request = request,
                    };
                    this.OnNewRequest?.Invoke(this, newRequestArgs);


                }, this.Url);
                this.server.Run();
                this.IsRunning = true;
            }
            catch (Exception ex)
            {
                log.Error($"Exception serving content.  Ex: {ex}");
            }
            Thread.Sleep(100);
            --retryLimit;
        } while (!this.IsRunning && retryLimit > 0);
    }



}
