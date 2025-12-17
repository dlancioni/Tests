using System.Diagnostics;
public static class Tasks
{
    public async static Task InvokeArrayOfTasks()
    {
        Stopwatch sw = Stopwatch.StartNew();

        int[] input = { 100, 100 };      

        Console.WriteLine($"Total de tasks {input.Length}");

        Task<long>[] tasks = input.Select(n => 
        {
            Console.WriteLine($"Agendando cálculo de F({n})...");
            return Task.Run(() => Utils.RunCpuIntensiveTask(n));
        }).ToArray();

        long[] results = await Task.WhenAll(tasks);

        Parallel.Invoke(
            () => Utils.RecursiveFibonacci(40),
            () => Utils.RecursiveFibonacci(40),
            () => Utils.RecursiveFibonacci(40),
            () => Utils.RecursiveFibonacci(40)
        );

        sw.Stop();
        Console.WriteLine($"Elapsed time {sw.Elapsed.TotalSeconds:F3} segundos.");
        Console.WriteLine("Done");
    }    
}
