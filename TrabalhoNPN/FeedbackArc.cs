namespace TrabalhoNPN
{
    public class FeedbackArc
    {
        // Contadores estáticos para rastrear o número de comparações e trocas realizadas durante a execução
        public static int Comparisons { get; private set; } = 0;
        public static int Swaps { get; private set; } = 0;

        // Método para calcular a ordenação dos vértices do grafo
        public static List<int> ComputeOrdering(Graph graph)
        {
            // Reseta os contadores antes de iniciar a computação
            Comparisons = 0;
            Swaps = 0;

            int n = graph.VertexCount; // Número total de vértices no grafo
            var inDegrees = new int[n]; // Array para armazenar os graus de entrada de cada vértice
            var outDegrees = new int[n]; // Array para armazenar os graus de saída de cada vértice

            // Calcula os graus de entrada e saída de cada vértice
            for (int i = 0; i < n; i++)
            {
                foreach (var neighbor in graph.AdjacencyList[i])
                {
                    outDegrees[i]++; // Incrementa o grau de saída do vértice atual
                    inDegrees[neighbor]++; // Incrementa o grau de entrada do vértice vizinho
                }
            }

            // Conjunto de vértices ainda não processados
            var vertices = new HashSet<int>(Enumerable.Range(0, n));
            var ordering = new List<int>(); // Lista para armazenar a ordenação final dos vértices

            // Enquanto ainda houver vértices não processados
            while (vertices.Count > 0)
            {
                // Inicializa as variáveis para encontrar o vértice com maior diferença entre graus de saída e entrada
                int maxDiff = int.MinValue;
                int selectedVertex = -1;

                // Itera sobre todos os vértices restantes no conjunto
                foreach (var v in vertices)
                {
                    int diff = outDegrees[v] - inDegrees[v]; // Calcula a diferença
                    Comparisons++; // Incrementa o contador de comparações
                    if (diff > maxDiff)
                    {
                        maxDiff = diff; // Atualiza a maior diferença encontrada
                        selectedVertex = v; // Atualiza o vértice selecionado
                    }
                }

                // Remove o vértice selecionado do conjunto e adiciona à ordenação
                vertices.Remove(selectedVertex);
                ordering.Add(selectedVertex);
                Swaps++; // Considera a remoção como uma "troca" para fins de contabilidade

                // Atualiza os graus de entrada dos vizinhos do vértice selecionado
                foreach (var neighbor in graph.AdjacencyList[selectedVertex])
                {
                    if (vertices.Contains(neighbor))
                    {
                        inDegrees[neighbor]--; // Reduz o grau de entrada do vizinho
                    }
                }
            }

            // Retorna a ordenação calculada
            return ordering;
        }
    }
}
