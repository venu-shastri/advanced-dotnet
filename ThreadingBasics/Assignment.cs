using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadingBasics
{
    public class SearchService
    {
        Dictionary<string, string> _bigStore = new Dictionary<string, string>();
        public SearchService()
        {
            _bigStore.Add("key1", "KeyOne");
            _bigStore.Add("key2", "KeyTwo");
            _bigStore.Add("key3", "KeyThree");
        }
        public string Search(string key)
        {
            System.Threading.Thread.Sleep(2000);
            if (_bigStore.ContainsKey(key))
            {
                return _bigStore[key];
            }
            return "Not Found";
        }

    }
    
}
