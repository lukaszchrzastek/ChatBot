using System.Runtime.CompilerServices;

namespace ChatBot.Infrastructure.Extensions
{
	public static class AsyncEnumerableExtensions
	{
		public static async IAsyncEnumerable<(T Item, bool IsLast)> WithLastFlag<T>(
			this IAsyncEnumerable<T> source,
			[EnumeratorCancellation] CancellationToken cancellationToken = default)
		{
			await using var enumerator = source.GetAsyncEnumerator(cancellationToken);

			if (!await enumerator.MoveNextAsync())
				yield break;

			var current = enumerator.Current;

			while (await enumerator.MoveNextAsync())
			{
				yield return (current, false);
				current = enumerator.Current;
			}

			yield return (current, true);
		}
	}
}