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
                yield return new Person { Id = 5, Name = "Vasea", PhoneNumber = "564613165465", PaidForCars = 25000 };
                yield return new Person { Id = 4, Name = "Petrica", PhoneNumber = "15646465465", PaidForCars = 35000 };
                yield return new Person { Id = 1, Name = "Oleg", PhoneNumber = "564684646", PaidForCars = 27000 };
                yield return new Person { Id = 3, Name = "Valera", PhoneNumber = "46546465456", PaidForCars = 29000 };
                yield return new Person { Id = 2, Name = "Tolea", PhoneNumber = "6546464646", PaidForCars = 22000 };
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
                yield return new Car { Id = 3, Name = "MERCEDES", RegistryNumber = "IWA 750", EngineVolume = 3.2, MaxSpeed = 250
                                      ,Proprietars = new List<Person> { person.ElementAt(3)
                                      ,person.ElementAt(2)
                                      ,person.ElementAt(4) } };
                yield return new Car { Id = 4, Name = "AUDI", RegistryNumber = "KCL 357", EngineVolume = 6.6, MaxSpeed = 290
                                      ,Proprietars = new List<Person> { person.ElementAt(3) } };
                yield return new Car { Id = 5, Name = "VW", RegistryNumber = "WPS 635", EngineVolume = 4.0, MaxSpeed = 220
                                      ,Proprietars = new List<Person> { person.ElementAt(4)
                                      ,person.ElementAt(3)
                                      ,person.ElementAt(2) } };
            }
            Func<IEnumerable<Car>> cars = Cars;
            var car = cars();
            
            //FILTERING

            var personView_1 = person.Where(p => p.PaidForCars >= 27000);
            Console.WriteLine(personView_1.Count());
            foreach (var a in personView_1)
                Console.WriteLine(a.Name);
            Console.WriteLine("=============================================");

            var personView_2 = person.Where(p => p.PaidForCars >= 27000 && p.Name.StartsWith("V"));            
            foreach (var a in personView_2)
                Console.WriteLine(a.Name);
            Console.WriteLine("=============================================");

            var personView_3 = person.Take(2);           
            foreach (var a in personView_3)
                Console.WriteLine(a.Name);
            Console.WriteLine("=============================================");

            var personView_4 = person.Skip(4);
            foreach (var a in personView_4)
                Console.WriteLine(a.Name);
            Console.WriteLine("=============================================");

            var personView_5 = person.SkipLast(3);
            foreach (var a in personView_5)
                Console.WriteLine(a.Name);
            Console.WriteLine("=============================================");

            var personView_6 = person.SkipWhile(p => p.PaidForCars < 27000);
            foreach (var a in personView_6)
                Console.WriteLine(a.Name);
            Console.WriteLine("=============================================");

            var personView_7 = person.TakeWhile(p => p.PaidForCars < 27000);
            foreach (var a in personView_7)
                Console.WriteLine(a.Name);
            Console.WriteLine("=============================================");

            //PROJECTION

            var personCar_2 = car.Select(c => c.Name);            
            foreach (var a in personCar_2)
                Console.WriteLine(a);
            Console.WriteLine("=============================================");

            var personCar_3 = car.Select(c => new { CarName = c.Name, RegistryNumber = c.RegistryNumber, EngineVolume = c.EngineVolume });            
            foreach (var a in personCar_3)
                Console.WriteLine(a);
            Console.WriteLine("=============================================");

            var personCar_4 = car.SelectMany(c => c.Proprietars, (i, j) => new { CarName = i.Name, ProprietarName = j.Name });            
            foreach (var a in personCar_4)
                Console.WriteLine(a.CarName + " - " + a.ProprietarName);
            Console.WriteLine("=============================================");

            var personCar_5 = car.SelectMany(c => c.Proprietars.Where(n => n.Name.StartsWith("V")), (i, j) => new { CarName = i.Name, ProprietarName = j.Name });
            Console.WriteLine(personCar_5.Count());
            foreach (var a in personCar_5)
                Console.WriteLine(a.CarName + " - " + a.ProprietarName);
            Console.WriteLine("=============================================");

            var personCar_6 = car.Select(c => new { carNames = c.Name, ProprietarsName = c.Proprietars.Select(s => s.Name) });
            foreach (var a in personCar_6)
                Console.WriteLine(a.carNames + " - " + string.Join(" , ", a.ProprietarsName));
            Console.WriteLine("=============================================");

            //JOINING

            var personCar_1 = car.Join(person, c => c.Id, p => p.Id, (c, p) => new { PersonName = p.Name, CarName = c.Name });
            Console.WriteLine(personCar_1.Count());
            foreach (var a in personCar_1)
                Console.WriteLine(a.PersonName + " - " + a.CarName);
            Console.WriteLine("=============================================");            

            var personCar_7 = person.Zip(car, (p, c) => new { carName = c.Name, p.PaidForCars });
            foreach (var a in personCar_7)
                Console.WriteLine(a.carName + " - " + a.PaidForCars);
            Console.WriteLine("=============================================");

            //ORDERING

            var personView_8 = person.OrderBy(p => p.Name);
            foreach (var a in personView_8)
                Console.WriteLine(a.Name + " - " + a.PaidForCars);
            Console.WriteLine("=============================================");

            var personView_9 = person.OrderBy(p => p.Name).ThenBy(p => p.PaidForCars);
            foreach (var a in personView_9)
                Console.WriteLine(a.Name + " - " + a.PaidForCars);
            Console.WriteLine("=============================================");

            //GROUPING

            var carView_9 = car.GroupBy(c => c.MaxSpeed);
            foreach (var a in carView_9)
            {
                Console.WriteLine(a.Key);
                foreach (var b in a)
                    Console.WriteLine(b.Name);
            }
            Console.WriteLine("=============================================");

            //GROUPING

            var carView_10 = car.Concat(car);
            foreach (var a in carView_10)
                Console.WriteLine(a.Name);
            Console.WriteLine("=============================================");

            var carView_11 = carView_10.Union(carView_10);
            foreach (var a in carView_11)
                Console.WriteLine(a.Name);
            Console.WriteLine("=============================================");

            //Conversion Methods

            var carView_12 = car.ToList();
            foreach (var a in carView_12)
                Console.WriteLine(a.Name);
            Console.WriteLine("=============================================");

            //Element Operators

            var carView_13 = car.FirstOrDefault();
            Console.WriteLine(carView_13.Name);
            Console.WriteLine("=============================================");

            carView_11.ToList().Clear();
            var carView_14 = carView_11.DefaultIfEmpty();
            Console.WriteLine(carView_14.First().Name = "JIGULI");
            Console.WriteLine("=============================================");

            //Aggregation Methods

            var personView_10 = person.Select(p => p.PaidForCars).Aggregate((i, j) => i + j);
            Console.WriteLine(personView_10);
            Console.WriteLine("=============================================");

            var personView_11 = person.Count();
            Console.WriteLine(personView_11);
            Console.WriteLine("=============================================");

            var personView_12 = person.Count(p => p.PaidForCars > 25000);
            Console.WriteLine(personView_12);
            Console.WriteLine("=============================================");

            //Quantifiers

            var carView_15 = car.Select(c => c.Name).Contains("BMW");
            Console.WriteLine(carView_15);
            Console.WriteLine("=============================================");

            //Generation Methods     
            
            var carView_16 = Enumerable.Range(0, 30);
            foreach (var a in carView_16)
                Console.Write(a);
            Console.WriteLine("\n=============================================");

            var carView_17 = Enumerable.Repeat("*", 20);
            foreach (var a in carView_17)
                Console.Write(a);
            Console.WriteLine("\n=============================================");

            var carView_18 = Enumerable.Empty<int>();
            Console.WriteLine(carView_18.Count());
            Console.WriteLine("=============================================");

            //Closure

            Func<int, int> func = Car.GetAFunc().Item1;
            int t = Car.GetAFunc().Item2;
            Console.WriteLine(t);
            Console.WriteLine(func(5));
            int t1 = Car.GetAFunc().Item2;
            Console.WriteLine(t1);
            Console.WriteLine(func(6));            
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

        public static (Func<int, int>, int) GetAFunc()
        {
            var myVar = 1;
            Func<int, int> inc = delegate (int var1) {
                myVar = myVar + 1;
                return var1 + myVar;
            };
            var u = (inc, myVar);
            return u;
        }
        //public static Func<int, int> GetAFunc1()
        //{
        //    var myVar = 1;
        //    Func<int, int> inc = delegate (int var1) {
        //        myVar = myVar + 1;
        //        return var1 + myVar;
        //    };
        //    return inc;
        //}

    }
    class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int PaidForCars { get; set; }
    }
}
