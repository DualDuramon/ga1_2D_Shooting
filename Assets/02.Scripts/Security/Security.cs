using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class Security
{
    private static readonly byte[] _key = Encoding.UTF8.GetBytes("12345678901234567890123456789012"); // 32 bytes
    private static readonly byte[] _iv = Encoding.UTF8.GetBytes("1234567890123456"); // 16 bytes

    public static string Encrypt(string plainText)
    {
        using Aes aes = Aes.Create();

        aes.Key = _key;
        aes.IV = _iv;

        using ICryptoTransform encryptor = aes.CreateEncryptor();
        using MemoryStream memoryStream = new MemoryStream();
        using CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

        cryptoStream.Write(plainBytes, 0, plainBytes.Length);
        cryptoStream.FlushFinalBlock();

        return Convert.ToBase64String(memoryStream.ToArray());
    }

    public static string Decrypt(string encryptedText)
    {
        using Aes aes = Aes.Create();
        aes.Key = _key;
        aes.IV = _iv;

        using ICryptoTransform decryptor = aes.CreateDecryptor();

        byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

        using MemoryStream memoryStream = new MemoryStream(encryptedBytes);
        using CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        using StreamReader reader = new StreamReader(cryptoStream);

        return reader.ReadToEnd();

    }
}
