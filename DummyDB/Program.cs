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
            List<Reader> readers = Check.CheckDataReader("Data\\Reader.csv");
            List<Book> books = Check.CheckDataBook("Data\\Book.csv");
            List<ReaderBook> readerBooks = Check.CheckDataReaderBook("Data\\ReaderBook.csv");

            Screen.EnterInform(books, readerBooks);
        }
    }
}
