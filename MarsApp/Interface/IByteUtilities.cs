namespace MarsApp.Interface
{
    /// <summary>
    /// Interface for byte utilities
    /// </summary>
    public interface IByteUtilities
    {
        /// <summary>
        /// Convert a string into a byte[]
        /// </summary>
        /// <param name="str">String to transform</param>
        /// <returns>new byte[]</returns>
        byte[] GetBytes(string charToTransform);

        /// <summary>
        /// Aknowledge the byte by checking the dico of Ascii language allowed
        /// </summary>
        /// <param name="byteToCompare">char to compare</param>
        /// <returns>True is byte valid, false otherwise</returns>
        bool IsByteValidInAscii(byte byteToCompare);
    }
}
