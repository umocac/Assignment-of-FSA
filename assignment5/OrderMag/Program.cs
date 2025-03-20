using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OrderMag
{
    public class Program
    {
        static void Main(string[] args)
        {
            OrderService service = new OrderService();
            try
            {
                service.CreateOrder("1234", "aaaa", "www.", 13);
                service.CreateOrder("1235", "ddd", "www.", 19);
                service.CreateOrder("1236", "ccc", "wqw.", 13);
                service.CreateOrder("1237", "ddd", "www.", 11);
                service.CreateOrder("1238", "ddd", "www.", 14);
                service.CreateOrder("1222", "ddd", "www.", 15);
                service.CreateOrder("1211", "ddd", "www.", 13);
                service.CreateOrder("1111", "eee", "ewc", 123);

                service.ChangeOrder("1236", price: 17);
                List<OrderDetails> od1 = service.FindOrder(obname: "ddd", client: "www.");
                Show(od1);
                service.DeleteOrder("1211");

                service.ShowOrder("id");
            }
            catch (ODopFail ex)
            {
                Console.WriteLine(ex.Message);
            }

            //service.ShowOrder("id");//默认按订单号显示
            //service.ShowOrder("client");//按客户名称显示
            //service.ShowOrder("obname");//按商品名称显示
            service.ShowOrder("price");//按商品价格显示


            Console.ReadKey();
        }
        public static void Show(List<OrderDetails> od)
        {
            Console.WriteLine("------查询结果------");
            foreach (OrderDetails od2 in od)
            {
                Console.WriteLine(od2);
            }
        }

        public abstract class Order
        {
            protected string id {  get; set; }
            protected string obname {  get; set; }
            protected string client {  get; set; }
            protected int price { get; set; }
        }

        public class OrderDetails : Order
        {
            public OrderDetails(string id, string obname, string client, int price)
            {
                this.id = id;
                this.obname = obname;   
                this.client = client;
                this.price = price;
            }
            public void InfChange(string obname = "", string client = "", int price = -1)
            {
                if (obname != "") this.obname = obname;
                if (client != "") this.client = client;
                if (price != -1) this.price = price;
            }
            public override bool Equals(object obj)
            {
                OrderDetails od = obj as OrderDetails;
                if( od == null ) { throw new System.ArgumentException(); }
                if (od.id == this.id) { return  true; }
                return false;
            }
            public override string ToString()
            {
                return $"订单号 {id,-18}, 商品名称 {obname,-15}, 客户 {client,-8}, 价格 {price,-8};";
            }
            public int CompareTo(object obj, string way) 
            {
                OrderDetails order = obj as OrderDetails;
                switch (way)
                {
                    case "obname":
                        return this.obname.CompareTo(order.obname);
                    case "client":
                        return this.client.CompareTo(order.client);
                    case "price":
                        return this.price.CompareTo(order.price);
                    default:
                        return this.id.CompareTo(order.id);
                }
            }
            public string Getid() {  return this.id; }
            public string Getobname() {  return this.obname; }
            public string Getclient() {  return this.client; }
            public int Getprice() { return this.price; }   
        }

        public class OrderService
        {
            List<OrderDetails> odlist = new List<OrderDetails>();
            public void CreateOrder(string id, string obname, string client, int price)
            {
                OrderDetails od = new OrderDetails(id, obname, client, price);
                foreach (OrderDetails or in odlist)
                {
                    if (or.Equals(od)){ throw new ODopFail("订单号相同，创建失败！"); }
                }
                odlist.Add(od);
            }
            public void DeleteOrder(string id)
            {
                OrderDetails od = new OrderDetails(id, "", "", 0);
                int index = odlist.IndexOf(od);
                if (index == -1) { throw new ODopFail("找不到指定订单，删除失败！"); }
                odlist.RemoveAt(index);
            }
            public void ChangeOrder(string id, string obname = "", string client = "", int price = -1)
            {
                OrderDetails od = new OrderDetails(id, obname, client, price);
                int index = odlist.IndexOf(od);
                if (index == -1) { throw new ODopFail("找不到指定订单，修改失败！"); }
                odlist[index].InfChange(obname, client, price);
            }
            void OsSort(string way = "")
            {
                odlist.Sort((od1, od2) => od1.CompareTo(od2, way));
            }
            public void ShowOrder(string way)
            {
                Console.WriteLine(new String('-', 82));
                OsSort(way); 
                foreach (OrderDetails od in odlist)
                {
                    Console.WriteLine(od.ToString());
                }
            }
            public List<OrderDetails> FindOrder(string id = "", string obname = "", string client = "", int price = -1)
            {
                var query = odlist;
                if (!string.IsNullOrEmpty(id))
                {
                    query = query.Where(odt => odt.Getid() == id).ToList();
                }
                if (!string.IsNullOrEmpty(obname))
                {
                    query = query.Where(odt => odt.Getobname() == obname).ToList();
                }
                if (!string.IsNullOrEmpty(client))
                {
                    query = query.Where(odt => odt.Getclient() == client).ToList();
                }
                if (price != -1)
                {
                    query = query.Where(odt => odt.Getprice() == price).ToList();
                }
                query.Sort((od1, od2) => od1.CompareTo(od2, "price"));
                return query.ToList();
            }
        }

        public class ODopFail : ApplicationException
        {
            public ODopFail(string message) : base(message) { }
        }
    }
}
