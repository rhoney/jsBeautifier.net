using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace net.jsBeautifier
{
	static class Program
	{
		#region Main Method
		[STAThread]
		static void Main (string[] args)
		{
			ResourceExtractor.ensureResources();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Program.startBeautifier(args);
		}
		#endregion

		#region Private Methods
		private static void beautifyBackground (string sourceFile, string destinationFile, int? indent, bool? bracesInNewLine, bool? preserveEmptyLines,
				bool? detectPackers, bool? keepArrayIndent)
		{
			JsBeautifier b=new JsBeautifier();

			b.BeautifierReady += delegate(object sender, EventArgs e)
			{
				if (indent.HasValue) b.setIndentation(indent.Value);
				if (bracesInNewLine.HasValue) b.setBracesNewLine(bracesInNewLine.Value);
				if (preserveEmptyLines.HasValue) b.setPreserveEmptyLines(preserveEmptyLines.Value);
				if (detectPackers.HasValue) b.setDetectPackers(detectPackers.Value);
				if (keepArrayIndent.HasValue) b.setKeepArrayIndent(keepArrayIndent.Value);

				string beautified=b.getBeautifiedScript(System.IO.File.ReadAllText(sourceFile));

				System.IO.File.WriteAllText(destinationFile, beautified);

				b.Close();
			};

			b.Shown += delegate(object sender, EventArgs e)
			{
				b.WindowState = FormWindowState.Minimized;
				b.Location = new System.Drawing.Point(800, 800);
				b.Size = new System.Drawing.Size(100, 100);
			};

			b.ShowDialog();
		}

		private static void startBeautifier (string[] args)
		{
			string sourceFile=null, destinationFile=null;
			int? indent=null;
			bool? bracesInNewLine=null, preserveEmptyLines=null, detectPackers=null, keepArrayIndent=null;

			if (args.Length > 0)
			{
				foreach (string str in args)
				{
					int index=str.IndexOf('=');
					if (index != -1)
					{
						string key=str.Substring(0, index);
						string value=str.Substring(index + 1);

						switch (key)
						{
							case "sourceFile":
								sourceFile = value;
								break;

							case "destinationFile":
								destinationFile = value;
								break;

							case "indent":
								indent = int.Parse(value);
								break;

							case "bracesInNewLine":
								bracesInNewLine = bool.Parse(value);
								break;

							case "preserveEmptyLines":
								preserveEmptyLines = bool.Parse(value);
								break;

							case "detectPackers":
								detectPackers = bool.Parse(value);
								break;

							case "keepArrayIndent":
								keepArrayIndent = bool.Parse(value);
								break;
						}
					}

				}
			}

			if (string.IsNullOrEmpty(sourceFile))
			{
				Application.Run(new JsBeautifier());
			}
			else
			{
				if (string.IsNullOrEmpty(destinationFile))
				{
					destinationFile = sourceFile;
				}
				Program.beautifyBackground(sourceFile, destinationFile, indent, bracesInNewLine, preserveEmptyLines, detectPackers, keepArrayIndent);
			}
		}
		#endregion
	}
}
