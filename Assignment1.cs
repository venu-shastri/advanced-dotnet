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
    
    class Program{
    static void Main()
        {
            string[] keys = { "key1","No key", "key2", "key3" };
            
            for(int i = 0; i < keys.Length; i++)
            {
                SearchButton_Click(keys[i]);
            }
            Console.WriteLine("End OF Main");
            
        }
        
        static void SearchButton_Click(string key)
        {
            SearchService _serviceRef = new SearchService();
           string result= _serviceRef.Search(key);
            Console.WriteLine(result);


        }
 }
