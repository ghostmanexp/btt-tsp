namespace ConsoleApp1
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Uso: ConsoleApp1.exe <versão> <caminho do arquivo>");
            }
            
            // Verifica se o arquivo foi especificado e existe
            if (args.Length == 2 && File.Exists(args[1]))
            {
                try
                {
                    // Lê o arquivo especificado
                    var (pontos, tempoMatriz) = LeArquivo(args[1]);
                    Console.WriteLine("Arquivo lido com sucesso.");
                    Console.WriteLine($"Pontos: {string.Join(", ", pontos)}");
                    Console.WriteLine("Matriz de tempo:");
                    for (var i = 0; i < tempoMatriz.GetLength(0); i++)
                    {
                        for (var j = 0; j < tempoMatriz.GetLength(1); j++)
                        {
                            Console.Write($"{tempoMatriz[i, j]} ");
                        }

                        Console.WriteLine();
                    }
                    
                    // Verifica se o número de pontos excede 15
                    if (pontos.Length > 15)
                    {
                        Console.WriteLine("Erro: O número de pontos não pode exceder 15.");
                        return;
                    }

                    // Cria uma instância do solucionador TSP com base na versão especificada
                    var runInstance = RunFactory.Create(args[0], pontos, tempoMatriz);
                    runInstance.Execute();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao processar os arquivos: {ex.Message}");
                }
            }
            else
            {
                if (!File.Exists(args[1]))
                {
                    Console.WriteLine("Arquivo não encontrado ou não especificado.");
                }
                if (args[0] is not "v1" and not "v2")
                {
                    Console.WriteLine("Versão inválida. Use 'v1' ou 'v2'.");
                }
            }

            Console.WriteLine("Pressione qualquer tecla para sair...");
            Console.ReadKey();
        }

        /// <summary>
        /// Lê um arquivo contendo uma lista de pontos e uma matriz de tempo, 
        /// e retorna os dados como uma tupla.
        /// </summary>
        private static (string[] pontos, int[,] tempoMatriz) LeArquivo(string path)
        {
            string[] pontos;
            int[,] tempoMatriz;

            var lines = File.ReadAllLines(path);
            if (lines.Length < 2)
            {
                throw new InvalidDataException(
                    "O arquivo deve conter pelo menos uma linha de pontos e uma matriz de tempo.");
            }

            // A primeira linha contém os pontos
            pontos = lines[0].Split(',');

            var n = pontos.Length;
            tempoMatriz = new int[n, n];
            if (lines.Length - 1 != n)
            {
                throw new InvalidDataException(
                    "O número de linhas na matriz de tempo não corresponde ao número de pontos.");
            }

            // As linhas seguintes contêm a matriz de tempo
            for (var i = 1; i <= n; i++)
            {
                var values = lines[i].Split(',').Select(int.Parse).ToArray();
                for (var j = 0; j < n; j++)
                {
                    tempoMatriz[i - 1, j] = values[j];
                }
            }

            return (pontos, tempoMatriz);
        }
    }
}