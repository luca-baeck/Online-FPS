// Decompiled with JetBrains decompiler
// Type: BayatGames.SaveGameFree.Examples.ExampleSaveScale
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using BayatGames.SaveGameFree.Types;
using UnityEngine;

namespace BayatGames.SaveGameFree.Examples
{
  public class ExampleSaveScale : MonoBehaviour
  {
    public Transform target;
    public bool loadOnStart = true;
    public string identifier = "exampleSaveScale.dat";

    private void Start()
    {
      if (!this.loadOnStart)
        return;
      this.Load();
    }

    private void Update()
    {
      Vector3 localScale = this.target.localScale;
      localScale.x += Input.GetAxis("Horizontal");
      localScale.y += Input.GetAxis("Vertical");
      this.target.localScale = localScale;
    }

    private void OnApplicationQuit() => this.Save();

    public void Save() => SaveGame.Save<Vector3Save>(this.identifier, (Vector3Save) this.target.localScale, SerializerDropdown.Singleton.ActiveSerializer);

    public void Load() => this.target.localScale = (Vector3) SaveGame.Load<Vector3Save>(this.identifier, new Vector3Save(1f, 1f, 1f), SerializerDropdown.Singleton.ActiveSerializer);
  }
}
