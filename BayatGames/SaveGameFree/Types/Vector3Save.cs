// Decompiled with JetBrains decompiler
// Type: BayatGames.SaveGameFree.Types.Vector3Save
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace BayatGames.SaveGameFree.Types
{
  [Serializable]
  public struct Vector3Save
  {
    public float x;
    public float y;
    public float z;

    public Vector3Save(float x)
    {
      this.x = x;
      this.y = 0.0f;
      this.z = 0.0f;
    }

    public Vector3Save(float x, float y)
    {
      this.x = x;
      this.y = y;
      this.z = 0.0f;
    }

    public Vector3Save(float x, float y, float z)
    {
      this.x = x;
      this.y = y;
      this.z = z;
    }

    public Vector3Save(Vector2 vector)
    {
      this.x = vector.x;
      this.y = vector.y;
      this.z = 0.0f;
    }

    public Vector3Save(Vector3 vector)
    {
      this.x = vector.x;
      this.y = vector.y;
      this.z = vector.z;
    }

    public Vector3Save(Vector4 vector)
    {
      this.x = vector.x;
      this.y = vector.y;
      this.z = vector.z;
    }

    public Vector3Save(Vector2Save vector)
    {
      this.x = vector.x;
      this.y = vector.y;
      this.z = 0.0f;
    }

    public Vector3Save(Vector3Save vector)
    {
      this.x = vector.x;
      this.y = vector.y;
      this.z = vector.z;
    }

    public Vector3Save(Vector4Save vector)
    {
      this.x = vector.x;
      this.y = vector.y;
      this.z = vector.z;
    }

    public static implicit operator Vector3Save(Vector2 vector) => new Vector3Save(vector);

    public static implicit operator Vector2(Vector3Save vector) => new Vector2(vector.x, vector.y);

    public static implicit operator Vector3Save(Vector3 vector) => new Vector3Save(vector);

    public static implicit operator Vector3(Vector3Save vector) => new Vector3(vector.x, vector.y, vector.z);

    public static implicit operator Vector3Save(Vector4 vector) => new Vector3Save(vector);

    public static implicit operator Vector4(Vector3Save vector) => new Vector4(vector.x, vector.y, vector.z);

    public static implicit operator Vector3Save(Vector2Save vector) => new Vector3Save(vector);

    public static implicit operator Vector2Save(Vector3Save vector) => new Vector2Save(vector);

    public static implicit operator Vector3Save(Vector4Save vector) => new Vector3Save(vector);

    public static implicit operator Vector4Save(Vector3Save vector) => new Vector4Save(vector);
  }
}
