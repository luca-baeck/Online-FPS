// Decompiled with JetBrains decompiler
// Type: BayatGames.SaveGameFree.Encoders.ISaveGameEncoder
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

namespace BayatGames.SaveGameFree.Encoders
{
  public interface ISaveGameEncoder
  {
    string Encode(string input, string password);

    string Decode(string input, string password);
  }
}
