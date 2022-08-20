// Decompiled with JetBrains decompiler
// Type: BayatGames.SaveGameFree.SaveGameWeb
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using BayatGames.SaveGameFree.Encoders;
using BayatGames.SaveGameFree.Serializers;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace BayatGames.SaveGameFree
{
  public class SaveGameWeb
  {
    private static string m_DefaultUsername = "savegamefree";
    private static string m_DefaultPassword = "$@ve#game%free";
    private static string m_DefaultURL = "http://www.example.com";
    private static bool m_DefaultEncode = false;
    private static string m_DefaultEncodePassword = "h@e#ll$o%^";
    private static ISaveGameSerializer m_DefaultSerializer = (ISaveGameSerializer) new SaveGameJsonSerializer();
    private static ISaveGameEncoder m_DefaultEncoder = (ISaveGameEncoder) new SaveGameSimpleEncoder();
    private static Encoding m_DefaultEncoding = Encoding.UTF8;
    protected string m_Username;
    protected string m_Password;
    protected string m_URL;
    protected bool m_Encode;
    protected string m_EncodePassword;
    protected ISaveGameSerializer m_Serializer;
    protected ISaveGameEncoder m_Encoder;
    protected Encoding m_Encoding;
    protected UnityWebRequest m_Request;
    protected bool m_IsError;
    protected string m_Error = "";

    public static string DefaultUsername
    {
      get => SaveGameWeb.m_DefaultUsername;
      set => SaveGameWeb.m_DefaultUsername = value;
    }

    public static string DefaultPassword
    {
      get => SaveGameWeb.m_DefaultPassword;
      set => SaveGameWeb.m_DefaultPassword = value;
    }

    public static string DefaultURL
    {
      get => SaveGameWeb.m_DefaultURL;
      set => SaveGameWeb.m_DefaultURL = value;
    }

    public static bool DefaultEncode
    {
      get => SaveGameWeb.m_DefaultEncode;
      set => SaveGameWeb.m_DefaultEncode = value;
    }

    public static string DefaultEncodePassword
    {
      get => SaveGameWeb.m_DefaultEncodePassword;
      set => SaveGameWeb.m_DefaultEncodePassword = value;
    }

    public static ISaveGameSerializer DefaultSerializer
    {
      get
      {
        if (SaveGameWeb.m_DefaultSerializer == null)
          SaveGameWeb.m_DefaultSerializer = (ISaveGameSerializer) new SaveGameJsonSerializer();
        return SaveGameWeb.m_DefaultSerializer;
      }
      set => SaveGameWeb.m_DefaultSerializer = value;
    }

    public static ISaveGameEncoder DefaultEncoder
    {
      get
      {
        if (SaveGameWeb.m_DefaultEncoder == null)
          SaveGameWeb.m_DefaultEncoder = (ISaveGameEncoder) new SaveGameSimpleEncoder();
        return SaveGameWeb.m_DefaultEncoder;
      }
      set => SaveGameWeb.m_DefaultEncoder = value;
    }

    public static Encoding DefaultEncoding
    {
      get
      {
        if (SaveGameWeb.m_DefaultEncoding == null)
          SaveGameWeb.m_DefaultEncoding = Encoding.UTF8;
        return SaveGameWeb.m_DefaultEncoding;
      }
      set => SaveGameWeb.m_DefaultEncoding = value;
    }

    public virtual string Username
    {
      get => this.m_Username;
      set => this.m_Username = value;
    }

    public virtual string Password
    {
      get => this.m_Password;
      set => this.m_Password = value;
    }

    public virtual string URL
    {
      get => this.m_URL;
      set => this.m_URL = value;
    }

    public virtual bool Encode
    {
      get => this.m_Encode;
      set => this.m_Encode = value;
    }

    public virtual string EncodePassword
    {
      get => this.m_EncodePassword;
      set => this.m_EncodePassword = value;
    }

    public virtual ISaveGameSerializer Serializer
    {
      get
      {
        if (this.m_Serializer == null)
          this.m_Serializer = (ISaveGameSerializer) new SaveGameJsonSerializer();
        return this.m_Serializer;
      }
      set => this.m_Serializer = value;
    }

    public virtual ISaveGameEncoder Encoder
    {
      get
      {
        if (this.m_Encoder == null)
          this.m_Encoder = (ISaveGameEncoder) new SaveGameSimpleEncoder();
        return this.m_Encoder;
      }
      set => this.m_Encoder = value;
    }

    public virtual Encoding Encoding
    {
      get
      {
        if (this.m_Encoding == null)
          this.m_Encoding = Encoding.UTF8;
        return this.m_Encoding;
      }
      set => this.m_Encoding = value;
    }

    public virtual UnityWebRequest Request => this.m_Request;

    public virtual bool IsError => this.m_IsError;

    public virtual string Error => this.m_Error;

    public SaveGameWeb()
      : this(SaveGameWeb.DefaultUsername)
    {
    }

    public SaveGameWeb(string username)
      : this(username, SaveGameWeb.DefaultPassword)
    {
    }

    public SaveGameWeb(string username, string password)
      : this(username, password, SaveGameWeb.DefaultURL)
    {
    }

    public SaveGameWeb(string username, string password, string url)
      : this(username, password, url, SaveGameWeb.DefaultEncode)
    {
    }

    public SaveGameWeb(string username, string password, string url, bool encode)
      : this(username, password, url, encode, SaveGameWeb.DefaultEncodePassword)
    {
    }

    public SaveGameWeb(
      string username,
      string password,
      string url,
      bool encode,
      string encodePassword)
      : this(username, password, url, encode, encodePassword, SaveGameWeb.DefaultSerializer)
    {
    }

    public SaveGameWeb(
      string username,
      string password,
      string url,
      bool encode,
      string encodePassword,
      ISaveGameSerializer serializer)
      : this(username, password, url, encode, encodePassword, serializer, SaveGameWeb.DefaultEncoder)
    {
    }

    public SaveGameWeb(
      string username,
      string password,
      string url,
      bool encode,
      string encodePassword,
      ISaveGameSerializer serializer,
      ISaveGameEncoder encoder)
      : this(username, password, url, encode, encodePassword, serializer, encoder, SaveGameWeb.DefaultEncoding)
    {
    }

    public SaveGameWeb(
      string username,
      string password,
      string url,
      bool encode,
      string encodePassword,
      ISaveGameSerializer serializer,
      ISaveGameEncoder encoder,
      Encoding encoding)
    {
      this.m_Username = username;
      this.m_Password = password;
      this.m_URL = url;
      this.m_Encode = encode;
      this.m_EncodePassword = encodePassword;
      this.m_Serializer = serializer;
      this.m_Encoder = encoder;
      this.m_Encoding = encoding;
    }

    public virtual IEnumerator Save<T>(string identifier, T obj)
    {
      MemoryStream memoryStream = new MemoryStream();
      this.Serializer.Serialize<T>(obj, (Stream) memoryStream, this.Encoding);
      string str = this.Encoding.GetString(memoryStream.ToArray());
      if (this.Encode)
        str = this.Encoder.Encode(str, this.EncodePassword);
      yield return (object) this.Send(identifier, str, "save");
      if (this.m_IsError)
        Debug.LogError((object) this.m_Error);
      else
        Debug.Log((object) "Data successfully saved.");
    }

    public virtual IEnumerator Download(string identifier)
    {
      yield return (object) this.Send(identifier, (string) null, "load");
      if (this.m_IsError)
        Debug.LogError((object) this.m_Error);
      else
        Debug.Log((object) "Data successfully downloaded.");
    }

    public virtual T Load<T>(string identifier) => this.Load<T>(identifier, default (T));

    public virtual T Load<T>(string identifier, T defaultValue)
    {
      if ((object) defaultValue == null)
        defaultValue = default (T);
      T obj = defaultValue;
      if (!this.m_IsError && !string.IsNullOrEmpty(this.m_Request.downloadHandler.text))
      {
        string str = this.m_Request.downloadHandler.text;
        if (this.Encode)
          str = this.Encoder.Decode(str, this.EncodePassword);
        MemoryStream memoryStream = new MemoryStream(this.Encoding.GetBytes(str));
        obj = this.Serializer.Deserialize<T>((Stream) memoryStream, this.Encoding);
        memoryStream.Dispose();
        if ((object) obj == null)
          obj = defaultValue;
      }
      return obj;
    }

    public virtual IEnumerator Send(string identifier, string data, string action)
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>()
      {
        {
          nameof (identifier),
          identifier
        },
        {
          nameof (action),
          action
        },
        {
          "username",
          this.Username
        }
      };
      if (!string.IsNullOrEmpty(data))
        dictionary.Add(nameof (data), data);
      if (!string.IsNullOrEmpty(this.Password))
        dictionary.Add("password", this.Password);
      this.m_Request = UnityWebRequest.Post(this.URL, dictionary);
      yield return (object) this.m_Request.Send();
      if (this.m_Request.isNetworkError || this.m_Request.isHttpError)
      {
        this.m_IsError = true;
        this.m_Error = this.m_Request.error;
      }
      else if (this.m_Request.downloadHandler.text.StartsWith("Error"))
      {
        this.m_IsError = true;
        this.m_Error = this.m_Request.downloadHandler.text;
      }
      else
        this.m_IsError = false;
    }
  }
}
