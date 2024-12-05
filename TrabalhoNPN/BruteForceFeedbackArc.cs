using System.Collections.Generic;
using System.Linq;

namespace TrabalhoNPN
{
    public class BruteForceFeedbackArc
    {
        public static int Comparisons { get; private set; } = 0;

        public static List<int> ComputeOrdering(Graph graph)
        {
            Comparisons = 0; // Reseta contador de comparações

            int n = graph.VertexCount;
            var vertices = Enumerable.Range(0, n).ToList();
            var permutations = GetPermutations(vertices, n);

            int minInversions = int.MaxValue;
            List<int> bestOrdering = null;

            foreach (var permutation in permutations)
            {
                int inversions = CountInversions(graph, permutation.ToList());
                Comparisons++; // Contabiliza a permutação analisada
                if (inversions < minInversions)
                {
                    minInversions = inversions;
                    bestOrdering = permutation.ToList();
                }
            }

            return bestOrdering;
        }

        private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1)
                return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                            (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        private static int CountInversions(Graph graph, List<int> ordering)
        {
            var position = new Dictionary<int, int>();
            for (int i = 0; i < ordering.Count; i++)
                position[ordering[i]] = i;

            int inversions = 0;

            for (int i = 0; i < graph.VertexCount; i++)
            {
                foreach (var neighbor in graph.AdjacencyList[i])
                {
                    if (position[i] > position[neighbor])
                        inversions++; // Conta uma inversão
                }
            }

            return inversions;
        }
    }
}
