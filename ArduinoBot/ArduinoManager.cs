using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;

namespace ArduinoBot
{
    public class ArduinoManager
    {
        public class ArduinoBoard
        {
            private Stream _stream;
            private BinaryWriter _sw;
            private BinaryReader _sr;

            public string Port { get; private set; }
            public Version Version { get; private set; }
            public string Name { get; private set; }

            internal ArduinoBoard(string port,Stream stream)
            {
                _stream = stream;
                _sw = new BinaryWriter(stream);
                _sr = new BinaryReader(stream);
                Port = port;
            }

            public void Start()
            {
                while (_sr.ReadChar() != 'w');
                _sw.Write('s');
                while (_sr.ReadChar() != 'o');
                Version = Version.Parse(ReadString());
                Name = ReadString();


            }

            private string ReadString()
            {
                return _sr.ReadString();
            }

            public void Close()
            {
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

