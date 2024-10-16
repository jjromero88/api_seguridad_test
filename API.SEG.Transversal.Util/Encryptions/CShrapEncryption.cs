using System.Security.Cryptography;
using System.Text;

namespace API.SEG.Transversal.Util.Encryptions
{
    public class CShrapEncryption
    {
        // This constant string is used as a "salt" value for the PasswordDeriveBytes function calls.
        // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        private static readonly byte[] initVectorBytes = Encoding.ASCII.GetBytes("tu89geji340t89u2");

        // This constant is used to determine the keysize of the encryption algorithm.
        private const int keysize = 256;

        public static string EncryptString(string plainText, string passPhrase)
        {
            try
            {
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                using (PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null))
                {
                    byte[] keyBytes = password.GetBytes(keysize / 8);
                    using (RijndaelManaged symmetricKey = new RijndaelManaged())
                    {
                        symmetricKey.Mode = CipherMode.CBC;
                        using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes))
                        {
                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                                {
                                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                    cryptoStream.FlushFinalBlock();
                                    byte[] cipherTextBytes = memoryStream.ToArray();
                                    //+ => -, / => _ and = => .
                                    return Convert.ToBase64String(cipherTextBytes).Replace('+', '-').Replace('/', '_').Replace('=', '.');
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public static string DecryptString(string cipherText, string passPhrase)
        {
            try
            {
                byte[] cipherTextBytes = Convert.FromBase64String(cipherText.Replace('-', '+').Replace('_', '/').Replace('.', '='));
                using (PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null))
                {
                    byte[] keyBytes = password.GetBytes(keysize / 8);
                    using (RijndaelManaged symmetricKey = new RijndaelManaged())
                    {
                        symmetricKey.Mode = CipherMode.CBC;
                        using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes))
                        {
                            using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
                            {
                                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                                {
                                    byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                                    int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                    string dato = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                                    return dato;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public static string DecryptArray(string listakeys, string passPhrase)
        {
            string result = null;

            if (!string.IsNullOrEmpty(listakeys))
            {
                string[] arrSerialKey = listakeys.TrimEnd(',').Split(',');

                for (int index = 0; index < arrSerialKey.Length; index++)
                    result += DecryptString(arrSerialKey[index], passPhrase) + ',';

                result = string.IsNullOrEmpty(result) ? null : result.TrimEnd(',');
            }

            return result;
        }
        /*
        public static string EncryptString(string pstrToEncrypt, string pstrEncryptionKey)
        {
         * 
            byte[] key = null;
            byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
            byte[] inputByteArray = null;
            try
            {
                key = Encoding.UTF8.GetBytes(pstrEncryptionKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Encoding.UTF8.GetBytes(pstrToEncrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public static string DecryptString(string pstrToDecrypt, string pstrEncryptionKey)
        {
            string cipherText = pstrToDecrypt.Replace(" ", "+");
            byte[] key = null;
            byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
            byte[] inputByteArray = new byte[cipherText.Replace(" ", "+").Length];
            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(pstrEncryptionKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(cipherText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                Encoding Encoding = Encoding.UTF8;
                return Encoding.GetString(ms.ToArray());
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public string DecryptStringReport(string pstrToDecrypt, string pstrEncryptionKey)
        {
            string cipherText = pstrToDecrypt.Replace(" ", "+");
            byte[] key = null;
            byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
            byte[] inputByteArray = new byte[cipherText.Replace(" ", "+").Length];
            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(pstrEncryptionKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(cipherText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                Encoding Encoding = Encoding.UTF8;
                return Encoding.GetString(ms.ToArray());
            }
            catch
            {
                return pstrToDecrypt;
            }
        }*/

        public static string SplitConcatStrEncrypt(string Str, string Delimit, string pstrEncryptionKey)
        {
            string ReturnStr = "";
            foreach (var objValue in Str.Split(new[] { Delimit }, StringSplitOptions.RemoveEmptyEntries))
            {
                ReturnStr = ReturnStr + EncryptString(objValue, pstrEncryptionKey) + ",";
            }
            return ReturnStr.TrimEnd(',');
        }
        /*
        public string conactTextDescrypt(string strObject, string strSimboloText,string strSimboloSeparador)
        {
            string strCadena = "";
            foreach (var sText in strObject.ToString().Trim().Split(new[] { strSimboloText }, StringSplitOptions.RemoveEmptyEntries))
            {
                strCadena = strCadena + new CShrapEncryption().DecryptString(sText.ToString().Trim()) + strSimboloSeparador;
            }

            return strCadena.Remove(strCadena.Length - 1); 
        }
        public string conactText(string strObject, string strSimboloText, string strSimboloSeparador)
        {
            string strCadena = "";
            foreach (var sText in strObject.ToString().Trim().Split(new[] { strSimboloText }, StringSplitOptions.RemoveEmptyEntries))
            {
                strCadena = strCadena + sText.ToString().Trim() + strSimboloSeparador;
            }

            return strCadena.Remove(strCadena.Length - 1); 
        }*/
        public static string GenerateKey()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789|$/.*!";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 100)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
    }
}
