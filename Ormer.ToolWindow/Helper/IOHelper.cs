using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ormer.ToolWindow.Helper
{
    class IOHelper
    {
        /// <summary>
        /// Returns true if the given file path is a folder.
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>True if a folder</returns>
        public static bool IsFolder(string path)
        {
            return ((File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory);
        }
    }
}
