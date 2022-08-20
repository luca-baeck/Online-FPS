// Decompiled with JetBrains decompiler
// Type: UI_Game
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Game : MonoBehaviourPunCallbacks
{
  public GameObject MunitionPump;
  public GameObject MunitionMP;
  public GameObject MunitionAR;
  public GameObject WaffePump;
  public GameObject WaffeMP;
  public GameObject WaffeAR;
  public GameObject FadenkreuzPump;
  public GameObject FadenkreuzMP;
  public GameObject FadenKreuzAR;
  public GameObject gCanvas;
  public Text[] Ar;
  public Text[] Mp;
  public Text[] Pump;
  public Text Granate;
  public Text Munition;
  private Canvas canvas;

  private void Start()
  {
    GameObject.FindGameObjectWithTag("MusicManager").GetComponent<MusicManager>().PlayGame();
    this.canvas = this.gCanvas.GetComponent<Canvas>();
  }

  public override void OnDisconnected(DisconnectCause cause)
  {
    PlayFabClientAPI.ForgetAllCredentials();
    SceneManager.LoadScene("LogIn");
  }

  private void Update()
  {
    this.Granate.text = GetBinds.Granate.ToString();
    foreach (Text text in this.Ar)
      text.text = GetBinds.AR.ToString();
    foreach (Text text in this.Pump)
      text.text = GetBinds.Pump.ToString();
    foreach (Text text in this.Mp)
      text.text = GetBinds.MP.ToString();
    this.canvas.worldCamera = Camera.main;
    if (SingleShot.WeaponName == "Shotgun")
    {
      this.MunitionAR.SetActive(false);
      this.MunitionPump.SetActive(true);
      this.MunitionMP.SetActive(false);
      this.WaffeAR.SetActive(false);
      this.WaffePump.SetActive(true);
      this.WaffeMP.SetActive(false);
      this.FadenKreuzAR.SetActive(false);
      this.FadenkreuzMP.SetActive(false);
      this.FadenkreuzPump.SetActive(true);
    }
    if (SingleShot.WeaponName == "MP5")
    {
      this.MunitionAR.SetActive(false);
      this.MunitionPump.SetActive(false);
      this.MunitionMP.SetActive(true);
      this.WaffeAR.SetActive(false);
      this.WaffePump.SetActive(false);
      this.WaffeMP.SetActive(true);
      this.FadenKreuzAR.SetActive(false);
      this.FadenkreuzMP.SetActive(true);
      this.FadenkreuzPump.SetActive(false);
    }
    if (SingleShot.WeaponName == "Sig552")
    {
      this.MunitionAR.SetActive(true);
      this.MunitionPump.SetActive(false);
      this.MunitionMP.SetActive(false);
      this.WaffeAR.SetActive(true);
      this.WaffePump.SetActive(false);
      this.WaffeMP.SetActive(false);
      this.FadenKreuzAR.SetActive(true);
      this.FadenkreuzMP.SetActive(false);
      this.FadenkreuzPump.SetActive(false);
    }
    this.Munition.text = SingleShot.leftBullets.ToString() + "/" + (object) SingleShot.maxBullets;
  }
}
