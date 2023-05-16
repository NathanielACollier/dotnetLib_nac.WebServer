using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace nac.WebServer.lib;

public static class TCPUtillity
{
    private static Log log = new();


    /// <summary>
    /// Could be that another process claims this port, so you may have to retry to start the web server
    /// </summary>
    /// <returns></returns>
    public static int FreeTcpPort()
    {
        TcpListener l = new TcpListener(IPAddress.Loopback, 0);
        l.Start();
        int port = ((IPEndPoint)l.LocalEndpoint).Port;
        l.Stop();
        log.Info($"Found port {port}");
        return port;
    }
}
