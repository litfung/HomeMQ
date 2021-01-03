using System;

namespace HomeMQ.Core
{
    public class LightControlMessage
    {
        public bool Setting { get; set; }
        public string ColorSet { get; set; }
        public int Brightness { get; set; }
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
        public int Warmth { get; set; }

        public LightControlMessage()
        {
            Setting = true;
            ColorSet = "color";
            Brightness = 50;
            Red = 100;
            Green = 150;
            Blue = 200;
            Warmth = 1000;
        }

    }
}
