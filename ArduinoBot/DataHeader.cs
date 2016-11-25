using System;

namespace ArduinoBot
{
    public class DataHeader
    {
        public DataID ID { get; private set; }
        public ushort Length { get; private set; }

        public DataHeader(DataID id, ushort length)
        {
            ID = id;
            Length = length;
        }
    }
}

