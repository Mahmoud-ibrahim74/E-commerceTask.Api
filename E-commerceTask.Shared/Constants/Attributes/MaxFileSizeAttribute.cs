using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace E_commerceTask.Shared.Constants.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly long _maxFileSize;

        public MaxFileSizeAttribute(long maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var fileSizeInMB = file.Length / SD.FileSettings.MB;
                if (fileSizeInMB > _maxFileSize)
                {
                    return new ValidationResult($"Maximum allowed file size is {_maxFileSize} MB.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
