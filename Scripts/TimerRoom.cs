// Decompiled with JetBrains decompiler
// Type: TimerRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TimerRoom : MonoBehaviourPunCallbacks
{
  private bool startTimer;
  private double timerIncrementValue;
  private double startTime;
  private double actualTime;
  [SerializeField]
  private double timer = 20.0;
  private int seconds;
  public Text text;
  public PhotonView PV;
  public static Hashtable Overview = new Hashtable();

  private void Start()
  {
    TimerRoom.Overview.Clear();
    if (!PhotonNetwork.LocalPlayer.IsMasterClient)
      return;
    MonoBehaviour.print((object) (Time.time.ToString() + " time"));
    this.startTime = (double) Time.time;
    Hashtable propertiesToSet = new Hashtable();
    propertiesToSet.Add((object) "StartTime", (object) this.startTime);
    PhotonNetwork.CurrentRoom.SetCustomProperties(propertiesToSet);
  }

  public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
  {
    object message;
    if (!propertiesThatChanged.TryGetValue((object) "StartTime", out message))
      return;
    MonoBehaviour.print(message);
    this.startTime = (double) Time.time;
    this.startTimer = true;
  }

  private void Update()
  {
    if (!this.startTimer)
      return;
    this.timerIncrementValue = (double) Time.time - this.startTime;
    this.actualTime = this.timer - this.timerIncrementValue;
    this.seconds = (int) this.actualTime;
    TimeSpan timeSpan = new TimeSpan(0, 0, this.seconds);
    int hours = timeSpan.Hours;
    int minutes = timeSpan.Minutes;
    int seconds = timeSpan.Seconds;
    string str1 = minutes.ToString();
    string str2 = seconds.ToString();
    if (minutes < 10)
      str1 = "0" + minutes.ToString();
    if (seconds < 10)
      str2 = "0" + seconds.ToString();
    this.text.text = "Verbleibende Zeit: " + str1 + ":" + str2 + "s";
    if (this.seconds > 0)
      return;
    this.startTimer = false;
    MonoBehaviour.print((object) "Game Over");
    this.text.text = "Verbleibende Zeit: 0:00s";
    if (!this.PV.IsMine)
      return;
    this.PV.RPC("RPC_End", RpcTarget.All);
  }

  [PunRPC]
  private void RPC_End()
  {
    foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("PlayerManager"))
    {
      MonoBehaviour.print((object) "PM");
      if (gameObject.GetComponent<PhotonView>().IsMine)
      {
        PlayerManager component = gameObject.GetComponent<PlayerManager>();
        MonoBehaviour.print((object) "found");
        TimerRoom.Overview.Add((object) "Kills", (object) component.kills);
        TimerRoom.Overview.Add((object) "Tode", (object) component.deaths);
        MonoBehaviour.print((object) TimerRoom.Overview[(object) "Kills"].ToString());
        double num1 = component.deaths == 0 ? (double) component.kills : (double) component.kills / (double) component.deaths;
        float num2 = UnityEngine.Random.Range(0.0f, 1000000f);
        TimerRoom.Overview.Add((object) "KD", (object) num1);
        TimerRoom.Overview.Add((object) "Random", (object) num2);
        TimerRoom.Overview.Add((object) "Id", (object) PlayFab_Log.id);
        MonoBehaviour.print((object) TimerRoom.Overview[(object) "Kills"].ToString());
        MonoBehaviour.print((object) TimerRoom.Overview[(object) "Tode"].ToString());
        MonoBehaviour.print((object) TimerRoom.Overview[(object) "KD"].ToString());
      }
    }
    if (!PhotonNetwork.LocalPlayer.IsMasterClient)
      return;
    PhotonNetwork.LoadLevel("EndScene");
  }
}
