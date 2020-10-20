using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TargetApp
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
    public class PatientInfo : Person //, ISerializable, IXmlSerializable
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

    public class PatientInfoBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            if (typeName.Contains("PatientInfo"))
            {
                return typeof(PatientInfo);
            }
            if (typeName.Contains("AddressInfo"))
            {
                return typeof(AddressInfo);
            }
            return null;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            PatientInfo _patinet = DeepBinaryDeSerailzation() as PatientInfo;
        }
        static object DeepBinaryDeSerailzation()
        {
            //1.Formatter { Binary ,SOAP }
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter _binFormatter = 
                new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            _binFormatter.Binder = new PatientInfoBinder();
            System.IO.StreamReader rd = new System.IO.StreamReader(@"D:\knowledge\Advancd-Dotnet\advanced-dotnet\ValidationLibrary\ObjectPersistance\PatientInfo.bin");
           
            object target = _binFormatter.Deserialize(rd.BaseStream);
            return target;

        }
    }
}
