namespace NeoCli
{
	/// <summary>
	/// Specifies the requirement type of command argument values.
	/// </summary>
	public enum Necessity
	{
		/// <summary>
		/// No value is allowed for the command.
		/// </summary>
		None,
		/// <summary>
		/// A value is required for the command.
		/// </summary>
		Required,
		/// <summary>
		/// A value is optional for the command.
		/// </summary>
		Optional
	}
}
