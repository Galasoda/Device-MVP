using System.ComponentModel;
using SBC_2D.Shared;
using SBC_2D.Infrastructures.Parameter;


namespace SBC_2D.ViewModels
{
    public class Form2Vm : INotifyPropertyChanged
    {
        public string EditModelName => "Edit";
        private bool _isMapModeBypass;
        private bool _isUpperBrBypass;
        private bool _isLowerBrBypass;
        private bool _isLdsBypass;
        private bool _isPcbRotate;
        private int _thicknessZeroBias;
        private int _thickness;
        private int _thicknessMaxRange;
        private int _pcbCellsX;
        private int _pcbCellsY;
        private int _pcbBlocksX;
        private int _pcbBlocksY;
        private string _selectedModelName = "";
        private BindingList<string> _modelNames = new BindingList<string>();
        private PcbCount _pcbCount;

        public PcbCount PcbCount
        {
            get => _pcbCount;
            set
            {
                _pcbCount = value;
                OnPropertyChanged(nameof(IsSinglePcb));
                OnPropertyChanged(nameof(IsDualPcb));
            }
        }

        public bool IsSinglePcb => _pcbCount == PcbCount.Single;

        public bool IsDualPcb => _pcbCount == PcbCount.Dual;

        public bool IsMapModeBypass
        {
            get => _isMapModeBypass;
            set
            {
                if (value != _isMapModeBypass)
                {
                    _isMapModeBypass = value;
                    if (_isMapModeBypass)
                    {
                        _isUpperBrBypass = false;
                        _isLowerBrBypass = false;
                        OnPropertyChanged(nameof(IsUpperBrBypass));
                        OnPropertyChanged(nameof(IsLowerBrBypass));
                    }
                    OnPropertyChanged(nameof(IsMapModeBypass));
                }
            }
        }


        public bool IsUpperBrBypass
        {
            get => _isUpperBrBypass;
            set
            {
                if (value != _isUpperBrBypass)
                {
                    _isUpperBrBypass = value;
                    if (_isUpperBrBypass)
                    {
                        _isMapModeBypass = false;
                        _isLowerBrBypass = false;
                        OnPropertyChanged(nameof(IsMapModeBypass));
                        OnPropertyChanged(nameof(IsLowerBrBypass));
                    }
                    OnPropertyChanged(nameof(IsUpperBrBypass));
                }
            }
        }

        public bool IsLowerBrBypass
        {
            get => _isLowerBrBypass;
            set
            {
                if (value != _isLowerBrBypass)
                {
                    _isLowerBrBypass = value;
                    if (_isLowerBrBypass)
                    {
                        _isMapModeBypass = false;
                        _isUpperBrBypass = false;
                        OnPropertyChanged(nameof(IsMapModeBypass));
                        OnPropertyChanged(nameof(IsUpperBrBypass));
                    }
                    OnPropertyChanged(nameof(IsLowerBrBypass));
                }
            }
        }

        public bool IsLdsBypass
        {
            get => _isLdsBypass;
            set
            {
                if (value != _isLdsBypass)
                {
                    _isLdsBypass = value;
                    OnPropertyChanged(nameof(IsLdsBypass));
                }
            }
        }

        public bool IsPcbRotate
        {
            get => _isPcbRotate;
            set { _isPcbRotate = value; OnPropertyChanged(nameof(IsPcbRotate)); }
        }

        public int ThicknessZeroBias
        {
            get => _thicknessZeroBias;
            set { _thicknessZeroBias = value; OnPropertyChanged(nameof(ThicknessZeroBias)); }
        }

        public int Thickness
        {
            get => _thickness;
            set
            {
                _thickness = value;
                OnPropertyChanged(nameof(Thickness));
                OnPropertyChanged(nameof(MaxThickness));
            }
        }

        public int ThicknessMaxRange
        {
            get => _thicknessMaxRange;
            set
            {
                _thicknessMaxRange = value;
                OnPropertyChanged(nameof(ThicknessMaxRange));
                OnPropertyChanged(nameof(MaxThickness));
            }
        }

        public int MaxThickness => Thickness + ThicknessMaxRange;

        public int PcbCellsX
        {
            get => _pcbCellsX;
            set { _pcbCellsX = value; OnPropertyChanged(nameof(PcbCellsX)); }
        }

        public int PcbCellsY
        {
            get => _pcbCellsY;
            set { _pcbCellsY = value; OnPropertyChanged(nameof(PcbCellsY)); }
        }

        public int PcbBlocksX
        {
            get => _pcbBlocksX;
            set { _pcbBlocksX = value; OnPropertyChanged(nameof(PcbBlocksX)); }
        }

        public int PcbBlocksY
        {
            get => _pcbBlocksY;
            set { _pcbBlocksY = value; OnPropertyChanged(nameof(PcbBlocksY)); }
        }

        public string SelectedModelName
        {
            get => _selectedModelName;
            set { _selectedModelName = value; OnPropertyChanged(nameof(SelectedModelName)); }
        }

        public BindingList<string> ModelNames
        {
            get => _modelNames;
            set { _modelNames = value; OnPropertyChanged(nameof(ModelNames)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateForm(Parameters model)
        {
            SelectedModelName = model.Name;
            IsMapModeBypass = model.IsMapModeBypass;
            IsUpperBrBypass = model.IsUpperBrBypass;
            IsLowerBrBypass = model.IsLowerBrBypass;
            IsLdsBypass = model.IsLdsBypass;
            IsPcbRotate = model.IsPcbRotate;
            PcbCount = (PcbCount)model.PcbCount;
            PcbCellsX = model.PcbCellsX;
            PcbCellsY = model.PcbCellsY;
            PcbBlocksX = model.PcbBlocksX;
            PcbBlocksY = model.PcbBlocksY;
            ThicknessZeroBias = model.ThicknessZeroBias;
            Thickness = model.Thickness;
            ThicknessMaxRange = model.ThicknessPosTolerance;
        }

        public Parameters ToModel()
        {
            return new Parameters
            {
                Name = EditModelName,
                IsMapModeBypass = IsMapModeBypass,
                IsUpperBrBypass = IsUpperBrBypass,
                IsLowerBrBypass = IsLowerBrBypass,
                IsLdsBypass = IsLdsBypass,
                IsPcbRotate = IsPcbRotate,
                ThicknessZeroBias = ThicknessZeroBias,
                Thickness = Thickness,
                ThicknessPosTolerance = ThicknessMaxRange,
                PcbCount = (int)PcbCount,
                PcbCellsX = PcbCellsX,
                PcbCellsY = PcbCellsY,
                PcbBlocksX = PcbBlocksX,
                PcbBlocksY = PcbBlocksY
            };
        }
    }
}
