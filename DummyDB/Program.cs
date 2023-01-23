using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection.PortableExecutable;

namespace DummyDB
{
    class Program
    {
        static void Main()
        {
            List<Reader> readers = Check.GetReaderData("Data\\Reader.csv");
            List<Book> books = Check.GetBookData("Data\\Book.csv");
            List<ReaderBook> readerBooks = Check.GetReaderBookData("Data\\ReaderBook.csv");

            Screen.EnterInform(books, readerBooks);
        }
    }
}
