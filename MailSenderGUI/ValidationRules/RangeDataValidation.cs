using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MailSender.GUI.ValidationRules
{
    public class RangeDataValidation : ValidationRule
    {
        public int? Min { get; set; }
        public int? Max { get; set; }

        public override ValidationResult Validate(object value, CultureInfo culture)
        {
            if (!(value is int int_value))
                try
                {
                    int_value = Convert.ToInt32(value);
                }
                catch
                {
                    return new ValidationResult(false, $"Входное значение {value} не может быть преобразовано в целое число");
                }

            if (Min.HasValue && int_value < Min.Value)
                return new ValidationResult(false, $"Значение {value} < {Min}");
            if (Max.HasValue && int_value > Max.Value)
                return new ValidationResult(false, $"Значение {value} > {Max}");

            return ValidationResult.ValidResult;
        }
    }
}
