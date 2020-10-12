using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    
    
    public class Employee
    {
        [ValidationLibrary.RequiredAttribute]
        [ValidationLibrary.Length(Length =10)]
        
        public string Name { get; set; }
        [ValidationLibrary.Range(23,70)]
        public int Age { get; set; }

        public string Email { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Employee _emp = new Employee();

            ValidationLibrary.Validator _validator = new ValidationLibrary.Validator();
            _validator.Validate(_emp);
        }
    }
}
