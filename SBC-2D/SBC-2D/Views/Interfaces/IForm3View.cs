using SBC_2D.Shared;
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
        List<IDeviceConnectionView> DeviceConnectionViews { get; }
        List<IIoView> InputViews { get; }
        List<IIoView> OutputViews { get; }
        IDeviceConnectionView AddDeviceConnectionView();
        IIoView AddInputView(int number);
        IOutView AddOutputView(int number);
    }
}
