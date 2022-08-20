// Decompiled with JetBrains decompiler
// Type: GetBinds
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetBinds : MonoBehaviour
{
  public static KeyCode links;
  public static KeyCode rechts;
  public static KeyCode Vorwärts;
  public static KeyCode Rückwärts;
  public static KeyCode sprinten;
  public static KeyCode ducken;
  public static KeyCode springen;
  public static KeyCode Playerlist;
  public static KeyCode Schießen;
  public static KeyCode Zielen;
  public static KeyCode Granate;
  public static KeyCode Nachladen;
  public static KeyCode AR;
  public static KeyCode Pump;
  public static KeyCode MP;
  public static float MouseX;
  public static float MouseY;
  public bool Ping;
  public bool FPS;
  public GameObject Laden;
  public Text FPS_T;
  public Text Ping_t;
  private float deltaTime;

  private void Start()
  {
    this.Ping = false;
    this.FPS = false;
    this.Laden.SetActive(true);
    this.loadSettings();
  }

  private void Update()
  {
    this.Ping_t.text = !this.Ping ? "" : PhotonNetwork.GetPing().ToString() + "ms";
    if (this.FPS)
    {
      this.deltaTime += (float) (((double) Time.deltaTime - (double) this.deltaTime) * 0.10000000149011612);
      this.FPS_T.text = Mathf.Ceil(1f / this.deltaTime).ToString();
    }
    else
      this.FPS_T.text = "";
  }

  private void loadSettings() => this.GetUserData(PlayFab_Log.id);

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
    GetBinds.links = (KeyCode) System.Enum.Parse(typeof (KeyCode), "A");
    GetBinds.rechts = (KeyCode) System.Enum.Parse(typeof (KeyCode), "D");
    GetBinds.springen = (KeyCode) System.Enum.Parse(typeof (KeyCode), "Space");
    GetBinds.sprinten = (KeyCode) System.Enum.Parse(typeof (KeyCode), "LeftShift");
    GetBinds.ducken = (KeyCode) System.Enum.Parse(typeof (KeyCode), "LeftControl");
    GetBinds.Vorwärts = (KeyCode) System.Enum.Parse(typeof (KeyCode), "W");
    GetBinds.Rückwärts = (KeyCode) System.Enum.Parse(typeof (KeyCode), "S");
    GetBinds.Playerlist = (KeyCode) System.Enum.Parse(typeof (KeyCode), result.Data["Playerlist"].Value);
    MonoBehaviour.print((object) "p");
    GetBinds.Schießen = (KeyCode) System.Enum.Parse(typeof (KeyCode), "Mouse0");
    GetBinds.Zielen = (KeyCode) System.Enum.Parse(typeof (KeyCode), "Mouse1");
    GetBinds.Granate = (KeyCode) System.Enum.Parse(typeof (KeyCode), result.Data["Granate"].Value);
    MonoBehaviour.print((object) "g");
    GetBinds.Nachladen = (KeyCode) System.Enum.Parse(typeof (KeyCode), result.Data["Nachladen"].Value);
    MonoBehaviour.print((object) "n");
    GetBinds.AR = (KeyCode) System.Enum.Parse(typeof (KeyCode), result.Data["AR"].Value);
    GetBinds.Pump = (KeyCode) System.Enum.Parse(typeof (KeyCode), result.Data["Pump"].Value);
    GetBinds.MP = (KeyCode) System.Enum.Parse(typeof (KeyCode), result.Data["MP"].Value);
    MonoBehaviour.print((object) ("AR" + GetBinds.AR.ToString()));
    GetBinds.MouseY = float.Parse(result.Data["MouseY"].Value);
    GetBinds.MouseX = float.Parse(result.Data["MouseX"].Value);
    if (result.Data["Ping"].Value.ToString() == "true")
      this.Ping = true;
    else if (result.Data["Ping"].Value.ToString() == "false")
      this.Ping = false;
    if (result.Data["FPS"].Value.ToString() == "true")
      this.FPS = true;
    else if (result.Data["FPS"].Value.ToString() == "false")
      this.FPS = false;
    this.Laden.SetActive(false);
  }), (Action<PlayFabError>) (error =>
  {
    Debug.Log((object) "Got error retrieving user data:");
    Debug.Log((object) error.GenerateErrorReport());
    this.loadSettings();
  }));
}
