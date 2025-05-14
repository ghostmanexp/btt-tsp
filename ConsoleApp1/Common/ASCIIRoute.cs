namespace ConsoleApp1.Common;

public class ASCIIRoute
{
    /// <summary>
    /// Classe para visualização da rota no console usando caracteres ASCII.
    /// </summary>
    public class RouteVisualizer
    {
        /// <summary>
        /// Cria uma visualização simples da rota usando caracteres ASCII.
        /// </summary>
        /// <param name="points">Lista de pontos</param>
        /// <param name="path">Caminho a ser visualizado</param>
        /// <param name="timeMatrix">Matriz de tempo</param>
        /// <returns>String contendo a visualização</returns>
        public static string VisualizeInConsole(string[] points, List<int> path, int[,] timeMatrix)
        {
            var n = points.Length;
            
            // Criar uma grade simples para a visualização ASCII
            var grid = new string[n * 2 + 1, n * 2 + 1];
            
            // Inicializar a grade com espaços
            for (var i = 0; i < grid.GetLength(0); i++)
            {
                for (var j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i, j] = " ";
                }
            }
            
            // Colocar pontos na grade
            for (var i = 0; i < n; i++)
            {
                var row = i * 2 + 1;
                var col = i * 2 + 1;
                grid[row, col] = points[i];
            }
            
            // Desenhar as conexões do caminho
            for (var i = 0; i < path.Count - 1; i++)
            {
                var from = path[i];
                var to = path[i + 1];
                var time = timeMatrix[from, to];
                
                var fromRow = from * 2 + 1;
                var fromCol = from * 2 + 1;
                var toRow = to * 2 + 1;
                var toCol = to * 2 + 1;
                
                // Desenhar linhas horizontais e verticais
                if (fromRow == toRow) // Mesma linha
                {
                    var minCol = Math.Min(fromCol, toCol);
                    var maxCol = Math.Max(fromCol, toCol);
                    for (var col = minCol + 1; col < maxCol; col++)
                    {
                        grid[fromRow, col] = "-";
                    }
                }
                else if (fromCol == toCol) // Mesma coluna
                {
                    var minRow = Math.Min(fromRow, toRow);
                    var maxRow = Math.Max(fromRow, toRow);
                    for (var row = minRow + 1; row < maxRow; row++)
                    {
                        grid[row, fromCol] = "|";
                    }
                }
                else // Diagonal
                {
                    var rowStep = fromRow < toRow ? 1 : -1;
                    var colStep = fromCol < toCol ? 1 : -1;
                    var row = fromRow + rowStep;
                    var col = fromCol + colStep;
                    while (row != toRow && col != toCol)
                    {
                        grid[row, col] = "\\";
                        row += rowStep;
                        col += colStep;
                    }
                }
                
                // Adicionar o tempo na metade do caminho
                var midRow = (fromRow + toRow) / 2;
                var midCol = (fromCol + toCol) / 2;
                grid[midRow, midCol] = time.ToString();
            }
            
            // Construir a string de visualização
            var sb = new System.Text.StringBuilder();
            for (var i = 0; i < grid.GetLength(0); i++)
            {
                for (var j = 0; j < grid.GetLength(1); j++)
                {
                    sb.Append(grid[i, j]);
                }
                sb.AppendLine();
            }
            
            return sb.ToString();
        }
    }
}