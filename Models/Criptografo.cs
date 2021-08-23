using System;
using System.Security.Cryptography;
using System.Text;

namespace Biblioteca.Models{
    public class Criptografo{
        public static string TextoCriptografado(string text){
            MD5 MD5Hasher = MD5.Create();

            byte[] by = Encoding.Default.GetBytes(text);
            byte[] bytesCript = MD5Hasher.ComputeHash(by);

            StringBuilder builder = new StringBuilder();

            for(int i = 0; i < bytesCript.Length; i++){
                string debugB = bytesCript[i].ToString("x2");
                builder.Append(debugB);
            }

            return builder.ToString();
        }
    }
}