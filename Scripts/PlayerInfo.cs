// Decompiled with JetBrains decompiler
// Type: PlayerInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviourPunCallbacks
{
  public Player myPlayer;
  public Text name;
  public Text Kills;
  public Text Tode;
  public GameObject master;
  public GameObject Me;
  public GameObject Ar;
  public GameObject Mp;
  public GameObject Pump;

  private void Start()
  {
    this.Kills.text = "Kills: 0";
    this.Tode.text = "Tode: 0";
  }

  public void SetTodeAndKills(int d, int k)
  {
    this.Tode.text = "Tode: " + d.ToString();
    this.Kills.text = "Kills: " + k.ToString();
  }

  public void SetPlayerInfo(Player player)
  {
    MonoBehaviour.print((object) "setInfo");
    MonoBehaviour.print((object) ("InfoName" + player.NickName));
    string nickName = player.NickName;
    MonoBehaviour.print((object) nickName);
    this.myPlayer = player;
    MonoBehaviour.print((object) this.myPlayer.NickName);
    this.name.text = nickName;
    if (this.myPlayer.IsMasterClient)
      this.master.SetActive(true);
    else
      this.master.SetActive(false);
    if (this.myPlayer == PhotonNetwork.LocalPlayer)
      this.Me.SetActive(true);
    else
      this.Me.SetActive(false);
  }

  public override void OnMasterClientSwitched(Player newMasterClient)
  {
    if (this.myPlayer != newMasterClient)
      return;
    this.master.SetActive(true);
  }

  private void GetCurrentRoomPlayers()
  {
    bool flag = false;
    foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
    {
      if (!flag && player.Value == this.myPlayer)
        flag = true;
    }
    if (flag)
      return;
    Object.Destroy((Object) this.gameObject);
  }

  private void Update()
  {
    GameObject[] gameObjectsWithTag = GameObject.FindGameObjectsWithTag("PlayerManager");
    MonoBehaviour.print((object) ("Gefunden" + (object) gameObjectsWithTag.Length));
    foreach (GameObject gameObject in gameObjectsWithTag)
    {
      PlayerManager component = gameObject.GetComponent<PlayerManager>();
      if (component.PV.Owner == this.myPlayer)
      {
        this.SetTodeAndKills(component.deaths, component.kills);
        MonoBehaviour.print((object) "SetKills");
      }
    }
    if (this.myPlayer.IsMasterClient)
      this.master.SetActive(true);
    else
      this.master.SetActive(false);
    this.GetCurrentRoomPlayers();
    Hashtable hashtable = new Hashtable();
    Hashtable customProperties = this.myPlayer.CustomProperties;
    MonoBehaviour.print((object) "Richtig");
    if ((int) customProperties[(object) "itemIndex"] == 0)
    {
      this.Ar.SetActive(false);
      this.Mp.SetActive(false);
      this.Pump.SetActive(true);
    }
    if ((int) customProperties[(object) "itemIndex"] == 2)
    {
      this.Ar.SetActive(true);
      this.Mp.SetActive(false);
      this.Pump.SetActive(false);
    }
    if ((int) customProperties[(object) "itemIndex"] != 1)
      return;
    this.Ar.SetActive(false);
    this.Mp.SetActive(true);
    this.Pump.SetActive(false);
  }
}
