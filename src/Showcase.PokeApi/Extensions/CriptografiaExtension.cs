using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Showcase.PokeApi.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class CriptografiaExtension
    {
        private static readonly RandomNumberGenerator _randomNumberGenerator = RandomNumberGenerator.Create();

        public static string Decriptar(this string dadoCriptografado)
        {
            byte[] input, output;
            ICryptoTransform decryptor;
            Tuple<byte[], byte[]> tuple;

            using (var aes = Aes.Create())
            {
                tuple = InicializarAlgoritmo();

                using (decryptor = aes.CreateDecryptor(tuple.Item1, tuple.Item2))
                {
                    input = ConverterStringParaBytes(dadoCriptografado);
                    output = decryptor.TransformFinalBlock(input, 0, input.Length);
                }
            }

            var decrypt = Encoding.UTF8.GetString(output);

            return decrypt[4..];
        }

        public static string Encriptar(this string str)
        {
            byte[] input, output;
            ICryptoTransform encryptor;
            Tuple<byte[], byte[]> tuple;

            var buffer = new byte[2];

            _randomNumberGenerator.GetBytes(buffer);

            var salt = ConverterBytesParaString(buffer);

            using (var aes = Aes.Create())
            {
                tuple = InicializarAlgoritmo();

                using (encryptor = aes.CreateEncryptor(tuple.Item1, tuple.Item2))
                {
                    input = Encoding.UTF8.GetBytes(salt + str);
                    output = encryptor.TransformFinalBlock(input, 0, input.Length);
                }
            }

            return ConverterBytesParaString(output);
        }

        private static Tuple<byte[], byte[]> InicializarAlgoritmo()
        {
            var key = new byte[] { 0x33, 0x13, 0x82, 0x46, 0x0d, 0x90, 0x2d, 0x13, 0x41, 0xdc, 0x73, 0xdb, 0x8b, 0x99, 0x0f, 0x97 };
            var iv = new byte[] { 0x25, 0xc5, 0x21, 0x3d, 0xad, 0xeb, 0xf8, 0x13, 0xd3, 0x6d, 0x47, 0x00, 0x70, 0xb6, 0x00, 0x2c };

            return Tuple.Create(key, iv);
        }

        private static byte[] ConverterStringParaBytes(string str)
        {
            return Enumerable.Range(0, str.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(str.Substring(x, 2), 16))
                .ToArray();
        }

        private static string ConverterBytesParaString(byte[] array)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < array.Length; i++)
                sb.Append(array[i].ToString("X2"));

            return sb.ToString();
        }
    }
}