using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.ViewModels
{
    public class FormMainVm : INotifyPropertyChanged
    {
        private string _version;
        private string _modelName;
        private string _machineStatus;
        private string _userRole;

        public string ModelName
        {
            get => _modelName;
            set
            {
                if (_modelName != value)
                {
                    _modelName = value;
                    OnPropertyChanged(nameof(ModelName)); 
                }
            }
        }

        public string Version
        {
            get => _version;
            set
            {
                if (_version != value)
                {
                    _version = value;
                    OnPropertyChanged(nameof(Version));
                }
            }
        }

        public string MachineStatus
        {
            get => _machineStatus;
            set
            {
                if (_machineStatus != value)
                {
                    _machineStatus = value;
                    OnPropertyChanged(nameof(MachineStatus));
                }
            }
        }

        public string UserRole
        {
            get => _userRole;
            set
            {
                if (_userRole != value)
                {
                    _userRole = value;
                    OnPropertyChanged(nameof(UserRole));
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
