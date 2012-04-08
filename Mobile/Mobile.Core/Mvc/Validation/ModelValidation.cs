using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Mobile.Core.Mvc.Validation
{
    public class ModelValidation
    {
        public static IEnumerable<Error> Validate(object objValidate , ControllerContext controllerContext)
        {
            
            ModelMetadata metadata = ModelMetadataProviders.Current.GetMetadataForType(() => objValidate, objValidate.GetType());
            foreach (ModelMetadata propertyMetadata in metadata.Properties)
            {
                foreach (ModelValidator propertyValidator in propertyMetadata.GetValidators(controllerContext))
                {
                    foreach (ModelValidationResult propertyResult in propertyValidator.Validate(metadata.Model))
                    {
                        yield return new Error
                        {
                            MemberName = propertyMetadata.PropertyName,// DefaultModelBinder.CreateSubPropertyName(propertyMetadata.PropertyName, propertyResult.MemberName),
                            Message = propertyResult.Message
                        };
                    }
                }
            }
            
        }

        
    }
}
