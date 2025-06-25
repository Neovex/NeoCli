namespace NeoCli
{
	/// <summary>
	/// Extension methods for initializing the ArgumentParser.
	/// </summary>
	public static class InitializerExtension
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ArgumentParser"/> class.
		/// </summary>
		/// <param name="args">The command line arguments.</param>
		/// <param name="appName">The application name.</param>
		/// <param name="appDescription">The application description.</param>
		/// <returns>A new <see cref="ArgumentParser"/> instance.</returns>
		public static ArgumentParser Init(this string[] args, string appName, string appDescription)
			=> new ArgumentParser(args, appName, appDescription);
	}
}
