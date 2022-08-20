// Decompiled with JetBrains decompiler
// Type: BayatGames.SaveGameFree.Types.QuaternionSave
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace BayatGames.SaveGameFree.Types
{
  [Serializable]
  public struct QuaternionSave
  {
    public float x;
    public float y;
    public float z;
    public float w;

    public QuaternionSave(float x)
    {
      this.x = x;
      this.y = 0.0f;
      this.z = 0.0f;
      this.w = 0.0f;
    }

    public QuaternionSave(float x, float y)
    {
      this.x = x;
      this.y = y;
      this.z = 0.0f;
      this.w = 0.0f;
    }

    public QuaternionSave(float x, float y, float z)
    {
      this.x = x;
      this.y = y;
      this.z = z;
      this.w = 0.0f;
    }

    public QuaternionSave(float x, float y, float z, float w)
    {
      this.x = x;
      this.y = y;
      this.z = z;
      this.w = w;
    }

    public QuaternionSave(Quaternion quaternion)
    {
      this.x = quaternion.x;
      this.y = quaternion.y;
      this.z = quaternion.z;
      this.w = quaternion.w;
    }

    public static implicit operator QuaternionSave(Quaternion quaternion) => new QuaternionSave(quaternion);

    public static implicit operator Quaternion(QuaternionSave quaternion) => new Quaternion(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
  }
}
