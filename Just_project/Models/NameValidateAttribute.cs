
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Just_project.Models
{
    internal class NameValidateAttribute : Attribute, IModelValidator
    {
        public string[] NotAllowed { get; set; }
        public string ErrorMessage { get; set; }
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            if (NotAllowed.Contains(context.Model as string))
                return new List<ModelValidationResult> {
                new ModelValidationResult("", ErrorMessage)
            };
            else
                return Enumerable.Empty<ModelValidationResult>();
        }
    }
}