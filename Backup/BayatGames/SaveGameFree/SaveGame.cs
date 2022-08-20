// Decompiled with JetBrains decompiler
// Type: BayatGames.SaveGameFree.SaveGame
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using BayatGames.SaveGameFree.Encoders;
using BayatGames.SaveGameFree.Serializers;
using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace BayatGames.SaveGameFree
{
  public static class SaveGame
  {
    public static SaveGame.SaveHandler SaveCallback;
    public static SaveGame.LoadHandler LoadCallback;
    private static ISaveGameSerializer m_Serializer = (ISaveGameSerializer) new SaveGameJsonSerializer();
    private static ISaveGameEncoder m_Encoder = (ISaveGameEncoder) new SaveGameSimpleEncoder();
    private static Encoding m_Encoding = Encoding.UTF8;
    private static bool m_Encode = false;
    private static SaveGamePath m_SavePath = SaveGamePath.PersistentDataPath;
    private static string m_EncodePassword = "h@e#ll$o%^";
    private static bool m_LogError = false;

    public static event SaveGame.SaveHandler OnSaved;

    public static event SaveGame.LoadHandler OnLoaded;

    public static ISaveGameSerializer Serializer
    {
      get
      {
        if (SaveGame.m_Serializer == null)
          SaveGame.m_Serializer = (ISaveGameSerializer) new SaveGameJsonSerializer();
        return SaveGame.m_Serializer;
      }
      set => SaveGame.m_Serializer = value;
    }

    public static ISaveGameEncoder Encoder
    {
      get
      {
        if (SaveGame.m_Encoder == null)
          SaveGame.m_Encoder = (ISaveGameEncoder) new SaveGameSimpleEncoder();
        return SaveGame.m_Encoder;
      }
      set => SaveGame.m_Encoder = value;
    }

    public static Encoding DefaultEncoding
    {
      get
      {
        if (SaveGame.m_Encoding == null)
          SaveGame.m_Encoding = Encoding.UTF8;
        return SaveGame.m_Encoding;
      }
      set => SaveGame.m_Encoding = value;
    }

    public static bool Encode
    {
      get => SaveGame.m_Encode;
      set => SaveGame.m_Encode = value;
    }

    public static SaveGamePath SavePath
    {
      get => SaveGame.m_SavePath;
      set => SaveGame.m_SavePath = value;
    }

    public static string EncodePassword
    {
      get => SaveGame.m_EncodePassword;
      set => SaveGame.m_EncodePassword = value;
    }

    public static bool LogError
    {
      get => SaveGame.m_LogError;
      set => SaveGame.m_LogError = value;
    }

    public static void Save<T>(string identifier, T obj) => SaveGame.Save<T>(identifier, obj, SaveGame.Encode, SaveGame.EncodePassword, SaveGame.Serializer, SaveGame.Encoder, SaveGame.DefaultEncoding, SaveGame.SavePath);

    public static void Save<T>(string identifier, T obj, bool encode) => SaveGame.Save<T>(identifier, obj, encode, SaveGame.EncodePassword, SaveGame.Serializer, SaveGame.Encoder, SaveGame.DefaultEncoding, SaveGame.SavePath);

    public static void Save<T>(string identifier, T obj, string encodePassword) => SaveGame.Save<T>(identifier, obj, SaveGame.Encode, encodePassword, SaveGame.Serializer, SaveGame.Encoder, SaveGame.DefaultEncoding, SaveGame.SavePath);

    public static void Save<T>(string identifier, T obj, ISaveGameSerializer serializer) => SaveGame.Save<T>(identifier, obj, SaveGame.Encode, SaveGame.EncodePassword, serializer, SaveGame.Encoder, SaveGame.DefaultEncoding, SaveGame.SavePath);

    public static void Save<T>(string identifier, T obj, ISaveGameEncoder encoder) => SaveGame.Save<T>(identifier, obj, SaveGame.Encode, SaveGame.EncodePassword, SaveGame.Serializer, encoder, SaveGame.DefaultEncoding, SaveGame.SavePath);

    public static void Save<T>(string identifier, T obj, Encoding encoding) => SaveGame.Save<T>(identifier, obj, SaveGame.Encode, SaveGame.EncodePassword, SaveGame.Serializer, SaveGame.Encoder, encoding, SaveGame.SavePath);

    public static void Save<T>(string identifier, T obj, SaveGamePath savePath) => SaveGame.Save<T>(identifier, obj, SaveGame.Encode, SaveGame.EncodePassword, SaveGame.Serializer, SaveGame.Encoder, SaveGame.DefaultEncoding, savePath);

    public static void Save<T>(
      string identifier,
      T obj,
      bool encode,
      string password,
      ISaveGameSerializer serializer,
      ISaveGameEncoder encoder,
      Encoding encoding,
      SaveGamePath path)
    {
      if (string.IsNullOrEmpty(identifier))
        throw new ArgumentNullException(nameof (identifier));
      if (serializer == null)
        serializer = SaveGame.Serializer;
      if (encoding == null)
        encoding = SaveGame.DefaultEncoding;
      string str1 = SaveGame.IsFilePath(identifier) ? identifier : (path == SaveGamePath.PersistentDataPath || path != SaveGamePath.DataPath ? string.Format("{0}/{1}", (object) Application.persistentDataPath, (object) identifier) : string.Format("{0}/{1}", (object) Application.dataPath, (object) identifier));
      if ((object) obj == null)
        obj = default (T);
      Directory.CreateDirectory(Path.GetDirectoryName(str1));
      Stream stream = !encode ? (!SaveGame.IOSupported() ? (Stream) new MemoryStream() : (Stream) File.Create(str1)) : (Stream) new MemoryStream();
      serializer.Serialize<T>(obj, stream, encoding);
      if (encode)
      {
        string base64String = Convert.ToBase64String(((MemoryStream) stream).ToArray());
        string contents = encoder.Encode(base64String, password);
        if (SaveGame.IOSupported())
        {
          File.WriteAllText(str1, contents, encoding);
        }
        else
        {
          PlayerPrefs.SetString(str1, contents);
          PlayerPrefs.Save();
        }
      }
      else if (!SaveGame.IOSupported())
      {
        string str2 = encoding.GetString(((MemoryStream) stream).ToArray());
        PlayerPrefs.SetString(str1, str2);
        PlayerPrefs.Save();
      }
      stream.Dispose();
      if (SaveGame.SaveCallback != null)
        SaveGame.SaveCallback((object) obj, identifier, encode, password, serializer, encoder, encoding, path);
      if (SaveGame.OnSaved == null)
        return;
      SaveGame.OnSaved((object) obj, identifier, encode, password, serializer, encoder, encoding, path);
    }

    public static T Load<T>(string identifier) => SaveGame.Load<T>(identifier, default (T), SaveGame.Encode, SaveGame.EncodePassword, SaveGame.Serializer, SaveGame.Encoder, SaveGame.DefaultEncoding, SaveGame.SavePath);

    public static T Load<T>(string identifier, T defaultValue) => SaveGame.Load<T>(identifier, defaultValue, SaveGame.Encode, SaveGame.EncodePassword, SaveGame.Serializer, SaveGame.Encoder, SaveGame.DefaultEncoding, SaveGame.SavePath);

    public static T Load<T>(string identifier, bool encode, string encodePassword) => SaveGame.Load<T>(identifier, default (T), encode, encodePassword, SaveGame.Serializer, SaveGame.Encoder, SaveGame.DefaultEncoding, SaveGame.SavePath);

    public static T Load<T>(string identifier, ISaveGameSerializer serializer) => SaveGame.Load<T>(identifier, default (T), SaveGame.Encode, SaveGame.EncodePassword, serializer, SaveGame.Encoder, SaveGame.DefaultEncoding, SaveGame.SavePath);

    public static T Load<T>(string identifier, ISaveGameEncoder encoder) => SaveGame.Load<T>(identifier, default (T), SaveGame.Encode, SaveGame.EncodePassword, SaveGame.Serializer, encoder, SaveGame.DefaultEncoding, SaveGame.SavePath);

    public static T Load<T>(string identifier, Encoding encoding) => SaveGame.Load<T>(identifier, default (T), SaveGame.Encode, SaveGame.EncodePassword, SaveGame.Serializer, SaveGame.Encoder, encoding, SaveGame.SavePath);

    public static T Load<T>(string identifier, SaveGamePath savePath) => SaveGame.Load<T>(identifier, default (T), SaveGame.Encode, SaveGame.EncodePassword, SaveGame.Serializer, SaveGame.Encoder, SaveGame.DefaultEncoding, savePath);

    public static T Load<T>(string identifier, T defaultValue, bool encode) => SaveGame.Load<T>(identifier, defaultValue, encode, SaveGame.EncodePassword, SaveGame.Serializer, SaveGame.Encoder, SaveGame.DefaultEncoding, SaveGame.SavePath);

    public static T Load<T>(string identifier, T defaultValue, string encodePassword) => SaveGame.Load<T>(identifier, defaultValue, SaveGame.Encode, encodePassword, SaveGame.Serializer, SaveGame.Encoder, SaveGame.DefaultEncoding, SaveGame.SavePath);

    public static T Load<T>(string identifier, T defaultValue, ISaveGameSerializer serializer) => SaveGame.Load<T>(identifier, defaultValue, SaveGame.Encode, SaveGame.EncodePassword, serializer, SaveGame.Encoder, SaveGame.DefaultEncoding, SaveGame.SavePath);

    public static T Load<T>(string identifier, T defaultValue, ISaveGameEncoder encoder) => SaveGame.Load<T>(identifier, defaultValue, SaveGame.Encode, SaveGame.EncodePassword, SaveGame.Serializer, encoder, SaveGame.DefaultEncoding, SaveGame.SavePath);

    public static T Load<T>(string identifier, T defaultValue, Encoding encoding) => SaveGame.Load<T>(identifier, defaultValue, SaveGame.Encode, SaveGame.EncodePassword, SaveGame.Serializer, SaveGame.Encoder, encoding, SaveGame.SavePath);

    public static T Load<T>(string identifier, T defaultValue, SaveGamePath savePath) => SaveGame.Load<T>(identifier, defaultValue, SaveGame.Encode, SaveGame.EncodePassword, SaveGame.Serializer, SaveGame.Encoder, SaveGame.DefaultEncoding, savePath);

    public static T Load<T>(
      string identifier,
      T defaultValue,
      bool encode,
      string password,
      ISaveGameSerializer serializer,
      ISaveGameEncoder encoder,
      Encoding encoding,
      SaveGamePath path)
    {
      if (string.IsNullOrEmpty(identifier))
        throw new ArgumentNullException(nameof (identifier));
      if (serializer == null)
        serializer = SaveGame.Serializer;
      if (encoding == null)
        encoding = SaveGame.DefaultEncoding;
      if ((object) defaultValue == null)
        defaultValue = default (T);
      T obj = defaultValue;
      string str = SaveGame.IsFilePath(identifier) ? identifier : (path == SaveGamePath.PersistentDataPath || path != SaveGamePath.DataPath ? string.Format("{0}/{1}", (object) Application.persistentDataPath, (object) identifier) : string.Format("{0}/{1}", (object) Application.dataPath, (object) identifier));
      if (!SaveGame.Exists(str, path))
      {
        Debug.LogWarningFormat("The specified identifier ({1}) does not exists. please use Exists () to check for existent before calling Load.\nreturning the default(T) instance.", (object) str, (object) identifier);
        return obj;
      }
      Stream stream;
      if (encode)
      {
        string input = !SaveGame.IOSupported() ? PlayerPrefs.GetString(str) : File.ReadAllText(str, encoding);
        stream = (Stream) new MemoryStream(Convert.FromBase64String(encoder.Decode(input, password)), true);
      }
      else if (SaveGame.IOSupported())
      {
        stream = (Stream) File.OpenRead(str);
      }
      else
      {
        string s = PlayerPrefs.GetString(str);
        stream = (Stream) new MemoryStream(encoding.GetBytes(s));
      }
      T loadedObj = serializer.Deserialize<T>(stream, encoding);
      stream.Dispose();
      if ((object) loadedObj == null)
        loadedObj = defaultValue;
      if (SaveGame.LoadCallback != null)
        SaveGame.LoadCallback((object) loadedObj, identifier, encode, password, serializer, encoder, encoding, path);
      if (SaveGame.OnLoaded != null)
        SaveGame.OnLoaded((object) loadedObj, identifier, encode, password, serializer, encoder, encoding, path);
      return loadedObj;
    }

    public static bool Exists(string identifier) => SaveGame.Exists(identifier, SaveGame.SavePath);

    public static bool Exists(string identifier, SaveGamePath path)
    {
      if (string.IsNullOrEmpty(identifier))
        throw new ArgumentNullException(nameof (identifier));
      string str = SaveGame.IsFilePath(identifier) ? identifier : (path == SaveGamePath.PersistentDataPath || path != SaveGamePath.DataPath ? string.Format("{0}/{1}", (object) Application.persistentDataPath, (object) identifier) : string.Format("{0}/{1}", (object) Application.dataPath, (object) identifier));
      if (!SaveGame.IOSupported())
        return PlayerPrefs.HasKey(str);
      bool flag = Directory.Exists(str);
      if (!flag)
        flag = File.Exists(str);
      return flag;
    }

    public static void Delete(string identifier) => SaveGame.Delete(identifier, SaveGame.SavePath);

    public static void Delete(string identifier, SaveGamePath path)
    {
      if (string.IsNullOrEmpty(identifier))
        throw new ArgumentNullException(nameof (identifier));
      string str = SaveGame.IsFilePath(identifier) ? identifier : (path == SaveGamePath.PersistentDataPath || path != SaveGamePath.DataPath ? string.Format("{0}/{1}", (object) Application.persistentDataPath, (object) identifier) : string.Format("{0}/{1}", (object) Application.dataPath, (object) identifier));
      if (!SaveGame.Exists(str, path))
        return;
      if (SaveGame.IOSupported())
        File.Delete(str);
      else
        PlayerPrefs.DeleteKey(str);
    }

    public static void Clear() => SaveGame.DeleteAll(SaveGame.SavePath);

    public static void Clear(SaveGamePath path) => SaveGame.DeleteAll(path);

    public static void DeleteAll() => SaveGame.DeleteAll(SaveGame.SavePath);

    public static void DeleteAll(SaveGamePath path)
    {
      string path1 = "";
      switch (path)
      {
        case SaveGamePath.PersistentDataPath:
          path1 = Application.persistentDataPath;
          break;
        case SaveGamePath.DataPath:
          path1 = Application.dataPath;
          break;
      }
      if (SaveGame.IOSupported())
      {
        DirectoryInfo directoryInfo = new DirectoryInfo(path1);
        foreach (FileSystemInfo file in directoryInfo.GetFiles())
          file.Delete();
        foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
          directory.Delete(true);
      }
      else
        PlayerPrefs.DeleteAll();
    }

    public static FileInfo[] GetFiles() => SaveGame.GetFiles(string.Empty, SaveGame.SavePath);

    public static FileInfo[] GetFiles(string identifier) => SaveGame.GetFiles(identifier, SaveGame.SavePath);

    public static FileInfo[] GetFiles(string identifier, SaveGamePath path)
    {
      if (string.IsNullOrEmpty(identifier))
        identifier = string.Empty;
      string str = SaveGame.IsFilePath(identifier) ? identifier : (path == SaveGamePath.PersistentDataPath || path != SaveGamePath.DataPath ? string.Format("{0}/{1}", (object) Application.persistentDataPath, (object) identifier) : string.Format("{0}/{1}", (object) Application.dataPath, (object) identifier));
      FileInfo[] files = new FileInfo[0];
      if (!SaveGame.Exists(str, path) || !Directory.Exists(str))
        return files;
      files = new DirectoryInfo(str).GetFiles();
      return files;
    }

    public static DirectoryInfo[] GetDirectories() => SaveGame.GetDirectories(string.Empty, SaveGame.SavePath);

    public static DirectoryInfo[] GetDirectories(string identifier) => SaveGame.GetDirectories(identifier, SaveGame.SavePath);

    public static DirectoryInfo[] GetDirectories(string identifier, SaveGamePath path)
    {
      if (string.IsNullOrEmpty(identifier))
        identifier = string.Empty;
      string str = SaveGame.IsFilePath(identifier) ? identifier : (path == SaveGamePath.PersistentDataPath || path != SaveGamePath.DataPath ? string.Format("{0}/{1}", (object) Application.persistentDataPath, (object) identifier) : string.Format("{0}/{1}", (object) Application.dataPath, (object) identifier));
      DirectoryInfo[] directories = new DirectoryInfo[0];
      if (!SaveGame.Exists(str, path) || !Directory.Exists(str))
        return directories;
      directories = new DirectoryInfo(str).GetDirectories();
      return directories;
    }

    public static bool IOSupported() => Application.platform != RuntimePlatform.WebGLPlayer && Application.platform != RuntimePlatform.MetroPlayerARM && Application.platform != RuntimePlatform.MetroPlayerX64 && Application.platform != RuntimePlatform.MetroPlayerX86 && Application.platform != RuntimePlatform.SamsungTVPlayer && Application.platform != RuntimePlatform.tvOS && Application.platform != RuntimePlatform.PS4;

    public static bool IsFilePath(string str)
    {
      bool flag = false;
      if (Path.IsPathRooted(str))
      {
        try
        {
          Path.GetFullPath(str);
          flag = true;
        }
        catch (Exception ex)
        {
          flag = false;
        }
      }
      return flag;
    }

    public delegate void SaveHandler(
      object obj,
      string identifier,
      bool encode,
      string password,
      ISaveGameSerializer serializer,
      ISaveGameEncoder encoder,
      Encoding encoding,
      SaveGamePath path);

    public delegate void LoadHandler(
      object loadedObj,
      string identifier,
      bool encode,
      string password,
      ISaveGameSerializer serializer,
      ISaveGameEncoder encoder,
      Encoding encoding,
      SaveGamePath path);
  }
}
