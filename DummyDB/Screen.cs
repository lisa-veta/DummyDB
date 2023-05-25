using System;
using System.Collections.Generic;
using System.Text;

namespace DummyDB
{
    class Screen
    {
        private static int maxLenFirst = "Aвтор".Length;
        private static int maxLenSecond = "Название".Length;
        private static int maxLenThird = "Читает".Length;
        private static int maxLenFourth = DateTime.MinValue.ToString().Length;
        private static StringBuilder table = new StringBuilder();
        public static void EnterInform(List<Book> books, List<ReaderBook> readerBooks)
        {
            GetMaxLen(books, readerBooks);
            StartEnter();
            FillData(readerBooks);
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

        private static void StartEnter()
        {
            table.Append("|" + " " + new string('-', maxLenFirst) + " ");
            table.Append("|" + " " + new string('-', maxLenSecond) + " ");
            table.Append("|" + " " + new string('-', maxLenThird) + " ");
            table.Append("|" + " " + new string('-', maxLenFourth) + " " + "|");

            Console.Write("| Aвтор" + new string(' ', maxLenFirst - "Aвтор".Length) + " ");
            Console.Write("| Название" + new string(' ', maxLenSecond - "Название".Length) + " ");
            Console.Write("| Читает" + new string(' ', maxLenThird - "Читает".Length) + " ");
            Console.WriteLine("| Взял" + new string(' ', maxLenFourth - "Взял".Length) + " |");
            Console.WriteLine(table.ToString());
        }

        private static void FillData(List<ReaderBook> readerBooks)
        {
            foreach (ReaderBook readerBook in readerBooks)
            {
                Console.Write($"| {readerBook.Book.Author}" + new string(' ', maxLenFirst - readerBook.Book.Author.Length + 1));
                Console.Write($"| {readerBook.Book.Name}" + new string(' ', maxLenSecond - readerBook.Book.Name.Length + 1));

                if (readerBook.ReturnDate == DateTime.MinValue)
                {
                    Console.Write($"| {readerBook.Reader.FullName}" + new string(' ', maxLenThird - readerBook.Reader.FullName.Length + 1));
                    Console.WriteLine($"| {readerBook.TakeDate}" + " |");
                }
                else
                {
                    Console.Write($"| " + new string(' ', maxLenThird + 1));
                    Console.WriteLine("| " + new string(' ', maxLenFourth) + " |");
                }
                Console.WriteLine(table.ToString());
            }
        }
    }
}
