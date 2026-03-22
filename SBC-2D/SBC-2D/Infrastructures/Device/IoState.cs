using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Infrastructures.Device
{
    public class IoState
    {
        public bool[] Dis { get; private set; }
        public bool[] Dos { get; private set; }

        public IoState(int diCount, int doCount)
        {
            Dis = new bool[diCount];
            Dos = new bool[doCount];
        }

        public bool UpdateDis(ReadOnlySpan<bool> data)
        {
            if (Dis == null)
                Dis = new bool[data.Length];

            if (Dis.Length != data.Length)
                return false;

            bool changed = false;

            for (int i = 0; i < data.Length; i++)
            {
                if (Dis[i] != data[i])
                {
                    Dis[i] = data[i];
                    changed = true;
                }
            }
            return changed;
        }

        public bool UpdateDos(ReadOnlySpan<bool> data)
        {
            if (Dos == null)
                Dos = new bool[data.Length];

            if (Dos.Length != data.Length)
                return false;

            bool changed = false;

            for (int i = 0; i < data.Length; i++)
            {
                if (Dos[i] != data[i])
                {
                    Dos[i] = data[i];
                    changed = true;
                }
            }
            return changed;
        }
    }
}
