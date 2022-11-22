////////////////////////////////////////////////////////////////////////////////
//
// Author: Megan Toth
// Course: CSCI-2210-001 - Data Structures
// Assignment: Project 6
// Description: The Book.cs file is made to store the data from the books.csv file
//
///////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project6
{
    internal class Book : IComparable<Book>
    {
        public string Title;
        public string Author;
        public int Pages;
        public string Publisher;
        public int SortKey;

        public Book(string title, string author, int pages, string publisher)
        {
            Title = title;
            Author = author;
            Pages = pages;
            Publisher = publisher;
        }

        public Book(string title, string author, int pages, string publisher, int sortKey)
        {
            Title = title;
            Author = author;
            Pages = pages;
            Publisher = publisher;
            SortKey = sortKey;
        }

        /// <summary>
        /// The CompareTo method determines how the SortKey function works for each key type
        /// </summary>
        /// <param name="other"></param>
        /// <returns>The types of data being compared</returns>
        public int CompareTo(Book? other)
        {
            if(SortKey == 0)
            {
                return this.Title.CompareTo(other.Title);

            }
            else if (SortKey == 1)
            {
                return this.Author.CompareTo(other.Author);
            }
            else
            {
                return this.Publisher.CompareTo(other.Publisher);
            }
        }

        /// <summary>
        /// The Print method displayes each book and its information in a formatted way
        /// </summary>
        public void Print()
        {
            Console.WriteLine($"Title: {Title} \nAuthor: {Author} \nPages: {Pages} \nPublisher: {Publisher}");
        }
    }
}
