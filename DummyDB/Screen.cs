using System.Text;

namespace DummyDB
{
    class Screen
    {
        public static void EnterInform(List<Book> books, List<ReaderBook> readerBooks)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Cписок всех книг в библиотеке:\n");
            foreach(Book book in books)
            {
                Console.Write($"\"{book.Name}\", {book.Author}");
                foreach(ReaderBook readerBook in readerBooks)
                {
                    if (book.Id == readerBook.Book.Id && readerBook.ReturnDate == DateTime.MinValue)
                        Console.Write($", читает {readerBook.Reader.FullName}");
                }
                Console.WriteLine("\n");
            }
        }
    }
}
