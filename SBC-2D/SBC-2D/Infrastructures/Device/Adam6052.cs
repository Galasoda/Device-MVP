using Advantech.Adam;
using SBC_2D.Infrastructures.Device;
using SBC_2D.Infrastructures.Ini;
using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading;

public class Adam6052 : IIoDevice, IConnectableDevice, IDisposable
{
    private AdamSocket _adamModbus;
    private readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);

    public event Action<string, bool> ConnectionChanged;

    public string Name { get; }
    public bool IsConnected { get; private set; }
    public int DiCount { get; } = 8;
    public int DoCount { get; } = 8;

    private readonly int[] _diAddress;
    private readonly int[] _doAddress;

    public Adam6052(string name)
    {
        Name = name;
        _adamModbus = new AdamSocket();
        _diAddress = Enumerable.Range(1, DiCount).ToArray();
        _doAddress = Enumerable.Range(17, DoCount).ToArray();
    }


    public bool Connect(IConnectionConfig config)
    {
        _lock.Wait();
        try
        {
            SocketConfig cfg = config as SocketConfig;
            _adamModbus?.Disconnect();
            _adamModbus = new AdamSocket();
            _adamModbus.SetTimeout(2000, 25, 25);
            IsConnected = _adamModbus.Connect(cfg.Address, ProtocolType.Tcp, cfg.Port);
            if (IsConnected)
                _adamModbus.SetSocketOpt(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, 1);
            ConnectionChanged?.Invoke(Name, IsConnected);
            return IsConnected;
        }
        finally { _lock.Release(); }
    }

    public void Disconnect()
    {
        _lock.Wait();
        try
        {
            _adamModbus?.Disconnect();
            _adamModbus = null;
            IsConnected = false;
            ConnectionChanged?.Invoke(Name, IsConnected);
        }
        finally { _lock.Release(); }
    }

    public bool CheckConnection()
    {
        _lock.Wait();
        try
        {
            bool isNowConnected = false;
            if (_adamModbus != null && _adamModbus.Connected)
            {
                try
                {
                    _adamModbus.Modbus().ReadHoldingRegs(0, 1, out byte[] _);
                    isNowConnected = true;
                }
                catch { isNowConnected = false; }
            }

            if (IsConnected != isNowConnected)
            {
                IsConnected = isNowConnected;
                ConnectionChanged?.Invoke(Name, IsConnected);
            }

            return IsConnected;
        }
        finally { _lock.Release(); }
    }

    public bool ReadDi(int channel, out bool data)
    {
        data = false;
        if (channel < 0 || channel >= DiCount) return false;
        if (!IsConnected) return false;
        return ReadByAddress(_diAddress[channel], out data);
    }

    public bool ReadAllDi(out bool[] data)
    {
        data = Array.Empty<bool>();
        if (!IsConnected) return false;
        return ReadsByAddress(1, DiCount, out data);
    }

    public bool ReadDo(int channel, out bool data)
    {
        data = false;
        if (channel < 0 || channel >= DoCount) return false;
        if (!IsConnected) return false;
        return ReadByAddress(_doAddress[channel], out data);
    }

    public bool ReadAllDo(out bool[] data)
    {
        data = Array.Empty<bool>();
        if (!IsConnected) return false;
        return ReadsByAddress(17, DoCount, out data);
    }

    public bool WriteDo(int channel, bool data)
    {
        if (channel < 0 || channel >= DoCount) return false;
        if (!IsConnected) return false;
        return WriteByAddress(_doAddress[channel], data);
    }

    public bool InverseDo(int channel, out bool result)
    {
        result = false;
        if (!ReadDo(channel, out bool current)) return false;
        if (!WriteDo(channel, !current)) return false;
        result = !current;
        return true;
    }

    private bool ReadByAddress(int address, out bool data)
    {
        _lock.Wait();
        try
        {
            bool success = _adamModbus.Modbus()
                .ReadCoilStatus(address, 1, out bool[] result);
            data = success ? result[0] : false;
            return success;
        }
        finally { _lock.Release(); }
    }

    private bool ReadsByAddress(int startAddress, int count, out bool[] data)
    {
        _lock.Wait();
        try
        {
            bool success = _adamModbus.Modbus()
                .ReadCoilStatus(startAddress, count, out bool[] result);
            data = success ? result : Array.Empty<bool>();
            return success;
        }
        finally { _lock.Release(); }
    }

    private bool WriteByAddress(int address, bool data)
    {
        _lock.Wait();
        try
        {
            return _adamModbus.Modbus().ForceSingleCoil(address, data);
        }
        finally { _lock.Release(); }
    }

    public void Dispose() => Disconnect();
}