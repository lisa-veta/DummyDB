using System.Text;

namespace DummyDB
{
    class Check
    {
        public static List<uint> idReader = new List<uint>();
        public static List<uint> idBook = new List<uint>();
        public static List<Reader> readers = new List<Reader>();
        public static List<Book> books = new List<Book>();

        private static int idData = 0;
        private static int authorName = 1;
        private static int idBookData = 1;
        private static int bookName = 2;
        private static int takeDate = 2;
        private static int bookDate = 3;
        private static int returnDate = 3;
        private static int caseN = 4;
        private static int shelfN = 5;
        private static int minYear = 1500;
        private static int maxYear = 2023;
        private static int columnLenReader = 2;
        private static int columnLenBook = 6;
        private static int columnLenReaderBook = 4;


        public static List<Reader> CheckDataReader(string path)
        {
            int count = 1;
            foreach(string line in File.ReadAllLines(path, Encoding.Default))
            {
                string[] data = line.Split(';');
                Reader reader = new Reader();

                CheckSize(data.Length, columnLenReader, path);

                reader.Id = CheckReaderId(count, data, path);
                reader.FullName = CheckStringLine(count, 2, data[1], path);
                
                readers.Add(reader);
                count++;
            }
            return readers;
        }

        static uint CheckReaderId(int count, string[] data, string path)
        {
            if (uint.TryParse(data[0], out uint id))
            {
                if (idReader.Contains(id))
                {
                    throw new Exception($"Ошибка в файле <{path}>, в строке номер {count}, столбец номер 1. Описание ошибки: повторяющийся ID");
                }
                idReader.Add(id);
                return id;
            }
            else
            {
                throw new Exception($"Ошибка в файле <{path}>, в строке номер {count}, столбец номер 1. Описание ошибки: некорректное ID. ID должно быть целым, уникальным, положительным числом");
            }
        }

        static string CheckStringLine(int count, int column, string line, string path)
        {
            if (line == "")
            {
                throw new Exception($"Ошибка в файле <{path}>, в строке номер {count}, столбце номер {column}. Описание ошибки: отсутствует имя читателя");
            }
            return line;
        }

        public static List<Book> GetBookData(string path)
        {
            int count = 1;
            foreach (string line in File.ReadAllLines(path, Encoding.Default))
            {
                string[] data = line.Split(';');
                Book book = new Book();

                CheckSize(data.Length, 6, path);

                book.Id = CheckBookId(count, data, path);
                book.Author = CheckStringLine(count, 2, data[1], path);
                book.Name = CheckStringLine(count, 3, data[2], path);
                book.PublicationDate = CheckBookPublicationDate(count, data, path);
                book.СaseNumber = CheckBookСaseNumber(count, data, path);
                book.ShelfNumber = CheckBookShelfNumber(count, data, path);

                books.Add(book);
                count++;
            }
            return books;
        }

        static uint CheckBookId(int count, string[] data, string path)
        {
            if (uint.TryParse(data[0], out uint id))
            {
                if (idBook.Contains(id))
                {
                    throw new Exception($"Ошибка в файле <{path}>, в строке номер {count}, столбце номер 1. Описание ошибки: повторяющийся ID");
                }
                idBook.Add(id);
                return id;
            }
            else
            {
                throw new Exception($"Ошибка в файле <{path}>, в строке номер {count}, столбце номер 1. Описание ошибки: некорректное ID. ID должно быть целым, уникальным, положительным числом.");
            }
        }

        static uint CheckBookPublicationDate(int count, string[] data, string path)
        {
            if (uint.TryParse(data[3], out uint publicationDate))
            {
                if (publicationDate > 1500 && publicationDate < 2024)
                {
                   return publicationDate;
                }
                else
                {
                    throw new Exception($"Ошибка в файле <{path}>, в строке номер {count}, столбце номер 4. Описание ошибки: некорректный год издания книги");
                }
            }
            else
            {
                throw new Exception($"Ошибка в файле <{path}>, в строке номер {count}, столбце номер 4. Описание ошибки: некорректный год издания книги");
            }
        }

