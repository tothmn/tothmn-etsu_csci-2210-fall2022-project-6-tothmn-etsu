////////////////////////////////////////////////////////////////////////////////
//
// Author: Megan Toth
// Course: CSCI-2210-001 - Data Structures
// Assignment: Project 6
// Description: The Program.cs file reads each book into a Book object and adds it to the AVL Tree. It also demonstrates the functionality 
// of the tree using a Menu
//
///////////////////////////////////////////////////////////////////////////////
using System.DataStructures;

namespace Project6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StreamReader stream = new StreamReader(@".\books.csv");

            AvlTree<Book> CurrentTree;
            AvlTree<Book> TitleTree = new();
            AvlTree<Book> AuthorTree = new();
            AvlTree<Book> PublisherTree = new();
            AvlTree<Book> CheckedOut = new();

            List<string> fields = new();

            while(stream.Peek() > -1)
            {
                fields = ProcessCSVLine(stream.ReadLine());
                Book book = new Book(fields[0], fields[1], Int32.Parse(fields[2]), fields[3], 0);
                TitleTree.Add(book);

                book.SortKey = 1;
                AuthorTree.Add(book);

                book.SortKey = 2;
                PublisherTree.Add(book);
            }
            CurrentTree = TitleTree;

            Dictionary<string, dynamic> menuActions = new();

            bool isRunning = true;

            menuActions["1"] = new Action(() => { DisplayBooks(TitleTree); });
            menuActions["2"] = new Action(() => { DisplayBooks(AuthorTree); });
            menuActions["3"] = new Action(() => { DisplayBooks(PublisherTree); });
            menuActions["4"] = new Action(() => { DisplayBooks(CheckedOut); });
            menuActions["5"] = new Action(() => { CheckIn(); });
            menuActions["6"] = new Action(() => { CheckOut(); });
            menuActions["7"] = new Action(() => { isRunning = false; });



            while (isRunning)
            {
                //display menu options
                Console.Clear();
                Console.WriteLine("1: Sort by Title");
                Console.WriteLine("2: Sort by Author");
                Console.WriteLine("3: Sort by Publisher");
                Console.WriteLine("4. Display Checked-Out Books");
                Console.WriteLine("5: Check-In Book");
                Console.WriteLine("6: Check-Out Book");
                Console.WriteLine("7: Exit");
                //ask for menu option
                //get menu option
                string input = Console.ReadLine();
                //do menu option
                if (menuActions.ContainsKey(input))
                {

                    menuActions[input]();

                }
            }

            /// <summary>
            /// The CheckIn method checks in a book by adding book object to Title, Author, and Publisher trees 
            /// and removes the book object from the CheckedOut tree
            /// </summary>
            void CheckIn()
            {
                Console.WriteLine("Please type in a book title to check in.");
                string title = Console.ReadLine().ToLower();
                List<Book> library = CheckedOut.GetInorderEnumerator().ToList();
                foreach (Book b in library)
                {
                    if (b.Title.ToLower() == title)
                    {
                        TitleTree.Add(b);
                        AuthorTree.Add(b);
                        PublisherTree.Add(b);
                        CheckedOut.Remove(b);
                    }

                }
            }

            /// <summary>
            /// The CheckOut method checks out a book by removing book object from Title, Author, and Publisher trees 
            /// and adds the book object to the CheckedOut tree
            /// </summary>
            void CheckOut()
            {
                Console.WriteLine("Please type in a book title to check out.");
                string title = Console.ReadLine();
                List<Book> library = TitleTree.GetInorderEnumerator().ToList();
                foreach (Book b in library)
                {
                    if (b.Title.ToLower() == title.ToLower())
                    {
                        TitleTree.Remove(b);
                        AuthorTree.Remove(b);
                        PublisherTree.Remove(b);
                        CheckedOut.Add(b);
                    }
                }
            }

        }
        /// <summary>
        /// The DisplayBooks method displays a formaatted list of books from temporary tree
        /// </summary>
        /// <param name="tree"> Tree created for book library </param>
        static void DisplayBooks(AvlTree<Book> tree)
        {
            Console.Clear();
            List<Book> library = tree.GetInorderEnumerator().ToList();
            foreach (Book b in library)
            {
                b.Print();
            }
            Console.WriteLine("\nPress Enter to Continue...");
            Console.ReadLine();
        }

        /// <summary>
        /// The ProcessCSVLine method reads the book.csv file line by line and adds it to a List of type string
        /// </summary>
        /// <param name="lineFromCSV"> Each line of the csv file</param>
        /// <returns> The cleaned strings </returns>
        static List<string> ProcessCSVLine(string lineFromCSV)
        {
            // Split it based on a comma
            string[] rawBookParts = lineFromCSV.Split(",");
            // Create a list of book parts that represent the columns in the CSV
            // We can treat anything that goes into this list as sanitized data.
            List<string> sanitizedBookParts = new List<string>();
            // Iterate through each book part found by naively spliting on the comma alone.
            string cleanString = string.Empty;
            for (int i = 0; i < rawBookParts.Length; i++)
            {
                // Add the newest item split by the comma alone to a new string
                cleanString += rawBookParts[i];
                // If the string starts with a quote, but does not end with a quote,
                // then we know that the full text from the string hasn't been
                // read in yet, and that the rest of the text be in a future
                // element of rawBookParts.
                if (cleanString.StartsWith("\"") && !cleanString.EndsWith("\""))
                {
                    continue;
                }
                // Once it is verified that the clean string contains the entire
                // text of the column, we can add it to our list of sanitized
                // book parts. This is also a good time to remove the quotes
                // at the beginning and end of the string if they exist.
                sanitizedBookParts.Add(cleanString.Replace("\"", String.Empty));
                // Reset the clean string for the next iteration.
                cleanString = String.Empty;
            }
            return sanitizedBookParts;
        }
    }
}