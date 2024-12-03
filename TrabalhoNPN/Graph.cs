

namespace TrabalhoNPN
{
    public class Graph
    {
        public int VertexCount { get; }
        public List<int>[] AdjacencyList { get; }

        public Graph(int vertexCount)
        {
            VertexCount = vertexCount;
            AdjacencyList = new List<int>[vertexCount];
            for (int i = 0; i < vertexCount; i++)
            {
                AdjacencyList[i] = new List<int>();
            }
        }

        public void AddEdge(int from, int to)
        {
            AdjacencyList[from].Add(to);
        }
    }

}
