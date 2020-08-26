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
    class FileReader
    {
       
        private string fullPathForFile { get; set; }
        private object locker { get; set; }
        private List<string> fileComments { get; set; }
        private Logger logger { get; set; }



        public FileReader(List<string> listOfCommentsfromFile, object locker, string filePath)
        {
            fileComments= listOfCommentsfromFile;
            this.locker = locker;
            fullPathForFile = filePath;

        }

        
        //method to read file
        public void ReadFile()
        {
            string singleLineComments = "//";
            string openedMultiLineComments = "/*";
            string closedMultiLineComments = "*/";
            string[] wordsFromFileLine;
            bool isMultiCommentOpened = false;
            bool isMulticommentClosed = true;
            string commentedPartofString;
            int positionFrom;
            int positionTo;

            logger = LogManager.GetCurrentClassLogger();
           
            using (StreamReader streamReader = new StreamReader(fullPathForFile))
            {
                while (!streamReader.EndOfStream)
                {
                   
                    string fileString = streamReader.ReadLine();
                    
                    if (fileString.IndexOf(singleLineComments) > -1)
                    {
                        positionFrom = fileString.IndexOf(singleLineComments) + singleLineComments.Length;
                        commentedPartofString = fileString.Substring(positionFrom);
                        wordsFromFileLine = commentedPartofString.Split(new char[] { ' ', ',', '.', '"', '/', '*', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        AddToList(fileComments, wordsFromFileLine);

                    }
                    else if (fileString.IndexOf(openedMultiLineComments) > -1)
                    {
                        if (fileString.IndexOf(closedMultiLineComments) > -1)
                        {
                            positionFrom = fileString.IndexOf(openedMultiLineComments) + openedMultiLineComments.Length;
                            positionTo = fileString.IndexOf(closedMultiLineComments);

                            commentedPartofString = fileString.Substring(positionFrom, positionTo - positionFrom);
                            wordsFromFileLine = commentedPartofString.Split(new char[] { ' ', ',', '.', '"', '/', '*', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                            AddToList(fileComments, wordsFromFileLine);
                            isMulticommentClosed = true;
                        }
                        else
                        {
                            positionFrom = fileString.IndexOf(openedMultiLineComments) + openedMultiLineComments.Length;
                            commentedPartofString = fileString.Substring(positionFrom);
                            wordsFromFileLine = commentedPartofString.Split(new char[] { ' ', ',', '.', '"', '/', '*', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                            AddToList(fileComments, wordsFromFileLine);
                            isMultiCommentOpened = true;
                            isMulticommentClosed = false;
                        }
                    }
                    else if (isMultiCommentOpened == true)
                    {
                        if (fileString.IndexOf(closedMultiLineComments) > -1)
                        {
                            positionTo = fileString.IndexOf(closedMultiLineComments);

                            commentedPartofString = fileString.Substring(0, positionTo);
                            wordsFromFileLine = commentedPartofString.Split(new char[] { ' ', ',', '.', '"', '/', '*', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                            AddToList(fileComments, wordsFromFileLine);
                            isMultiCommentOpened = false;
                            isMulticommentClosed = true;
                        }
                        else
                        {
                            wordsFromFileLine = fileString.Split(new char[] { ' ', ',', '.', '"', '/', '*', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                            AddToList(fileComments, wordsFromFileLine);
                        }

                    }


                }
                if (isMulticommentClosed == false)
                {
                    Console.WriteLine("Error in file is found: */ is missing");
                    Environment.Exit(0);
                }

            }

            
        }

        //method to copy array to the list
        void AddToList(List<String> list, string[] array)
        {
            lock (locker)
            {
                for (int index = 0; index < array.Length; index++)
                {
                    list.Add(array[index]);
                    Thread currentThread = Thread.CurrentThread;
                    logger.Debug($"\"{array[index]}\" was added to the list to check spelling by \"{currentThread.Name}\"");
                }
            }
        }
    }
}
