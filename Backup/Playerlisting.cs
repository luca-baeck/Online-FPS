// Decompiled with JetBrains decompiler
// Type: Playerlisting
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class Playerlisting : MonoBehaviour
{
  [SerializeField]
  private Text _text;
  [SerializeField]
  private GameObject Krone;

  public Player Player { get; private set; }

  public void SetPlayerInfo(Player player)
  {
    this.Player = player;
    this._text.text = player.NickName;
    if (player.IsMasterClient)
      this.Krone.SetActive(true);
    else
      this.Krone.SetActive(false);
  }
}
