using System.Security.Cryptography;
using System.Text;
using System;

namespace FinanceAPI.Shared.Extensions
{
    public static class HashingExtensions
    {
        private static MD5 _md5Hasher = MD5.Create();

        public static int HashValue<T>(
            this T value) where T : struct
        {
            return BitConverter.ToInt32(
                value: _md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(value.ToString())),
                startIndex: default(int));
        }

        public static int HashValue(
            this string value)
        {
            return BitConverter.ToInt32(
                value: _md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(value)),
                startIndex: default(int));
        }
    }
}
