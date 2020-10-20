
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

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
    public class PatientInfo : Person, ISerializable, IXmlSerializable
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

        #region Custom Deep Serialization 
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("timestamp", DateTime.Now.ToString());
            info.AddValue(nameof(this.secretPassCode), this.secretPassCode);
            info.AddValue(nameof(this.MRN), this.MRN);
        }
        //Constructor invoked from Deserializer 
        public PatientInfo(SerializationInfo info, StreamingContext context)
        {
            string timeStamp = info.GetString("timestamp");
            this.secretPassCode = info.GetString(nameof(this.secretPassCode));
            this.MRN = info.GetString(nameof(this.MRN));

        }
        #endregion
        #region Custom Shallow Serialization
        public XmlSchema GetSchema()
        {
            //throw new NotImplementedException();
            return null;
        }

        //DeSerailzation
        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        //Seralization
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString(nameof(this.MRN), this.MRN);
            writer.WriteAttributeString(nameof(this.Name), this.Name);
            writer.WriteElementString("timestamp", System.DateTime.Now.ToLongTimeString());
            

          
        }
        #endregion Custom Shallow Serialization

    }

    class Program
    {
        static void Main(string[] args)
        {
            PatientInfo _patientInstance = new PatientInfo()
            {
                MRN = "MRN001",
                Name = "Tom",
                Age = 30,
                ContactNumber = "1234567890",
                Address = new AddressInfo { City = "BLR", State = "KA", PinCode = "560077", Street = "BLR" }
            };

           // DeepBinarySerailzation(_patientInstance);

            // _patientInstance = null;
            // GC.Collect();

            // object target= DeepBinaryDeSerailzation();
           // Console.WriteLine( target.ToString());
            ShallowSerialization(_patientInstance);
        }

        static void DeepBinarySerailzation(object target)
        {
            //1.Formatter { Binary ,SOAP }
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter _binFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.StreamWriter wr = new System.IO.StreamWriter($"../../{target.GetType().Name}custom.bin");

            _binFormatter.Serialize(wr.BaseStream, target);
            wr.Flush();
            wr.Close();
        }
        static object DeepBinaryDeSerailzation()
        {
            //1.Formatter { Binary ,SOAP }
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter _binFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.StreamReader rd= new System.IO.StreamReader($"../../PatientInfocustom.bin");

           object target= _binFormatter.Deserialize(rd.BaseStream);
            return target;
            
        }
        static void ShallowSerialization(object target)
        {
            System.Xml.Serialization.XmlSerializer _serializer = new System.Xml.Serialization.XmlSerializer(target.GetType());
            System.IO.StreamWriter wr = new System.IO.StreamWriter($"../../{target.GetType().Name}.xml");
            _serializer.Serialize(wr.BaseStream, target);
            wr.Flush();
            wr.Close();
        }
    }
}
