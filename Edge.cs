using System.Numerics;

namespace Alcorithms
{
    internal class Edge<V, E>(E? value, V target) where V : notnull, IEquatable<V> where E : IAdditionOperators<E, E, E>
	{
		public V Target { get; set; } = target;
		public E Value { get; init; } = value ?? default!;
    }
}
