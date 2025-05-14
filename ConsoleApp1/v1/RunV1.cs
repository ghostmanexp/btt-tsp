namespace ConsoleApp1.v1;

public class RunV1(string[] points, int[,] timeMatrix) : IRunFactory
{
    /// <summary>
    /// Executa um teste e exibe os resultados.
    /// </summary>
    public void Execute()
    {
        Console.WriteLine($"Executando TSP (v1):");

        var solver = new Common.TSP(points, timeMatrix);
        var result = solver.Solve();

        Console.WriteLine($"Rota ótima: {result.OptimalRoute}");
        Console.WriteLine($"Tempo total: {result.TotalTime}");
        Console.WriteLine();
    }
}