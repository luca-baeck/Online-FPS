// Decompiled with JetBrains decompiler
// Type: KillText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class KillText : MonoBehaviour
{
  public Text text;

  private void Start() => this.text.text = "";

  public void madeKill(string name)
  {
    this.text.text = "Eliminiert: " + name;
    this.Invoke("weg", 2f);
  }

  private void weg() => this.text.text = "";
}
