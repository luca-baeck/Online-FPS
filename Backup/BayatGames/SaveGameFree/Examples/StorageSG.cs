// Decompiled with JetBrains decompiler
// Type: BayatGames.SaveGameFree.Examples.StorageSG
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using System;

namespace BayatGames.SaveGameFree.Examples
{
  [Serializable]
  public class StorageSG
  {
    public DateTime myDateTime;

    public StorageSG() => this.myDateTime = DateTime.UtcNow;
  }
}
