using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OrderMagForms
{
    public class OrderDetails
    {
        public string id{ get; set; }
        public string obname { get; set; }
        public string client {  get; set; }
        public double price { get; set; }
        public OrderDetails(string id, string obname, string client, double price)
        {
            this.id = id;
            this.obname = obname;
            this.client = client;
            this.price = price;
        }
        public void InfChange(string obname = "", string client = "", double price = -1)
        {
            if (obname != "") this.obname = obname;
            if (client != "") this.client = client;
            if (price != -1) this.price = price;
        }
        public override bool Equals(object obj)
        {
            OrderDetails od = obj as OrderDetails;
            if (od == null) { throw new System.ArgumentException(); }
            if (od.id == this.id) { return true; }
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
    }

    public class OrderService
    {
        static OrderService ods = new OrderService();
        public List<OrderDetails> odlist = new List<OrderDetails>();
        public List<OrderDetails> findret = new List<OrderDetails>();        
        public static OrderService ODS
        {
            get
            {
                if (ods == null) { ods = new OrderService(); }
                return ods;
            }
        }
        public void CreateOrder(string id, string obname, string client, double price)
        {
            OrderDetails od = new OrderDetails(id, obname, client, price);
            foreach (OrderDetails or in odlist)
            {
                if (or.Equals(od)) { throw new ODopFail("订单号相同，创建失败！"); }
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
        public void ChangeOrder(string id, string obname = "", string client = "", double price = -1)
        {
            OrderDetails od = new OrderDetails(id, obname, client, price);
            int index = odlist.IndexOf(od);
            if (index == -1) { throw new ODopFail("找不到指定订单，修改失败！"); }
            odlist[index].InfChange(obname, client, price);
        }
        public void OsSort(string way = "")
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
        public void FindOrder(string id = "", string obname = "", string client = "", double price = -1)
        {
            var query = odlist;
            if (!string.IsNullOrEmpty(id))
            {
                query = query.Where(odt => odt.id == id).ToList();
            }
            if (!string.IsNullOrEmpty(obname))
            {
                query = query.Where(odt => odt.obname == obname).ToList();
            }
            if (!string.IsNullOrEmpty(client))
            {
                query = query.Where(odt => odt.client == client).ToList();
            }
            if (price != -1)
            {
                query = query.Where(odt => odt.price == price).ToList();
            }
            query.Sort((od1, od2) => od1.CompareTo(od2, "price"));
            findret = query.ToList();
        }
    }

    public class ODopFail : ApplicationException
    {
        public ODopFail(string message) : base(message) { }
    }
}
