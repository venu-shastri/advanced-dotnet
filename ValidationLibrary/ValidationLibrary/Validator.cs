using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ValidationLibrary
{
    public class ValidationResult
    {
        public string PropertyName { get; set; }
        public string ErrorContent { get; set; }

    }
    [AttributeUsage(AttributeTargets.Property,AllowMultiple =true)]
    public abstract class ValidatorAttribute:System.Attribute
    {
        public abstract bool Validate(object value);
        public string ErrorMessage { get; set; }
        
    }

   
    public class RequiredAttribute:ValidatorAttribute
    {
        //
        public override bool Validate(object value)
        {
            Thread.Sleep(2000);
            string data = value as string;
            return !string.IsNullOrEmpty(data);
        }
    }


   
    public class RangeAttribute : ValidatorAttribute
    {
        int max;
        int min;
        public RangeAttribute(int min,int max)
        {
            this.max = max;
            this.min = min;
        }
        public override bool Validate(object value)
        {
            Thread.Sleep(2000);
            int _value;
            if(Int32.TryParse(value as string,out _value))
            {
                if(_value > this.min && _value < this.max)
                {
                    return true;
                }
            }
            return false;
        }
    }


    public class LengthAttribute:ValidatorAttribute
    {
        public int Length { get; set; }
        public override bool Validate(object value)
        {
            Thread.Sleep(2000);
            string data = value as string;
            if (data != null)
            {
                return data.Length <= Length;
            }
            return false;

        }
    }
    public class Validator
    {
        public bool Validate(object target,out List<ValidationResult> validationSummary)
        {
            bool isValid = false;
            validationSummary = new List<ValidationResult>();
            //Discover Object Type Details (Reflection)
            //Reflection :- API to query assembly metadata @runtime

           System.Type _typeDetails= target.GetType();
            if (_typeDetails.IsClass)
            {
                System.Reflection.PropertyInfo[] properties = _typeDetails.GetProperties();
                foreach(var property in properties)
                {
                  
                    //Query Property Metdata for the existance of Custom Attribute - RequiredAttribute
                    ValidatorAttribute[] validatorAttributes= property.GetCustomAttributes(typeof(ValidatorAttribute), true) as ValidatorAttribute[];
                    if (validatorAttributes != null && validatorAttributes.Length > 0)
                    {
                        
                        foreach(ValidatorAttribute validator in validatorAttributes)
                        {
                            if(!validator.Validate(property.GetValue(target)))
                            {
                                if (validationSummary == null)
                                {
                                    validationSummary = new List<ValidationResult>();
                                }
                                ValidationResult _vr = new ValidationResult() {
                                    PropertyName = property.Name, 
                                    ErrorContent = validator.ErrorMessage };
                                validationSummary.Add(_vr);
                                

                            }
                        }
                        
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("target is not an instance of class");
            }
            if(validationSummary.Count ==0)
            {
                isValid = true;
               
            }
            return isValid;

        }
    }
}
