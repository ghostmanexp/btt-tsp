using ConsoleApp1.v1;
using ConsoleApp1.v2;

namespace ConsoleApp1;

public static class RunFactory
{
    /// <summary>
    /// A função Create da classe RunFactory é responsável por criar instâncias de diferentes implementações do
    /// algoritmo TSP (Traveling Salesman Problem) com base na versão especificada (v1 ou v2).
    /// Ela recebe como parâmetros a versão, uma lista de pontos e uma matriz de tempo, retornando uma instância que
    /// implementa a interface IRunFactory. Caso a versão seja inválida, uma exceção ArgumentException é lançada.
    /// </summary>
    /// <param name="version">Versão do algoritmo (v1 ou v2)</param>
    /// <param name="points">Lista de pontos</param>
    /// <param name="timeMatrix">Matriz de tempo</param>
    /// <returns>Instância de IRunFactory</returns>
    /// <exception cref="ArgumentException">Lança uma exceção se a versão não for válida</exception>
    /// <remarks>Cria uma instância de IRunFactory com base na versão especificada</remarks>
    /// <example>
    /// <code>
    /// var runInstance = RunFactory.Create("v1", pontos, tempoMatriz);
    /// runInstance.Execute();
    /// </code>
    /// </example>
    /// <remarks>
    /// Esta classe é responsável por criar instâncias de diferentes versões do algoritmo TSP.
    /// </remarks>
    /// <remarks>
    /// Esta implementação é útil para manter o código organizado e permitir a fácil adição de novas versões no futuro.
    /// </remarks>
    public static IRunFactory Create(string version, string[] points, int[,] timeMatrix)
    {
        return version switch
        {
            "v1" => new RunV1(points, timeMatrix),
            "v2" => new RunV2(points, timeMatrix),
            _ => throw new ArgumentException("Versão inválida. Use 'v1' ou 'v2'.")
        };
    }
}