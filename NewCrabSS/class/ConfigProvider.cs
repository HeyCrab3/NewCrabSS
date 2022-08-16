using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCrabSS.ConfigProvider
{
    internal class ConfigProvider
    {
        public class Config
        {
            public int confVersion { get; set; }
            public string minRam { get; set; }
            public string maxRam { get; set; }
            public string javaPath { get; set; }
            public string other { get; set; }
        }
        public class JsonData
        {
            public int confVersion { get; set; }
            public string minRam { get; set; }
            public string maxRam { get; set; }
            public string javaPath { get; set; }
            public string other { get; set; }
        }
        public class HWID
        {
            public int code { get; set; }
            public string msg { get; set; }
        }
    }
}