        static uint CheckBookСaseNumber(int count, string[] data, string path)
        {
            if (uint.TryParse(data[4], out uint caseNumber))
            {
                return caseNumber;
            }
            else
            {
                throw new Exception($"Ошибка в файле <{path}>, в строке номер {count}, столбце номер 5. Описание ошибки: некорректный номер шкафа");
            }
        }

        static uint CheckBookShelfNumber(int count, string[] data, string path)
        {
            if (uint.TryParse(data[5], out uint shelfNumber))
            {
                return shelfNumber;
            }
            else
            {
                throw new Exception($"Ошибка в файле <{path}>, в строке номер {count}, столбце номер 6. Описание ошибки: некорректный номер полки ");
            }
        }

        public static List<ReaderBook> GetReaderBookData(string path)
        {
            List<ReaderBook> readerBooks = new List<ReaderBook>();
            int count = 1;
            foreach (string line in File.ReadAllLines(path, Encoding.Default))
            {
                string[] data = line.Split(';');
                ReaderBook readerBook = new ReaderBook();

                CheckSize(data.Length, 4, path);

                readerBook.Reader = CheckReader(count, data[0], path);
                readerBook.Book = CheckBook(count, data[1], path);
                readerBook.TakeDate = CheckDate(count, data[2], path, 3);
                readerBook.ReturnDate = CheckDate(count, data[3], path, 4);

                CheckTakeReturnTime(readerBook.TakeDate, readerBook.ReturnDate, path, count);

                readerBooks.Add(readerBook);
                count++;
            }
            return readerBooks;
        }

        static Reader CheckReader(int count, string readerId, string path)
        {
            if (!uint.TryParse(readerId, out uint id))
            {
                throw new Exception($"Ошибка в файле <{path}>, в строке номер {count}, в столбце номер 1. Описание ошибки: некорректный ID читателя");
            }

            foreach (Reader reader in readers)
            {
                if(reader.Id == id)
                {
                    return reader;
                }
            }
            throw new ArgumentException($"Ошибка в файле <{path}>, в строке номер {count}, в столбце номер 1. Описание ошибки: введен несуществующий ID читателя");
        }

        static Book CheckBook(int count, string bookId, string path)
        {
            if (!uint.TryParse(bookId, out uint id))
            {
                throw new Exception($"Ошибка в файле <{path}>, в строке номер {count}, в столбце номер 2. Описание ошибки: некоррекнтый ID книги");
            }

            foreach (Book book in books)
            {
                if (book.Id == id)
                {
                    return book;
                }
            }
            throw new ArgumentException($"Ошибка в файле <{path}>, в строке номер {count}, в столбце номер 2, Описание ошибки: введен несуществующий ID книги");
        }

        static DateTime CheckDate(int count, string date, string path, int column)
        {
            if (column == 4 && date == "")
            {
                return DateTime.MinValue;
            }
            if (DateTime.TryParse(date, out DateTime dateTime))
            {
                return dateTime;
            }
            else
            {
                throw new Exception($"Ошибка в файле <{path}>, в строке номер {count}, в столбце номер {column}. Описание ошибки: некоррекнтная дата");
            }
        }

        static void CheckSize(int rightLen, int len, string path)
        {
            if (rightLen != len)
            {
                throw new Exception($"Количество столбцов в файле <{path}> не соответствует заданому стандарту");
            }
        }

        static void CheckTakeReturnTime(DateTime takeTime, DateTime returnTime, string path, int count)
        {
            if (returnTime == DateTime.MinValue)
                return;
            if(takeTime > returnTime)
                throw new Exception($"Некорректные даты в файле {path}, строка {count}, 3 и 4 столбец. Описание ошибки: дата выдачи не может быть позже даты сдачи книги");
        }
    }
}
