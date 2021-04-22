using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLRObjectPersisterFxLib
{

    [AttributeUsage(AttributeTargets.Property)]
    public class XMLAttributeAttribute : System.Attribute
    {

    }
    [AttributeUsage(AttributeTargets.Property)]
    public class XMLElementAttribute : System.Attribute
    {

    }


    public enum FormatType
    {
        XML,
        JSON,CSV
    }
    public class PropertyMap
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public Type DataType { get; set; }

        
    }
    public class XMLPropertyMap : PropertyMap
    {
        public bool IsAttribute { get; set; }
        public bool IsElement { get; set; }
    }
   
    public class ObjectPersister
    {
        private System.Type _sourceType;
        public ObjectPersister(System.Type sourceType)
        {
            if (!sourceType.IsClass)
            {
                throw new InvalidOperationException("SourceType must be of Class Type");
            }
            this._sourceType = sourceType;
        }

        public List<System.Reflection.PropertyInfo> GetAllPropertyInfo(object source)
        {
            
            System.Reflection.PropertyInfo[] properties = this._sourceType.GetProperties(
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            return properties.ToList();

          
        }
        public void Persist(object source,FormatType targetFormat)
        {
            
            
             switch (targetFormat)
            {
                case FormatType.XML: this.ObjectToXML(source); break;
                case FormatType.JSON: break;
                case FormatType.CSV: break;
            }
        }
        private void ObjectToXML(object source)
        {
           System.Xml.XmlWriter _writer= System.Xml.XmlWriter.Create(source.GetType().Name + new Guid().ToString());
            _writer.WriteStartDocument();
            _writer.WriteStartElement(source.GetType().Name);
           IEnumerable<XMLPropertyMap> properties= GetXMLPropertyMap(source);
           IEnumerable<XMLPropertyMap> attributeProp= properties.Where(p => p.IsAttribute == true);
           foreach(XMLPropertyMap property in attributeProp)
            {
                _writer.WriteAttributeString(property.Name, property.Value.ToString());
            }
            IEnumerable<XMLPropertyMap> elementProp = properties.Where(p => p.IsElement == true);
            foreach (XMLPropertyMap property in elementProp)
            {
                _writer.WriteElementString(property.Name, property.Value.ToString());
            }
            _writer.WriteEndElement();
            _writer.WriteEndDocument();
            _writer.Flush();
            _writer.Close();

           

        }
        private IEnumerable<XMLPropertyMap> GetXMLPropertyMap(object source)
        {
            List<XMLPropertyMap> _properties = new List<XMLPropertyMap>();
            foreach (System.Reflection.PropertyInfo property in GetAllPropertyInfo(source))
            {
                object[] attributes = property.GetCustomAttributes(true);
                XMLPropertyMap xmlPropertyMap = new XMLPropertyMap();
                xmlPropertyMap.Name = property.Name;
                xmlPropertyMap.DataType = property.PropertyType;
                xmlPropertyMap.Value = property.GetValue(source);

                foreach (object attribute in attributes)
                {
                    if (attribute is XMLAttributeAttribute)
                    {
                        xmlPropertyMap.IsAttribute = true;
                    }
                    if (attribute is XMLElementAttribute)
                    {
                        xmlPropertyMap.IsElement = true;
                    }
                }
                _properties.Add(xmlPropertyMap);
            }
            return _properties;
        }
     
    }
}
