using System.Collections;

namespace NeoCli
{
	/// <summary>
	/// Represents the result of parsing command line arguments.
	/// </summary>
	/// <param name="parsedArgs">The parsed command line arguments.</param>
	/// <param name="documentation">The documentation string.</param>
	public class CommandCollection(List<Command> parsedArgs, string documentation) : IEnumerable<Command>
	{
		private readonly List<Command> _parsedArgs = parsedArgs;

		/// <summary>
		/// Gets the documentation string for the command line arguments.
		/// </summary>
		public string Documentation { get; } = documentation;

		/// <summary>
		/// Determines whether the specified key exists in the parsed arguments.
		/// </summary>
		/// <param name="key">The key or alias to check.</param>
		/// <returns><c>true</c> if the key exists; otherwise, <c>false</c>.</returns>
		public bool Contains(object key) => Get(key) != null;

		/// <summary>
		/// Gets the <see cref="Command"/> associated with the specified key or alias.
		/// </summary>
		/// <param name="key">The key or alias.</param>
		/// <returns>The <see cref="Command"/> if found; otherwise, <c>null</c>.</returns>
		public Command? Get(object key)
		{
			string? keyStr = key.ToString();
			if (keyStr == null) return null;
			return _parsedArgs.FirstOrDefault(a => a.Key == keyStr || a.Alias == keyStr);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		public IEnumerator<Command> GetEnumerator() => _parsedArgs.GetEnumerator();

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}