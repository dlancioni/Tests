using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Concurrent; // Adicione este namespace

public class Example
{
   public static void Main(string[] args)
   {
      try {Console.Clear();} catch {}

      var total = 1_000_000_000;
      Random gerador = new Random();

      ConcurrentBag<Person> p1 = new ConcurrentBag<Person>();
      Stopwatch watch = Stopwatch.StartNew();
      Parallel.For(0, total,
                   index => 
                   { 
                        p1.Add(new Person() { Name = $"Nome {index}", Salary = 10 * index, Category = gerador.Next(1, 4) });
                   } );
      watch.Stop();
      Console.WriteLine($"Elapsed time {watch.Elapsed.TotalSeconds:F3} segundos for {p1.Count} ites.");                   

      List<Person> p2 = new List<Person>();
      watch = Stopwatch.StartNew();
      for (int index = 0; index < total; index++)
      {
         p2.Add(new Person() { Name = $"Nome {index}", Salary = 10 * index, Category = gerador.Next(1, 4) });
      };
      watch.Stop();
      Console.WriteLine($"Elapsed time {watch.Elapsed.TotalSeconds:F3} segundos for {p2.Count} ites.");                   
   }   
}
