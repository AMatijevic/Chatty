using Chatty.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Chatty.Domain.Common
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; protected set; }

        public string CreatedBy { get; protected set; }

        public DateTime Created { get; protected set; }

        public string LastModifiedBy { get; protected set; }

        public DateTime? LastModified { get; protected set; }

        internal virtual void Validate()
        {
            var validationResults = GetValidateResults().Where(r => r != ValidationResult.Success).ToList();
            if (validationResults.Any())
            {
                throw new DomainModelException(validationResults.Select(v => v.ErrorMessage).ToArray());
            }
        }

        public IEnumerable<ValidationResult> GetValidateResults()
        {
            var validationContext = new ValidationContext(this);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(this, validationContext, validationResults, true);

            return validationResults;
        }
    }
}
