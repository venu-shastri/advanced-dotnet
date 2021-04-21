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
    public struct PropertyMap
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public Type DataType { get; set; }
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

        public List<PropertyMap> GetAllPropertyInfo(object source)
        {
            List<PropertyMap> _propertyMap = new List<PropertyMap>();
            System.Reflection.PropertyInfo[] properties = this._sourceType.GetProperties(
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
          if(properties.Length > 0)
            {
                foreach(System.Reflection.PropertyInfo property in properties)
                {
                   object[] attributes= property.GetCustomAttributes(true);
                    foreach(object attribute in attributes)
                    {
                        if (attribute is XMLElementAttribute) { }
                        if(attribute is XMLAttributeAttribute) { }
                    }
                    PropertyMap _property = new PropertyMap
                    {
                        Name = property.Name,
                        DataType = property.PropertyType,
                        Value = property.GetValue(source)
                    };
                    _propertyMap.Add(_property);

                }
            }
            return _propertyMap;
        }
        public void Persist(object source,FormatType targetFormat)
        {
            //Use Reflectionn to list public properties 
          List<PropertyMap>_propertyMap=  GetAllPropertyInfo(source);
            //xml,json,.......
            switch (targetFormat)
            {
                case FormatType.XML: this.ObjectToXML(source, _propertyMap); break;
                case FormatType.JSON: break;
                case FormatType.CSV: break;
            }
        }
        private void ObjectToXML(object source,List<PropertyMap> propertyList)
        {
           System.Xml.XmlWriter _writer= System.Xml.XmlWriter.Create(source.GetType().Name + new Guid().ToString());
            _writer.WriteStartDocument();
            _writer.WriteStartElement(source.GetType().Name);
            foreach(PropertyMap property in propertyList)
            {
                _writer.WriteElementString(property.Name, property.Value.ToString());
            }
            _writer.WriteEndElement();
            _writer.WriteEndDocument();
            _writer.Flush();
            _writer.Close();

           

        }
     
    }
}
