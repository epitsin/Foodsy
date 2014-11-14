namespace Foodsy.Common.CustomAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    public class DateTimeRangeAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "'{0}' must between {1:f} and {2:f}.";

        public DateTimeRangeAttribute(int minPlusDays, int maxPlusDays) 
            : base(DefaultErrorMessage)
        {
            this.MinDate = DateTime.Now.AddDays(minPlusDays);
            this.MaxDate = DateTime.Now.AddDays(maxPlusDays);
        }

        public DateTime MinDate { get; set; }

        public DateTime MaxDate { get; set; }

        public override bool IsValid(object value)
        {
            if (value == null || !(value is DateTime))
            {
                return true;
            }

            DateTime dateValue = (DateTime)value;
            return this.MinDate <= dateValue && dateValue <= this.MaxDate;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(
                CultureInfo.CurrentCulture,
                this.ErrorMessageString,
                name,
                this.MinDate,
                this.MaxDate);
        }
    }
}
