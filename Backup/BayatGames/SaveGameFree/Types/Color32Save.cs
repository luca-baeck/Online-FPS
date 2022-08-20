// Decompiled with JetBrains decompiler
// Type: BayatGames.SaveGameFree.Types.Color32Save
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace BayatGames.SaveGameFree.Types
{
  [Serializable]
  public struct Color32Save
  {
    public byte r;
    public byte g;
    public byte b;
    public byte a;

    public Color32Save(Color32 color)
    {
      this.r = color.r;
      this.g = color.g;
      this.b = color.b;
      this.a = color.a;
    }

    public static implicit operator Color32Save(Color32 color) => new Color32Save(color);

    public static implicit operator Color32(Color32Save color) => new Color32(color.r, color.g, color.b, color.a);
  }
}
