using System.Security.Cryptography;
using System.Text;

namespace PrefsWrapper.Encoders
{
    public class AesEncoder : IEncoder<byte[]>
    {
        private readonly AesManaged _aes;

        public AesEncoder(string password, string salt, int keySize = 128)
        {
            _aes = new AesManaged();
            _aes.KeySize = keySize;

            var bSalt = Encoding.UTF8.GetBytes(salt);
            var deriveBytes = new Rfc2898DeriveBytes(password, bSalt);
            _aes.Key = deriveBytes.GetBytes(_aes.KeySize / 8);
            _aes.IV = deriveBytes.GetBytes(_aes.BlockSize / 8);
        }

        public byte[] Encode(byte[] bytes)
        {
            using var encryptor = _aes.CreateEncryptor();
            return encryptor.TransformFinalBlock(bytes, 0, bytes.Length);
        }

        public byte[] Decode(byte[] bytes)
        {
            using var decryptor = _aes.CreateDecryptor();
            return decryptor.TransformFinalBlock(bytes, 0, bytes.Length);
        }
    }
}
