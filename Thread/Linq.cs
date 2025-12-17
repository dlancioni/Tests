using System.CodeDom;
using System.Diagnostics;
using System.Runtime.InteropServices.Swift;
public class Linq
{
    List<Person> people = new List<Person>();
    List<Category> categories = new List<Category>();    

    public Linq()
    {
        people.Add(new Person() { Name = "Nome 1", Salary = 100, Category = 1});
        people.Add(new Person() { Name = "Nome 2", Salary = 100, Category = 1 });
        people.Add(new Person() { Name = "Nome 3", Salary = 200, Category = 2 });
        people.Add(new Person() { Name = "Nome 4", Salary = 200, Category = 2 });
        people.Add(new Person() { Name = "Nome 5", Salary = 300, Category = 3 });

        categories.Add(new Category() { Id = 1, Name = "Categoria 1"});
        categories.Add(new Category() { Id = 2, Name = "Categoria 2"});        
    }

    public void Grouping()
    {
        var salaryByCategory = people.AsParallel().GroupBy(p => p.Category)
        .Select(group => new 
        {
            Categoria = group.Key,
            TotalSalario = group.Sum(p => p.Salary),
            Count = group.Count()
        })
        .OrderBy(r => r.Categoria);

        foreach (var item in salaryByCategory)
        {
            Console.WriteLine($"Categoria {item.Categoria}: R$ {item.TotalSalario:N2} (Total de {item.Count} pessoas)");
        }
    }  

    public void Join()
    {
        var results = from person in people        // Para cada 'person' na lista 'people'
                      join cat in categories       // Faça um JOIN com a lista 'categories'
                      on person.Category equals cat.Id // Onde person.Category é igual a cat.Id
                      select new                   // Projete um novo objeto anônimo contendo:
                      {
                          PersonName = person.Name,
                          CategoryName = cat.Name
                      };

        Console.WriteLine("Resultado do JOIN:");
        Console.WriteLine("---------------------");

        foreach (var item in results)
        {
            Console.WriteLine($"Pessoa: {item.PersonName}, Categoria: {item.CategoryName}");
        }
    }      

}
