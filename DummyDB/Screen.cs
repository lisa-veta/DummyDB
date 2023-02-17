using System.Text;

namespace DummyDB
{
    class Screen
    {
        private static int maxLenFirst = 0;
        private static int maxLenSecond = 0;
        private static int maxLenThird = 0;
        private static int maxLenFourth = 0;
        public static void EnterInform(List<Book> books, List<ReaderBook> readerBooks)
        {
            
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
                    if (book.Id == readerBook.Book.Id && readerBook.ReturnDate == DateTime.MinValue)
                        maxLenThird = readerBook.Reader.FullName.Length;
                }
            }
        }
    }
}
