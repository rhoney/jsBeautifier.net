Double-click jsBeautifier.net.exe to run the application in interactive mode.

The application can also be run from command-line to automate beautification tasks. Supported command-line options:

1) sourceFile - Required
The path to the javascript source file which needs to be beautified.

2) destinationFile - Optional
The path where the beautified code should be stored. If omitted, defaults to sourceFile.

3) indent - Optional
The number of spaces for each successive nested block.

4) bracesInNewLine - Optional
Whether to place braces in new lines.

5) preserveEmptyLines - Optional
Whether to preserve empty lines in source javascript.

6) detectPackers - Optional
Whether to detect packing in source script.

7) keepArrayIndent - Optional
Whether to preserve array indents.

For issues with third option onwards, please check the js-beautify docs:
https://github.com/einars/js-beautify
