// Decompiled with JetBrains decompiler
// Type: BayatGames.SaveGameFree.Types.MeshSave
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using System;
using System.Linq;
using UnityEngine;

namespace BayatGames.SaveGameFree.Types
{
  [Serializable]
  public class MeshSave
  {
    public Vector3Save[] vertices;
    public int[] triangles;
    public Vector2Save[] uv;
    public Vector3Save[] normals;
    public Color[] colors;
    public Color32[] colors32;

    public MeshSave(Mesh mesh)
    {
      this.vertices = Enumerable.Cast<Vector3Save>(mesh.vertices).ToArray<Vector3Save>();
      this.triangles = mesh.triangles;
      this.uv = Enumerable.Cast<Vector2Save>(mesh.uv).ToArray<Vector2Save>();
      this.normals = Enumerable.Cast<Vector3Save>(mesh.normals).ToArray<Vector3Save>();
      this.colors = Enumerable.Cast<Color>(mesh.colors).ToArray<Color>();
      this.colors32 = Enumerable.Cast<Color32>(mesh.colors32).ToArray<Color32>();
    }

    public static implicit operator MeshSave(Mesh mesh) => new MeshSave(mesh);

    public static implicit operator Mesh(MeshSave mesh) => new Mesh()
    {
      vertices = Enumerable.Cast<Vector3>(mesh.vertices).ToArray<Vector3>(),
      triangles = mesh.triangles,
      uv = Enumerable.Cast<Vector2>(mesh.uv).ToArray<Vector2>(),
      normals = Enumerable.Cast<Vector3>(mesh.normals).ToArray<Vector3>(),
      colors = Enumerable.Cast<Color>(mesh.colors).ToArray<Color>(),
      colors32 = Enumerable.Cast<Color32>(mesh.colors32).ToArray<Color32>()
    };
  }
}
