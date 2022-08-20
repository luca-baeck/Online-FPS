// Decompiled with JetBrains decompiler
// Type: Overview
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class Overview : MonoBehaviour
{
  public PlayerInfo playerInfo;
  public Transform Content;

  private void Start() => this.GetCurrentRoomPlayers();

  private void GetCurrentRoomPlayers()
  {
    foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
      this.AddPlayerListing(player.Value);
  }

  private void AddPlayerListing(Player player)
  {
    PlayerInfo playerInfo = Object.Instantiate<PlayerInfo>(this.playerInfo, this.Content);
    if (!((Object) playerInfo != (Object) null))
      return;
    playerInfo.SetPlayerInfo(player);
    MonoBehaviour.print((object) ("InfoName" + player.NickName));
  }
}
