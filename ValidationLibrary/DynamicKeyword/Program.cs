using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicKeyword
{
    public class StateBag:DynamicObject
    {
        Dictionary<string, object> _propertyStore = new Dictionary<string, object>();
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;
            if (_propertyStore.ContainsKey(binder.Name))
            {
                result = _propertyStore[binder.Name];
                    
                return true;
            }
            return false;
        }
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _propertyStore[binder.Name] = value;
            return true;
        }
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = null;
            if (binder.Name == "ClearAll")
            {
                _propertyStore.Clear();
                return true;
            }
            return false;

        }

    }
    public class A:System.Dynamic.DynamicObject
    {

        public void Task() { }
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = null;
            switch (binder.Name)
            {
                case "Test1":break;
            
            }
            return true;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //            Client(new A());

            dynamic _customer = new StateBag();
            _customer.Id = "Cust001";
            _customer.Name = "Tom";
            _customer.Age = 30;
            _customer.Address = "BLR";

            Console.WriteLine($"{_customer.Id} , {_customer.Name},{_customer.Age}, {_customer.Address}");
            _customer.Pan = "ADYPV0026G";
            Console.WriteLine($"{_customer.Id} , {_customer.Name},{_customer.Age}, {_customer.Address},{_customer.Pan}");
            _customer.ClearAll();




               
            
            
        }
        static void Client(dynamic obj)
        {
            //Reflection Code 
            //obj.GetType().GetMethod("Task").Invoke(obj, new object[] { });
            obj.Task1(10,20); // MSIL - RuntimeBinder Code 
        }
    }
}
