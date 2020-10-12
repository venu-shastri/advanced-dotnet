using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    
    
    public class Employee
    {
        
        [ValidationLibrary.RequiredAttribute(ErrorMessage ="Name Requires Value")]
        [ValidationLibrary.Length(Length =10,ErrorMessage ="Name Should Exceed More than 10 Chacters ")]
        
        public string Name { get; set; }
        [ValidationLibrary.Range(23,70,ErrorMessage ="Age Value Must be with in the range 23-70")]
        public int Age { get; set; }

        public string Email { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Employee _emp = new Employee() { Name = "Tom", Age = 10 };

            ValidationLibrary.Validator _validator = new ValidationLibrary.Validator();
            List<ValidationLibrary.ValidationResult> _summury = null;
            if(!_validator.Validate(_emp,out _summury))
            {
                foreach(var vr in _summury)
                {
                    Console.WriteLine($"{vr.PropertyName} && {vr.ErrorContent}");
                }

            }

            XMLFormatterLib.XMLFormatter _formatter = new XMLFormatterLib.XMLFormatter("D:/test.xml");
            _formatter.WriteObject(_emp);
        }
    }
}
