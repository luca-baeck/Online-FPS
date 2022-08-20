// Decompiled with JetBrains decompiler
// Type: Bestenliste
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Bestenliste : MonoBehaviour
{
  public Dropdown dp;
  private string WhichBoard = "";
  public GameObject Leaderboard_Player;
  public Text Info;
  public Transform content;
  public GameObject All;
  public GameObject Lobby;

  private void Start()
  {
    this.All.SetActive(false);
    this.Info.text = "";
  }

  private void OnEnable() => this.Load();

  public void Load()
  {
    this.Info.text = "Laden...";
    foreach (UnityEngine.Object @object in GameObject.FindGameObjectsWithTag("Leaderboard_Player"))
      UnityEngine.Object.Destroy(@object);
    if (this.dp.value == 0)
      this.WhichBoard = "Siege_Day";
    if (this.dp.value == 1)
      this.WhichBoard = "Siege_Month";
    if (this.dp.value == 2)
      this.WhichBoard = "Siege";
    this.LoadLeaderBoard();
  }

  public void LoadLeaderBoard() => PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest()
  {
    StartPosition = 0,
    StatisticName = this.WhichBoard
  }, new Action<GetLeaderboardResult>(this.OnLeaderboardSuccess), new Action<PlayFabError>(this.fail));

  private void OnLeaderboardSuccess(GetLeaderboardResult obj)
  {
    this.Info.text = "";
    int rank = 1;
    foreach (PlayerLeaderboardEntry leaderboardEntry in obj.Leaderboard)
    {
      UnityEngine.Object.Instantiate<GameObject>(this.Leaderboard_Player, this.content).GetComponent<LeaderboardPlayerScript>().SetUp(rank, leaderboardEntry.StatValue, leaderboardEntry.DisplayName, leaderboardEntry.PlayFabId);
      ++rank;
    }
  }

  private void fail(PlayFabError obj)
  {
    MonoBehaviour.print((object) nameof (fail));
    this.Info.text = "Error";
  }

  public void Show()
  {
    this.Lobby.SetActive(false);
    this.All.SetActive(true);
  }

  public void DeShow()
  {
    foreach (UnityEngine.Object @object in GameObject.FindGameObjectsWithTag("Leaderboard_Player"))
      UnityEngine.Object.Destroy(@object);
    this.Lobby.SetActive(true);
    this.All.SetActive(false);
  }
}
