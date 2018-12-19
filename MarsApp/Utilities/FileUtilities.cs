using MarsApp.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Unity;

namespace MarsApp.Utilities
{
    /// <summary>
    /// File utilities
    /// </summary>
    public class FileUtilities : IFileUtilities
    {
        /// <summary>
        /// Parse the file
        /// </summary>
        /// <param name="file">File to parse</param>
        /// <returns>Data formatted, empty list otherwise</returns>
        public IList<string> ParseFile(IUnityContainer container, string file)
        {
            var ret = new List<string>();

            var byteUtilities = container.Resolve<IByteUtilities>();

            try
            {
                // can throw exception
                var reader = File.OpenText(file);

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // get the line in bytes to aknowledge each char in ascii
                    var bytes = byteUtilities.GetBytes(line);
                    if (bytes.Length > 0)
                    {
                        var newBytes = bytes.Where(newByte => byteUtilities.IsByteValidInAscii(newByte));
                        if (newBytes.Count() > 0)
                            ret.Add(Encoding.UTF8.GetString(newBytes.ToArray()));
                    }
                }
            }
            catch(Exception e)
            {
                // to have 0 warnings
                var exception = e;
                ret = new List<string>();
            }

            return ret;
        }
    }
}
