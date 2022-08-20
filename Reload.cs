// Decompiled with JetBrains decompiler
// Type: Reload
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class Reload : MonoBehaviour
{
  public float ReloadTimer;
  public bool timerIsRunning;
  public GameObject gReload1;
  public GameObject gReload2;
  public GameObject gslider;
  public Text sekunden;
  public Slider slider;
  private float Timer;

  private void Start()
  {
    this.gReload1.SetActive(false);
    this.gReload2.SetActive(false);
    this.gslider.SetActive(false);
    this.slider.maxValue = 1f;
  }

  public void Reset() => this.Start();

  public void ReloadAnim(float ReloadTime)
  {
    this.gameObject.SetActive(true);
    MonoBehaviour.print((object) ReloadTime);
    this.gReload1.SetActive(true);
    this.gReload2.SetActive(true);
    this.gslider.SetActive(true);
    this.ReloadTimer = ReloadTime;
    this.timerIsRunning = true;
    this.Timer = ReloadTime;
  }

  private void Update()
  {
    if (!this.timerIsRunning)
      return;
    this.sekunden.text = ((float) ((double) Mathf.Round(this.ReloadTimer * 10f) / 10.0)).ToString() + "s";
    if ((double) this.ReloadTimer > 0.0)
    {
      this.ReloadTimer -= Time.deltaTime;
      float message = 1f - this.ReloadTimer / this.Timer;
      MonoBehaviour.print((object) message);
      this.slider.value = message;
    }
    else
    {
      MonoBehaviour.print((object) "time is up");
      this.ReloadTimer = 0.0f;
      this.Timer = 0.0f;
      this.timerIsRunning = false;
      this.gReload1.SetActive(false);
      this.gReload2.SetActive(false);
      this.gslider.SetActive(false);
    }
  }
}
