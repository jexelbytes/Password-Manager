using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;

namespace Password_Manager
{
    internal class seguridad
    {
        Random rand = new Random();


        public string encriptar(string cas, string hash)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(cas);

            MD5 md5 = MD5.Create();
            TripleDES tripleDES = TripleDES.Create();

            byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(hash);

            byte[] tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);

            rand.NextBytes(tmpSource);

            tripleDES.Key = tmpHash;

            rand.NextBytes(tmpHash);

            tripleDES.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripleDES.CreateEncryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            rand.NextBytes(data);

            return Convert.ToBase64String(result);
        }
        public string desencriptar(string cas, string hash)
        {
            byte[] data = Convert.FromBase64String(cas);

            MD5 md5 = MD5.Create();
            TripleDES tripleDES = TripleDES.Create();

            byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(hash);

            byte[] tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);

            rand.NextBytes(tmpSource);

            tripleDES.Key = tmpHash;

            rand.NextBytes(tmpHash);

            tripleDES.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripleDES.CreateDecryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);


            rand.NextBytes(data);

            string res = UTF8Encoding.UTF8.GetString(result);

            rand.NextBytes(result);

            return res;
        }
        public string ComputeHash(string sSourceData)
        {
            byte[] tmpSource = Encoding.UTF8.GetBytes(sSourceData);

            byte[] hash = new SHA512Managed().ComputeHash(tmpSource);

            rand.NextBytes(tmpSource);

            return Convert.ToBase64String(hash);
        }
        public string ComputeMD5(string sSourceData)
        {
            byte[] tmpSource = Encoding.UTF8.GetBytes(sSourceData);

            byte[] hash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);

            rand.NextBytes(tmpSource);

            return Convert.ToBase64String(hash);
        }
    }
}
