using System.Numerics;

namespace Alcorithms
{
	internal class Node<V, E> where V : notnull, IEquatable<V> where E : IAdditionOperators<E, E, E>
	{
		 public List<Edge<V, E>> Edges { get; init; } = [];
	}
}
