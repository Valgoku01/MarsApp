using MarsApp.Interface;

namespace MarsAppTest.Mocks
{
    /// <summary>
    /// Mock of ByteUtilities
    /// </summary>
    public class ByteUtilitiesMock : IByteUtilities
    {
        public bool ByteValid { get; set; }
        public byte[] Bytes { get; set; }

        public ByteUtilitiesMock()
        {

        }

        public byte[] GetBytes(string charToTransform)
        {
            return Bytes;
        }

        public bool IsByteValidInAscii(byte byteToCompare)
        {
            return ByteValid;
        }
    }
}
