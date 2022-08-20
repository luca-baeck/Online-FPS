// Decompiled with JetBrains decompiler
// Type: LobbyScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using BayatGames.SaveGameFree;
using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyScript : MonoBehaviourPunCallbacks
{
  public InputField CreateRoomI;
  public InputField JoinRoomI;
  public bool Roomprivate;
  public Text Error;
  public GameObject ErrorG;
  public GameObject Alles;
  public GameObject Laden;
  public GameObject RM;

  private void Start()
  {
    GameObject.FindGameObjectWithTag("MusicManager").GetComponent<MusicManager>().PlayLobby();
    if (SaveGame.Exists("Skin"))
      SpindManager.Skin = SaveGame.Load<string>("Skin");
    Cursor.visible = true;
    Screen.lockCursor = false;
    this.Roomprivate = true;
    this.ErrorG.SetActive(false);
    this.Alles.SetActive(true);
    this.Laden.SetActive(false);
    this.RM = GameObject.Find("RoomManager");
  }

  public void Beenden() => Application.Quit();

  public void CreateRoom()
  {
    this.Laden.SetActive(true);
    this.Alles.SetActive(false);
    this.ErrorG.SetActive(false);
    RoomOptions roomOptions = new RoomOptions();
    roomOptions.MaxPlayers = (byte) 6;
    if (this.Roomprivate)
      roomOptions.IsVisible = false;
    if (!this.Roomprivate)
      roomOptions.IsVisible = true;
    if (this.CreateRoomI.text.Length >= 1)
    {
      PhotonNetwork.CreateRoom(this.CreateRoomI.text, roomOptions);
    }
    else
    {
      this.Laden.SetActive(false);
      this.Alles.SetActive(true);
    }
  }

  public override void OnCreatedRoom()
  {
    this.Laden.SetActive(false);
    this.ErrorG.SetActive(false);
  }

  public override void OnCreateRoomFailed(short returnCode, string message)
  {
    this.Laden.SetActive(false);
    this.Alles.SetActive(true);
    MonoBehaviour.print((object) message);
    this.Error.text = "";
    this.Error.text = message;
    this.ErrorG.SetActive(true);
  }

  public void JoinRoom()
  {
    this.Laden.SetActive(true);
    this.Alles.SetActive(false);
    this.ErrorG.SetActive(false);
    PhotonNetwork.JoinRoom(this.JoinRoomI.text);
  }

  public override void OnJoinRoomFailed(short returnCode, string message)
  {
    this.Laden.SetActive(false);
    this.Alles.SetActive(true);
    MonoBehaviour.print((object) message);
    this.Error.text = "";
    this.Error.text = message;
    this.ErrorG.SetActive(true);
  }

  public void JoinRandom()
  {
    this.Laden.SetActive(true);
    this.Alles.SetActive(false);
    this.ErrorG.SetActive(false);
    PhotonNetwork.JoinRandomRoom();
  }

  public override void OnJoinRandomFailed(short returnCode, string message)
  {
    MonoBehaviour.print((object) message);
    this.Error.text = "";
    this.Error.text = message;
    this.ErrorG.SetActive(true);
    this.CreateOpenRoom();
  }

  public void CreateOpenRoom() => PhotonNetwork.CreateRoom(PlayFab_Log.Username + " 000" + (object) Random.Range(0, 100), new RoomOptions()
  {
    MaxPlayers = (byte) 6,
    IsVisible = true
  });

  public override void OnJoinedRoom() => SceneManager.LoadScene("GameJoined");

  public void IsPrivate() => this.Roomprivate = !this.Roomprivate;

  public void Abmelden()
  {
    this.Laden.SetActive(true);
    this.Alles.SetActive(false);
    PhotonNetwork.Disconnect();
    Object.Destroy((Object) this.RM);
  }

  public override void OnDisconnected(DisconnectCause cause)
  {
    SaveGame.Delete("Username");
    SaveGame.Delete("Password");
    PlayFabClientAPI.ForgetAllCredentials();
    SceneManager.LoadScene("LogIn");
  }

  public void Shop() => SceneManager.LoadScene(nameof (Shop));

  public void Spind() => SceneManager.LoadScene(nameof (Spind));
}
