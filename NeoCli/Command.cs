namespace NeoCli
{
	/// <summary>
	/// Represents a command line argument definition and its parsed values.
	/// </summary>
	/// <param name="Key">The command key.</param>
	/// <param name="Alias">The command alias.</param>
	/// <param name="HelpText">The help text for the command.</param>
	/// <param name="Excludes">A key of a command that is mutually exclusive with this command.</param>
	/// <param name="CommandValues">Specifies if the command requires, allows, or forbids values.</param>
	public record Command(string Key, string? Alias, string? HelpText, string? Excludes, Necessity CommandValues = Necessity.None)
	{
		/// <summary>
		/// Gets the list of values associated with this command.
		/// </summary>
		public List<string> Values { get; } = [];

		/// <summary>
		/// Gets the first value associated with this command, or <c>null</c> if none exist.
		/// </summary>
		public string? Value => Values.FirstOrDefault();

		/// <summary>
		/// Gets the formatted key string, including alias if present.
		/// </summary>
		public string KeyString => $"-{Key}{(string.IsNullOrWhiteSpace(Alias) ? string.Empty : $" -{Alias}")}";

		/// <summary>
		/// Gets the documentation string for this command.
		/// </summary>
		public string DocumentationString => $"{KeyString} - {HelpText}";

		/// <summary>
		/// Gets a value indicating whether this command has help text.
		/// </summary>
		public bool HasHelpText => !string.IsNullOrWhiteSpace(HelpText);
	}
}
