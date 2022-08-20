// Decompiled with JetBrains decompiler
// Type: BayatGames.SaveGameFree.Serializers.SaveGameJsonSerializer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using FullSerializer;
using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace BayatGames.SaveGameFree.Serializers
{
  public class SaveGameJsonSerializer : ISaveGameSerializer
  {
    public void Serialize<T>(T obj, Stream stream, Encoding encoding)
    {
      try
      {
        StreamWriter streamWriter = new StreamWriter(stream, encoding);
        fsSerializer fsSerializer = new fsSerializer();
        fsData data = new fsData();
        T instance = obj;
        ref fsData local = ref data;
        fsSerializer.TrySerialize<T>(instance, out local);
        streamWriter.Write(fsJsonPrinter.CompressedJson(data));
        streamWriter.Dispose();
      }
      catch (Exception ex)
      {
        Debug.LogException(ex);
      }
    }

    public T Deserialize<T>(Stream stream, Encoding encoding)
    {
      T instance = default (T);
      try
      {
        StreamReader streamReader = new StreamReader(stream, encoding);
        new fsSerializer().TryDeserialize<T>(fsJsonParser.Parse(streamReader.ReadToEnd()), ref instance);
        if ((object) instance == null)
          instance = default (T);
        streamReader.Dispose();
      }
      catch (Exception ex)
      {
        Debug.LogException(ex);
      }
      return instance;
    }
  }
}
