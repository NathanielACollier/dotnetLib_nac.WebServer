# nac.WebServer
+ Code moved from WebPad project to here so it can be used in other projects

## Example
```c#
var webMan = new nac.WebServer.WebServerManager();

webMan.OnNewRequest += (_s, _e) =>
{
    if(_e.Request.Url.LocalPath == "/"){
        nac.WebServer.lib.HttpHelper.WriteTextResponse(_e.Response, "text/html", @"
            <div style='color:green;'>Hello World!</div>
        ");
    }
};
webMan.Start();
```

