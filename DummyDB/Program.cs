using System.IO;

namespace DummyDB
{
    class Program
    {
        static void Main()
        {
            ///"Data\\Reader.csv"
            List<Reader> readers = new List<Reader>();
            List<Book> books = new List<Book>();
            List<ReaderBook> readerBooks = new List<ReaderBook>();

            readers = Check.CheckDataReader("Data\\Reader.csv");
        }
    }
    class Check
    {
        public static List<uint>? idReader = new List<uint>();
        public static List<uint>? idBook = new List<uint>();
        public static List<Reader> CheckDataReader(string path)
        {
            List<Reader> readers = new List<Reader>();
            int count = 1;
            foreach(string line in File.ReadAllLines(path))
            {
                string[] data = line.Split(';');
                Reader reader = new Reader();
                
                if (uint.TryParse(data[0], out uint num))
                {
                    if (idReader.Contains(num))
                    {
                        Console.WriteLine($"Ошибка в файле <{path}>, в строке номер {count}");
                        Console.WriteLine($"Описание ошибки: повторяющийся ID");
                        throw new Exception("Проверьте корретность данных в файле");
                    }
                    else
                    {
                        reader.Id = num;
                        idReader.Add(num);
                    }
                }
                else
                {
                    Console.WriteLine($"Ошибка в файле <{path}>, в строке номер {count}");
                    Console.WriteLine($"Описание ошибки: некорректное ID. ID должно быть целым, уникальным, положительным числом.");
                    throw new Exception("Проверьте корретность данных в файле (ID)");
                }

                reader.FullName = data[1];

                if(data.Length > 2)
                {
                    Console.WriteLine($"Ошибка в файле <{path}>");
                    Console.WriteLine($"Описание ошибки: количество столбцов в файле не соответствует заданому стандарту");
                    throw new Exception($"Проверьте корретность данных в файле <{path}>");
                }
                readers.Add(reader);
                count++;
            }
            return readers;
        }

        public static List<Book> CheckDataBook(string path)
        {
            List<Book> books = new List<Book>();
            
            int count = 1;

            foreach (string line in File.ReadAllLines(path))
            {
                string[] data = line.Split(';');
                if (data.Length != 6)
                {
                    Console.WriteLine($"Ошибка в файле <{path}>");
                    Console.WriteLine($"Описание ошибки: количество столбцов в файле не соответствует заданному стандарту");
                    throw new Exception($"Проверьте корретность данных в файле <{path}>");
                }

                Book book = new Book();
                book.Id = CheckBookId(count, data, path);
                book.Author = CheckBookAuthor(count, data[1], path);
                book.Name = CheckBookName(count, data[2], path);
                book.PublicationDate = CheckBookPublicationDate(count, data, path);
                book.СaseNumber = CheckBookСaseNumber(count, data, path);
                book.ShelfNumber = CheckBookShelfNumber(count, data, path);

                books.Add(book);
            }
            return books;
        }

        public static uint CheckBookId(int count, string[] data, string path)
        {
            uint id;
            if (uint.TryParse(data[0], out uint num))
            {
                if (idBook.Contains(num))
                {
                    Console.WriteLine($"Ошибка в файле <{path}>, в строке номер {count}, столбце номер 1");
                    Console.WriteLine($"Описание ошибки: повторяющийся ID");
                    throw new Exception("Проверьте корретность данных в файле");
                }
                else
                {
                    id = num;
                    idBook.Add(id);
                }
            }
            else
            {
                Console.WriteLine($"Ошибка в файле <{path}>, в строке номер {count}, столбце номер 1");
                Console.WriteLine($"Описание ошибки: некорректное ID. ID должно быть целым, уникальным, положительным числом.");
                throw new Exception("Проверьте корретность данных в файле (ID)");
            }
            return id;
        }
        
        public static string CheckBookAuthor(int count, string author, string path)
        {
            if(author == "")
            {
                Console.WriteLine($"Ошибка в файле <{path}>, в строке номер {count}, столбце номер 2");
                Console.WriteLine($"Описание ошибки: Отсутствует имя автора");
                throw new Exception($"Проверьте корретность данных в файле <{path}>");
            }
            else
            {
                return author;
            }
        }

        public static string CheckBookName(int count, string name, string path)
        {
            if (name == "")
            {
                Console.WriteLine($"Ошибка в файле <{path}>, в строке номер {count}, столбце номер 3");
                Console.WriteLine($"Описание ошибки: Отсутствует название произведения");
                throw new Exception($"Проверьте корретность данных в файле <{path}>");
            }
            else
            {
                return name;
            }
        }

        public static uint CheckBookPublicationDate(int count, string[] data, string path)
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
                    Console.WriteLine($"Ошибка в файле <{path}>, в строке номер {count}, столбце номер 4");
                    Console.WriteLine($"Описание ошибки: Некорректный год издания книги");
                    throw new Exception($"Проверьте корретность данных в файле <{path}>");
                }
            }
            else
            {
                Console.WriteLine($"Ошибка в файле <{path}>, в строке номер {count}, столбце номер 4");
                Console.WriteLine($"Описание ошибки: Некорректный год издания книги");
                throw new Exception($"Проверьте корретность данных в файле <{path}>");
            }
            return publicationDate;
        }

        public static uint CheckBookСaseNumber(int count, string[] data, string path)
        {
            uint caseNumber;
            if (uint.TryParse(data[4], out uint num))
            {
                caseNumber = num;
            }
            else
            {
                Console.WriteLine($"Ошибка в файле <{path}>, в строке номер {count}, столбце номер 5");
                Console.WriteLine($"Описание ошибки: Некорректный номер шкафа");
                throw new Exception($"Проверьте корретность данных в файле <{path}>");
            }
            return caseNumber;
        }

        public static uint CheckBookShelfNumber(int count, string[] data, string path)
        {
            uint shelfNumber;
            if (uint.TryParse(data[5], out uint num))
            {
                shelfNumber = num;
            }
            else
            {
                Console.WriteLine($"Ошибка в файле <{path}>, в строке номер {count}, столбце номер 6");
                Console.WriteLine($"Описание ошибки: Некорректный номер полки");
                throw new Exception($"Проверьте корретность данных в файле <{path}>");
            }
            return shelfNumber;
        }
    }
}
