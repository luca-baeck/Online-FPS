// Decompiled with JetBrains decompiler
// Type: DamageText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
  public float timerDes;
  public Text text1;
  public Color textColor;
  private float DISAPPEAR_TIMER_MAX = 1f;
  private Vector3 moveVector;

  public void Setup(float damage, float timeD)
  {
    MonoBehaviour.print((object) (nameof (damage) + (object) damage));
    MonoBehaviour.print((object) ("time" + (object) timeD));
    this.text1.text = string.Concat((object) damage);
    this.textColor = this.text1.color;
    this.timerDes = timeD;
    this.DISAPPEAR_TIMER_MAX = this.timerDes;
    this.moveVector = new Vector3(Random.Range(0.1f, 12f), Random.Range(0.1f, 12f)) * 60f;
  }

  private void Update()
  {
    if ((double) this.timerDes > (double) this.DISAPPEAR_TIMER_MAX * 0.5)
      this.transform.localScale += Vector3.one * 0.5f * Time.deltaTime;
    else
      this.transform.localScale -= Vector3.one * 0.5f * Time.deltaTime;
    this.timerDes -= Time.deltaTime;
    this.transform.position += this.moveVector * Time.deltaTime;
    this.moveVector -= this.moveVector * 8f * Time.deltaTime;
    if ((double) this.timerDes >= 0.0)
      return;
    this.textColor.a -= 3f * Time.deltaTime;
    this.text1.color = this.textColor;
    if ((double) this.textColor.a >= 0.0)
      return;
    Object.Destroy((Object) this.gameObject);
  }
}
