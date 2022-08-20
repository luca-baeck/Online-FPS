// Decompiled with JetBrains decompiler
// Type: SettingsLobby
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsLobby : MonoBehaviour
{
  public GameObject All;
  public GameObject Alles;
  public GameObject Load;
  public GameObject Settings1;
  public GameObject Error;
  public Slider MouseX;
  public Slider MouseY;
  public Toggle ping;
  public Toggle fps;
  private string pingT;
  private string fpsT;
  public string whichButton;
  public bool buttonBind;
  public Text MouseXt;
  public Text MouseYt;
  public Text Playerlist;
  public Text Granate;
  public Text Nachladen;
  public Text AR;
  public Text Pump;
  public Text MP;

  private void Start()
  {
    this.Settings1.SetActive(false);
    this.All.SetActive(false);
    this.Alles.SetActive(true);
    this.Load.SetActive(true);
    this.Error.SetActive(false);
  }

  private void Update()
  {
    float num1 = Mathf.Round(this.MouseX.value * 10f) / 10f;
    float num2 = Mathf.Round(this.MouseY.value * 10f) / 10f;
    this.MouseXt.text = num1.ToString();
    this.MouseYt.text = num2.ToString();
    if (!this.buttonBind || !Input.anyKeyDown)
      return;
    foreach (KeyCode keyCode in System.Enum.GetValues(typeof (KeyCode)))
    {
      if (Input.GetKeyDown(keyCode) && keyCode.ToString() != this.Playerlist.text && keyCode.ToString() != this.Granate.text && keyCode.ToString() != this.Nachladen.text && keyCode.ToString() != this.AR.text && keyCode.ToString() != this.Pump.text && keyCode.ToString() != this.MP.text && keyCode.ToString() != "W" && keyCode.ToString() != "D" && keyCode.ToString() != "S" && keyCode.ToString() != "A" && keyCode.ToString() != "LeftShift" && keyCode.ToString() != "LeftControl" && keyCode.ToString() != "Space" && keyCode.ToString() != "Mouse0" && keyCode.ToString() != "Mouse1" && keyCode.ToString() != "Escape")
      {
        this.Rebind(keyCode);
        this.buttonBind = false;
        break;
      }
    }
  }

  public void Playerlisti()
  {
    if (this.buttonBind)
      return;
    this.Playerlist.text = "";
    this.buttonBind = true;
    this.whichButton = "Playerlist";
  }

  public void Granatei()
  {
    if (this.buttonBind)
      return;
    this.Granate.text = "";
    this.buttonBind = true;
    this.whichButton = "Granate";
  }

  public void Nachladeni()
  {
    if (this.buttonBind)
      return;
    this.Nachladen.text = "";
    this.buttonBind = true;
    this.whichButton = "Nachladen";
  }

  public void ARi()
  {
    if (this.buttonBind)
      return;
    this.AR.text = "";
    this.buttonBind = true;
    this.whichButton = "AR";
  }

  public void Pumpi()
  {
    if (this.buttonBind)
      return;
    this.Pump.text = "";
    this.buttonBind = true;
    this.whichButton = "Pump";
  }

  public void MPi()
  {
    if (this.buttonBind)
      return;
    this.MP.text = "";
    this.buttonBind = true;
    this.whichButton = "MP";
  }

  public void Rebind(KeyCode keyC)
  {
    if (this.whichButton == "Playerlist")
      this.Playerlist.text = keyC.ToString();
    if (this.whichButton == "Granate")
      this.Granate.text = keyC.ToString();
    if (this.whichButton == "Nachladen")
      this.Nachladen.text = keyC.ToString();
    if (this.whichButton == "AR")
      this.AR.text = keyC.ToString();
    if (this.whichButton == "Pump")
      this.Pump.text = keyC.ToString();
    if (this.whichButton == "MP")
      this.MP.text = keyC.ToString();
    this.whichButton = "";
  }

  public void loadSettings()
  {
    this.Settings1.SetActive(true);
    this.All.SetActive(false);
    this.Alles.SetActive(false);
    this.Load.SetActive(true);
    this.GetUserData(PlayFab_Log.id);
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
    this.All.SetActive(true);
    this.Load.SetActive(false);
    this.Playerlist.text = result.Data["Playerlist"].Value;
    this.Granate.text = result.Data["Granate"].Value;
    this.Nachladen.text = result.Data["Nachladen"].Value;
    this.AR.text = result.Data["AR"].Value;
    this.Pump.text = result.Data["Pump"].Value;
    this.MP.text = result.Data["MP"].Value;
    this.MouseY.value = float.Parse(result.Data["MouseY"].Value);
    this.MouseX.value = float.Parse(result.Data["MouseX"].Value);
    this.fps.isOn = result.Data["FPS"].Value == "true";
    if (result.Data["Ping"].Value == "true")
      this.ping.isOn = true;
    else
      this.ping.isOn = false;
  }), (Action<PlayFabError>) (error =>
  {
    Debug.Log((object) "Got error retrieving user data:");
    Debug.Log((object) error.GenerateErrorReport());
    this.loadSettings();
  }));

  public void useSettings()
  {
    if (this.buttonBind)
      return;
    this.Error.SetActive(false);
    this.Settings1.SetActive(true);
    this.All.SetActive(false);
    this.Alles.SetActive(false);
    this.Load.SetActive(true);
    this.SetUserData();
  }

  public void SetUserData()
  {
    this.pingT = !this.ping.isOn ? "false" : "true";
    this.fpsT = !this.fps.isOn ? "false" : "true";
    MonoBehaviour.print((object) this.fpsT);
    PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
    {
      Data = new Dictionary<string, string>()
      {
        {
          "Playerlist",
          this.Playerlist.text
        },
        {
          "Granate",
          this.Granate.text
        },
        {
          "Nachladen",
          this.Nachladen.text
        },
        {
          "AR",
          this.AR.text
        },
        {
          "Pump",
          this.Pump.text
        },
        {
          "MP",
          this.MP.text
        },
        {
          "MouseY",
          this.MouseYt.text
        },
        {
          "MouseX",
          this.MouseXt.text
        },
        {
          "Ping",
          this.pingT
        },
        {
          "FPS",
          this.fpsT
        }
      }
    }, (Action<UpdateUserDataResult>) (result => this.SuccessSetUserData()), (Action<PlayFabError>) (error =>
    {
      Debug.Log((object) "Got error setting user data");
      Debug.Log((object) error.GenerateErrorReport());
      this.Error.SetActive(true);
    }));
  }

  private void SuccessSetUserData()
  {
    this.Settings1.SetActive(false);
    this.Alles.SetActive(true);
  }

  public void zurück()
  {
    if (this.buttonBind)
      return;
    this.Error.SetActive(false);
    this.Settings1.SetActive(false);
    this.Alles.SetActive(true);
  }
}
