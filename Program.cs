using System;
using System.Collections.Generic;
using System.Linq;

namespace Advanced_LINQ
{    
    class Program
    {
        static void Main(string[] args)
        {            
            IEnumerable<Person> Persons()
            {
                yield return new Person { Id = 5, Name = "Vasea", PhoneNumber = "564613165465", Paid = 25000 };
                yield return new Person { Id = 4, Name = "Petrica", PhoneNumber = "15646465465", Paid = 22000 };
                yield return new Person { Id = 1, Name = "Oleg", PhoneNumber = "564684646", Paid = 27000 };
                yield return new Person { Id = 3, Name = "Valera", PhoneNumber = "46546465456", Paid = 29000 };
                yield return new Person { Id = 2, Name = "Tolea", PhoneNumber = "6546464646", Paid = 35000 };
            }            
            Func<IEnumerable<Person>> persons = Persons;
            var person = persons();

            IEnumerable<Car> Cars()
            {
                yield return new Car { Id = 1, Name = "BMW", RegistryNumber = "CPK 722", EngineVolume = 2.0, MaxSpeed = 250
                                      , Proprietars = new List<Person> { person.ElementAt(1)
                                      ,person.ElementAt(4)
                                      ,person.ElementAt(0) } };
                yield return new Car { Id = 2, Name = "OPEL", RegistryNumber = "CKJ 229", EngineVolume = 2.8, MaxSpeed = 220
                                      ,Proprietars = new List<Person> { person.ElementAt(2)
                                      ,person.ElementAt(4) } };
                yield return new Car { Id = 3, Name = "MERCEDES", RegistryNumber = "IWA 750", EngineVolume = 3.2, MaxSpeed = 270
                                      ,Proprietars = new List<Person> { person.ElementAt(3)
                                      ,person.ElementAt(2)
                                      ,person.ElementAt(4) } };
                yield return new Car { Id = 4, Name = "AUDI", RegistryNumber = "KCL 357", EngineVolume = 6.6, MaxSpeed = 290
                                      ,Proprietars = new List<Person> { person.ElementAt(3) } };
                yield return new Car { Id = 5, Name = "VW", RegistryNumber = "WPS 635", EngineVolume = 4.0, MaxSpeed = 350
                                      ,Proprietars = new List<Person> { person.ElementAt(4)
                                      ,person.ElementAt(3)
                                      ,person.ElementAt(2) } };
            }
            Func<IEnumerable<Car>> cars = Cars;
            var car = cars();            

            var personCar_1 = car.Join(person, c => c.Id, p => p.Id, (c, p) => new { PersonName = p.Name, CarName = c.Name });
            Console.WriteLine(personCar_1.Count());
            foreach(var a in personCar_1)
                Console.WriteLine(a.PersonName + " - " + a.CarName);
            Console.WriteLine("=============================================");
            var personCar_2 = car.Select(c => c.Name);
            Console.WriteLine(personCar_2.Count());
            foreach (var a in personCar_2)
                Console.WriteLine(a);
            Console.WriteLine("=============================================");
            var personCar_3 = car.Select(c => new { CarName = c.Name, RegistryNumber = c.RegistryNumber, EngineVolume = c.EngineVolume });
            Console.WriteLine(personCar_3.Count());
            foreach (var a in personCar_3)
                Console.WriteLine(a);
            Console.WriteLine("=============================================");
            var personCar_4 = car.Select(c => c.Proprietars.Where(p => p.Name.StartsWith("V")));
            Console.WriteLine(personCar_4.Count());
            foreach (var a in personCar_4)
                Console.WriteLine(a.);
            Console.WriteLine("=============================================");

        }
    }
    class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RegistryNumber { get; set; }
        public double EngineVolume { get; set; }
        public int MaxSpeed { get; set; }
        public List<Person> Proprietars { get; set; }
    }
    class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int Paid { get; set; }
    }
}
