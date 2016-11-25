using System;
using System.Collections.Generic;
using System.Linq;

namespace ArduinoBot.DataEntries
{
    public class TemperaturEntry : DataEntry
    {
        public const ushort DataLength = 2;

        public TemperaturEntry(DataHeader header, byte[] datas) 
            : base(header)
        {
            ReadData(datas);
        }

        private void ReadData(byte[] datas)
        {
            List<ushort> values = new List<ushort>(Header.Length);

            for (int i = 0; i < Header.Length; i++)
            {
                values.Add(BitConverter.ToUInt16(datas, i * DataLength));
            }

            Value = values.Sum(i =>i) / values.Count;
        }
    }
}

