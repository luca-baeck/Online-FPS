// Decompiled with JetBrains decompiler
// Type: GameJoined
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameJoined : MonoBehaviourPunCallbacks
{
  public int Ping;
  public int minPlayer;
  public Text pingAnzeige;
  public Text fpsAnzeige;
  public string fpsText;
  public float deltaTime;
  public GameObject ZuwenigeSpieler;
  public Text RoomName;
  public GameObject StartGameButton;
  public GameObject pingG;
  public GameObject FpsG;

  private void Start()
  {
    GameObject.FindGameObjectWithTag("MusicManager").GetComponent<MusicManager>().PlayPreGame();
    PhotonNetwork.Instantiate(Path.Combine("Photon", "VoiceChat"), Vector3.zero, Quaternion.identity);
    this.load();
    this.ZuwenigeSpieler.SetActive(false);
    this.StartGameButton.SetActive(PhotonNetwork.IsMasterClient);
    this.RoomName.text = PhotonNetwork.CurrentRoom.Name.ToString();
    MonoBehaviour.print((object) PhotonNetwork.CurrentRoom.Name.ToString());
  }

  public void load()
  {
    MonoBehaviour.print((object) nameof (load));
    this.GetUserData(PlayFab_Log.id);
    this.ZuwenigeSpieler.SetActive(false);
  }

  public override void OnDisconnected(DisconnectCause cause)
  {
    PlayFabClientAPI.ForgetAllCredentials();
    SceneManager.LoadScene("LogIn");
  }

  private void GetUserData(string myPlayFabeId) => PlayFabClientAPI.GetUserData(new GetUserDataRequest()
  {
    PlayFabId = myPlayFabeId,
    Keys = (List<string>) null
  }, (Action<GetUserDataResult>) (result =>
  {
    Debug.Log((object) "Got user data:");
    if (result.Data == null || !result.Data.ContainsKey("Ancestor"))
      Debug.Log((object) "No Ancestor");
    else
      Debug.Log((object) ("Ancestor: " + result.Data["Ancestor"].Value));
    if (result.Data["FPS"].Value == "true")
      this.FpsG.SetActive(true);
    else
      this.FpsG.SetActive(false);
    if (result.Data["Ping"].Value == "true")
      this.pingG.SetActive(true);
    else
      this.pingG.SetActive(false);
  }), (Action<PlayFabError>) (error =>
  {
    Debug.Log((object) "Got error retrieving user data:");
    Debug.Log((object) error.GenerateErrorReport());
    this.load();
  }));

  public override void OnMasterClientSwitched(Player newMasterClient) => this.StartGameButton.SetActive(PhotonNetwork.IsMasterClient);

  private void Update()
  {
    this.Ping = PhotonNetwork.GetPing();
    this.pingAnzeige.text = "Ping: " + (object) this.Ping + "ms";
    this.deltaTime += (float) (((double) Time.deltaTime - (double) this.deltaTime) * 0.10000000149011612);
    this.fpsText = Mathf.Ceil(1f / this.deltaTime).ToString();
  }

  private void FixedUpdate() => this.fpsAnzeige.text = this.fpsText + "fps";

  public void LeaveGame()
  {
    PhotonNetwork.LeaveRoom();
    this.ZuwenigeSpieler.SetActive(false);
  }

  public override void OnLeftRoom()
  {
    SceneManager.LoadScene("Lobby");
    this.ZuwenigeSpieler.SetActive(false);
  }

  public void StartGame()
  {
    if (PhotonNetwork.CurrentRoom.Players.Count >= this.minPlayer)
    {
      PhotonNetwork.CurrentRoom.IsOpen = false;
      PhotonNetwork.LoadLevel("Multiplayer");
      this.ZuwenigeSpieler.SetActive(false);
    }
    else
      this.ZuwenigeSpieler.SetActive(true);
  }
}
