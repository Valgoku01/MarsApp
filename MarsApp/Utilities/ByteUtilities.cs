using MarsApp.Interface;
using System;
using System.Text;

namespace MarsApp.Utilities
{
    /// <summary>
    /// Class made to manipulate bytes
    /// </summary>
    public class ByteUtilities : IByteUtilities
    {
        /// <summary>
        /// Convert a string into a byte[]
        /// </summary>
        /// <param name="str">String to transform</param>
        /// <returns>new byte[]</returns>
        public byte[] GetBytes(string charToTransform)
        {
            try
            {
                // can throw exception
                return Encoding.ASCII.GetBytes(charToTransform);
            }
            catch (Exception e)
            {
                // to have 0 warnings
                var exception = e;
                return new byte[0];
            }
        }

        /// <summary>
        /// Aknowledge the byte by checking the dico of Ascii language allowed
        /// </summary>
        /// <param name="byteToCompare">char to compare</param>
        /// <returns>True is byte valid, false otherwise</returns>
        public bool IsByteValidInAscii(byte byteToCompare)
        {
            // 0 to 9
            // A to Z
            // a to z
            // space
            // vertical tab
            return byteToCompare > 47 && byteToCompare < 58 ||
                byteToCompare > 64 && byteToCompare < 91 ||
                byteToCompare > 96 && byteToCompare < 123 ||
                byteToCompare == 32 ||
                byteToCompare == 11;
        }

        /// <summary>
        /// Check if the char is empty of vertical tab
        /// </summary>
        /// <param name="charToCompare">Char to compare</param>
        /// <returns>True, false otherwise</returns>
        public bool IsCharEmptyOrTab(char charToCompare)
        {
            return charToCompare == 32 || charToCompare == 11;
        }
    }
}
