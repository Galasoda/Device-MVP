using System;
using System.Collections.Generic;

namespace SBC_2D.Infrastructures.Device
{
    public class IoDeviceContext
    {
        public IIoDevice Device { get; }
        public int DiStart { get; }
        public int DoStart { get; }
        public IoState State { get; }

        public event Action<IReadOnlyDictionary<int, bool>> SystemDisUpdated;
        public event Action<IReadOnlyDictionary<int, bool>> SystemDosUpdated;

        public IoDeviceContext(IIoDevice device, int diStart, int doStart, IoState state)
        {
            Device = device;
            DiStart = diStart;
            DoStart = doStart;
            State = state;
        }

        public void UpdateDis()
        {
            Device.ReadAllDi(out bool[] dis);
            State.UpdateDis(dis);
            var systemDis = new Dictionary<int, bool>(dis.Length);
            for (int i = 0; i < dis.Length; i++)
                systemDis[ToSystemDi(i)] = dis[i];
            SystemDisUpdated?.Invoke(systemDis);
        }

        public void UpdateDos()
        {
            Device.ReadAllDo(out bool[] dos);
            State.UpdateDos(dos);
            var systemDos = new Dictionary<int, bool>(dos.Length);
            for (int i = 0; i < dos.Length; i++)
                systemDos[ToSystemDo(i)] = dos[i];
            SystemDosUpdated?.Invoke(systemDos);
        }

        // 裝置 index → 系統 index
        public int ToSystemDi(int deviceIndex)
        {
            if (deviceIndex < 0 || deviceIndex >= Device.DiCount)
                throw new ArgumentOutOfRangeException(nameof(deviceIndex),
                    $"DI device index {deviceIndex} out of range [0, {Device.DiCount}).");
            return DiStart + deviceIndex;
        }

        public int ToSystemDo(int deviceIndex)
        {
            if (deviceIndex < 0 || deviceIndex >= Device.DoCount)
                throw new ArgumentOutOfRangeException(nameof(deviceIndex),
                    $"DO device index {deviceIndex} out of range [0, {Device.DoCount}).");
            return DoStart + deviceIndex;
        }

        public int ToDeviceDi(int systemIndex)
        {
            if (!OwnsDi(systemIndex))
                throw new ArgumentOutOfRangeException(nameof(systemIndex),
                    $"System DI index {systemIndex} does not belong to this device.");
            return systemIndex - DiStart;
        }

        public int ToDeviceDo(int systemIndex)
        {
            if (!OwnsDo(systemIndex))
                throw new ArgumentOutOfRangeException(nameof(systemIndex),
                    $"System DO index {systemIndex} does not belong to this device.");
            return systemIndex - DoStart;
        }

        public bool TryToDeviceDi(int systemIndex, out int deviceIndex)
        {
            deviceIndex = -1;
            if (!OwnsDi(systemIndex)) return false;
            deviceIndex = systemIndex - DiStart;
            return true;
        }

        public bool TryToDeviceDo(int systemIndex, out int deviceIndex)
        {
            deviceIndex = -1;
            if (!OwnsDo(systemIndex)) return false;
            deviceIndex = systemIndex - DoStart;
            return true;
        }

        public bool OwnsDi(int systemIndex)
            => systemIndex >= DiStart && systemIndex < DiStart + Device.DiCount;

        public bool OwnsDo(int systemIndex)
            => systemIndex >= DoStart && systemIndex < DoStart + Device.DoCount;

    }
}


