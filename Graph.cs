using System.Collections.Generic;
using System.Numerics;
using System.Xml.Linq;

namespace Alcorithms
{
	internal class Graph<V, E> where V : notnull, IEquatable<V> where E : struct, IAdditionOperators<E, E, E>
	{
		public static readonly Func<IEnumerable<int?>, int?> DEFAULT_PRED = col => !col.Any() ? null : col.Min();

		public Dictionary<V, Node<V, E>> Nodes { get; init; } = [];

		public E? Trace(V start, V finish, HashSet<V> allowed, Func<IEnumerable<E?>, E?> predicate, E? sum = null, params V[] passed)
		{
			E? res = sum;
			HashSet<V> _passed= [.. passed, start];
			var node = Nodes[start];

			if (start.Equals(finish)) 
			{
				return sum;
			}
			else
			{
				res = Sum(res, predicate(node.Edges.Where(e => !_passed.Contains(e.Target) && (allowed.Contains(e.Target) || e.Target.Equals(finish)))
									   .Select(e => Trace(e.Target, finish, allowed, predicate, e.Value, [.. _passed]))));
				return res;
			}
			static E? Sum(E? a, E? b)
			{
				if (a is E aVal)
				{
					if (b is E bVal)
					{
						return aVal + bVal;
					}
					else
					{
						return null;
					}
				}
				else
				{
					return b;
				}
			}
		}

		public static Graph<V, E> Parse(string input, char partDelimiter = ';', char groupDelimiter = ',', char innerDelimiter = '-') 
		{
			Graph<V, E> res = new();
			var parts = input.Split(partDelimiter);
			(var nodes, var edges) = (parts[0], parts[1]);
			foreach (var n in nodes.Split(groupDelimiter))
			{
				res.Nodes.Add(Parse<V>(n!), new());
			}
			foreach (var e in edges.Split(groupDelimiter))
			{
				var edgeData = e.Split(innerDelimiter);
				(var from, var to, var value) = (Parse<V>(edgeData[0]),Parse<V>(edgeData[1]),Parse<E>(edgeData[2]));
				res.Nodes[from].Edges.Add(new(value, to));
			}
			return res;
			static T Parse<T>(string s) => (T)Convert.ChangeType(s, typeof(T));
		}
	}
}
