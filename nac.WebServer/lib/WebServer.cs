﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace nac.WebServer.lib;

// original from: http://www.technical-recipes.com/2016/creating-a-web-server-in-c/

public class WebServer
{
    private static Log log = new();
    private readonly HttpListener _listener = new HttpListener();
    private readonly Action<HttpListenerRequest, HttpListenerResponse> _responderMethod;

    public WebServer(Action<HttpListenerRequest, HttpListenerResponse> method, params string[] prefixes)
    {
        if (!HttpListener.IsSupported)
        {
            throw new NotSupportedException("Needs Windows XP SP2, Server 2003 or later.");
        }

        // URI prefixes are required eg: "http://localhost:8080/test/"
        if (prefixes == null || prefixes.Length == 0)
        {
            throw new ArgumentException("URI prefixes are required");
        }

        if (method == null)
        {
            throw new ArgumentException("responder method required");
        }

        foreach (var s in prefixes)
        {
            _listener.Prefixes.Add(s);
        }

        _responderMethod = method;
        _listener.Start();
    }



    public void Run()
    {
        ThreadPool.QueueUserWorkItem(o =>
        {
            log.Info("Webserver running...");
            try
            {
                while (_listener.IsListening)
                {
                    ThreadPool.QueueUserWorkItem(c =>
                    {
                        var ctx = c as HttpListenerContext;
                        try
                        {
                            if (ctx == null)
                            {
                                return;
                            }

                            _responderMethod(ctx.Request, ctx.Response);
                        }
                        catch(Exception ex)
                        {
                            // ignored
                            log.Error($"Web Server Exception: {ex}");
                        }
                        finally
                        {
                            // always close the stream
                            if (ctx != null)
                            {
                                // sometimes output stream is closed already
                                try
                                {
                                    ctx.Response.OutputStream.Close();
                                }
                                catch (Exception ex)
                                {
                                    log.Error($"Outputstream might have been closed already.  Exception: {ex}");
                                }
                                
                            }
                        }
                    }, _listener.GetContext());
                }
            }
            catch (Exception ex)
            {
                // ignored
            }
        });
    }

    public void Stop()
    {
        _listener.Stop();
        _listener.Close();
    }
}
