using MarsApp.Interface;
using System.Collections.Generic;
using Unity;

namespace MarsAppTest.Mocks
{
    /// <summary>
    /// Mock of FileUtilities
    /// </summary>
    public class FileUtilitiesMock : IFileUtilities
    {
        public IList<string> Lines { get; set; }

        public FileUtilitiesMock()
        {
            Lines = new List<string>();
        }

        public IList<string> ParseFile(IUnityContainer container, string file)
        {
            return Lines;
        }
    }
}
