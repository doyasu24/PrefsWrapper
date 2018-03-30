using System.Security.Cryptography;
using System.Text;

namespace PrefsWrapper.Encoders
{
    public class AesEncoder : IEncoder<byte[]>
    {
        readonly AesManaged aes;

        public AesEncoder(string password, string salt, int keySize = 128)
        {
            aes = new AesManaged();
            aes.KeySize = keySize;

            var bSalt = Encoding.UTF8.GetBytes(salt);
            var deriveBytes = new Rfc2898DeriveBytes(password, bSalt);
            aes.Key = deriveBytes.GetBytes(aes.KeySize / 8);
            aes.IV = deriveBytes.GetBytes(aes.BlockSize / 8);
        }

        public byte[] Encode(byte[] bytes)
        {
            using (var encryptor = aes.CreateEncryptor())
            {
                return encryptor.TransformFinalBlock(bytes, 0, bytes.Length);
            }
        }

        public byte[] Decode(byte[] bytes)
        {
            using (var decryptor = aes.CreateDecryptor())
            {
                return decryptor.TransformFinalBlock(bytes, 0, bytes.Length);
            }
        }
    }
}
