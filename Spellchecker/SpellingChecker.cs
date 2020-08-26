using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.Threading;
using NLog;

namespace Spellchecker
{
    class SpellingChecker
    {
        private string fullPathForDictionary { get; set; }
        private object locker { get; set; }
        private List<string> fileComments { get; set; }
        private Logger logger { get; set; }

        public SpellingChecker(List<string> listOfCommentsfromFile, object locker, string dictionaryPath)
         {
            fileComments = listOfCommentsfromFile;
            this.locker = locker;
            fullPathForDictionary = dictionaryPath;
        }
        public void CheckSpelling()
        {
            List<string> dictionaryWords = new List<string>();
            int errorCounter = 0;
            logger = LogManager.GetCurrentClassLogger();

            using (StreamReader streamReader = new StreamReader(fullPathForDictionary))
            {
                while (!streamReader.EndOfStream)
                {
                    dictionaryWords.Add(streamReader.ReadLine());
                }
            }

            lock (locker)
            {
                foreach (string wordFromFileComments in fileComments)
                {

                    foreach (string wordFromDictionary in dictionaryWords)
                    {
                        if (wordFromFileComments.Length == wordFromDictionary.Length)
                        {

                            Thread currentThread = Thread.CurrentThread;
                            logger.Debug($"\"{wordFromFileComments}\" is checking for spelling by \"{currentThread.Name}\"");
                            string wordFromFileinLowCase = wordFromFileComments.ToLower();
                            string wordFromDictionaryinLowCase = wordFromDictionary.ToLower();
                            errorCounter = 0;
                            char[] wordFromFileByLetters = wordFromFileinLowCase.ToCharArray();
                            char[] wordFromDictionaryByLetters = wordFromDictionaryinLowCase.ToCharArray();
                            
                            for (int index = 0; index < wordFromFileByLetters.Length; index++)
                            {
                                if (wordFromFileByLetters[index] != wordFromDictionaryByLetters[index])
                                {
                                    errorCounter++;
                                }
                            }
                            if (wordFromFileComments.Length <= 3 && errorCounter == 1)
                            {
                                Console.WriteLine($"The following word is not correct: {wordFromFileComments}");
                            }
                            else if (wordFromFileComments.Length > 3 && errorCounter <= 2 && errorCounter > 0)
                            {
                                Console.WriteLine($"The following word is not correct: {wordFromFileComments}");
                            }
                        }
                    }
                }
            }
        }
    }


}
