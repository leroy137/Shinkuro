using System;
using System.Collections.Generic;
using System.Text;

namespace Shinkuro.Models
{
    public class MessageLog
    {

        public LogType Type { get; set; }
        public DateTime Time { get; set; }
        public String Message { get; set; }

        public MessageLog()
        {

        }

        public MessageLog(LogType type, String message)
        {
            Type = type;
            Time = DateTime.Now;
            Message = message;
        }
    }


    public enum LogType { 
        Error,
        Warrning,
        Successfull,
        Information,
        Simple
    }

}
