using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using TrabalhoNPN;

BenchmarkSwitcher
    .FromAssembly(typeof(Program).Assembly)
    .Run(args);

[ShortRunJob]
[MemoryDiagnoser]
public class Algoritimo
{
    private Graph graph;

    [Params(5, 10, 50, 100)] // Número de vértices
    public int VertexCount { get; set; }

    [Params("Cycle", "Dense", "Sparse", "Random")] // Tipos de grafos
    public string GraphType { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        graph = GraphType switch
        {
            "Cycle" => GenerateCycleGraph(VertexCount),
            "Dense" => GenerateDenseGraph(VertexCount),
            "Sparse" => GenerateSparseGraph(VertexCount),
            "Random" => GenerateRandomGraph(VertexCount, 0.3), // 30% de probabilidade de aresta
            _ => throw new ArgumentException("Tipo de grafo inválido")
        };
    }


    [Benchmark]
    public void Teste()
    {
        var ordering = FeedbackArc.ComputeOrdering(graph);
    }

    [Benchmark]
    public void BruteForceTest()
    {
        var ordering = BruteForceFeedbackArc.ComputeOrdering(graph);
    }

    private Graph GenerateCycleGraph(int vertices)
    {
        var graph = new Graph(vertices);
        for (int i = 0; i < vertices; i++)
        {
            graph.AddEdge(i, (i + 1) % vertices); // Círculo fechado
        }
        return graph;
    }

    private Graph GenerateDenseGraph(int vertices)
    {
        var graph = new Graph(vertices);
        for (int i = 0; i < vertices; i++)
        {
            for (int j = 0; j < vertices; j++)
            {
                if (i != j) graph.AddEdge(i, j); // Conecta todos os vértices
            }
        }
        return graph;
    }

    private Graph GenerateSparseGraph(int vertices)
    {
        var graph = new Graph(vertices);
        for (int i = 0; i < vertices - 1; i++)
        {
            graph.AddEdge(i, i + 1); // Conexões em linha
        }
        return graph;
    }

    private Graph GenerateRandomGraph(int vertices, double edgeProbability)
    {
        var rand = new Random();
        var graph = new Graph(vertices);
        for (int i = 0; i < vertices; i++)
        {
            for (int j = 0; j < vertices; j++)
            {
                if (i != j && rand.NextDouble() < edgeProbability)
                {
                    graph.AddEdge(i, j);
                }
            }
        }
        return graph;
    }
}
