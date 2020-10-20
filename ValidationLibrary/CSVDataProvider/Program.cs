
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVDataProvider
{
    public class Patient
    {

        public string MRN { get; set; }
        public string Name { get; set; }
        public int Age{ get; set; }
        public string ContactNumber { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            CSVLinqDataProvider _provider = new CSVLinqDataProvider();
           IEnumerable<Patient> _patientList= _provider.GetData("..//..//patients.csv");

            var result = from p in _patientList
                         where p.Age > 35
                         select new { Mrn = p.MRN, Name = p.Name };
            foreach(var item in result)
            {
                Console.WriteLine($"{item.Mrn} , {item.Name}");
            }

        }
    }
}
