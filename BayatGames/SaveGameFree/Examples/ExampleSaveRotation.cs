// Decompiled with JetBrains decompiler
// Type: BayatGames.SaveGameFree.Examples.ExampleSaveRotation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using BayatGames.SaveGameFree.Types;
using UnityEngine;

namespace BayatGames.SaveGameFree.Examples
{
  public class ExampleSaveRotation : MonoBehaviour
  {
    public Transform target;
    public bool loadOnStart = true;
    public string identifier = "exampleSaveRotation.dat";

    private void Start()
    {
      if (!this.loadOnStart)
        return;
      this.Load();
    }

    private void Update()
    {
      Vector3 eulerAngles = this.target.rotation.eulerAngles;
      eulerAngles.z += Input.GetAxis("Horizontal");
      this.target.rotation = Quaternion.Euler(eulerAngles);
    }

    private void OnApplicationQuit() => this.Save();

    public void Save() => SaveGame.Save<QuaternionSave>(this.identifier, (QuaternionSave) this.target.rotation, SerializerDropdown.Singleton.ActiveSerializer);

    public void Load() => this.target.rotation = (Quaternion) SaveGame.Load<QuaternionSave>(this.identifier, (QuaternionSave) Quaternion.identity, SerializerDropdown.Singleton.ActiveSerializer);
  }
}
