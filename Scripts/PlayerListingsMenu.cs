// Decompiled with JetBrains decompiler
// Type: PlayerListingsMenu
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListingsMenu : MonoBehaviourPunCallbacks
{
  [SerializeField]
  private Transform _content;
  [SerializeField]
  private Playerlisting _playerListing;
  private List<Playerlisting> _listings = new List<Playerlisting>();

  private void Awake() => this.GetCurrentRoomPlayers();

  private void GetCurrentRoomPlayers()
  {
    foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
      this.AddPlayerListing(player.Value);
  }

  public override void OnMasterClientSwitched(Player newMasterClient)
  {
    int index = this._listings.FindIndex((Predicate<Playerlisting>) (x => x.Player == newMasterClient));
    if (index == -1)
      return;
    UnityEngine.Object.Destroy((UnityEngine.Object) this._listings[index].gameObject);
    this._listings.RemoveAt(index);
    Playerlisting playerlisting = UnityEngine.Object.Instantiate<Playerlisting>(this._playerListing, this._content);
    if (!((UnityEngine.Object) playerlisting != (UnityEngine.Object) null))
      return;
    playerlisting.SetPlayerInfo(newMasterClient);
    this._listings.Add(playerlisting);
  }

  private void AddPlayerListing(Player player)
  {
    Playerlisting playerlisting = UnityEngine.Object.Instantiate<Playerlisting>(this._playerListing, this._content);
    if (!((UnityEngine.Object) playerlisting != (UnityEngine.Object) null))
      return;
    playerlisting.SetPlayerInfo(player);
    this._listings.Add(playerlisting);
  }

  public override void OnPlayerEnteredRoom(Player newPlayer) => this.AddPlayerListing(newPlayer);

  public override void OnPlayerLeftRoom(Player otherPlayer)
  {
    int index = this._listings.FindIndex((Predicate<Playerlisting>) (x => x.Player == otherPlayer));
    if (index == -1)
      return;
    UnityEngine.Object.Destroy((UnityEngine.Object) this._listings[index].gameObject);
    this._listings.RemoveAt(index);
  }
}
