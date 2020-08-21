using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;


namespace Spellchecker
{
    class Spellchecker
    {

        string basePath = @"D:\Spellchecker\";
        string fullPath_for_Dictionary;
        string fullPath_for_File;
       // bool endOfMulticomment = true;
        List<string> File_comments = new List<string>();



        //method for utility launch
        public void LaunchUtility()
        {

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
            
            this.fullPath_for_Dictionary = Create_Full_Path_for_File(basePath, userData[1]);
            this.fullPath_for_File = Create_Full_Path_for_File(basePath, userData[2]);
            //ReadFile(fullPath_for_File);
        }


        //method for full path creation to get the file
        string Create_Full_Path_for_File(string base_path, string relative_path)
        {
            string full_path = Path.Combine(base_path, relative_path);
            return full_path;

        }


        

        //method to copy array to the list
        void AddToList(ref List<String> list, string[] array)
        {
            lock (list)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    list.Add(array[i]);
                }
            }
        }
        //method to read file
        public void ReadFile(/*string FilePath*/)
        {
            string subString1 = "//";
            string subString2 = "/*";
            string subString3 = "*/";
            string[] File_;
            bool startOfMultiComment = false;
            bool endOfMulticomment = true;
            string tempString;
            int pFrom;
            int pTo;

            List<String> File = new List<string>();

            using (StreamReader sr = new StreamReader(this.fullPath_for_File))
            {
                while (!sr.EndOfStream)
                {
                    //FileRows.Add(sr.ReadLine());
                    string s = sr.ReadLine();
                    

                    if (s.IndexOf(subString1) > 0)
                    {
                        pFrom = s.IndexOf(subString1) + subString1.Length;
                        tempString = s.Substring(pFrom);
                        /*Regex regex = new Regex(@"(\w*)");
                        MatchCollection matches = regex.Matches(tempString);
                        if (matches.Count > 0)
                        {
                            foreach (Match match in matches)
                                Console.WriteLine(match.Value);
                        }*/

                        File_ = tempString.Split(new char[] { ' ', ',', '.', '"', '/', '*', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        AddToList(ref File_comments, File_);

                    }
                    else if (s.IndexOf(subString2) > 0)
                    {
                        if (s.IndexOf(subString3) > 0)
                        {
                            pFrom = s.IndexOf(subString2) + subString2.Length;
                            pTo = s.IndexOf(subString3);

                            tempString = s.Substring(pFrom, pTo - pFrom);
                            File_ = tempString.Split(new char[] { ' ', ',', '.', '"', '/', '*', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                            AddToList(ref File_comments, File_);
                            endOfMulticomment = true;
                        }
                        else
                        {
                            pFrom = s.IndexOf(subString2) + subString2.Length;
                            tempString = s.Substring(pFrom);
                            File_ = tempString.Split(new char[] { ' ', ',', '.', '"', '/', '*', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                            AddToList(ref File_comments, File_);
                            startOfMultiComment = true;
                            endOfMulticomment = false;
                        }
                    }
                    else if (startOfMultiComment == true)
                    {
                        if (s.IndexOf(subString3) > 0)
                        {
                            pTo = s.IndexOf(subString3);

                            tempString = s.Substring(0, pTo);
                            File_ = tempString.Split(new char[] { ' ', ',', '.', '"', '/', '*', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                            AddToList(ref File_comments, File_);
                            startOfMultiComment = false;
                            endOfMulticomment = true;
                        }
                        else
                        {
                            File_ = s.Split(new char[] { ' ', ',', '.', '"', '/', '*', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                            AddToList(ref File_comments, File_);
                        }

                    }
                }
                if (endOfMulticomment == false)
                {
                    Console.WriteLine("Error in file is found: */ is missing");
                    Environment.Exit(0);
                }    

            }

            /*foreach (string s in File_comments)
            {
                Console.WriteLine(s);
            }*/

        }

        //method to check spelling
        public void CheckSpelling()
        {
            //string file_path_for_dictionary = @"D:\Spellchecker\dictionary.txt";
            List<string> Dictionary = new List<string>();
            int error = 0;
            /*this.File_comments.Add("do");
            this.File_comments.Add("while");
            this.File_comments.Add("multyplication");
            this.File_comments.Add("table");*/

            using (StreamReader sr = new StreamReader(this.fullPath_for_Dictionary) /*(file_path_for_dictionary)*/)
            {
                while (!sr.EndOfStream)
                {
                    Dictionary.Add(sr.ReadLine());
                }
            }

            lock (this.File_comments)
            {
                foreach (string list in this.File_comments)
                {
                    
                    foreach (string dictionary_ in Dictionary)
                    {
                        if (list.Length == dictionary_.Length)
                        {
                            string tempListString = list.ToLower();
                            string tempDictionaryString = dictionary_.ToLower();
                            error = 0;
                            char[] word_from_list = tempListString.ToCharArray();
                            char[] word_from_dictionary = tempDictionaryString.ToCharArray();
                            for (int i = 0; i < word_from_list.Length; i++)
                            {
                                if (word_from_list[i] != word_from_dictionary[i])
                                {
                                    error++;
                                }
                            }
                            if (list.Length <= 3 && error == 1)
                            {
                                Console.WriteLine($"The following word is not correct: {list}");
                            }
                            else if (list.Length > 3 && error <= 2 && error>0)
                            {
                                Console.WriteLine($"The following word is not correct: {list}");
                            }
                        }
                    }
                }
                }
            }
        }
    }

