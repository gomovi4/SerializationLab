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
        private List<string> fileComments { get; set; }
        private AutoResetEvent waitHandleForSpellingChecker { get; set; }
        private AutoResetEvent waitHandleForFileReader { get; set; }
        private char[] splitSymbols = new char[] { ' ' };
        private string[] userData { get; set; }


        //method for utility launch
        public void LaunchUtility()
        {

            
            string appPath = AppDomain.CurrentDomain.BaseDirectory;


            Console.WriteLine("Please input command in one row exactly in the follwoing fomat:\"csharpspell\" DICTIONARY_name FILE_name to launch the utility");
            string userString = Console.ReadLine();
            userData = SplitString(userString);

            while (userData[0] != "csharpspell")
            {
                Console.WriteLine("Please provide \"csharpspell\" command to lauch the utility. \"csharpspell\" DICTIONARY_name FILE_name should be populated in one row via space.");
                userString = Console.ReadLine();
               userData = SplitString(userString);
            }

            while (userData.Length != 3)
            {
                Console.WriteLine("Please specify Dictionary and File to run the utility.\"csharpspell\" DICTIONARY_name FILE_name should be populated in one row via space.");
                userString = Console.ReadLine();
                userData = SplitString(userString);
            }    
            
            fullPathForDictionary = CreateFullPathForFile(appPath, userData[1]);
            
            fullPathForFile = CreateFullPathForFile(appPath, userData[2]);
                       
            fileComments = new List<string>();

            waitHandleForSpellingChecker = new AutoResetEvent(false);

            waitHandleForFileReader = new AutoResetEvent(false);


            SpellingChecker spellingChecker = new SpellingChecker(fileComments, waitHandleForSpellingChecker, waitHandleForFileReader,fullPathForDictionary);
            
            FileReader fileReader = new FileReader(fileComments, waitHandleForSpellingChecker, waitHandleForFileReader, fullPathForFile);
           
            Thread  readerThread = new Thread(fileReader.ReadFile);
            readerThread.Name = "Comments reader";
            readerThread.Start();
            
            
            Thread checkThread = new Thread(spellingChecker.CheckSpelling);
            checkThread.Name = "Spelling checker";
            checkThread.Start();
            
        }


        //method for full path creation to get the file
        string CreateFullPathForFile(string rootPath, string fileName)
        {
            return Path.Combine(rootPath, fileName);
          
        }

        //method to split the string by words
        string[] SplitString(string lineOfWords)
        {
            return lineOfWords.Split(splitSymbols, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}

