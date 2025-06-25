# NeoCli
Simple small commandline argument parser.
### Setup
```C#
using NeoCli;

var clArgs = args.Init(nameof(FileHelper), "Simple file transmission tool for local networks.")
	.Register("s", ArgType.send, "Starts in send mode - this mode is default when started with file params", "h", CommandValues.Optional)
	.Register("h", ArgType.host, "Starts in host mode - optional download directory path", "s", CommandValues.Optional)
	.Register("t", ArgType.tcp, "Tcp listening port for receiving files", null, CommandValues.Required)
	.Register("u", ArgType.udp, $"UDP Broadcast Port - default: {_UDPPort}", null, CommandValues.Required)
	.Parse(out string? parseError);
```
### Usage
#### Error reporting
```C#
if (!string.IsNullOrWhiteSpace(parseError))
{
	Console.WriteLine(parseError);
	return;
}
```
#### Application documentation
```C#
if (clArgs.Empty)
{
	Console.WriteLine(clArgs.Documentation);
	return;
}
```
#### Access & Parsing
```C#
// Flags
if(clArgs.Contains(CLKey.send))
{
	// send
}

// Single element with parsing
_port = clArgs.TryGet(CLKey.tcp, out int v) ? v : defaultPort;

// Multiple elements
_files = clArgs[CLKey.fileList]?.Values ?? [];
```
