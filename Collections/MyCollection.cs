using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Collections
{
    class MyCollection <T>: ICollection<T>
    {


        private List<T> myList=new List<T>();
        private SortedList<int, T> mySortedList=new SortedList<int, T>();
        private T[] tempArray=new T[5];
        private int size;
        private bool readOnly;


        int ICollection<T>.Count { get; }
        public int Count ()
        {
            if (isMyListFilled())
            {
                return myList.Count;
            }
            else
            {
                return mySortedList.Values.Count;
            }

        }

        public bool IsReadOnly => readOnly;

        

        public bool MakeCollectionIsReadOnly()
        {
            readOnly = true;
            return readOnly;
        }



        //adds new element to List<T> if number of elements <=5 and adds new element to SortedList<T,T> if number of elements >6

        public void Add (T item)
        {
            if (readOnly)
            { throw new ReadOnlyException(); }
            if (size < 5)
            {
                //List<T> myList = new List<T>();
                myList.Add(item);
            }
            else if (isMyListFilled())
            {
                myList.CopyTo(tempArray);
                myList.Clear();
                int len = tempArray.Length;
                //SortedList<int, T> mySortedList = new SortedList<int, T>();
                for (int i = 0; i < len; i++)
                {
                    mySortedList.Add(i + 1, tempArray[i]);
                }
                mySortedList.Add(size+1,item);

            }
            else 
            {
                mySortedList.Add(size+1, item);
            }
            size++;
            
        }

        public void Clear()
        {
            if (readOnly)
            { throw new ReadOnlyException(); }
            myList = new List<T>();
            mySortedList = new SortedList<int, T>();
            size = 0;
          
        }

        public bool Contains(T item)
        {
            if (isMyListFilled())
            {
                return myList.Contains(item);
            }
            else
            {
                return mySortedList.Values.Contains(item);
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (isMyListFilled())
            {
                myList.CopyTo(array,arrayIndex);
            }
            else
            {
                mySortedList.Values.CopyTo(array,arrayIndex);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            
            if (isMyListFilled())
            {
                
                return myList.GetEnumerator();
            } else
            {
                return mySortedList.Values.GetEnumerator();
            }           
        }
        
        public bool Remove(T item)
        {

            if (readOnly)
            { throw new ReadOnlyException(); }
            bool response = false;
            if (isMyListFilled())
            {

                response = myList.Remove(item);
            }
            else
            {
                mySortedList.RemoveAt(mySortedList.IndexOfValue(item));
                response = true;
                if (mySortedList.Count < 6)
                {
                    myList = new List<T>(mySortedList.Values);
                    mySortedList.Clear();
                }

            }
            if (response == true)
            {
                size--;
            }
            return response;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private bool isMyListFilled()
        {
            return myList != null && myList.Count > 0;
        }
    }
    
}
