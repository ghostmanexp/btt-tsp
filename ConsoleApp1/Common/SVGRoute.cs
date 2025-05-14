using System.Drawing;
using System.Text;

namespace ConsoleApp1.Common;

public class SVGRoute
{

    /// <summary>
    /// Gera uma representação SVG da rota.
    /// </summary>
    /// <param name="points">Lista de pontos</param>
    /// <param name="path">Caminho a ser visualizado</param>
    /// <param name="timeMatrix">Matriz de tempo</param>
    /// <returns>String contendo o SVG</returns>
    private static string GenerateSVG(string[] points, List<int> path, int[,] timeMatrix)
    {
        var n = points.Length;

        // Configurações do SVG
        const int width = 600;
        const int height = 400;
        const int margin = 80;
        const int radius = 20;

        // Calcular posições dos pontos em círculo
        var angleStep = 2 * Math.PI / n;
        var positions = new PointF[n];

        for (var i = 0; i < n; i++)
        {
            var angle = i * angleStep;
            var x = (float)(width / 2 + (width / 2 - margin) * Math.Cos(angle));
            var y = (float)(height / 2 + (height / 2 - margin) * Math.Sin(angle));
            positions[i] = new PointF(x, y);
        }

        // Construir o SVG
        var svg = new StringBuilder();
        svg.AppendLine($"<svg width=\"{width}\" height=\"{height}\" xmlns=\"http://www.w3.org/2000/svg\">");

        // Desenhar as linhas do caminho
        svg.AppendLine("<g stroke=\"#5A6ACF\" stroke-width=\"2\">");

        for (var i = 0; i < path.Count - 1; i++)
        {
            var from = path[i];
            var to = path[i + 1];
            var time = timeMatrix[from, to];

            // Linha do caminho
            svg.AppendLine(
                $"<line x1=\"{positions[from].X}\" y1=\"{positions[from].Y}\" x2=\"{positions[to].X}\" y2=\"{positions[to].Y}\" />");

            // Tempo na metade do caminho
            var midX = (positions[from].X + positions[to].X) / 2;
            var midY = (positions[from].Y + positions[to].Y) / 2;

            // Ajustar posição do texto para evitar sobreposição com a linha
            double dx = positions[to].X - positions[from].X;
            double dy = positions[to].Y - positions[from].Y;
            var angle = Math.Atan2(dy, dx);
            var offsetX = (float)(10 * Math.Sin(angle));
            var offsetY = (float)(-10 * Math.Cos(angle));

            svg.AppendLine(
                $"<text x=\"{midX + offsetX}\" y=\"{midY + offsetY}\" fill=\"black\" text-anchor=\"middle\" font-size=\"12\">{time}</text>");
        }

        svg.AppendLine("</g>");

        // Desenhar os pontos
        svg.AppendLine("<g font-family=\"Arial\" font-size=\"14\">");

        for (var i = 0; i < n; i++)
        {
            var x = positions[i].X;
            var y = positions[i].Y;

            // Círculo para o ponto
            svg.AppendLine(
                $"<circle cx=\"{x}\" cy=\"{y}\" r=\"{radius}\" fill=\"#E6F7FF\" stroke=\"#1890FF\" stroke-width=\"2\" />");

            // Label do ponto
            svg.AppendLine(
                $"<text x=\"{x}\" y=\"{y + 5}\" text-anchor=\"middle\" font-weight=\"bold\">{points[i]}</text>");
        }

        svg.AppendLine("</g>");

        // Título da rota
        var routeDescription = string.Join(" → ", path.Select(i => points[i]));
        svg.AppendLine(
            $"<text x=\"{width / 2}\" y=\"20\" text-anchor=\"middle\" font-family=\"Arial\" font-size=\"16\" font-weight=\"bold\">Rota: {routeDescription}</text>");

        svg.AppendLine("</svg>");

        return svg.ToString();
    }

    /// <summary>
    /// Salva a visualização da rota em um arquivo SVG.
    /// </summary>
    /// <param name="points">Lista de pontos</param>
    /// <param name="path">Caminho a ser visualizado</param>
    /// <param name="timeMatrix">Matriz de tempo</param>
    /// <param name="filename">Nome do arquivo para salvar</param>
    public static void SaveToFile(string[] points, List<int> path, int[,] timeMatrix, string filename)
    {
        var svg = GenerateSVG(points, path, timeMatrix);
        File.WriteAllText(filename, svg);
        Console.WriteLine($"Visualização da rota salva no arquivo: {filename}");
    }
}