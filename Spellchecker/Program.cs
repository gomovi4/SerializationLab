using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.Threading;

namespace Spellchecker
{
    class Program
    {
        static void Main(string[] args)
        {

            Spellchecker spellchecker = new Spellchecker();
            spellchecker.LaunchUtility();
           Thread th1 = new Thread(spellchecker.ReadFile);
           th1.Start();
           th1.Join(1200);
            
           Thread th2 = new Thread(spellchecker.CheckSpelling);
           th2.Start();
           th2.Join(1000);

            }
        
    }
}
