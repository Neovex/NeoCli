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

		public bool Empty => _parsedArgs.Count == 0;

		/// <summary>
		/// Gets the <see cref="Command"/> associated with the specified key or alias.
		/// </summary>
		/// <param name="key">The key or alias.</param>
		/// <returns>The <see cref="Command"/> if found; otherwise, <c>null</c>.</returns>
		public Command? this[object key]
		{
			get
			{
				string? keyStr = key.ToString();
				if (keyStr == null) return null;
				return _parsedArgs.FirstOrDefault(a => a.Key == keyStr || a.Alias == keyStr);
			}
		}

		/// <summary>
		/// Determines whether the specified key exists in the parsed arguments.
		/// </summary>
		/// <param name="key">The key or alias to check.</param>
		/// <returns><c>true</c> if the key exists; otherwise, <c>false</c>.</returns>
		public bool Contains(object key) => this[key] != null;

		/// <summary>
		/// Tries to get the value associated with the specified key and convert it to the specified type.
		/// </summary>
		/// <typeparam name="T">The type to convert the value to. Must implement <see cref="IConvertible"/>.</typeparam>
		/// <param name="key">The key or alias to look up.</param>
		/// <param name="value">When this method returns, contains the converted value if the key exists and conversion succeeds; otherwise, the default value for type <typeparamref name="T"/>.</param>
		/// <returns><c>true</c> if the value was found and converted successfully; otherwise, <c>false</c>.</returns>
		public bool TryGet<T>(object key, out T value) where T : IConvertible
		{
			value = default!;
			var cmd = this[key];
			if (cmd?.Value is null) return false;
			try
			{
				value = (T)Convert.ChangeType(cmd.Value, typeof(T));
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		public IEnumerator<Command> GetEnumerator() => _parsedArgs.GetEnumerator();

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}