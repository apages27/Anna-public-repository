using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace CineViewEmulatorForTCPTesting
{
    public partial class Emulator : Form
    {
        private const int PortNumber = 9761;
        private const string ServerIp = "127.0.0.1";
        private readonly Dispatcher _dispatcher = Dispatcher.CurrentDispatcher;
        private TcpClient _client;
        private Dictionary<char, string> _dictionary;
        private TcpListener _listener;

        public Emulator()
        {
            InitializeComponent();

            CreateASCIIHexDictionary();

            CreateTCPServer();

            Task.Run(() => { ListenForIncomingMessages(); });
        }

        private void CreateASCIIHexDictionary()
        {
            _dictionary = new Dictionary<char, string>
            {
                {'î', "0xee"},
                {'g', "0x67"},
                {'h', "0x68"},
                {'j', "0x6A"},
                {'k', "0x6B"},
                {'Y', "0x59"},
                {'Z', "0x5A"}
            };
        }

        private void AppendMessage(string message, params object[] args)
        {
            var finalMessage = string.Format(message, args);

            Action process =
                () => { resultsTextBox.Text += $"{finalMessage}{Environment.NewLine}"; };

            _dispatcher.BeginInvoke(process);
        }

        public void CreateTCPServer()
        {
            _listener = new TcpListener(IPAddress.Any, PortNumber);
        }

        private void ListenForIncomingMessages()
        {
            AppendMessage("Listening...");

            while (true)
            {
                _listener.Start();

                if (!_listener.Pending()) continue;

                var streamReceived = ReceiveIncomingMessage();

                var dataReceived = ParseIncomingMessage(streamReceived);

                AppendMessage(string.Format("Received: " + dataReceived));

                ResetListener();
            }
        }

        private NetworkStream ReceiveIncomingMessage()
        {
            _client = _listener.AcceptTcpClient();

            return _client.GetStream();
        }

        private void ResetListener()
        {
            _client?.Close();

            _listener?.Stop();
        }

        private string ParseIncomingMessage(NetworkStream streamReceived)
        {
            var buffer = new byte[_client.ReceiveBufferSize];

            var bytesRead = streamReceived.Read(buffer, 0, _client.ReceiveBufferSize);

            var dataReceived =
                ConvertASCIIToStringRepresentationOfHex(Encoding.Default.GetString(buffer, 0, bytesRead));

            return dataReceived;
        }

        private string ConvertASCIIToStringRepresentationOfHex(string dataReceived)
        {
            var hexString = "";

            foreach (var item in dataReceived)
            {
                if (_dictionary.ContainsKey(item))
                    hexString += $"{_dictionary[item]} ";
                else
                    hexString += $"{item} ";
            }

            return hexString;
        }

        private void sendTestMsgButton_Click(object sender, EventArgs e)
        {
            var textToSend = DateTime.Now.ToString();

            var client = new TcpClient(ServerIp, PortNumber);

            var stream = client.GetStream();

            var bytesToSend = Encoding.ASCII.GetBytes(textToSend);

            AppendMessage(string.Format("Sending: " + textToSend));

            stream.Write(bytesToSend, 0, bytesToSend.Length);
        }
    }
}