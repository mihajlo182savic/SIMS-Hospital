using System.Globalization;
using System.Windows.Controls;

namespace SIMS_Projekat_Bolnica_Zdravo.Windows
{
    public class CreateRoomValidationRules : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                int r;
                if (int.TryParse(s, out r))
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "Please enter a valid int value.");
            }
            catch
            {
                return new ValidationResult(false, "Unknown error occured.");
            }
        }
    }

    public class MinMaxValidationRule : ValidationRule
    {
        public int Min
        {
            get;
            set;
        }

        public int Max
        {
            get;
            set;
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value is int)
            {
                int d = (int)value;
                if (d < Min) return new ValidationResult(false, "Floors start at 0");
                if (d > Max) return new ValidationResult(false, "Last floor is 3");
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "Unknown error occured.");
            }
        }
    }
}

