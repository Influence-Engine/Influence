using System;
using System.IO;

namespace Influence
{
    /// <summary>
    /// Represents a source file with properties for its path, name, and extension. <br/>
    /// Provides functionality to retrieve the file's content as a string.
    /// </summary>
    public class SourceFile
    {
        /// <summary>Gets the full path of the source file.</summary>
        public string Path { get; private set; }

        /// <summary>Gets the name of the source file without its extension.</summary>
        public string Name => System.IO.Path.GetFileNameWithoutExtension(Path);

        /// <summary>Gets the extension of the source file.</summary>
        public string Extension => System.IO.Path.GetExtension(Path).ToLowerInvariant();

        /// <summary>Initializes a new instance of the SourceFile class with the specified path.</summary>
        /// <param name="path">The full path of the source file.</param>
        public SourceFile(string path)
        {
            this.Path = path;
        }

        /// <summary>Retrieves and returns the content of the source file as a string.</summary>
        /// <returns>The content of the source file.</returns>
        public string GetFileContent()
        {
            Console.WriteLine(Path);
            using (StreamReader reader = new StreamReader(Path))
                return reader.ReadToEnd();
        }
    }
}
