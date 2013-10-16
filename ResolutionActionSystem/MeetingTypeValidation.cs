using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ResolutionActionSystemLogic;

namespace ResolutionActionSystem
{
    class MeetingTypeValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var meeting = (MeetingType) value;
            if (meeting != null)
            {
                return  new ValidationResult(false,"Meeting Type cannot be null");
            }
            return new ValidationResult(true,null);
        }
    }
}
