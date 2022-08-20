// Decompiled with JetBrains decompiler
// Type: LeaderboardPlayerScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class LeaderboardPlayerScript : MonoBehaviour
{
  public RawImage img;
  public Text Rank;
  public Text Name;
  public Text Value;
  public GameObject Me;
  public Color c;

  public void SetUp(int rank, int value, string name, string id)
  {
    if (id == PlayFab_Log.id)
    {
      this.Me.SetActive(true);
      this.img.color = this.c;
    }
    else
    {
      this.Me.SetActive(false);
      this.img.color = new Color((float) byte.MaxValue, (float) byte.MaxValue, (float) byte.MaxValue, 0.0f);
    }
    this.Rank.text = rank.ToString() + ".";
    this.Name.text = name;
    this.Value.text = value.ToString() + " Siege";
  }
}
