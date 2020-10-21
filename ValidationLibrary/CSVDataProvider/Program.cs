
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVDataProvider
{
   
    class Program
    {
        static void Main(string[] args)
        {
            CSVLinqDataProvider _provider = new CSVLinqDataProvider();
           List<CSVLine> _patientList= _provider.GetData("..//..//patients.csv").ToList();

            var result = from dynamic line  in _patientList
                         where Int32.Parse( line.Age) > 35
                         select new { Mrn = line.Mrn, Name = line.Name };
            foreach(var item in result)
            {
                Console.WriteLine($"{item.Mrn} , {item.Name}");
            }

        }
    }

    public class CSVLine : System.Dynamic.DynamicObject
    {
        List<string> _headerContent;
        List<string> _lineContent;
        public CSVLine(string header, string line)
        {
            _headerContent = header.Split(',').ToList();
            _lineContent = line.Split(',').ToList();

        }
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;

            int position=_headerContent.IndexOf(binder.Name);
            if (position < _lineContent.Count)
            {
                object value = _lineContent[position];
                result = value;
                return true;
            }

            return false;
        }


    }
    public class CSVLinqDataProvider
    {
        public  IEnumerable<CSVLine> GetData(string filePath)
        {
            List<CSVLine> _lineList = new List<CSVLine>();
            string headerLine = default(string);
            if (ValidatePath(filePath))
            {
                bool isHeaderPorcessed = false;
                System.IO.StreamReader rd = new System.IO.StreamReader(filePath);
                do
                {
                    if (!isHeaderPorcessed)
                    {
                        headerLine=rd.ReadLine();
                        isHeaderPorcessed = true;
                        
                    }
                    else {

                        CSVLine _lineObj = new CSVLine(headerLine, rd.ReadLine());
                        _lineList.Add(_lineObj);
                    }
                }
                while (!rd.EndOfStream) ;
                
            }
            return _lineList;
        }

        bool ValidatePath(string filePath) { return true; }

        

        

    }
}
