using SBC_2D.Infrastructures.Ini;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Shared
{
    public static class Helper
    {
        public static bool TryConvert(Type targetType, object value, out object result)
        {
            result = null;
            try
            {
                if (value == null)
                    return !targetType.IsValueType;

                if (targetType.IsEnum)
                {
                    result = Enum.Parse(targetType, value.ToString());
                    return true;
                }

                Type underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;
                result = Convert.ChangeType(value, underlyingType);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static object ConvertValue(Type targetType, object value)
        {
            if (value == null)
                return null;

            if (targetType.IsEnum)
                return Enum.Parse(targetType, value.ToString());

            Type underlyingType = Nullable.GetUnderlyingType(targetType);
            if (underlyingType != null)
                return Convert.ChangeType(value, underlyingType);

            return Convert.ChangeType(value, targetType);
        }
    }
}
