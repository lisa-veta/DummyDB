using System.Text;

namespace DummyDB
{
    class Check
    {
        public static List<uint> idReader = new List<uint>();
        public static List<uint> idBook = new List<uint>();
        public static List<Reader> readers = new List<Reader>();
        public static List<Book> books = new List<Book>();
        public static List<Reader> CheckDataReader(string path)
        {
            int count = 1;
            foreach(string line in File.ReadAllLines(path, Encoding.Default))
            {
                string[] data = line.Split(';');
                Reader reader = new Reader();

                CheckSize(data.Length, 2, path);

                reader.Id = CheckReaderId(count, data, path);
                reader.FullName = CheckReaderFullName(count, data[1], path);
                
                readers.Add(reader);
                count++;
            }
            return readers;
        }

        static uint CheckReaderId(int count, string[] data, string path)
        {
            uint id;
            if (uint.TryParse(data[0], out uint num))
            {
                if (idReader.Contains(num))
                {
                    throw new Exception($"Ошибка в файле <{path}>, в строке номер {count}, столбец номер 1. Описание ошибки: повторяющийся ID");
                }
                else
                {
                    id = num;
                    idReader.Add(id);
                }
            }
            else
            {
                throw new Exception($"Ошибка в файле <{path}>, в строке номер {count}, столбец номер 1. Описание ошибки: некорректное ID. ID должно быть целым, уникальным, положительным числом.");
            }
            return id;
        }

        static string CheckReaderFullName(int count, string fullName, string path)
        {
            if (fullName == "")
            {
                throw new Exception($"Ошибка в файле <{path}>, в строке номер {count}, столбце номер 2. Описание ошибки: Отсутствует имя автора");
            }
            else
            {
                return fullName;
            }
        }

        public static List<Book> CheckDataBook(string path)
        {
            int count = 1;
            foreach (string line in File.ReadAllLines(path, Encoding.Default))
            {
                string[] data = line.Split(';');
                CheckSize(data.Length, 6, path);

                Book book = new Book();
                book.Id = CheckBookId(count, data, path);
                book.Author = CheckBookAuthor(count, data[1], path);
                book.Name = CheckBookName(count, data[2], path);
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
            uint id;
            if (uint.TryParse(data[0], out uint num))
            {
                if (idBook.Contains(num))
                {
                    throw new Exception($"Ошибка в файле <{path}>, в строке номер {count}, столбце номер 1. Описание ошибки: повторяющийся ID");
                }
                else
                {
                    id = num;
                    idBook.Add(id);
                }
            }
            else
            {
                throw new Exception($"Ошибка в файле <{path}>, в строке номер {count}, столбце номер 1. Описание ошибки: некорректное ID. ID должно быть целым, уникальным, положительным числом.");
            }
            return id;
        }
        
        static string CheckBookAuthor(int count, string author, string path)
        {
            if(author == "")
            {
                throw new Exception($"Ошибка в файле <{path}>, в строке номер {count}, столбце номер 2. Описание ошибки: Отсутствует имя автора");
            }
            else
            {
                return author;
            }
        }

        static string CheckBookName(int count, string name, string path)
        {
            if (name == "")
            {
                throw new Exception($"Ошибка в файле <{path}>, в строке номер {count}, столбце номер 3. Описание ошибки: Отсутствует название произведения");
            }
            else
            {
                return name;
            }
        }

        static uint CheckBookPublicationDate(int count, string[] data, string path)
        {
            uint publicationDate;
            if (uint.TryParse(data[3], out uint num))
            {
                if (num > 1500 || num < 2024)
                {
                    publicationDate = num;
                }
                else
                {
                    throw new Exception($"Ошибка в файле <{path}>, в строке номер {count}, столбце номер 4. Описание ошибки: Некорректный год издания книги");
                }
            }
            else
            {
                throw new Exception($"Ошибка в файле <{path}>, в строке номер {count}, столбце номер 4. Описание ошибки: Некорректный год издания книги");
            }
            return publicationDate;
        }

        static uint CheckBookСaseNumber(int count, string[] data, string path)
        {
            uint caseNumber;
            if (uint.TryParse(data[4], out uint num))
            {
                caseNumber = num;
            }
            else
            {
                throw new Exception($"Ошибка в файле <{path}>, в строке номер {count}, столбце номер 5. Описание ошибки: Некорректный номер шкафа");
            }
            return caseNumber;
        }

        static uint CheckBookShelfNumber(int count, string[] data, string path)
        {
            uint shelfNumber;
            if (uint.TryParse(data[5], out uint num))
            {
                shelfNumber = num;
            }
            else
            {
                throw new Exception($"Ошибка в файле <{path}>, в строке номер {count}, столбце номер 6. Описание ошибки: Некорректный номер полки ");
            }
            return shelfNumber;
        }

        public static List<ReaderBook> CheckDataReaderBook(string path)
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
            uint id;
            if (uint.TryParse(readerId, out uint num))
            {
                id = num;
            }
            else
            {
                throw new Exception($"Ошибка в файле <{path}>, в строке номер {count}, в столбце номер 1, некорректный ID читателя");
            }

            foreach (Reader reader in readers)
            {
                if(reader.Id == id)
                {
                    return reader;
                }
            }
            throw new ArgumentException($"Ошибка в файле <{path}>, в строке номер {count}, в столбце номер 1, введен несуществующий читатель");
        }

        static Book CheckBook(int count, string bookId, string path)
        {
            uint id;
            if (uint.TryParse(bookId, out uint num))
            {
                id = num;
            }
            else
            {
                throw new Exception($"Ошибка в файле <{path}>, в строке номер {count}, в столбце номер 2, некоррекнтый ID книги");
            }

            foreach (Book book in books)
            {
                if (book.Id == id)
                {
                    return book;
                }
            }
            throw new ArgumentException($"Ошибка в файле <{path}>, в строке номер {count}, в столбце номер 2, введена несуществующая книга");
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
                throw new Exception($"Ошибка в файле <{path}>, в строке номер {count}, в столбце номер {column}, некоррекнтная дата");
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
                throw new Exception($"Некорректные даты в файле {path}, строка {count}, 3 и 4 столбец. Дата выдачи не может быть позже даты сдачи книги");
        }
    }
}
