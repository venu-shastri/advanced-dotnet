using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReuseProbs
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> resultList = new List<string>();
            string[] names = { "Bnil", "Venu", "Sudeep", "Anupama" };
            for(int i = 0; i < names.Length; i++)
            {
                if(names[i].Length > 4)
                {
                    resultList.Add(names[i]);
                }
            }

            foreach (var item in resultList)
            {
                Console.WriteLine(item);

            }

        }
    }
}
