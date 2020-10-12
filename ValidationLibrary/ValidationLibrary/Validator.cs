using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ValidationLibrary
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple =true)]
    public abstract class ValidatorAttribute:System.Attribute
    {
        public abstract bool Validate(object value);
        
    }

   
    public class RequiredAttribute:ValidatorAttribute
    {
        //
        public override bool Validate(object value)
        {

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
        public bool Validate(object target)
        {
            bool isValid = false;

            //Discover Object Type Details (Reflection)
            //Reflection :- API to query assembly metadata @runtime

           System.Type _typeDetails= target.GetType();
            if (_typeDetails.IsClass)
            {
                System.Reflection.PropertyInfo[] properties = _typeDetails.GetProperties();
                foreach(var property in properties)
                {
                    Console.WriteLine($"{property.Name} && {property.GetValue(target)}");
                    //Query Property Metdata for the existance of Custom Attribute - RequiredAttribute
                    ValidatorAttribute[] validatorAttributes= property.GetCustomAttributes(typeof(ValidatorAttribute), true) as ValidatorAttribute[];
                    if (validatorAttributes != null && validatorAttributes.Length > 0)
                    {
                        
                        foreach(ValidatorAttribute validator in validatorAttributes)
                        {
                            if(validator.Validate(property.GetValue(target)))
                            {

                            }
                        }
                        
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("target is not an instance of class");
            }
            return isValid;

        }
    }
}
