using ConsoleApp1.Common;

namespace ConsoleApp1.v2;

public class RunV2(string[] points, int[,] timeMatrix) : IRunFactory
{
    /// <summary>
    /// Executa um teste e exibe os resultados.
    /// </summary>
    public void Execute()
    {
        Console.WriteLine($"=== Excecutando NN TSP (v2) ===");
        Console.WriteLine("Método Exato (Programação Dinâmica):");

        // Solução exata usando programação dinâmica
        var exactSolver = new Common.TSP(points, timeMatrix);
        var exactResult = exactSolver.Solve();

        Console.WriteLine($"Rota ótima: {exactResult.OptimalRoute}");
        Console.WriteLine($"Tempo total: {exactResult.TotalTime}");

        // Solução heurística usando o vizinho mais próximo
        Console.WriteLine("\nMétodo Heurístico (Vizinho Mais Próximo):");
        var heuristic = new NearestNeighborTSP(points, timeMatrix);
        var heuristicResult = heuristic.Solve();

        Console.WriteLine($"Rota encontrada: {heuristicResult.Route}");
        Console.WriteLine($"Tempo total: {heuristicResult.TotalTime}");

        // Diferença percentual entre as soluções
        var difference = ((double)heuristicResult.TotalTime - exactResult.TotalTime) / exactResult.TotalTime * 100;
        Console.WriteLine($"Diferença: {difference:F2}% maior que a solução ótima");

        // Visualização da rota
        Console.WriteLine("\nVisualização da Rota Ótima:");
        var visualization = ASCIIRoute.RouteVisualizer.VisualizeInConsole(points, exactSolver.GetPath(), timeMatrix);
        Console.WriteLine(visualization);

        Console.WriteLine(new string('-', 50));
    }
}