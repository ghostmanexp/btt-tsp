namespace ConsoleApp1.Common;

/// <summary>
/// Solucionador TSP usando a heurística do Vizinho Mais Próximo.
/// Esta implementação é mais rápida, mas não garante a solução ótima.
/// </summary>
public class NearestNeighborTSP
{
    private readonly string[] _points; // Pontos de entrega
    private readonly int[,] _timeMatrix; // Matriz de tempos
    private readonly int n; // Número de pontos
    private readonly List<int> _path; // Caminho encontrado

    /// <summary>
    /// Inicializa o solucionador TSP com pontos e matriz de tempo.
    /// </summary>
    /// <param name="points">Lista de nomes dos pontos de entrega</param>
    /// <param name="timeMatrix">Matriz de adjacência com tempos de deslocamento</param>
    public NearestNeighborTSP(string[] points, int[,] timeMatrix)
    {
        _points = points;
        _timeMatrix = timeMatrix;
        n = points.Length;
        _path = [];
    }

    /// <summary>
    /// Encontra uma rota usando a heurística do Vizinho Mais Próximo.
    /// </summary>
    /// <param name="startPoint">Ponto inicial (padrão 0)</param>
    /// <returns>Tupla contendo (rota_encontrada, tempo_total)</returns>
    public (string Route, int TotalTime) Solve(int startPoint = 0)
    {
        _path.Clear();
            
        // Conjunto de pontos ainda não visitados
        var unvisited = new HashSet<int>();
        for (var i = 0; i < n; i++)
        {
            if (i != startPoint)
                unvisited.Add(i);
        }
            
        // Começa no ponto inicial
        var currentPoint = startPoint;
        _path.Add(currentPoint);
        var totalTime = 0;
            
        // Enquanto houver pontos não visitados
        while (unvisited.Count > 0)
        {
            var nearestPoint = -1;
            var minTime = int.MaxValue;
                
            // Encontra o vizinho mais próximo
            foreach (var point in unvisited.Where(point => _timeMatrix[currentPoint, point] < minTime))
            {
                minTime = _timeMatrix[currentPoint, point];
                nearestPoint = point;
            }
                
            // Atualiza o ponto atual e adiciona ao caminho
            currentPoint = nearestPoint;
            _path.Add(currentPoint);
            unvisited.Remove(currentPoint);
            totalTime += minTime;
        }
            
        // Retorna ao ponto inicial
        totalTime += _timeMatrix[currentPoint, startPoint];
        _path.Add(startPoint);
            
        // Formata a saída
        var route = string.Join(" -> ", _path.Select(i => _points[i]));
        
        // Salva o SVG da rota
        SVGRoute.SaveToFile(_points, _path, _timeMatrix, "NearestNeighborTSP.svg");
            
        return (route, totalTime);
    }

    /// <summary>
    /// Retorna o caminho como uma lista de índices.
    /// </summary>
    public List<int> GetPath()
    {
        return _path;
    }
}