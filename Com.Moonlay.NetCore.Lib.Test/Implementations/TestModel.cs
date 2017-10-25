using Com.Moonlay.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Moonlay.NetCore.Lib.Test.Implementations
{
    public class TestModel : StandardEntity, IValidatableObject
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(this.Code))
                yield return new ValidationResult("Code is required", new List<string> { "Code" });

            if (string.IsNullOrWhiteSpace(this.Name))
                yield return new ValidationResult("Name is required", new List<string> { "Name" });

            if (!string.IsNullOrWhiteSpace(this.Description) && this.Description.Length > 255)
                yield return new ValidationResult("Exceeded length", new List<string> { "Description" });
        }
    }
}
