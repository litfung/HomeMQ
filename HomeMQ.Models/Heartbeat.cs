using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMQ.Models
{
    public class StatusCheck
    {
    }

    public class StartPoll
    {

    }

    public class StopPoll
    {

    }

    public class BoontonStartup
    {

    }

    public class BoontonCloseSensors
    {

    }

    public class BoontonResetSensors
    {

    }

    public class BoontonLoadFromConfig
    {
        public string Serial { get; set; }
        public string Filename { get; set; }
        public BoontonLoadFromConfig(string serial, string filename)
        {
            Serial = serial;
            Filename = filename;
        }
    }


}
