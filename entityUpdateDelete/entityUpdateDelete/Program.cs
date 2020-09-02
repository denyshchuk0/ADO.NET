using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace entityUpdateDelete
{
    class Program
    {
        static void Main(string[] args)
        {
            UniversityEntities context = new UniversityEntities();

            foreach (var item in context.Students)
            {
                Console.WriteLine($"{item.Name} {item.Surname} -- {item.Group.Name}");
            }

            //Console.WriteLine("enter name");
            //string name = Console.ReadLine();
            //context.Students.Remove(context.Students.FirstOrDefault(x=>x.Name == name));
            //context.SaveChanges();

            //foreach (var item in context.Students)
            //{
            //    Console.WriteLine($"{item.Name} {item.Surname} -- {item.Group.Name}");
            //}

            //Console.WriteLine("enter group");
            //string group = Console.ReadLine();
            //var students = context.Students.FirstOrDefault(x => x.Name == "Alina");
            //students.Group.Name = group;
            //context.SaveChanges();

            //foreach (var item in context.Students)
            //{
            //    Console.WriteLine($"{item.Name} {item.Surname} -- {item.Group.Name}");
            //}
            //Console.WriteLine();

            Console.WriteLine("Вивести всіх студентів групи А123");
            foreach (var item in context.Students.Where(x => x.Group.Name == "B123"))
            {
                Console.WriteLine($"{item.Name} {item.Surname} -- {item.Group.Name}");
            }

            Console.WriteLine("Знайти кількість студентів у групах Р123 та Р456 разом");
            Console.WriteLine(context.Students.Where(x => x.Group.Name == "B123" || x.Group.Name == "Р456").Count());

            Console.WriteLine("Знайти студента з максимальною оцінкою по предмету С++");
            var point = context.Achievements.Where(x => x.Subject.Name == "C#").Select(x=>x.Student.Surname).Max();

         
            Console.WriteLine(point);

            Console.WriteLine("Вивести всі предмети, які читає Андрій Трофімчук");
            var teachers = context.TeachersGroups.Where(y => y.Teacher.Name == "Andrii" && y.Teacher.Surname == "Trofimchuk").Select(x=>x.Subject);
            
            foreach (var item in teachers)
            {
                Console.WriteLine($"{item.Name}");
            }

            Console.WriteLine("Знайти, скільки студентів з іменем Оля є в БД");
            Console.WriteLine(context.Students.Where(x => x.Name == "Sergey").Count());

            Console.WriteLine("Студенту з мінімальною оцінкою по предмету С# змінити прізвище");
            var stud = context.Achievements.Where(x => x.Subject.Name == "C#").Select(x => x.Student.Id).Min();
            var students = context.Students.FirstOrDefault(x => x.Id == stud);
            students.Surname = "COMPLITE";
            context.SaveChanges();

        }
    }
}
