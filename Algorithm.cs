namespace Alcorithms
{

	internal static class Algorithm
	{
#if DEBUG
	const char SEPARATOR = SEPARATOR;
#else
	const char SEPARATOR = '~';
#endif

		public static (Graph<int, int> graph, Action<Graph<int, int>> algorithm) ParseArgs(string[] args)
		{
			return args switch
			{
				["short", string s] => (Graph<int, int>.Parse(s), ShortestPath),
				["dijkstra", string start, string s] => (Graph<int, int>.Parse(s), g => Dijkstra(g, int.Parse(start))),
				_ => throw new Exception("Неверный набор аргументов"),
			};
		}

		public static void ShortestPath(Graph<int, int> graph)
		{
			for (int k = 0; k <= graph.Nodes.Count; k++)
			{
				Console.WriteLine($"k = {k}");
				for (int from = 1; from <= graph.Nodes.Count; from++)
				{
					Console.Write($"V{from}:");
					Console.Write(SEPARATOR);
					for (int to = 1; to <= graph.Nodes.Count; to++)
					{
						Console.Write($"V{to}:");
						if (from == to)
						{
							Console.Write('0');
						}
						else
						{
							var res = graph.Trace(from, to, StartNumbers(k).ToHashSet(), Graph<int, int>.DEFAULT_PRED);
							Console.Write(res is not null ? res : "∞");
						}
						Console.Write(SEPARATOR);
					}
					Console.WriteLine();
				}
			}
			static IEnumerable<int> StartNumbers(int maxNum)
			{
				for (int i = 0; i <= maxNum; i++)
				{
					yield return i;
				}
			}
		}
	
		public static void Dijkstra(Graph<int, int> graph, int startNode)
		{
			int i = 0;
			DijkstraStruct dijkstra = new(i++, [startNode], null, null, GetValues(startNode, []));
			PrintDijkstra(dijkstra);

			while (dijkstra.S.Count < graph.Nodes.Count)
			{
				var notPassedNodes = dijkstra.Values.Where(kv => kv.Value is not null && !dijkstra.S.Contains(kv.Key));
				var notPassedNullableNodes = dijkstra.Values.Where(kv => kv.Value is null && !dijkstra.S.Contains(kv.Key));

				var newNode = notPassedNodes.Any() 
					? notPassedNodes.OrderBy(kv => kv.Value).First()
					: notPassedNullableNodes.First();

				dijkstra = MergeDijkstra(UpdateDijkstra(new(i++, [.. dijkstra.S, newNode.Key], newNode.Key, newNode.Value, GetValues(newNode.Key, dijkstra.S))));
				PrintDijkstra(dijkstra);
			}

			Dictionary<int, int?> GetValues(int node, IEnumerable<int> allowed) 
				=> graph.Nodes.Where(n => n.Key != startNode).ToDictionary(n => n.Key, n => graph.Trace(node, n.Key, [.. allowed], Graph<int, int>.DEFAULT_PRED));

			static DijkstraStruct UpdateDijkstra(DijkstraStruct d) 
				=> new(d.Id, d.S, d.W, d.D, d.Values.ToDictionary(kv => kv.Key, kv => kv.Key == d.W ? d.D : kv.Value is int val ? val + d.D ?? 0 : kv.Value));

			DijkstraStruct MergeDijkstra(DijkstraStruct a)
			{
				var newValues = a.Values.ToDictionary(kv => kv.Key, kv => NullableMin(kv.Value, dijkstra.Values[kv.Key]));
				return new(a.Id, a.S, a.W, NullableMin(a.D, newValues[a.W!.Value]), newValues);
			}

			static void PrintDijkstra(DijkstraStruct d) 
				=> Console.WriteLine(string.Join(SEPARATOR,
									 [d.Id,
									 $"{{{string.Join(", ", d.S.Select(v => $"V{v}"))}}}",
									 d.W is int w ? $"V{w}" : "-",
									 d.D?.ToString() ?? "-",
									 ..d.Values.Select(kv => $"V{kv.Key}:{kv.Value?.ToString() ?? "∞"}")]));

			static int? NullableMin(int? a, int? b) => a is int x ? b is int y ? int.Min(x, y) : a : b;
		}

        private readonly record struct DijkstraStruct(int Id, HashSet<int> S, int? W, int? D, Dictionary<int, int?> Values);
    }
}
