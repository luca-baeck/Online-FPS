// Decompiled with JetBrains decompiler
// Type: BayatGames.SaveGameFree.Encoders.SaveGameSimpleEncoder
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BayatGames.SaveGameFree.Encoders
{
  public class SaveGameSimpleEncoder : ISaveGameEncoder
  {
    private const int Keysize = 256;
    private const int DerivationIterations = 1000;

    public string Encode(string input, string password)
    {
      byte[] numArray1 = SaveGameSimpleEncoder.Generate256BitsOfRandomEntropy();
      byte[] numArray2 = SaveGameSimpleEncoder.Generate256BitsOfRandomEntropy();
      byte[] bytes1 = Encoding.UTF8.GetBytes(input);
      byte[] bytes2 = new Rfc2898DeriveBytes(password, numArray1, 1000).GetBytes(32);
      using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
      {
        rijndaelManaged.BlockSize = 256;
        rijndaelManaged.Mode = CipherMode.CBC;
        rijndaelManaged.Padding = PaddingMode.PKCS7;
        using (ICryptoTransform encryptor = rijndaelManaged.CreateEncryptor(bytes2, numArray2))
        {
          using (MemoryStream memoryStream = new MemoryStream())
          {
            using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, encryptor, CryptoStreamMode.Write))
            {
              cryptoStream.Write(bytes1, 0, bytes1.Length);
              cryptoStream.FlushFinalBlock();
              byte[] array = ((IEnumerable<byte>) ((IEnumerable<byte>) numArray1).Concat<byte>((IEnumerable<byte>) numArray2).ToArray<byte>()).Concat<byte>((IEnumerable<byte>) memoryStream.ToArray()).ToArray<byte>();
              memoryStream.Close();
              cryptoStream.Close();
              return Convert.ToBase64String(array);
            }
          }
        }
      }
    }

    public string Decode(string input, string password)
    {
      byte[] source = Convert.FromBase64String(input);
      byte[] array1 = ((IEnumerable<byte>) source).Take<byte>(32).ToArray<byte>();
      byte[] array2 = ((IEnumerable<byte>) source).Skip<byte>(32).Take<byte>(32).ToArray<byte>();
      byte[] array3 = ((IEnumerable<byte>) source).Skip<byte>(64).Take<byte>(source.Length - 64).ToArray<byte>();
      byte[] bytes = new Rfc2898DeriveBytes(password, array1, 1000).GetBytes(32);
      using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
      {
        rijndaelManaged.BlockSize = 256;
        rijndaelManaged.Mode = CipherMode.CBC;
        rijndaelManaged.Padding = PaddingMode.PKCS7;
        using (ICryptoTransform decryptor = rijndaelManaged.CreateDecryptor(bytes, array2))
        {
          using (MemoryStream memoryStream = new MemoryStream(array3))
          {
            using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, decryptor, CryptoStreamMode.Read))
            {
              byte[] numArray = new byte[array3.Length];
              int count = cryptoStream.Read(numArray, 0, numArray.Length);
              memoryStream.Close();
              cryptoStream.Close();
              return Encoding.UTF8.GetString(numArray, 0, count);
            }
          }
        }
      }
    }

    private static byte[] Generate256BitsOfRandomEntropy()
    {
      byte[] data = new byte[32];
      new RNGCryptoServiceProvider().GetBytes(data);
      return data;
    }
  }
}
