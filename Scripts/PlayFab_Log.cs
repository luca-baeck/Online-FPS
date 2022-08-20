// Decompiled with JetBrains decompiler
// Type: PlayFab_Log
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using BayatGames.SaveGameFree;
using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayFab_Log : MonoBehaviourPunCallbacks
{
  public static string Username;
  public InputField regUsername;
  public InputField regEmail;
  public InputField regPassword;
  public InputField logUsername;
  public InputField logPassword;
  public GameObject regPanel;
  public GameObject logPanel;
  public GameObject zurPanel;
  public GameObject Error;
  public GameObject Alles;
  public GameObject Laden;
  public GameObject VersionFalsch;
  public GameObject AccountBan;
  public GameObject News;
  public Text ErrorText;
  public Text NameText;
  public Text BanGrund;
  public Text BanDauer;
  public bool speichern;
  public Text NewsTitleText;
  public Text NewsBodyText;
  public GameObject V;
  public InputField Password;
  public Text space;
  public Text ErrorPun;
  public GameObject ErrorPunGO;
  public static string id;
  public bool existsName;
  public bool existsPassword;
  public GameObject RoomManager;

  private void Start()
  {
    UnityEngine.Object.Instantiate<GameObject>(this.RoomManager, Vector3.zero, Quaternion.identity);
    this.LosGehts();
  }

  public void Exit() => Application.Quit();

  private void LosGehts()
  {
    this.VersionFalsch.SetActive(false);
    this.AccountBan.SetActive(false);
    this.Alles.SetActive(true);
    this.Laden.SetActive(false);
    this.existsName = SaveGame.Exists("Username");
    this.existsPassword = SaveGame.Exists("Password");
    if (this.existsName && !this.existsPassword)
      SaveGame.Delete("Username");
    if (!this.existsName && this.existsPassword)
      SaveGame.Delete("Password");
    this.existsName = SaveGame.Exists("Username");
    this.existsPassword = SaveGame.Exists("Password");
    if (this.existsPassword && this.existsName)
    {
      this.logUsername.text = SaveGame.Load<string>("Username");
      this.logPassword.text = SaveGame.Load<string>("Password");
      MonoBehaviour.print((object) this.logUsername.text);
      MonoBehaviour.print((object) ("Password: " + this.logPassword.text));
      this.LogIn();
    }
    Cursor.visible = true;
    this.V.SetActive(false);
    this.regPanel.SetActive(false);
    this.Error.SetActive(false);
    this.zurPanel.SetActive(false);
    this.News.SetActive(false);
    this.speichern = true;
  }

  private void Fail(PlayFabError obj)
  {
    this.Error.SetActive(true);
    Debug.Log((object) "Fail1");
    this.ErrorText.text = obj.Error.ToString();
    if (obj.Error.ToString() == "AccountBanned")
    {
      foreach (KeyValuePair<string, List<string>> errorDetail in obj.ErrorDetails)
      {
        this.AccountBan.SetActive(true);
        this.BanGrund.text = "Grund: " + errorDetail.Key;
        this.BanDauer.text = "Ban aktiv bis: " + errorDetail.Value[0] + " (UTC)";
      }
    }
    this.Alles.SetActive(true);
    this.Laden.SetActive(false);
  }

  public void Toggle() => this.speichern = !this.speichern;

  public void Register()
  {
    this.AccountBan.SetActive(false);
    this.Alles.SetActive(false);
    this.Laden.SetActive(true);
    this.Error.SetActive(false);
    PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest()
    {
      TitleId = PlayFabSettings.TitleId,
      Email = this.regEmail.text,
      Username = this.regUsername.text,
      Password = this.regPassword.text
    }, new Action<RegisterPlayFabUserResult>(this.SuccessR), new Action<PlayFabError>(this.Fail));
  }

  private void SuccessR(RegisterPlayFabUserResult obj) => this.ContactEmail();

  public void ContactEmail() => PlayFabClientAPI.AddOrUpdateContactEmail(new AddOrUpdateContactEmailRequest()
  {
    EmailAddress = this.regEmail.text
  }, new Action<AddOrUpdateContactEmailResult>(this.SuccessE), new Action<PlayFabError>(this.Fail));

  private void SuccessE(AddOrUpdateContactEmailResult obj)
  {
    MonoBehaviour.print((object) "email");
    this.DisplayName();
  }

  public void DisplayName()
  {
    PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest()
    {
      DisplayName = this.regUsername.text
    }, new Action<UpdateUserTitleDisplayNameResult>(this.SuccessD), new Action<PlayFabError>(this.Fail));
    this.SetUserData();
  }

  public void SetUserData() => PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
  {
    Data = new Dictionary<string, string>()
    {
      {
        "Playerlist",
        "Tab"
      },
      {
        "Granate",
        "G"
      },
      {
        "Nachladen",
        "R"
      },
      {
        "AR",
        "Alpha1"
      },
      {
        "Pump",
        "Alpha2"
      },
      {
        "MP",
        "Alpha3"
      },
      {
        "MouseX",
        "1"
      },
      {
        "MouseY",
        "1"
      },
      {
        "Ping",
        "true"
      },
      {
        "FPS",
        "true"
      }
    }
  }, (Action<UpdateUserDataResult>) (result => this.SuccessSetUserData()), (Action<PlayFabError>) (error =>
  {
    Debug.Log((object) "Got error setting user data");
    Debug.Log((object) error.GenerateErrorReport());
  }));

  private void SuccessSetUserData()
  {
    this.Laden.SetActive(false);
    this.Alles.SetActive(true);
  }

  private void SuccessD(UpdateUserTitleDisplayNameResult obj)
  {
    Debug.Log((object) "Success Registration");
    this.regPanel.SetActive(false);
    this.Error.SetActive(false);
    this.logPanel.SetActive(true);
    this.regEmail.text = "";
    this.regUsername.text = "";
    this.regPassword.text = "";
    this.logUsername.text = "";
    this.logPassword.text = "";
  }

  public void LogIn()
  {
    this.AccountBan.SetActive(false);
    this.Alles.SetActive(false);
    this.Laden.SetActive(true);
    this.Error.SetActive(false);
    this.V.SetActive(false);
    PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest()
    {
      TitleId = PlayFabSettings.TitleId,
      Username = this.logUsername.text,
      Password = this.logPassword.text
    }, new Action<LoginResult>(this.SuccessL), new Action<PlayFabError>(this.Fail));
  }

  private void SuccessL(LoginResult obj)
  {
    Debug.Log((object) "Success LogIn");
    this.Error.SetActive(false);
    this.regPanel.SetActive(false);
    if (this.speichern)
    {
      SaveGame.Encode = false;
      SaveGame.Delete("Username");
      SaveGame.Delete("Password");
      SaveGame.Save<string>("Username", this.logUsername.text);
      SaveGame.Save<string>("Password", this.logPassword.text);
      MonoBehaviour.print((object) ("Log Password: " + this.logPassword.text));
    }
    this.GetAccountInfo();
  }

  public void KontoErstellen()
  {
    this.logPanel.SetActive(false);
    this.regPanel.SetActive(true);
    this.Error.SetActive(false);
    this.zurPanel.SetActive(false);
  }

  public void ZurückUndAnmelden()
  {
    this.regPanel.SetActive(false);
    this.logPanel.SetActive(true);
    this.Error.SetActive(false);
    this.zurPanel.SetActive(false);
  }

  public void Zurücksetzen()
  {
    this.logPanel.SetActive(false);
    this.regPanel.SetActive(false);
    this.Error.SetActive(false);
    this.zurPanel.SetActive(true);
  }

  public void GetAccountInfo() => PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), new Action<GetAccountInfoResult>(this.SuccessI), new Action<PlayFabError>(this.Fail));

  private void SuccessI(GetAccountInfoResult obj)
  {
    Debug.Log((object) "info success");
    PlayFab_Log.id = obj.AccountInfo.PlayFabId;
    this.GetPlayerData();
    MonoBehaviour.print((object) obj.AccountInfo.Username);
    MonoBehaviour.print((object) obj.AccountInfo.Created);
    PlayFab_Log.Username = obj.AccountInfo.Username.ToString();
    Debug.Log((object) PlayFab_Log.Username);
  }

  public void GetNews() => PlayFabClientAPI.GetTitleNews(new GetTitleNewsRequest()
  {
    Count = new int?()
  }, new Action<GetTitleNewsResult>(this.SuccessN), new Action<PlayFabError>(this.Fail));

  private void SuccessN(GetTitleNewsResult obj)
  {
    this.NameText.text = PlayFab_Log.Username;
    this.News.SetActive(true);
    this.ErrorPunGO.SetActive(false);
    MonoBehaviour.print((object) "success news");
    int count = obj.News.Count;
    MonoBehaviour.print((object) obj.News.Count);
    MonoBehaviour.print((object) obj.News[0].Title.ToString());
    MonoBehaviour.print((object) obj.News[0].Body.ToString());
    MonoBehaviour.print((object) obj.News[count - 1].Title.ToString());
    MonoBehaviour.print((object) obj.News[count - 1].Body.ToString());
    if (Application.version.ToString() == obj.News[count - 1].Body.ToString())
    {
      this.Alles.SetActive(true);
      this.Laden.SetActive(false);
      this.NewsTitleText.text = obj.News[0].Title.ToString();
      this.NewsBodyText.text = obj.News[0].Body.ToString();
    }
    else
    {
      this.Alles.SetActive(false);
      this.Laden.SetActive(false);
      this.VersionFalsch.SetActive(true);
    }
  }

  public void ZumSpiel()
  {
    this.connectPhoton();
    this.ErrorPunGO.SetActive(false);
  }

  public void connectPhoton()
  {
    this.Alles.SetActive(false);
    this.Laden.SetActive(true);
    PhotonNetwork.NickName = PlayFab_Log.Username;
    PhotonNetwork.ConnectUsingSettings();
    MonoBehaviour.print((object) "Connecting");
  }

  public override void OnConnectedToMaster()
  {
    MonoBehaviour.print((object) "Connected");
    PhotonNetwork.AutomaticallySyncScene = true;
    PhotonNetwork.JoinLobby();
  }

  public override void OnJoinedLobby() => SceneManager.LoadScene("Lobby");

  public override void OnDisconnected(DisconnectCause cause)
  {
    base.OnDisconnected(cause);
    MonoBehaviour.print((object) "error");
    this.ErrorPunGO.SetActive(true);
    this.ErrorPun.text = cause.ToString();
    this.Alles.SetActive(true);
    this.Laden.SetActive(false);
  }

  public void ResetPassword()
  {
    this.AccountBan.SetActive(false);
    this.Alles.SetActive(false);
    this.Laden.SetActive(true);
    this.Error.SetActive(false);
    string text = this.Password.text;
    if (!(text != ""))
      return;
    PlayFabClientAPI.SendAccountRecoveryEmail(new SendAccountRecoveryEmailRequest()
    {
      Email = text,
      TitleId = "E3D0C"
    }, new Action<SendAccountRecoveryEmailResult>(this.SuccessZ), new Action<PlayFabError>(this.FailZ));
  }

  private void SuccessZ(SendAccountRecoveryEmailResult obj)
  {
    MonoBehaviour.print((object) "Email wurde versendet");
    this.Password.text = "";
    this.space.text = "Hura:)";
  }

  private void FailZ(PlayFabError obj)
  {
    this.Alles.SetActive(true);
    this.Laden.SetActive(false);
    this.Error.SetActive(true);
    Debug.Log((object) "Fail1");
    this.ErrorText.text = obj.Error.ToString();
    this.Password.text = "";
    this.space.text = "Ohjee";
  }

  private void GetPlayerData() => PlayFabClientAPI.GetPlayerProfile(new GetPlayerProfileRequest()
  {
    PlayFabId = PlayFab_Log.id,
    ProfileConstraints = new PlayerProfileViewConstraints()
    {
      ShowContactEmailAddresses = true
    }
  }, new Action<GetPlayerProfileResult>(this.SuccessG), new Action<PlayFabError>(this.Fail));

  private void SuccessG(GetPlayerProfileResult obj)
  {
    MonoBehaviour.print((object) "hura");
    List<ContactEmailInfoModel> contactEmailAddresses = obj.PlayerProfile.ContactEmailAddresses;
    if (contactEmailAddresses.Count <= 0)
      return;
    MonoBehaviour.print((object) contactEmailAddresses[0].EmailAddress);
    MonoBehaviour.print((object) contactEmailAddresses[0].VerificationStatus);
    EmailVerificationStatus? verificationStatus1 = contactEmailAddresses[0].VerificationStatus;
    EmailVerificationStatus verificationStatus2 = EmailVerificationStatus.Confirmed;
    if (!(verificationStatus1.GetValueOrDefault() == verificationStatus2 & verificationStatus1.HasValue))
    {
      this.Alles.SetActive(true);
      this.Laden.SetActive(false);
      this.V.SetActive(true);
    }
    else
    {
      this.GetUserData(PlayFab_Log.id);
      this.logPanel.SetActive(false);
    }
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
    this.GetNews();
  }), (Action<PlayFabError>) (error =>
  {
    Debug.Log((object) "Got error retrieving user data:");
    Debug.Log((object) error.GenerateErrorReport());
    this.ErrorText.text = error.GenerateErrorReport();
    this.Alles.SetActive(true);
    this.Laden.SetActive(false);
  }));
}
