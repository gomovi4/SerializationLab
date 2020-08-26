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

            SpellcheckerRun spellcheckerRun = new SpellcheckerRun();
            spellcheckerRun.LaunchUtility();

        }

    }
}
