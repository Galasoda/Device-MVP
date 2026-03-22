using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Views.Interfaces
{
    public interface IIoView
    {
        int SystemIndex { get; }
        void SetStatus(bool isOn);
        void SetNumber(int number);
        void SetDescription(string description);
    }
}
