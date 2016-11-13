using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace ArduinoBot
{
    public class ArduinoManager
    {
        public class ArduinoBoard
        {
            private Stream _stream;

            public string Name { get; private set; }

            internal ArduinoBoard(string name,Stream stream)
            {
                _stream = stream;
                Name = name;
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

            _boards.Add(port,board);

            return board;
        }

        public void Close(ArduinoBoard board)
        {
            board.Close();
            _boards.Remove(board.Name);

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

