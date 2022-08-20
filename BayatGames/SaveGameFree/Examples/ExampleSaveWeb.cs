// Decompiled with JetBrains decompiler
// Type: BayatGames.SaveGameFree.Examples.ExampleSaveWeb
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using BayatGames.SaveGameFree.Types;
using System.Collections;
using UnityEngine;

namespace BayatGames.SaveGameFree.Examples
{
  public class ExampleSaveWeb : MonoBehaviour
  {
    public Transform target;
    public bool loadOnStart = true;
    public string identifier = "exampleSaveWeb";
    public string username = "savegamefree";
    public string password = "$@ve#game%free";
    public string url = "http://www.example.com/savegamefree.php";
    public bool encode = true;
    public string encodePassword = "h@e#ll$o%^";

    private void Start() => this.Load();

    private void Update()
    {
      Vector3 position = this.target.position;
      position.x += Input.GetAxis("Horizontal");
      position.y += Input.GetAxis("Vertical");
      this.target.position = position;
    }

    public void Load() => this.StartCoroutine(this.LoadEnumerator());

    public void Save() => this.StartCoroutine(this.SaveEnumerator());

    private IEnumerator LoadEnumerator()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      ExampleSaveWeb exampleSaveWeb = this;
      SaveGameWeb web;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        exampleSaveWeb.target.position = (Vector3) web.Load<Vector3Save>(exampleSaveWeb.identifier, (Vector3Save) Vector3.zero);
        Debug.Log((object) "Download Done.");
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      Debug.Log((object) "Downloading...");
      web = new SaveGameWeb(exampleSaveWeb.username, exampleSaveWeb.password, exampleSaveWeb.url, exampleSaveWeb.encode, exampleSaveWeb.encodePassword, SerializerDropdown.Singleton.ActiveSerializer);
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) exampleSaveWeb.StartCoroutine(web.Download(exampleSaveWeb.identifier));
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    private IEnumerator SaveEnumerator()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      ExampleSaveWeb exampleSaveWeb = this;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        Debug.Log((object) "Upload Done.");
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      Debug.Log((object) "Uploading...");
      SaveGameWeb saveGameWeb = new SaveGameWeb(exampleSaveWeb.username, exampleSaveWeb.password, exampleSaveWeb.url, exampleSaveWeb.encode, exampleSaveWeb.encodePassword, SerializerDropdown.Singleton.ActiveSerializer);
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) exampleSaveWeb.StartCoroutine(saveGameWeb.Save<Vector3Save>(exampleSaveWeb.identifier, (Vector3Save) exampleSaveWeb.target.position));
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }
  }
}
