// Decompiled with JetBrains decompiler
// Type: PlayerManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.IO;
using UnityEngine;

public class PlayerManager : MonoBehaviourPunCallbacks
{
  public PhotonView PV;
  public GameObject camDeath;
  public GameObject IngameUI;
  private GameObject controller;
  public GameObject Splash_Death;
  public int kills;
  public int deaths;
  public Respawn respawn;
  public KillsAndDeaths killsAndDeaths;
  public KillText killText;

  private void Awake()
  {
    Screen.lockCursor = true;
    Cursor.visible = false;
    this.PV = this.GetComponent<PhotonView>();
    this.respawn = GameObject.Find("Respawn").GetComponent<Respawn>();
    this.killsAndDeaths = GameObject.Find("KillsUndDeaths").GetComponent<KillsAndDeaths>();
    this.killText = GameObject.Find("Kill").GetComponent<KillText>();
    if (!this.PV.IsMine)
      return;
    this.IngameUI = GameObject.Find("InGameGraphics");
    this.IngameUI.SetActive(false);
  }

  private void CreateController()
  {
    this.IngameUI.SetActive(true);
    Transform spawnpoint = SpawnManager.Instance.GetSpawnpoint();
    this.controller = PhotonNetwork.Instantiate(Path.Combine("Photon", "PlayerController"), spawnpoint.position, spawnpoint.rotation, data: new object[1]
    {
      (object) this.PV.ViewID
    });
  }

  private void Start()
  {
    if (!this.PV.IsMine)
      return;
    MonoBehaviour.print((object) "hi");
    PhotonNetwork.Instantiate(Path.Combine("Photon", "VoiceChat"), Vector3.zero, Quaternion.identity);
    this.CreateController();
  }

  public void Die(string killer)
  {
    if (killer != "von dir selbst")
      this.PV.RPC("RPC_NameE", RpcTarget.All, (object) PlayFab_Log.Username, (object) killer);
    ++this.deaths;
    this.PV.RPC("RPC_Die", RpcTarget.All, (object) this.controller.transform.position);
    this.killsAndDeaths.KillUpdate(OverviewEnable.kills, this.deaths);
    this.respawn.RespawnCountdown(5f, killer);
    this.IngameUI.SetActive(false);
    this.PV.RPC("RPC_KillsUndDeaths", RpcTarget.All, (object) this.deaths, (object) this.kills);
    PhotonNetwork.Destroy(this.controller);
    Object.Destroy((Object) Object.Instantiate<GameObject>(this.camDeath, this.controller.transform.position, this.controller.transform.rotation), 5f);
    this.StartCoroutine(this.NewPlayer());
    MonoBehaviour.print((object) killer);
  }

  private IEnumerator NewPlayer()
  {
    yield return (object) new WaitForSeconds(5f);
    this.CreateController();
  }

  private void Update()
  {
    MonoBehaviour.print((object) (this.PV.Owner.NickName + ": " + (object) this.kills + " Kills" + (object) this.deaths + " Deaths"));
    if (!this.PV.IsMine || this.kills == OverviewEnable.kills)
      return;
    this.kills = OverviewEnable.kills;
    this.killsAndDeaths.KillUpdate(OverviewEnable.kills, this.deaths);
    this.PV.RPC("RPC_KillsUndDeaths", RpcTarget.All, (object) this.deaths, (object) this.kills);
  }

  [PunRPC]
  private void RPC_NameE(string name, string killer)
  {
    MonoBehaviour.print((object) nameof (RPC_NameE));
    MonoBehaviour.print((object) ("Mörder " + killer));
    MonoBehaviour.print((object) ("Opfer " + name));
    if (!(PlayFab_Log.Username == killer) || !(killer != name))
      return;
    ++OverviewEnable.kills;
    MonoBehaviour.print((object) ("you killed " + name));
    this.killText.madeKill(name);
  }

  [PunRPC]
  private void RPC_KillsUndDeaths(int d, int k)
  {
    MonoBehaviour.print((object) "UpdateKills");
    if (this.PV.IsMine)
      return;
    this.kills = k;
    this.deaths = d;
    MonoBehaviour.print((object) "SetKillsandDeatgs");
  }

  [PunRPC]
  private void RPC_Die(Vector3 DiePos)
  {
    MonoBehaviour.print((object) "RPC Die wird ausgeführt");
    Object.Destroy((Object) Object.Instantiate<GameObject>(this.Splash_Death, DiePos, Quaternion.identity), 3f);
  }

  public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
  {
    if (!this.PV.IsMine || targetPlayer != this.PV.Owner)
      return;
    MonoBehaviour.print((object) "miener");
  }
}
