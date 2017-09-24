using System;
using System.Security.Cryptography;
using System.Text;

namespace ProjetoIntegrado.Funcoes
{
    public static class MD5
    {
        private const string chave = "E!09#x*&aTe$";

        public static string Criptografar(string Message)
        {
            byte[] Results;
            var UTF8 = new UTF8Encoding();
            var HashProvider = new MD5CryptoServiceProvider();
            var TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(chave));
            var TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            var DataToEncrypt = UTF8.GetBytes(Message);

            try
            {
                var Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return Convert.ToBase64String(Results);
        }
    }
}
