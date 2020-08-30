using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace linq
{
    class Good
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }

        public override string ToString()
        {
            return $"{Id}.{Title} | {Price} | {Category}";
        }
    }
    class Program
    {
    static List<Good> goods1 = new List<Good>()
        {
              new Good() { Id = 1, Title = "Nokia 1100", Price = 450.99, Category = "Mobile" },
              new Good() { Id = 2, Title = "Iphone 4", Price = 5000, Category = "Mobile" },
              new Good() { Id = 3, Title = "Refregirator 5000", Price = 2555, Category = "Kitchen" },
              new Good() { Id = 4, Title = "Mixer", Price = 150, Category = "Kitchen" },
              new Good() { Id = 5, Title = "Magnitola", Price = 1499, Category = "Car" }
        };
    static List<Good> goods2 = new List<Good>()
        {
        new Good() { Id = 6, Title = "Samsung Galaxy", Price = 3100, Category = "Mobile" },
        new Good() { Id = 7, Title = "Auto Cleaner", Price = 2300, Category = "Car" },
        new Good() { Id = 8, Title = "Owen", Price = 700, Category = "Kitchen" },
        new Good() { Id = 9, Title = "Siemens Turbo", Price = 3199, Category = "Mobile" },
        new Good() { Id = 10, Title = "Lighter", Price = 150, Category = "Car" }
        };

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            string connectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            DataClasses1DataContext context = new DataClasses1DataContext(connectionString);

            Console.WriteLine("1.Вибрати всі книги, кількість сторінок в яких більше 100");
            var books = context.Books.Where(x => x.Pages > 100);
            ReadBooks(books);

            Console.WriteLine("2. Вибрати всі книги, ім'я яких починається на літеру 'А' або 'а");
            books = context.Books.Where(x => x.Name.ToLower().StartsWith("a"));
            ReadBooks(books);

            Console.WriteLine("3. Вибрати всі книги автора William Shakespeare");
            books = context.Books.Where(x => x.Authors.Name == "William Shakespeare");
            ReadBooks(books);

            Console.WriteLine("5. Вибрати всі книги, ім'я в яких складається менше ніж з 10-ти символів");
            books = context.Books.Where(x => x.Name.Length < 10);
            ReadBooks(books);

            Console.WriteLine("6. Вибрати книгу з максимальною кількістю сторінок не американського автор");
            books = context.Books.Where(x => x.Pages == (context.Books.Select(y => y.Pages)).Max());
            ReadBooks(books);

            Console.WriteLine("7. Вибрати автора, який має найменше книг в базі даних");
            var author = context.Authors.Where(x => x.Books.Count() == context.Authors.Select(y => y.Books.Count()).Min());
            ReadAuthors(author);

            Console.WriteLine("8. Вибрати імена всіх авторів, крім американських, розташованих в алфавітном порядку");
            author = context.Authors.OrderBy(x => x.Name);
            ReadAuthors(author);

            int[] values1 = new int[5] { 1, 10, 5, 13, 4 };
            int[] values2 = new int[5] { 19, 1, 4, 9, 8 };
            Console.WriteLine("1) Посчитать среднее значение четных элементов, которые больше 10"); 
            var avg = values1.Concat(values2).Where(x => x % 2 == 0).Average();
            Console.WriteLine(avg);

            Console.WriteLine("2) Выбрать только уникальные элементы из массивов values1 и values2"); 
            var uniqe = values1.Concat(values2).Distinct();
            foreach(var i in uniqe)
                Console.WriteLine(i);

            Console.WriteLine("3) Найти максимальный элемент массива values2, который есть в массиве values1.");
            var maxElem = values2.Max();
            Console.WriteLine(maxElem);
            
            Console.WriteLine("4) Посчитать сумму элементов массивов values1 и values2, которые попадают в диапазон от 5 до 15."); 
            var sumElem = values1.Concat(values2).Where(x => x >= 5 && x <= 15).Sum();
            Console.WriteLine(sumElem);

            Console.WriteLine("5) Отсортировать элементы массивов values1 и values2 по убыванию."); 
            var sort = values1.Concat(values2).OrderByDescending(x => x);
            foreach(var item in sort)
              Console.Write("{"+item+ "}"+" ");

            List<Good> goods = goods1.Concat(goods2).ToList();
            Console.WriteLine("1) Выбрать товары категории Mobile, цена которых превышает 1000 грн.");
            var mobile = goods.Where(x => x.Category == "Mobile" && x.Price > 1000);
            ReadGoods(mobile);

            Console.WriteLine("2) Вывести название и цену тех товаров, которые не относятся к категории Kitchen цена которых превышает 1000 грн.");
            var noKit = goods.Where(x => x.Category != "Kitchen" && x.Price > 1000);
            ReadGoods(noKit);

            Console.WriteLine("3) Вывести название товара и его категорию, который имеет максимальную цену.");
            var maxPrice = goods.Where(x => x.Price == goods.Select(y => y.Price).Max());
            ReadGoods(maxPrice);

            Console.WriteLine("4) Вычислить среднее значение всех цен товаров.");
            var avgPrice = goods.Average(x => x.Price);
            Console.WriteLine(avg);
            
            Console.WriteLine("5) Вывести список категорий без повторений.");
            var names = goods.Select(x => x.Category).Distinct();
            ReadGoods(names);

            Console.WriteLine("6) Вывести названия тех товаров, цены которых совпадают.");
            var equalPrice = goods.Where(x => goods.Count(y => y.Price == x.Price) > 1);
            ReadGoods(equalPrice);

            Console.WriteLine("7) Вывести названия и категории товаров в алфавитном порядке, упорядоченных по названию.");
            ReadGoods(goods.OrderBy(x => x.Title));

            Console.WriteLine("8) Проверить, содержит ли категория Car товары, с ценой от 1000 до 2000 грн.");
            ReadGoods(goods.Where(x => x.Category == "Car" && (x.Price >= 1000 && x.Price <= 2000)));

            Console.WriteLine("9) Посчитать суммарное количество товаров категорий Сar и Mobile.");
            Console.WriteLine(goods.Where(x => x.Category == "Car" || x.Category == "Mobile").Count());
            
            Console.WriteLine("10) Вывести список категорий и количество товаров каждой категории.");
            var categories = goods.Select(x => x.Category).Distinct();
            foreach (var item in categories)
                Console.WriteLine($"{item} = {goods.Where(x => x.Category == item).Count()}");
        }

        private static void ReadBooks(IQueryable<Books> books)
        {
            foreach (var item in books)
                Console.WriteLine($"{item.Id} | {item.Name} | {item.Desc} | {item.Year} | {item.Pages} | {item.Authors.Name} | {item.Genres.Name}");
        }
        private static void ReadAuthors(IQueryable<Authors> books)
        {
            foreach (var item in books)
                Console.WriteLine($"{item.Id} | {item.Name} ");
        }
     
        private static void ReadGoods(IEnumerable goods)
        {
            foreach (var item in goods)
                Console.WriteLine(item);
        }
    }
    
}