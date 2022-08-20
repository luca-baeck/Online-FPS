// Decompiled with JetBrains decompiler
// Type: BayatGames.SaveGameFree.Examples.ExampleSavePosition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using BayatGames.SaveGameFree.Serializers;
using BayatGames.SaveGameFree.Types;
using UnityEngine;

namespace BayatGames.SaveGameFree.Examples
{
  public class ExampleSavePosition : MonoBehaviour
  {
    private string _encodePassword;
    public Transform target;
    public bool loadOnStart = true;
    public string identifier = "exampleSavePosition.dat";

    private void Start()
    {
      this._encodePassword = "12345678910abcdef12345678910abcdef";
      SaveGame.EncodePassword = this._encodePassword;
      SaveGame.Encode = true;
      SaveGame.Serializer = (ISaveGameSerializer) new SaveGameBinarySerializer();
      SaveGame.Save<StorageSG>("pizza2", new StorageSG());
      Debug.Log((object) SaveGame.Load<StorageSG>("pizza2").myDateTime.ToLocalTime().ToString());
      if (!this.loadOnStart)
        return;
      this.Load();
    }

    private void Update()
    {
      Vector3 position = this.target.position;
      position.x += Input.GetAxis("Horizontal");
      position.y += Input.GetAxis("Vertical");
      this.target.position = position;
    }

    private void OnApplicationQuit() => this.Save();

    public void Save() => SaveGame.Save<Vector3Save>(this.identifier, (Vector3Save) this.target.position, SerializerDropdown.Singleton.ActiveSerializer);

    public void Load() => this.target.position = (Vector3) SaveGame.Load<Vector3Save>(this.identifier, (Vector3Save) Vector3.zero, SerializerDropdown.Singleton.ActiveSerializer);
  }
}
