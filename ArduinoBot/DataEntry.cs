using System;

namespace ArduinoBot
{
    public abstract class DataEntry
    {
        public DataHeader Header { get; private set; }

        public double Value { get; protected set; }

        public DataEntry(DataHeader header)
        {
            Header = header;
        }
            
    }
}

