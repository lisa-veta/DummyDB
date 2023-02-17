using System;
using System.Text;

namespace DummyDB
{
    class Screen
    {
        private static int maxLenFirst = 5;
        private static int maxLenSecond = 8;
        private static int maxLenThird = 6;
        private static int maxLenFourth = 18;
        public static void EnterInform(List<Book> books, List<ReaderBook> readerBooks)
        {
            GetMaxLen(books, readerBooks);
            StartEnter(readerBooks);
        }
        private static void GetMaxLen(List<Book> books, List<ReaderBook> readerBooks)
        {
            foreach (Book book in books)
            {
                if(book.Author.Length > maxLenFirst)
                    maxLenFirst = book.Author.Length;
                if(book.Name.Length > maxLenSecond)
                    maxLenSecond = book.Name.Length;
                foreach (ReaderBook readerBook in readerBooks)
                {
                    if (book.Id == readerBook.Book.Id && readerBook.ReturnDate == DateTime.MinValue && readerBook.Reader.FullName.Length > maxLenThird)
                        maxLenThird = readerBook.Reader.FullName.Length;
                }
            }
        }

        private static void StartEnter(List<ReaderBook> readerBooks)
        {
            StringBuilder table = new StringBuilder();
            table.Append("|" + " " + new string('-', maxLenFirst) + " ");
            table.Append("|" + " " + new string('-', maxLenSecond) + " ");
            table.Append("|" + " " + new string('-', maxLenThird) + " ");
            table.Append("|" + " " + new string('-', maxLenFourth) + " " + "|");

            Console.Write("| Aвтор" + new string(' ', maxLenFirst - 5) + " ");
            Console.Write("| Название" + new string(' ', maxLenSecond - 8) + " ");
            Console.Write("| Читает" + new string(' ', maxLenThird - 6) + " ");
            Console.WriteLine("| Взял" + new string(' ', maxLenFourth - 4) + " |");
            Console.WriteLine(table.ToString());
            foreach (ReaderBook readerBook in readerBooks)
            {
                Console.Write($"| {readerBook.Book.Author}" + new string(' ', maxLenFirst - readerBook.Book.Author.Length) + " ");
                Console.Write($"| {readerBook.Book.Name}" + new string(' ', maxLenSecond - readerBook.Book.Name.Length) + " ");
                
                if (readerBook.ReturnDate == DateTime.MinValue)
                {
                    Console.Write($"| {readerBook.Reader.FullName}" + new string(' ', maxLenThird - readerBook.Reader.FullName.Length) + " ");
                    Console.WriteLine($"| {readerBook.TakeDate}" + new string(' ', maxLenFourth - 18) + " |");
                }
                else
                {
                    Console.Write($"| " + new string(' ', maxLenThird ) + " ");
                    Console.WriteLine("| " + new string(' ', maxLenFourth ) + " |");
                }
                Console.WriteLine(table.ToString());
            }
        }
    }
}
