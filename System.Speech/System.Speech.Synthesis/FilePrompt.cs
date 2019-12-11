using System.Diagnostics;

namespace System.Speech.Synthesis
{
	/// <filterpriority>2</filterpriority>
	[DebuggerDisplay("{_text}")]
	public class FilePrompt : Prompt
	{
		public FilePrompt(string path, SynthesisMediaType media)
			: this(new Uri(path, UriKind.Relative), media)
		{
		}

		public FilePrompt(Uri promptFile, SynthesisMediaType media)
			: base(promptFile, media)
		{
		}
	}
}
