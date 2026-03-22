using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Views.Interfaces
{
    public interface IOutView : IIoView
    {
        event EventHandler<int> OutputClicked;
    }
}
