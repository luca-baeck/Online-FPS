// Decompiled with JetBrains decompiler
// Type: Healthbar
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
  public Slider green;
  public Slider blue;
  public Text greent;
  public Text bluet;

  private void Start()
  {
    this.blue.maxValue = 100f;
    this.green.maxValue = 100f;
  }

  public void SetHealth(float bluelive, float greenlive)
  {
    this.blue.value = bluelive;
    this.green.value = greenlive;
    this.greent.text = greenlive.ToString();
    this.bluet.text = bluelive.ToString();
  }
}
