// Decompiled with JetBrains decompiler
// Type: BayatGames.SaveGameFree.Serializers.ISaveGameSerializer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using System.IO;
using System.Text;

namespace BayatGames.SaveGameFree.Serializers
{
  public interface ISaveGameSerializer
  {
    void Serialize<T>(T obj, Stream stream, Encoding encoding);

    T Deserialize<T>(Stream stream, Encoding encoding);
  }
}
