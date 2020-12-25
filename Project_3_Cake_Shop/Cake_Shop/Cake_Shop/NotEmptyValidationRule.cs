using System.Globalization;
using System.Windows.Controls;

namespace Cake_Shop
{
    public class NotEmptyValidationRule : ValidationRule
    {
        public string Message { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrWhiteSpace(value?.ToString()))
            {
                return new ValidationResult(false, Message ?? "Vui lòng nhập thông tin");
            }
            return ValidationResult.ValidResult;
        }
    }
}