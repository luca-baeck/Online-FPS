// Decompiled with JetBrains decompiler
// Type: BayatGames.SaveGameFree.Serializers.SaveGameXmlSerializer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

namespace BayatGames.SaveGameFree.Serializers
{
  public class SaveGameXmlSerializer : ISaveGameSerializer
  {
    public void Serialize<T>(T obj, Stream stream, Encoding encoding)
    {
      try
      {
        new XmlSerializer(typeof (T)).Serialize(stream, (object) obj);
      }
      catch (Exception ex)
      {
        Debug.LogException(ex);
      }
    }

    public T Deserialize<T>(Stream stream, Encoding encoding)
    {
      T obj = default (T);
      try
      {
        obj = (T) new XmlSerializer(typeof (T)).Deserialize(stream);
      }
      catch (Exception ex)
      {
        Debug.LogException(ex);
      }
      return obj;
    }
  }
}
