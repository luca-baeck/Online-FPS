// Decompiled with JetBrains decompiler
// Type: BayatGames.SaveGameFree.Types.Vector2Save
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace BayatGames.SaveGameFree.Types
{
  [Serializable]
  public struct Vector2Save
  {
    public float x;
    public float y;

    public Vector2Save(float x)
    {
      this.x = x;
      this.y = 0.0f;
    }

    public Vector2Save(float x, float y)
    {
      this.x = x;
      this.y = y;
    }

    public Vector2Save(Vector2 vector)
    {
      this.x = vector.x;
      this.y = vector.y;
    }

    public Vector2Save(Vector3 vector)
    {
      this.x = vector.x;
      this.y = vector.y;
    }

    public Vector2Save(Vector4 vector)
    {
      this.x = vector.x;
      this.y = vector.y;
    }

    public Vector2Save(Vector2Save vector)
    {
      this.x = vector.x;
      this.y = vector.y;
    }

    public Vector2Save(Vector3Save vector)
    {
      this.x = vector.x;
      this.y = vector.y;
    }

    public Vector2Save(Vector4Save vector)
    {
      this.x = vector.x;
      this.y = vector.y;
    }

    public static implicit operator Vector2Save(Vector2 vector) => new Vector2Save(vector);

    public static implicit operator Vector2(Vector2Save vector) => new Vector2(vector.x, vector.y);

    public static implicit operator Vector2Save(Vector3 vector) => new Vector2Save(vector);

    public static implicit operator Vector3(Vector2Save vector) => new Vector3(vector.x, vector.y);

    public static implicit operator Vector2Save(Vector4 vector) => new Vector2Save(vector);

    public static implicit operator Vector4(Vector2Save vector) => new Vector4(vector.x, vector.y);

    public static implicit operator Vector2Save(Vector3Save vector) => new Vector2Save(vector);

    public static implicit operator Vector3Save(Vector2Save vector) => new Vector3Save(vector);

    public static implicit operator Vector2Save(Vector4Save vector) => new Vector2Save(vector);

    public static implicit operator Vector4Save(Vector2Save vector) => new Vector4Save(vector);
  }
}
