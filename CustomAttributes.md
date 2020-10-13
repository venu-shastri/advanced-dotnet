### C#Cusom Attribute

---

> Reference Type Derived from System.Attribute

> Enable progrmer to write custom metadata  information  about program building blocks (class,assembly ,method , argument , field,constructor)

> Attributes are Great way to add metadata information in a declarative way

> Attribute has targets

```C#
//Attribute 
public class RequiredAttribute:System.Attribute
    {
        //
    }

 public class Employee
    {
        //Extending Name Property Metadata with RequiredAttribute
        [RequiredAttribute]
        public string Name { get; set; }
        public int Age { get; set; }

        public string Email { get; set; }
    }
```

```C#
//before Attruibte use
Property #1 (17000001)
	-------------------------------------------------------
		Prop.Name : Name (17000001)
		Flags     : [none] (00000000)
		CallCnvntn: [PROPERTY]
		hasThis 
		ReturnType: String
		No arguments.
		DefltValue: 
		Setter    : (06000002) set_Name
		Getter    : (06000001) get_Name
		0 Others

//Post Compilation - Attribute use
Property #1 (17000001)
	-------------------------------------------------------
		Prop.Name : Name (17000001)
		Flags     : [none] (00000000)
		CallCnvntn: [PROPERTY]
		hasThis 
		ReturnType: String
		No arguments.
		DefltValue: 
		Setter    : (06000002) set_Name
		Getter    : (06000001) get_Name
		0 Others
		CustomAttribute #1 (0c000004)
		-------------------------------------------------------
			CustomAttribute Type: 0a000011
			CustomAttributeName: ValidationLibrary.RequiredAttribute :: instance void .ctor()
			Length: 4
			Value : 01 00 00 00                                      >                <
			ctor args: ()




```

> When does Attribute Class Instantiated ?
>
> Attribute Class instaiated @ runtime when program reflect target attribute using **target.GetCustomAttributes()**

```C#
System.Reflection.PropertyInfo[] properties = _typeDetails.GetProperties();
                foreach(var property in properties)
                {
                    Console.WriteLine($"{property.Name} && {property.GetValue(target)}");
                    //Query Property Metdata for the existance of Custom Attribute - RequiredAttribute
                   RequiredAttribute[] reqAttributes= property.GetCustomAttributes(typeof(RequiredAttribute), true) as RequiredAttribute[];
                }
```



### Why Linq ?

----

```C#
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
        static IEnumerable<T> Where<T>(IEnumerable<T> source, Func<T, bool> predicate)
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
```

### Threading

----

> Dotnet Framework Support for Multithreading - Logical Threading  Model
>
> System.Threading namespace.

##### Thread From Programmer Perspective

---

- DataStructure - Describes - Execution Path 
  - Id
  - Priority
  - StartAddress (Entry and Exit Points)
  - State
  - Culture
  - Apartment
  - Mode - Background/Foreground
  - Name
  - Stack
  - Behaviors : sleep,pause,resume,wait ,stop,cancel,abort
- Logical/Managed  Thread  : - System.Threading.Thread (DataStructure)
  - ManagedThreadId 
  - Name
  - Culture
  - Apratment
    - MTA (Default)
  - Start Address - Depends on Delegate 
    - ThreadStart 
      - void()
    - ParameterizedThreadStart
      - void(object)
- CLR Execution
  - Each Dotnet Simple application (Console App) - execution begin  with two threads
    - Main Thread 
      - static void Main()
      - ManagedThreadId - **1**
    - Finalizer Thread 
      - ManagerThreadId - **2**
- How to spawn New Thread 

```C#
static void Main(string[] args)
        {

 System.Threading.Thread _threadOne = new System.Threading.Thread(
                new System.Threading.ThreadStart(Task)) { Name = "Thread1" };
            _threadOne.Start();
           

        }

        static void Task()
        {
			///.....
        }
    }
```



### When does CLR Shutdown

---

> When All the foreground threads complete there execution ; 
>
> Foreground threads can preempt process termination . When last foreground thread return from execution then all the background threads are aborted



#### Thread Pool Threads

---

 Provides a Pool of background mode threads that can be used to execute tasks such as Asynchronous I/o , Timers , Wait on behalf of another threads (Watch Dogs) 

> How to defines address of task for ThreadPool Thread

```
1. define Task / Job 
2.Signature of function  should  match with built in delegate WaitCallBack
3.Queeue the request 
```

```C#
static void NewBackGroundTask(object param){
        
           
           ///....
}

 WaitCallback _backGroundTaskPointer = new WaitCallback(NewBackGroundTask);
 ThreadPool.QueueUserWorkItem(_backGroundTaskPointer);
```

