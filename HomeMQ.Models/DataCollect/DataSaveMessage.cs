using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMQ.Models
{
    public class DataSaveMessage
    {
        public Header Header { get; set; }
        public DataPayload Payload { get; set; }
    }

    public class DataPayload
    {
        public float[] Data { get; set; }
    }
}
