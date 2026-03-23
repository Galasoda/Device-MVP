using SBC_2D.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Views.Interfaces
{
    public interface IForm3View
    {
        IReadOnlyList<IDeviceConnectionView> DeviceConnectionViews { get; }
        IReadOnlyList<IIoView> InputViews { get; }
        IReadOnlyList<IIoView> OutputViews { get; }
        IDeviceConnectionView AddDeviceConnectionView();
        IIoView AddInputView(int number);
        IOutView AddOutputView(int number);
        void ClearInputView();
        void ClearOutputView();
    }
}
