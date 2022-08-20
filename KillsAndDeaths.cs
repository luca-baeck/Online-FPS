// Decompiled with JetBrains decompiler
// Type: KillsAndDeaths
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class KillsAndDeaths : MonoBehaviour
{
  public Text Kills;
  public Text Deaths;

  private void Start()
  {
    this.Kills.text = 0.ToString();
    this.Deaths.text = 0.ToString();
  }

  public void KillUpdate(int _kills, int _deaths)
  {
    this.Kills.text = _kills.ToString();
    this.Deaths.text = _deaths.ToString();
  }
}
