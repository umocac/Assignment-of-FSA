using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderMag;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OrderMag.Program;

namespace Tests
{
    [TestClass()]
    public class OrderServiceTests
    {
        Program.OrderService os = new Program.OrderService();
        [TestMethod()]
        public void CreateOrderTest()
        {
            os.CreateOrder("1234", "abcd", "www", 66);
            // Assert
            var order = os.FindOrder(id: "1234");
            Assert.IsNotNull(order);
            Assert.AreEqual(1, order.Count);
            Assert.AreEqual("1234", order.First().Getid());
        }

        [TestMethod()]
        public void DeleteOrderTest()
        {
            os.CreateOrder("1234", "abcd", "www", 66);
            os.DeleteOrder("1234");
            Assert.ThrowsException<Program.ODopFail>(() => os.DeleteOrder("1234"));
        }

        [TestMethod()]
        public void ChangeOrderTest()
        {
            os.CreateOrder("1234", "abcd", "www", 66);
            os.ChangeOrder("1234", "bxxl", "qaq", 77);
            List<Program.OrderDetails> ods = os.FindOrder("1234");
            Assert.AreEqual("bxxl", ods.First().Getobname());
            Assert.AreEqual("qaq", ods.First().Getclient());
            Assert.AreEqual(77,ods.First().Getprice());
        }

        [TestMethod()]
        public void FindOrderTest()
        {
            os.CreateOrder("1234", "Product A", "Client X", 100);
            os.CreateOrder("1235", "Product B", "Client Y", 200);
            os.CreateOrder("1236", "Product C", "Client X", 300);

            var orders = os.FindOrder(client: "Client X");

            Assert.AreEqual(2, orders.Count);
            Assert.IsTrue(orders.All(o => o.Getclient() == "Client X"));
        }
    }
}