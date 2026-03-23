using SBC_2D.Infrastructures.Ini;
using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Policy;
using System.Text;
using System.Threading;

namespace SBC_2D.Infrastructures.Device
{
    public class KeyenceBarcodeReader : IBarcodeReaderDevice, IConnectableDevice
    {
        private Socket _socketClient;
        private SemaphoreSlim _socketClientLock;
        public string Name { get; private set; }
        public bool IsConnected { get; private set; }
        public event Action<string, bool> ConnectionChanged;

        public KeyenceBarcodeReader(string name)
        {
            _socketClientLock = new SemaphoreSlim(1, 1);
            Name = name;
        }

        public void Dispose()
        {
            Disconnect();
        }

        public bool Connect(IConnectionConfig config)
        {
            bool success = false;
            _socketClientLock.Wait();
            try
            {
                SocketConfig cfg = config as SocketConfig;
                if (_socketClient != null)
                    ShutdownClose();
                _socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IAsyncResult result = _socketClient.BeginConnect(IPAddress.Parse(cfg.Address.Trim()), cfg.Port, null, null);
                success = result.AsyncWaitHandle.WaitOne(2000, true);
                if (!success)
                {
                    IsConnected = false;
                    throw new Exception($"{nameof(KeyenceBarcodeReader)} Connect timeout.");
                }
                _socketClient.EndConnect(result);
                _socketClient.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger, new LingerOption(true, 0));
                _socketClient.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, 1);
                byte[] keepAlive = new byte[12];
                BitConverter.GetBytes((uint)1).CopyTo(keepAlive, 0);
                BitConverter.GetBytes((uint)3000).CopyTo(keepAlive, 4);
                BitConverter.GetBytes((uint)500).CopyTo(keepAlive, 8);
                _socketClient.IOControl(IOControlCode.KeepAliveValues, keepAlive, null);
                IsConnected = _socketClient.Connected;
                return true;
            }
            finally
            {
                ConnectionChanged?.Invoke(Name, success);
                _socketClientLock.Release();
            }
        }

        public void Disconnect()
        {
            _socketClientLock.Wait();
            try
            {
                ShutdownClose();
            }
            finally
            {
                _socketClientLock.Release();
            }
        }

        private void ShutdownClose()
        {
            if (_socketClient == null)
                return;
            try
            {
                _socketClient.Shutdown(SocketShutdown.Both);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                _socketClient.Close();
                ConnectionChanged?.Invoke(Name, _socketClient.Connected);
                _socketClient.Dispose();
                _socketClient = null;
            }
        }

        public bool CheckConnection()
        {
            _socketClientLock.Wait();
            try
            {
                bool isNowConnected = _socketClient != null && _socketClient.Connected;

                if (isNowConnected)
                {
                    try
                    {
                        int length = _socketClient.Send(Encoding.UTF8.GetBytes("KEYENCE\r"));
                        if (length <= 0)
                            isNowConnected = false;
                    }
                    catch 
                    { 
                        isNowConnected = false; 
                    }
                }

                if (IsConnected != isNowConnected)
                {
                    IsConnected = isNowConnected;
                    ConnectionChanged?.Invoke(Name, isNowConnected);
                }

                return IsConnected;
            }
            finally
            {
                _socketClientLock.Release();
            }
        }

        public string ReadBarcode(int timeout = 5000)
        {
            _socketClientLock.Wait();
            try
            {
                ClearSocketBuffer(_socketClient);
                int len;
                byte[] rBuffer = new byte[1024];
                StringBuilder sb = new StringBuilder();
                _socketClient.ReceiveTimeout = timeout;
                _socketClient.Send(Encoding.UTF8.GetBytes("LON\r"));
                while ((len = _socketClient.Receive(rBuffer)) > 0)
                {
                    sb.Append(Encoding.UTF8.GetString(rBuffer, 0, len));
                    if (sb.ToString().EndsWith("\r")) break;
                }
                if (sb.Equals("ERROR\r"))
                    return "ERROR";
                return sb.ToString().Trim('\r', ' ');
            }
            finally
            {
                _socketClientLock.Release();
            }
        }

        //catch (SocketException ex) when (ex.SocketErrorCode == SocketError.TimedOut)
        //{
        //    Logger.RecordLog(SysPaths.MyLogDir, $"{GetType().Name} {IP}:{Port} 讀取條碼超時");
        //    return "ERROR";
        //}
        //catch (Exception ex)
        //{
        //    Logger.RecordLog(SysPaths.MyLogDir, $"{GetType().Name} {IP}:{Port} 讀取條碼發生例外: {ex.Message}");
        //    return "ERROR";
        //}


        private void ClearSocketBuffer(Socket socket)
        {
            if (socket == null || !socket.Connected)
                return;

            socket.Blocking = false;
            byte[] tmp = new byte[1024];
            try
            {
                while (socket.Available > 0)
                {
                    socket.Receive(tmp);
                }
            }
            catch (SocketException ex)
            {
                if (ex.ErrorCode != 10035)
                    throw;
            }
            finally
            {
                socket.Blocking = true;
            }
        }
    }
}
