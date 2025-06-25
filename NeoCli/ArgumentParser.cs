namespace NeoCli
{
	/// <summary>
	/// Builds and parses command line arguments for an application.
	/// </summary>
	/// <param name="args">The command line arguments.</param>
	/// <param name="appName">The application name.</param>
	/// <param name="appDescription">The application description.</param>
	public class ArgumentParser(string[] args, string appName, string appDescription)
	{
		/// <summary>
		/// The prefix character for command line arguments.
		/// </summary>
		public const char PREFIX = '-';

		private readonly Dictionary<string, Command> _commands = [];
		private readonly string _appName = appName;
		private readonly string _appDescription = appDescription;

		/// <summary>
		/// Gets the command line arguments.
		/// </summary>
		public string[] Args { get; } = args;

		/// <summary>
		/// Registers a command with the builder.
		/// </summary>
		/// <param name="key">The command key.</param>
		/// <param name="alias">The command alias (optional).</param>
		/// <param name="helpText">The help text for the command (optional).</param>
		/// <param name="exclude">A key of a command that is mutually exclusive with this command (optional).</param>
		/// <param name="commandValues">Specifies if the command requires, allows, or forbids values.</param>
		/// <returns>The current <see cref="ArgumentParser"/> instance.</returns>
		public ArgumentParser Register(object key, object? alias = null, string? helpText = null,
			string? exclude = null, Necessity commandValues = Necessity.None)
		{
			string keyStr = key.ToString()!;
			_commands[keyStr] = new(keyStr, alias?.ToString(), helpText, exclude, commandValues);
			return this;
		}

		/// <summary>
		/// Parses the command line arguments.
		/// </summary>
		/// <param name="error">When this method returns, contains the error message if parsing failed; otherwise, an empty string.</param>
		/// <returns>A <see cref="CommandCollection"/> object representing the parsed arguments.</returns>
		public CommandCollection Parse(out string error)
		{
			error = string.Empty;
			Command? cmd = null;
			List<Command> cmds = new();
			foreach (var argument in Args)
			{
				if (argument[0] == PREFIX)
				{
					var key = argument.TrimStart(PREFIX);

					if (!_commands.TryGetValue(key, out cmd) &&
					   (cmd = _commands.Values.SingleOrDefault(c => c.Alias == key)) == null)
					{
						error = $"Command \"{argument}\" was not recognized. See help text for command list.";
						break;
					}

					if (!string.IsNullOrWhiteSpace(cmd.Excludes) && cmds.Any(c => c.Key == cmd.Excludes))
					{
						var other = _commands[cmd.Excludes];
						error = $"The commands \"{cmd.KeyString}\" and \"{other.KeyString}\" are mutually exclusive.";
						break;
					}

					cmds.Add(cmd);
				}
				else
				{
					if (cmd == null)
					{
						error = $"Unexpected value \"{argument}\". Did you miss a command?";
						break;
					}

					if (cmd.CommandValues == Necessity.None)
					{
						error = $"The command \"{cmd.KeyString}\" does not allow parameters.";
						break;
					}
					
					cmd.Values.Add(argument);
				}
			}

			var invalidCmd = cmds.FirstOrDefault(c => c.CommandValues == Necessity.Required && c.Values.Count == 0);
			if (invalidCmd != null) error = $"The command \"{invalidCmd.KeyString}\" is missing required parameters.";
			var doc = string.Join(Environment.NewLine, _commands.Values.Where(c => c.HasHelpText)
							.Select(c => c.DocumentationString)
							.Prepend(_appDescription)
							.Prepend(_appName));
			return new CommandCollection(cmds, doc);
		}
	}
}