using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ValidationLibrary
{

    [AttributeUsage(AttributeTargets.Property,AllowMultiple =false)]
    public class RequiredAttribute:System.Attribute
    {
        //
        public bool Validate(object value)
        {

            string data = value as string;
            return !string.IsNullOrEmpty(data);
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
                   RequiredAttribute[] reqAttributes= property.GetCustomAttributes(typeof(RequiredAttribute), true) as RequiredAttribute[];
                    if ( reqAttributes!=null && reqAttributes.Length > 0)
                    {
                        RequiredAttribute _instance = reqAttributes[0];
                        if (_instance.Validate(property.GetValue(target)))
                        {
                            isValid = true;
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
