using System;

namespace MediaLibrary
{
    public class EmptyTitleException : Exception
    {
        public EmptyTitleException(string message) : base(message) { }
    }

    public class InvalidRatingException : Exception
    {
        public InvalidRatingException(string message) : base(message) { }
    }

    public class MediaItem
    {
        protected string _title;
        protected int _rating;
        public int Year { get; set; }

        public string Title
        {
            get { return _title; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new EmptyTitleException("Название не может быть пустым.");
                _title = value;
            }
        }

        public int Rating
        {
            get { return _rating; }
            set
            {
                if (value < 1 || value > 10)
                    throw new InvalidRatingException("Оценка должна быть в диапазоне от 1 до 10.");
                _rating = value;
            }
        }

        public MediaItem(string title, int year, int rating)
        {
            Title = title;
            Year = year;
            Rating = rating;
        }

        public virtual void Describe()
        {
            Console.WriteLine($"{GetType().Name}: {Title} ({Year}) - Оценка: {Rating}/10");
        }

        public override string ToString()
        {
            return $"{Title} ({Year}) - {Rating}/10";
        }
    }

    public class Movie : MediaItem
    {
        public string Director { get; set; }
        public int Duration { get; set; }

        public Movie(string title, int year, int rating, string director, int duration)
            : base(title, year, rating)
        {
            Director = director;
            Duration = duration;
        }

        public override void Describe()
        {
            base.Describe();
            Console.WriteLine($"  Режиссер: {Director}, Длительность: {Duration} мин.");
        }
    }

    public class Book : MediaItem
    {
        public string Author { get; set; }
        public int Pages { get; set; }

        public Book(string title, int year, int rating, string author, int pages)
            : base(title, year, rating)
        {
            Author = author;
            Pages = pages;
        }

        public override void Describe()
        {
            base.Describe();
            Console.WriteLine($"  Автор: {Author}, Страниц: {Pages}");
        }
    }

    public class Game : MediaItem
    {
        public string Platform { get; set; }
        public string Studio { get; set; }

        public Game(string title, int year, int rating, string platform, string studio)
            : base(title, year, rating)
        {
            Platform = platform;
            Studio = studio;
        }

        public override void Describe()
        {
            base.Describe();
            Console.WriteLine($"  Платформа: {Platform}, Студия: {Studio}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MediaItem[] items = new MediaItem[]
            {
                new Movie("Inception", 2010, 9, "Christopher Nolan", 148),
                new Movie("The Matrix", 1999, 10, "Lana Wachowski", 136),
                new Movie("Interstellar", 2014, 9, "Christopher Nolan", 169),
                new Book("Dune", 1965, 10, "Frank Herbert", 412),
                new Book("1984", 1949, 9, "George Orwell", 328),
                new Book("The Hobbit", 1937, 8, "J.R.R. Tolkien", 310),
                new Game("The Witcher 3", 2015, 10, "PC", "CD Projekt Red"),
                new Game("Elden Ring", 2022, 9, "PS5", "FromSoftware"),
                new Game("Minecraft", 2011, 8, "PC", "Mojang")
            };

            bool running = true;
            while (running)
            {
                try
                {
                    Console.WriteLine("\nМеню:");
                    Console.WriteLine("1. Полиморфный вывод (Describe)");
                    Console.WriteLine("2. Показать таблицей");
                    Console.WriteLine("3. Добавить позицию");
                    Console.WriteLine("4. Поиск по названию");
                    Console.WriteLine("5. Средняя оценка");
                    Console.WriteLine("0. Выход");
                    Console.Write("Выбор: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            foreach (var item in items)
                            {
                                item.Describe();
                            }
                            break;

                        case "2":
                            Console.WriteLine($"{"Тип",-10} | {"Название",-25} | {"Год",-5} | {"Оценка",-6}");
                            Console.WriteLine(new string('-', 55));
                            foreach (var item in items)
                            {
                                Console.WriteLine($"{item.GetType().Name,-10} | {item.Title,-25} | {item.Year,-5} | {item.Rating,-6}");
                            }
                            break;

                        case "3":
                            Console.Write("Тип (1-Фильм, 2-Книга, 3-Игра): ");
                            string type = Console.ReadLine();
                            Console.Write("Название: ");
                            string title = Console.ReadLine();
                            Console.Write("Год: ");
                            if (!int.TryParse(Console.ReadLine(), out int year)) throw new Exception("Неверный формат года");
                            Console.Write("Оценка (1-10): ");
                            if (!int.TryParse(Console.ReadLine(), out int rating)) throw new Exception("Неверный формат оценки");

                            MediaItem newItem = null;
                            if (type == "1")
                            {
                                Console.Write("Режиссер: ");
                                string director = Console.ReadLine();
                                Console.Write("Длительность (мин): ");
                                if (!int.TryParse(Console.ReadLine(), out int duration)) throw new Exception("Неверный формат длительности");
                                newItem = new Movie(title, year, rating, director, duration);
                            }
                            else if (type == "2")
                            {
                                Console.Write("Автор: ");
                                string author = Console.ReadLine();
                                Console.Write("Страниц: ");
                                if (!int.TryParse(Console.ReadLine(), out int pages)) throw new Exception("Неверный формат страниц");
                                newItem = new Book(title, year, rating, author, pages);
                            }
                            else if (type == "3")
                            {
                                Console.Write("Платформа: ");
                                string platform = Console.ReadLine();
                                Console.Write("Студия: ");
                                string studio = Console.ReadLine();
                                newItem = new Game(title, year, rating, platform, studio);
                            }
                            else
                            {
                                throw new Exception("Неизвестный тип.");
                            }

                            Array.Resize(ref items, items.Length + 1);
                            items[items.Length - 1] = newItem;
                            Console.WriteLine("Добавлено.");
                            break;

                        case "4":
                            Console.Write("Введите подстроку для поиска: ");
                            string query = Console.ReadLine();
                            bool found = false;
                            foreach (var item in items)
                            {
                                if (item.Title.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    item.Describe();
                                    found = true;
                                }
                            }
                            if (!found) Console.WriteLine("Ничего не найдено.");
                            break;

                        case "5":
                            double sum = 0;
                            foreach (var item in items)
                            {
                                sum += item.Rating;
                            }
                            double average = (double)sum / items.Length;
                            Console.WriteLine($"Средняя оценка: {average:F2}");
                            break;

                        case "0":
                            running = false;
                            break;

                        default:
                            Console.WriteLine("Неверный выбор.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }
    }
}