
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Part4
    {
        public static void Run()
        {
            IOrder baseOrder = new BaseOrder("an box of sweets");
            baseOrder.GetDescription();
            baseOrder.GetPrice();
            baseOrder = new FastShippingAddition(baseOrder);
            baseOrder.GetDescription();
            baseOrder.GetPrice();
            baseOrder = new PresentBoxAddition(baseOrder);
            baseOrder.GetDescription();
            baseOrder.GetPrice();
        }
    }
    public interface IOrder
    {
        public float price { get; set; }
        public string Description { get; set; }
        public void GetPrice();
        public void GetDescription();
    }
    public class BaseOrder (string description) : IOrder
    {
        public float price { get; set; } = 100;
        public string Description { get; set; } = description;
        public void GetDescription()
        {
            Console.WriteLine(description);
        }

        public void GetPrice()
        {
            Console.WriteLine("Cost: "+price);
        }
    }
    public abstract class OrderAddition(IOrder order, float additionCost, string additionDescription) : IOrder
    {

        protected IOrder Order = order;
        public float price { get; set; } = order.price+additionCost;
        public string Description { get; set; } = order.Description + additionDescription;

        public void GetDescription()
        {
            Console.WriteLine(Description);
        }
        public void GetPrice()
        {
            Console.WriteLine("Cost: " + price);
        }
    }
    public class FastShippingAddition : OrderAddition
    {
        public FastShippingAddition(IOrder order) : base(order, 30,", With fast shipping")
        {
        }
    }
    public class PresentBoxAddition : OrderAddition
    {
        public PresentBoxAddition(IOrder order) : base(order, 20,", With present-styled box")
        {
        }
    }
}
