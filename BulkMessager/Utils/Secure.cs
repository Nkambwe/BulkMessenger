using System.Text;
using System.Security.Cryptography;

namespace BulkMessager.Utils {
    /// <summary>
    /// Secure data by encryption
    /// </summary>
    public class Secure {
        public static string EncryptString(string Message, string Passphrase) {
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = SHA256.HashData(Encoding.UTF8.GetBytes(Passphrase));
            aesAlg.Mode = CipherMode.CFB;
            aesAlg.Padding = PaddingMode.PKCS7;

            using ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            using MemoryStream msEncrypt = new();
            using (CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (StreamWriter swEncrypt = new(csEncrypt)) {
                swEncrypt.Write(Message);
            }

            byte[] encryptedBytes = msEncrypt.ToArray();

            // Combine IV and encrypted data
            byte[] result = new byte[aesAlg.IV.Length + encryptedBytes.Length];
            Buffer.BlockCopy(aesAlg.IV, 0, result, 0, aesAlg.IV.Length);
            Buffer.BlockCopy(encryptedBytes, 0, result, aesAlg.IV.Length, encryptedBytes.Length);

            return Convert.ToBase64String(result);
        }

        /// <summary>
        /// Decrypt messsage
        /// </summary>
        /// <param name="Message">Message to decrypt</param>
        /// <param name="key">Passkey</param>
        /// <returns></returns>
        public static string DecryptString(string Message, string key) {
            byte[] fullCipher = Convert.FromBase64String(Message);

            using Aes aesAlg = Aes.Create();
            aesAlg.Key = SHA256.HashData(Encoding.UTF8.GetBytes(key));
            aesAlg.Mode = CipherMode.CFB;
            aesAlg.Padding = PaddingMode.PKCS7;

            byte[] iv = new byte[aesAlg.IV.Length];
            byte[] cipherText = new byte[fullCipher.Length - aesAlg.IV.Length];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipherText, 0, cipherText.Length);

            using ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, iv);
            using MemoryStream msDecrypt = new(cipherText);
            using CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read);
            using StreamReader srDecrypt = new(csDecrypt);
            return srDecrypt.ReadToEnd();
        }
    }

}
