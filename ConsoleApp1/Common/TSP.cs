namespace ConsoleApp1.Common;

    /// <summary>
    /// Otimizador de Rotas de Entrega usando programação dinâmica (algoritmo Held-Karp)
    /// para resolver o Problema do Caixeiro Viajante (TSP).
    /// </summary>
    public class TSP
    {
        private readonly string[] _points; // Pontos de entrega
        private readonly int[,] _timeMatrix; // Matriz de tempos
        private readonly int _n; // Número de pontos
        private readonly Dictionary<string, int> _memo; // Para armazenar subproblemas resolvidos
        private readonly Dictionary<string, int> _nextPointMemo; // Para reconstrução do caminho
        private readonly List<int> _path; // Caminho ótimo

        /// <summary>
        /// Inicializa o solucionador TSP com pontos e matriz de tempo.
        /// </summary>
        /// <param name="points">Lista de nomes dos pontos de entrega</param>
        /// <param name="timeMatrix">Matriz de adjacência com tempos de deslocamento</param>
        public TSP(string[] points, int[,] timeMatrix)
        {
            _points = points;
            _timeMatrix = timeMatrix;
            _n = points.Length;
            _memo = new Dictionary<string, int>();
            _nextPointMemo = new Dictionary<string, int>();
            _path = [];
        }

        /// <summary>
        /// Encontra a rota mais eficiente usando programação dinâmica (algoritmo Held-Karp).
        /// </summary>
        /// <returns>Tupla contendo (rota_otima, tempo_total)</returns>
        public (string OptimalRoute, int TotalTime) Solve()
        {
            // Reinicia as estruturas
            _memo.Clear();
            _nextPointMemo.Clear();
            _path.Clear();
            
            // Chamada para a função recursiva, começando no ponto 0 (já visitado, máscara = 1)
            var minCost = SolveRecursive(0, 1);
            
            // Reconstrói o caminho
            ReconstructPath();
            
            // Salva o SVG da rota
            SVGRoute.SaveToFile(_points, _path, _timeMatrix, "TSP.svg");
            
            // Formata a saída como pedido
            var optimalRoute = string.Join(" -> ", _path.Select(i => _points[i]));
            
            return (optimalRoute, minCost);
        }

        /// <summary>
        /// Função recursiva com memorização para resolver o TSP.
        /// </summary>
        /// <param name="pos">Posição atual</param>
        /// <param name="visited">Máscara de bits representando os pontos visitados</param>
        /// <returns>Custo mínimo do caminho</returns>
        private int SolveRecursive(int pos, int visited)
        {
            // Se todos os pontos foram visitados, retorna ao ponto inicial
            if (visited == (1 << _n) - 1)
            {
                return _timeMatrix[pos, 0];
            }
            
            // Cria chave única para este estado
            var key = $"{pos},{visited}";
            
            // Verifica se este subproblema já foi resolvido
            if (_memo.ContainsKey(key))
            {
                return _memo[key];
            }
            
            // Inicializa com um valor grande
            var minCost = int.MaxValue;
            var minNext = -1;
            
            // Tenta visitar cada ponto não visitado
            for (var nextPos = 0; nextPos < _n; nextPos++)
            {
                if ((visited & (1 << nextPos)) == 0) // Se o ponto não foi visitado
                {
                    // Calcula o custo deste caminho
                    var newVisited = visited | (1 << nextPos);
                    var cost = _timeMatrix[pos, nextPos] + SolveRecursive(nextPos, newVisited);
                    
                    // Atualiza se encontrou um caminho melhor
                    if (cost >= minCost) continue;
                    
                    minCost = cost;
                    minNext = nextPos;
                }
            }
            
            // Memoriza este resultado para uso futuro
            _memo[key] = minCost;
            _nextPointMemo[key] = minNext;
            
            return minCost;
        }

        /// <summary>
        /// Reconstrói o caminho ótimo a partir da memorização.
        /// </summary>
        private void ReconstructPath()
        {
            _path.Add(0); // Começa no ponto inicial
            var pos = 0;
            var visited = 1; // Apenas o ponto inicial está visitado
            
            // Reconstrói o caminho passo a passo
            for (var i = 0; i < _n - 1; i++)
            {
                var key = $"{pos},{visited}";
                var nextPos = _nextPointMemo[key];
                _path.Add(nextPos);
                visited |= (1 << nextPos);
                pos = nextPos;
            }
            
            _path.Add(0); // Retorna ao ponto inicial
        }

        /// <summary>
        /// Retorna o caminho como uma lista de índices.
        /// </summary>
        public List<int> GetPath()
        {
            return _path;
        }
    }