using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLFormatterLib
{
    /*
     * Employee _emp = new Employee() { Name = "Tom", Age = 10 };

     * XMLFormatterLib.XMLFormatter _formatter = new XMLFormatterLib.XMLFormatter();
            _formatter.WriteObject(_emp);

    //Default All the properties transformed as an element
        <Employee>
                <Name>Tom</Name>
                <Age>30</Age>
        </Employee>

        or
       <Employee Name="Tom">
          <Age>30</Age>
       </Employee>

        or
        <Employee Name="Tom" Age=10 ></Employee>
       */
    public class XMLFormatter
    {
        string _path;
        public XMLFormatter(string path)
        {
            this._path = path;
        }
        public void WriteObject(object target)
        {
            //Code 
        }
    }
}
