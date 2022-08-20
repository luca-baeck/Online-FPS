// Decompiled with JetBrains decompiler
// Type: Respawn
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class Respawn : MonoBehaviour
{
  private bool timerisRunning;
  private float timer;
  public Text Anzeige;
  public GameObject gAnzeige;
  public Text Killer;
  public GameObject gKiller;

  private void Start()
  {
    this.gAnzeige.SetActive(false);
    this.gKiller.SetActive(false);
  }

  public void RespawnCountdown(float time, string name)
  {
    this.timerisRunning = true;
    this.timer = time;
    this.gAnzeige.SetActive(true);
    this.gKiller.SetActive(true);
    this.Killer.text = "Du wurdest von " + name + " getötet";
  }

  private void Update()
  {
    if (!this.timerisRunning)
      return;
    this.Anzeige.text = "Respawn in " + (object) (float) ((double) Mathf.Round(this.timer * 10f) / 10.0) + "s";
    if ((double) this.timer > 0.0)
    {
      this.timer -= Time.deltaTime;
    }
    else
    {
      this.timer = 0.0f;
      this.timerisRunning = false;
      this.gAnzeige.SetActive(false);
      this.gKiller.SetActive(false);
    }
  }
}
