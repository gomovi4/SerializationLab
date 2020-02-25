using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;



namespace Education_dotNet_Serialization_class
{
    class Serialization   
    {
       
        static void Main(String[] args) {

            Order order = new Order(4,5.5m);

            //BinaryFormatter

            /*FileStream fsc = new FileStream("d:\\testSR1.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            BinaryFormatter binaryFormatter = new BinaryFormatter();            
            binaryFormatter.Serialize(fsc, order);
            fsc.Close();

            FileStream fsr = File.OpenRead("d:\\testSR1.bin");
            BinaryFormatter binaryFormatter1 = new BinaryFormatter();
            Order neworder = (Order)binaryFormatter1.Deserialize(fsr);
            fsr.Close();

           Console.WriteLine("Count: {0}",neworder.Count);
           Console.WriteLine("Price: {0}", neworder.Price);
           Console.WriteLine("Total Price: {0}", neworder.TotalPrice);
           Console.ReadKey();*/

            //XMLSerializer

            FileStream fsc = new FileStream("d:\\testSR.xml", FileMode.Create, FileAccess.Write, FileShare.None);
            XmlSerializer xml = new XmlSerializer(typeof(Order));
            xml.Serialize(fsc, order);
            fsc.Close();

            FileStream fsr = File.OpenRead("d:\\testSR.xml");
            XmlSerializer xml1 = new XmlSerializer(typeof(Order));
            xml.Deserialize(fsr);
            fsr.Close();

            Console.WriteLine("Count: {0}",order.Count);
            Console.WriteLine("Price: {0}", order.Price);
            Console.WriteLine("Total Price: {0}", order.TotalPrice);
            Console.ReadKey();

           
        }

    }
}
