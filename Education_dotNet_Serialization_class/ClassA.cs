using System;
using System.Runtime.Serialization;


namespace Education_dotNet_Serialization_class
{

    [Serializable]
    //public class Order: IDeserializationCallback
    public class Order
    {
        public int Count;

        public decimal Price;
        [NonSerialized]
        public decimal TotalPrice;


        public Order() { }
        public Order(int count, decimal price)
        {
            this.Count = count;
            this.Price = price;
            this.TotalPrice = count * price;
        }

        /*public void OnDeserialization(object sender) {
            TotalPrice = Count * Price;
        }*/

        [OnDeserialized]
        private void onDeserialized(StreamingContext context) {
            TotalPrice = Count * Price;
        }
    }
}