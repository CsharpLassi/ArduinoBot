using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;
using ArduinoBot.DataEntries;

namespace ArduinoBot
{
    public class ArduinoManager
    {
        public class ArduinoBoard
        {
            private Stream _stream;
            private BinaryWriter _sw;
            private BinaryReader _sr;

            private CancellationTokenSource _canceltoken;
            private Thread _readthread;
            private AutoResetEvent _wd;


            public string Port { get; private set; }
            public Version Version { get; private set; }
            public string Name { get; private set; }

            public Dictionary<DataID,DataEntry> CurrentData { get; private set; }

            internal ArduinoBoard(string port,Stream stream)
            {
                CurrentData = new Dictionary<DataID, DataEntry>();

                _stream = stream;
                _sw = new BinaryWriter(stream);
                _sr = new BinaryReader(stream);

                Port = port;
            }

            public void Start()
            {
                Thread.Sleep(1000);
                while (_sr.ReadChar() != 'w');
                _sw.Write('s');
                while (_sr.ReadChar() != 'o');
                Version = Version.Parse(ReadString());
                Name = ReadString();

                _canceltoken = new CancellationTokenSource();
                _readthread = new Thread(ReadStream);
                _readthread.Start();
                _wd = new AutoResetEvent(false);
            }
            
            private void ReadStream()
            {
                var token = _canceltoken.Token;
                while (!token.IsCancellationRequested)
                {
                    var data = ReadData();
                    InsertData(data);
                }
                _wd.Set();
            }

            private void InsertData(DataEntry entry)
            {
                if (CurrentData.ContainsKey(entry.Header.ID))
                    CurrentData[entry.Header.ID] = entry;
                else
                    CurrentData.Add(entry.Header.ID, entry);
            }

            private DataEntry ReadData()
            {
                DataEntry result = null;
                do
                {
                    var id =(DataID)_sr.ReadUInt16();
                    var length = _sr.ReadUInt16();

                    DataHeader header = new DataHeader(id,length);

                    switch (id)
                    {
                        case DataID.InternalTemp:
                            result = new TemperaturEntry(header,_sr.ReadBytes(length* TemperaturEntry.DataLength));
                            break;
                        default:
                            break;
                    }
                } while (result == null);

                return result;
            }

            private string ReadString()
            {
                return _sr.ReadString();
            }

            public void Close()
            {
                _canceltoken.Cancel();
                _wd.WaitOne();

                _stream.Close();
            }
        }

        private Dictionary<string,ArduinoBoard> _boards = new Dictionary<string, ArduinoBoard>();
        public IEnumerable<ArduinoBoard> Boards { get { return _boards.Select( i => i.Value); } }

        public ArduinoManager()
        {
        }

        public ArduinoBoard OpenArduino(string port)
        {
            if (_boards.ContainsKey(port))
                throw new ArgumentException("Port wurde schon hinzugefügt.");

            SerialPort serialport = new SerialPort(port,9600);
            serialport.Open();




            ArduinoBoard board = new ArduinoBoard(port,serialport.BaseStream);
            board.Start();


            _boards.Add(port,board);

            return board;
        }

        public void Close(ArduinoBoard board)
        {
            board.Close();
            _boards.Remove(board.Port);

        }

        public void Close()
        {
            while (_boards.Count > 0)
            {
                var board = _boards.First().Value;
                Close(board);
            }
        }
    }
}

