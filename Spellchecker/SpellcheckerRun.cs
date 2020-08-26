using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.Threading;
using NLog;


namespace Spellchecker
{
    class SpellcheckerRun
    {

        public string basePath { get; set; }
        private string fullPathForDictionary { get; set; }
        private string fullPathForFile { get; set; }
        private object locker { get; set; }
        private List<string> fileComments { get; set; }
          

        
            //method for utility launch
            public void LaunchUtility()
        {

            basePath = @"D:\Spellchecker\";
            Console.WriteLine("Please input \"csharpspell\" DICTIONARY FILE to launch the utility");
            string userString = Console.ReadLine();
            string[] userData = userString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            while (userData[0] != "csharpspell")
            {
                Console.WriteLine("Please provide \"csharpspell\" command to lauch the utility");
                userString = Console.ReadLine();
                userData = userString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }

            while (userData.Length != 3)
            {
                Console.WriteLine("Please specify Dictionary and File to run the utility");
                userString = Console.ReadLine();
                userData = userString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }    
            
            fullPathForDictionary = CreateFullPathForFile(basePath, userData[1]);
            
            fullPathForFile = CreateFullPathForFile(basePath, userData[2]);
           
            locker = new object();
            
            fileComments = new List<string>();
            
            FileReader fileReader = new FileReader(fileComments,locker,fullPathForFile);
            
            Thread  readerThread = new Thread(fileReader.ReadFile);
            readerThread.Name = "Comments reader";
            readerThread.Start();
            readerThread.Join(1200);
            
            SpellingChecker spellingChecker = new SpellingChecker(fileComments, locker, fullPathForDictionary);
            
            Thread checkThread = new Thread(spellingChecker.CheckSpelling);
            checkThread.Name = "Spelling checker";
            checkThread.Start();
            checkThread.Join(1000);

        }


        //method for full path creation to get the file
        string CreateFullPathForFile(string rootPath, string relativePath)
        {
            return Path.Combine(rootPath, relativePath);
           

        }

    }
}

