using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.Threading;
using System.Linq;
using NLog;

namespace Spellchecker
{
    class SpellingChecker
    {
        private string fullPathForDictionary { get; set; }
        private object locker { get; set; }
        private List<string> fileComments { get; set; }
        private Logger logger { get; set; }
        private AutoResetEvent waitHandleForSpellingChecker { get; set; }
        private AutoResetEvent waitHandleForFileReader { get; set; }



        public SpellingChecker(List<string> listOfCommentsfromFile, AutoResetEvent waitHandleForSpellingChecker, AutoResetEvent waitHandleForFileReader, string dictionaryPath)
         {
            fileComments = listOfCommentsfromFile;
            this.waitHandleForSpellingChecker = waitHandleForSpellingChecker;
            this.waitHandleForFileReader = waitHandleForFileReader;
            fullPathForDictionary = dictionaryPath;
            
        }
        public void CheckSpelling()
        {
            ICollection<string> dictionaryWords = new List<string>();
            
            logger = LogManager.GetCurrentClassLogger();
            

            using (StreamReader streamReader = new StreamReader(fullPathForDictionary))
            {
                while (!streamReader.EndOfStream)
                {
                    dictionaryWords.Add(streamReader.ReadLine());
                }
            }
            Thread currentThread = Thread.CurrentThread;
            
            waitHandleForSpellingChecker.WaitOne();
            
                for (int i=0;i<fileComments.Count; i++)
                 {
                        bool isWordCorrect = false;
                        string wordFromLine = removeWordPosition(fileComments[i]);
                        string wordFromFileinLowCase = wordFromLine.ToLower();

                        logger.Debug($"\"{fileComments[i]}\" is checking for spelling by \"{currentThread.Name}\"");

                    foreach (string wordFromDictionary in dictionaryWords)
                    {
                   
                        string wordFromDictionaryinLowCase = wordFromDictionary.ToLower();
                    
                        if (wordFromFileinLowCase == wordFromDictionaryinLowCase)
                        {
                           isWordCorrect = true;
                            break;
                        }
                
                    }

                if (!isWordCorrect)
                {
                    logger.Debug($"\"{fileComments[i]}\" word is incorrect. Verified by \"{currentThread.Name}\"");
                    Console.WriteLine($"Spelling error is found: {fileComments[i]}");
                }
                
                waitHandleForFileReader.Set();
                waitHandleForSpellingChecker.WaitOne();
            }

        }

        //method to get word from the list without position in the file
        string removeWordPosition(string wordWithPosition)
        {
            int positionFrom = wordWithPosition.LastIndexOf(" ") + 1;
            return wordWithPosition.Substring(positionFrom);
        }
    }
}
