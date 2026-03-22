using SBC_2D.Infrastructures.Recipe;
using SBC_2D.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.ViewModels
{
    public class RecipeVm : INotifyPropertyChanged
    {
        private bool _isMapModeBypass;
        private bool _isUpperBrBypass;
        private bool _isLowerBrBypass;
        private bool _isLdsBypass;
        private bool _isPcbRotate;
        private int _thickness;
        private int _thicknessZeroBias;
        private int _thicknessTolerance;
        private int _pcbCellsX;
        private int _pcbCellsY;
        private int _pcbBlocksX;
        private int _pcbBlocksY;
        private int _pcbCount;
        private string _currentName = "";
        private BindingList<string> _allNames = new BindingList<string>();
        public string UnsavedName => "Edit";

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
            set
            {
                if (value != _isPcbRotate)
                {
                    _isPcbRotate = value;
                    OnPropertyChanged(nameof(IsPcbRotate));
                }
            }
        }

        public int ThicknessZeroBias
        {
            get => _thicknessZeroBias;
            set
            {
                if (value != _thicknessZeroBias)
                {
                    _thicknessZeroBias = value;
                    OnPropertyChanged(nameof(ThicknessZeroBias));
                }
            }
        }

        public int Thickness
        {
            get => _thickness;
            set
            {
                if (value != _thickness)
                {
                    _thickness = value;
                    OnPropertyChanged(nameof(Thickness));
                    OnPropertyChanged(nameof(MaxThickness));
                }
            }
        }

        public int ThicknessTolerance
        {
            get => _thicknessTolerance;
            set
            {
                if (value != _thicknessTolerance)
                {
                    _thicknessTolerance = value;
                    OnPropertyChanged(nameof(ThicknessTolerance));
                    OnPropertyChanged(nameof(MaxThickness));
                }
            }
        }

        public int MaxThickness => Thickness + ThicknessTolerance;

        public int PcbCellsX
        {
            get => _pcbCellsX;
            set
            {
                if (value != _pcbCellsX)
                {
                    _pcbCellsX = value;
                    OnPropertyChanged(nameof(PcbCellsX));
                }
            }
        }

        public int PcbCellsY
        {
            get => _pcbCellsY;
            set
            {
                if (value != _pcbCellsY)
                {
                    _pcbCellsY = value;
                    OnPropertyChanged(nameof(PcbCellsY));
                }
            }
        }

        public int PcbBlocksX
        {
            get => _pcbBlocksX;
            set
            {
                if (value != _pcbBlocksX)
                {
                    _pcbBlocksX = value;
                    OnPropertyChanged(nameof(PcbBlocksX));
                }
            }
        }

        public int PcbBlocksY
        {
            get => _pcbBlocksY;
            set
            {
                if (value != _pcbBlocksY)
                {
                    _pcbBlocksY = value;
                    OnPropertyChanged(nameof(PcbBlocksY));
                }
            }
        }

        public int PcbCount
        {
            get => _pcbCount;
            set
            {
                if (value != _pcbCount)
                {
                    _pcbCount = value;
                    OnPropertyChanged(nameof(PcbCount));
                }
            }
        }

        public string CurrentName
        {
            get => _currentName;
            set
            {
                if (value != _currentName)
                {
                    _currentName = value;
                    OnPropertyChanged(nameof(CurrentName));
                }
            }
        }

        public BindingList<string> AllNames
        {
            get => _allNames;
            set { _allNames = value; OnPropertyChanged(nameof(AllNames)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Initialize(List<string> allNames, Recipe recipe)
        {
            AllNames = new BindingList<string>(allNames);
            Update(recipe);
        }

        public void Update(Recipe recipe)
        {
            CurrentName = recipe.Name;
            IsMapModeBypass = recipe.IsMapModeBypass;
            IsUpperBrBypass = recipe.IsUpperBrBypass;
            IsLowerBrBypass = recipe.IsLowerBrBypass;
            IsLdsBypass = recipe.IsLdsBypass;
            IsPcbRotate = recipe.IsPcbRotate;
            PcbCount = recipe.PcbCount;
            PcbCellsX = recipe.PcbCellsX;
            PcbCellsY = recipe.PcbCellsY;
            PcbBlocksX = recipe.PcbBlocksX;
            PcbBlocksY = recipe.PcbBlocksY;
            ThicknessZeroBias = recipe.ThicknessZeroBias;
            Thickness = recipe.Thickness;
            ThicknessTolerance = recipe.ThicknessPosTolerance;
        }

        public Recipe ToModel()
        {
            return new Recipe
            {
                Name = UnsavedName,
                IsMapModeBypass = IsMapModeBypass,
                IsUpperBrBypass = IsUpperBrBypass,
                IsLowerBrBypass = IsLowerBrBypass,
                IsLdsBypass = IsLdsBypass,
                IsPcbRotate = IsPcbRotate,
                ThicknessZeroBias = ThicknessZeroBias,
                Thickness = Thickness,
                ThicknessPosTolerance = ThicknessTolerance,
                PcbCount = (int)PcbCount,
                PcbCellsX = PcbCellsX,
                PcbCellsY = PcbCellsY,
                PcbBlocksX = PcbBlocksX,
                PcbBlocksY = PcbBlocksY
            };
        }
    }
}