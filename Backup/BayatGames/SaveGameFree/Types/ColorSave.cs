// Decompiled with JetBrains decompiler
// Type: BayatGames.SaveGameFree.Types.ColorSave
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace BayatGames.SaveGameFree.Types
{
  [Serializable]
  public struct ColorSave
  {
    public float r;
    public float g;
    public float b;
    public float a;

    public ColorSave(Color color)
    {
      this.r = color.r;
      this.g = color.g;
      this.b = color.b;
      this.a = color.a;
    }

    public static implicit operator ColorSave(Color color) => new ColorSave(color);

    public static implicit operator Color(ColorSave color) => new Color(color.r, color.g, color.b, color.a);
  }
}
