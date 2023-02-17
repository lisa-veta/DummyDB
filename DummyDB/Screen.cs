using System.Text;

namespace DummyDB
{
    class Screen
    {
        private static int maxLenFirst = 5;
        private static int maxLenSecond = 8;
        private static int maxLenThird = 6;
        private static int maxLenFourth = 10;
        public static void EnterInform(List<Book> books, List<ReaderBook> readerBooks)
        {
            GetMaxLen(books, readerBooks);

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

        private static void StartEnter(List<Book> books, List<ReaderBook> readerBooks)
        {
            StringBuilder table = new StringBuilder();
            table.Append("|" + " " + string.Join("-", maxLenFirst));
            table.Append("|" + " " + string.Join("-", maxLenSecond));
            table.Append("|" + " " + string.Join("-", maxLenThird));
            table.Append("|" + " " + string.Join("-", maxLenFourth) + " " + "|");
            Console.Write("| ");
            for (int i = 0; i < books.Count; i++)
            {
                Console.Write("| ");
            }
        }
    }
}
