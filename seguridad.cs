using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Password_Manager
{
    internal class seguridad
    {
        public string ComputeHash(string sSourceData)
        {
            byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(sSourceData);

            byte[] tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);

            return Convert.ToBase64String(tmpHash);
        }

        public string encriptar(string cas, string hash)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(cas);

            MD5 md5 = MD5.Create();
            TripleDES tripleDES = TripleDES.Create();

            tripleDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripleDES.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripleDES.CreateEncryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return Convert.ToBase64String(result);
        }
        public string desencriptar(string cas, string hash)
        {
            byte[] data = Convert.FromBase64String(cas);

            MD5 md5 = MD5.Create();
            TripleDES tripleDES = TripleDES.Create();

            tripleDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripleDES.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripleDES.CreateDecryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return UTF8Encoding.UTF8.GetString(result);
        }
    }
}
