using System;
using System.Collections;
using System.Collections.Generic;

namespace Collections
{
    class Program
    {
        int[] temparray;
        static void Main(string[] args)
        {
            MyCollection<int> myCollection = new MyCollection<int>();
            myCollection.Add(1);
            myCollection.Add(2);
            myCollection.Add(3);
            myCollection.Add(4);
            myCollection.Add(5);
            myCollection.Add(6);
            myCollection.Add(7);

            myCollection.Clear();
            myCollection.Add(8);
            myCollection.Add(10);
            myCollection.Add(11);
            myCollection.Add(12);
            myCollection.Add(13);
            myCollection.Add(14);
            myCollection.Add(15);
            
            if (myCollection.Contains(9))
            {
                Console.WriteLine("Collection contains 9");
            }
            else
            {
                Console.WriteLine("Collection doesn't contain 9");
            }

            int[] temparray=new int[myCollection.Count()];
            myCollection.CopyTo(temparray,0);

            //myCollection.Remove(9);
            //myCollection.MakeCollectionIsReadOnly();
            myCollection.Remove(14);
            myCollection.Remove(15);

            foreach (int i in myCollection)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine("The following values were copied to array");
            foreach (int i in temparray)
            {
                Console.WriteLine(i);
            }

        }
    }
}
