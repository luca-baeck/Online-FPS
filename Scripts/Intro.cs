// Decompiled with JetBrains decompiler
// Type: Intro
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
  public float wait;

  private void Start()
  {
    this.Invoke("LoadScene", this.wait);
    GameObject.FindGameObjectWithTag("MusicManager").GetComponent<MusicManager>().PlayIntro();
  }

  private void LoadScene() => SceneManager.LoadScene("LogIn");
}
