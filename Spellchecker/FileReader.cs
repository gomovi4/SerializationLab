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
        private ICollection<string> fileComments { get; set; }
        private Logger logger { get; set; }

        private AutoResetEvent waitHandleForSpellingChecker { get; set; }
        private AutoResetEvent waitHandleForFileReader { get; set; }

        private char[] splitSymbols = new char[] { ' ', ',', '.', '"', '/', '*', '\t', '{', '}', '(', ')' };
        public FileReader(List<string> listOfCommentsfromFile, AutoResetEvent waitHandleForSpellingChecker, AutoResetEvent waitHandleForFileReader, string filePath)
        {
            fileComments= listOfCommentsfromFile;
            this.waitHandleForSpellingChecker = waitHandleForSpellingChecker;
            this.waitHandleForFileReader = waitHandleForFileReader;
            fullPathForFile = filePath;

        }

        
        //method to read file
        public void ReadFile()
        {
            string singleLineComments = "//";
            string openedMultiLineComments = "/*";
            string closedMultiLineComments = "*/";
            string[] wordsFromFileLine;
            string[] wordsWithPosisionInFile;
            bool isMultiCommentOpened = false;
            bool isMulticommentClosed = true;
            string commentedPartofString;
            int positionFrom;
            int positionTo;
            int lineCounter=0;
            

            logger = LogManager.GetCurrentClassLogger();
            

            using (StreamReader streamReader = new StreamReader(fullPathForFile))
            {
                while (!streamReader.EndOfStream)
                {
                   
                    string fileString = streamReader.ReadLine();
                    lineCounter++;
                    if (fileString.IndexOf(singleLineComments) > -1)
                    {
                        positionFrom = fileString.IndexOf(singleLineComments) + singleLineComments.Length;
                        commentedPartofString = fileString.Substring(positionFrom);
                        wordsFromFileLine = SplitString(commentedPartofString);
                        wordsWithPosisionInFile = AppendWordPosition(wordsFromFileLine, lineCounter, fileString);
                        AddToList(fileComments, wordsWithPosisionInFile);

                    }
                    else if (fileString.IndexOf(openedMultiLineComments) > -1)
                    {
                        if (fileString.IndexOf(closedMultiLineComments) > -1)
                        {
                            positionFrom = fileString.IndexOf(openedMultiLineComments) + openedMultiLineComments.Length;
                            positionTo = fileString.IndexOf(closedMultiLineComments);

                            commentedPartofString = fileString.Substring(positionFrom, positionTo - positionFrom);
                            wordsFromFileLine = SplitString(commentedPartofString);
                            wordsWithPosisionInFile = AppendWordPosition(wordsFromFileLine, lineCounter, fileString);
                            AddToList(fileComments, wordsWithPosisionInFile);
                            isMulticommentClosed = true;
                        }
                        else
                        {
                            positionFrom = fileString.IndexOf(openedMultiLineComments) + openedMultiLineComments.Length;
                            commentedPartofString = fileString.Substring(positionFrom);
                            wordsFromFileLine = SplitString(commentedPartofString);
                            wordsWithPosisionInFile = AppendWordPosition(wordsFromFileLine, lineCounter, fileString);
                            AddToList(fileComments, wordsWithPosisionInFile);
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
                            wordsFromFileLine = SplitString(commentedPartofString);
                            wordsWithPosisionInFile = AppendWordPosition(wordsFromFileLine, lineCounter, fileString);
                            AddToList(fileComments, wordsWithPosisionInFile);
                            isMultiCommentOpened = false;
                            isMulticommentClosed = true;
                        }
                        else
                        {
                            wordsFromFileLine = SplitString(fileString);
                            wordsWithPosisionInFile = AppendWordPosition(wordsFromFileLine, lineCounter, fileString);
                            AddToList(fileComments, wordsWithPosisionInFile);
                        }

                    }


                }
                if (isMulticommentClosed == false)
                {
                    Thread currentThread = Thread.CurrentThread;
                    logger.Error($"Error in file is found: */ is missing. Issue is found by \"{currentThread.Name}\"");
                    Console.WriteLine("Error in file is found: */ is missing");
                    Environment.Exit(0);
                }

                if (streamReader.EndOfStream)
                {
                    waitHandleForSpellingChecker.Set();
                }

            }

            
        }

        //method to copy array to the list
        void AddToList(ICollection<String> list, string[] array)
        {
            
                for (int index = 0; index < array.Length; index++)
                {
                    list.Add(array[index]);
                    Thread currentThread = Thread.CurrentThread;
                    logger.Debug($"\"{array[index]}\" was added to the list to check spelling by \"{currentThread.Name}\"");
                    waitHandleForSpellingChecker.Set();
                    waitHandleForFileReader.WaitOne();
                }
            
        }
       
        //method to split the string by words
        string[] SplitString(string lineOfWords)
        {

            return lineOfWords.Split(splitSymbols, StringSplitOptions.RemoveEmptyEntries);
        }
        //method to add line and column of the word
        string[] AppendWordPosition(string[] words, int lineNumber, string fileLine)
        {
            string[] wordsWithPosition=new string[words.Length];
            for (int index=0; index < words.Length; index++)
            {
                StringBuilder stringBuilder = new StringBuilder();
                wordsWithPosition[index]=stringBuilder.Append("Line: ").Append(lineNumber).Append(" Row: ").Append(FindWordColumn(fileLine,words[index])).Append(" Word: ").Append(words[index]).ToString();
             }

            return wordsWithPosition;
        }

        //method to find word column
        int FindWordColumn(string fileLine, string foundWord)
        {
            return fileLine.IndexOf(foundWord) + 1;
        }
    }
}
