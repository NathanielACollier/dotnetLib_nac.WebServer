using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace nac.WebServer.lib;

public class Log
{
    /*
     Used for internal library logging
     */
    public class LogMessage
    {
        public string Level { get; set; }
        public string Message { get; set; }
        public string CallingMemberName { get; set; }
        public Type CallerType { get; set; }
    }

    private Type callerType;

    public static event EventHandler<LogMessage> OnNewMessage;

    public Log()
    {
        var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
        var _class_to_log = mth.ReflectedType;
        this.callerType = _class_to_log;
    }

    public void Info(string message, [CallerMemberName] string callerMemberName = "")
    {
        OnNewMessage?.Invoke(this, new LogMessage
        {
            Level = "INFO",
            Message = message,
            CallingMemberName = callerMemberName,
            CallerType = callerType
        });
    }


    public void Error(string message, [CallerMemberName] string callerMemberName = "")
    {
        OnNewMessage?.Invoke(this, new LogMessage
        {
            Level = "ERROR",
            Message = message,
            CallingMemberName = callerMemberName,
            CallerType = callerType
        });
    }
}
