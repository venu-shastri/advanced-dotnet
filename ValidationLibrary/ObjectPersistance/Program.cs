
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ObjectPersistance
{
    [Serializable]
    public class Person
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string ContactNumber { get; set; }

        

    }
    [Serializable]
    public class AddressInfo
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public string PinCode { get; set; }


    }
    [Serializable]//Deep Serialization 
    public  class PatientInfo:Person
    {
        public string MRN { get; set; }
        private string secretPassCode;
        public AddressInfo Address { get; set; }

        public PatientInfo()
        {
            this.secretPassCode = new Random().Next(1000, 5000).ToString();
        }
        public override string ToString()
        {
            StringBuilder _builder = new StringBuilder();
            _builder.Append(this.secretPassCode)
                    .Append(",")
                    .Append(this.MRN)
                    .Append(",")
                    .Append(this.Name)
                    .Append(",")
                    .Append(this.ContactNumber);
            return _builder.ToString();
            
         }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //PatientInfo _patientInstance = new PatientInfo()
            //{
            //    MRN = "MRN001",
            //    Name = "Tom",
            //    Age = 30,
            //    ContactNumber = "1234567890",
            //    Address = new AddressInfo { City = "BLR", State = "KA", PinCode = "560077", Street = "BLR" }
            //};

            //DeepBinarySerailzation(_patientInstance);

           object target= DeepBinaryDeSerailzation();
           Console.WriteLine( target.ToString());
        }

        static void DeepBinarySerailzation(object target)
        {
            //1.Formatter { Binary ,SOAP }
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter _binFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.StreamWriter wr = new System.IO.StreamWriter($"../../{target.GetType().Name}.bin");

            _binFormatter.Serialize(wr.BaseStream, target);
            wr.Flush();
            wr.Close();
        }
        static object DeepBinaryDeSerailzation()
        {
            //1.Formatter { Binary ,SOAP }
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter _binFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.StreamReader rd= new System.IO.StreamReader($"../../PatientInfo.bin");

           object target= _binFormatter.Deserialize(rd.BaseStream);
            return target;
            
        }
    }
}
