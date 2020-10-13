using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ReuseProbs
{

    class Item
    {

    }
    class ItemList:IEnumerable<Item>
    {
        Item[] items = new Item[10];

        public IEnumerator<Item> GetEnumerator()
        {
            return new ItemIterator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private class ItemIterator : IEnumerator<Item>
        {
            public Item Current => throw new NotImplementedException();

            object IEnumerator.Current => throw new NotImplementedException();

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public bool MoveNext()
            {
                throw new NotImplementedException();
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }
    }
    

    
    class Program
    {
        static void Main(string[] args)
        {
          
            ItemList _itemList = new ItemList();
            foreach(Item item in _itemList)
            {

            }

            string[] names = { "Bnil", "Venu", "Sudeep", "Anupama" };

            List<string> resultList = Query_v1(names);
            resultList = Query_v2(names, new Func<string, bool>(CheckStringLengthGreaterThan4));
            resultList = Query_v3(names, (item) => { return item.Contains("a"); });
            List<int> intList = Query_v3<int>(new int[] { }, (item) => { return item % 2 == 0; });
            foreach (var item in resultList)
            {
                Console.WriteLine(item);

            }

        }

        static bool CheckStringLengthGreaterThan4(string item)
        {
            return item.Length > 4;
        }
        static List<string> Query_v1(string[] source)
        {

            List<string> resultList = new List<string>();
           
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i].Length > 4)
                {
                    resultList.Add(source[i]);
                }
            }
            return resultList;

        }
        static List<string> Query_v2(string[] source,Func<string,bool> predicate)
        {

            List<string> resultList = new List<string>();

            for (int i = 0; i < source.Length; i++)
            {
                if (predicate.Invoke(source[i]))
                {
                    resultList.Add(source[i]);
                }
            }
            return resultList;

        }
        static List<T> Query_v3<T>(T[] source, Func<T, bool> predicate)
        {

            List<T> resultList = new List<T>();

            for (int i = 0; i < source.Length; i++)
            {
                if (predicate.Invoke(source[i]))
                {
                    resultList.Add(source[i]);
                }
            }
            return resultList;


        }
        static IEnumerable<T> Query_v4<T>(IEnumerable<T> source, Func<T, bool> predicate)
        {

            List<T> resultList = new List<T>();

            //IEnumerator<T> iterator=source.GetEnumerator();
            //try
            //{
            //    while (iterator.MoveNext())
            //    {
            //        T item = iterator.Current;
            //        if (predicate.Invoke(item))
            //        {
            //            resultList.Add(item);
            //        }
            //    }
            //}
            //finally
            //{
            //    if(iterator is IDisposable)
            //    {
            //        iterator.Dispose();
            //    }
            //}

            foreach(T item in source)
            {
                if (predicate.Invoke(item))
                {
                    resultList.Add(item);
                }
            }

                

            return resultList;

        }
    }
}
