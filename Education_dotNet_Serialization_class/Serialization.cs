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
        const string relativePath = "Serialization";
        static void Main(String[] args) {

            string baseFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appStorageFolder = Path.Combine(baseFolder, "Education_dotNet_Serialization_class");
            string fullPath = Path.Combine(appStorageFolder, relativePath);
            Directory.CreateDirectory(fullPath);
            
             Order order = new Order(4, 5.5m);

            //BinaryFormatter

            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using (FileStream fstreamCreateFile = new FileStream(Path.Combine(fullPath, "serialization.bin"), FileMode.Create, FileAccess.Write, FileShare.None))
            {

                binaryFormatter.Serialize(fstreamCreateFile, order);
            }

            using (FileStream fstreamReadFile = File.OpenRead(Path.Combine(fullPath, "serialization.bin")))
            {
                Order neworder = (Order)binaryFormatter.Deserialize(fstreamReadFile);
                
                Console.WriteLine($"Count: {order.Count}");
                Console.WriteLine($"Price: {order.Price}");
                Console.WriteLine($"Total Price: {order.TotalPrice}");
                Console.ReadKey();   
            }

            //XMLSerializer

           /* XmlSerializer xmlSerializer = new XmlSerializer(typeof(Order));
            using (FileStream fstreamCreateFile = new FileStream(Path.Combine(fullPath, "serialization.xml"), FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlSerializer.Serialize(fstreamCreateFile, order);
            }

            
            using (FileStream fstreamReadFile = File.OpenRead(Path.Combine(fullPath, "serialization.xml")))
            {

                xmlSerializer.Deserialize(fstreamReadFile);
            }

            Console.WriteLine($"Count: {order.Count}");
            Console.WriteLine($"Price: {order.Price}");
            Console.WriteLine($"Total Price: {order.TotalPrice}");
            Console.ReadKey();            */

        }

    }
}
