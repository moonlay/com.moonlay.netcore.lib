using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Moonlay.NetCore.Lib.Service
{
    public class ServiceValidationExeption : Exception
    {
        public ServiceValidationExeption(ValidationContext validationContext, IEnumerable<ValidationResult> validationResults) : this("Validation Error", validationContext, validationResults)
        {

        }
        public ServiceValidationExeption(string message, ValidationContext validationContext, IEnumerable<ValidationResult> validationResults) : base(message)
        {
            this.ValidationContext = validationContext;
            this.ValidationResults = validationResults;
        }

        public ValidationContext ValidationContext { get; private set; }
        public IEnumerable<ValidationResult> ValidationResults { get; private set; }
    }
}
