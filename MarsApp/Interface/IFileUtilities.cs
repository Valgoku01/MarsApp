using System.Collections.Generic;
using Unity;

namespace MarsApp.Interface
{
    /// <summary>
    /// Interface file utilities
    /// </summary>
    public interface IFileUtilities
    {
        /// <summary>
        /// Parse the file
        /// </summary>
        /// <param name="container">Unity container</param>
        /// <param name="file">File to parse</param>
        /// <returns>Data formatted, empty list otherwise</returns>
        IList<string> ParseFile(IUnityContainer container, string file);
    }
}
