namespace TrabalhoNPN
{
    public class FeedbackArc
    {
        public static int Comparisons { get; private set; } = 0;
        public static int Swaps { get; private set; } = 0;

        public static List<int> ComputeOrdering(Graph graph)
        {
            Comparisons = 0;
            Swaps = 0;

            int n = graph.VertexCount;
            var inDegrees = new int[n];
            var outDegrees = new int[n];

            // Calcula graus de entrada e saída
            for (int i = 0; i < n; i++)
            {
                foreach (var neighbor in graph.AdjacencyList[i])
                {
                    outDegrees[i]++;
                    inDegrees[neighbor]++;
                }
            }

            var vertices = new HashSet<int>(Enumerable.Range(0, n));
            var ordering = new List<int>();

            while (vertices.Count > 0)
            {
                // Encontra o vértice com a máxima diferença entre graus de saída e entrada
                int maxDiff = int.MinValue;
                int selectedVertex = -1;

                foreach (var v in vertices)
                {
                    int diff = outDegrees[v] - inDegrees[v];
                    Comparisons++;
                    if (diff > maxDiff)
                    {
                        maxDiff = diff;
                        selectedVertex = v;
                    }
                }

                // Remove o vértice selecionado e atualiza os graus
                vertices.Remove(selectedVertex);
                ordering.Add(selectedVertex);
                Swaps++; // Contabiliza como uma operação de swap

                foreach (var neighbor in graph.AdjacencyList[selectedVertex])
                {
                    if (vertices.Contains(neighbor))
                    {
                        inDegrees[neighbor]--;
                    }
                }
            }

            return ordering;
        }
    }
}
