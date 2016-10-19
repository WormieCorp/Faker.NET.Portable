public static class Parsing
{
	public static string[] ParseReleaseNotes(ICakeContext context, FilePath changelog)
	{
		var newNotes = new List<string>();
		
		try
		{
			var notes = context.ParseReleaseNotes(changelog).Notes;

			foreach (var note in notes)
			{
				if (note.StartsWith("__") && note.EndsWith("__"))
				{
					newNotes.Add("");
					newNotes.Add("");
					newNotes.Add(note.Trim('_').Trim());
					newNotes.Add("");
				}
				else if (note.StartsWith("-"))
				{
					int index = note.IndexOf(')');
					if (index > 0)
					{
						string issue = note.Substring(index + 1).Trim();
						newNotes.Add(note.Substring(0, 2) + issue);
					}
					else
					{
						newNotes.Add(note);
					}
				}
				else if (note.IndexOf("part of this release") > 0)
				{
					continue;
				}
				else
				{
					newNotes.Add(note);
				}
			}
		} catch {}

		return newNotes.ToArray();
	}
}
