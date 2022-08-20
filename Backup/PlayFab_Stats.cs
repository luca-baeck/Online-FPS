// Decompiled with JetBrains decompiler
// Type: PlayFab_Stats
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayFab_Stats : MonoBehaviour
{
  public static int Meter;
  public static int Coins;
  public int highscore;
  public int TotalCoins;
  public Text high;
  public Text point;
  public GameObject list;
  public GameObject Menu;
  public GameObject HowTo;
  public GameObject FailConnection;
  public GameObject MultiMenu;
  public Text t1;
  public Text t2;
  public Text t3;
  public Text t4;
  public Text t5;

  private void Start()
  {
    this.Menu.SetActive(true);
    this.list.SetActive(false);
    this.HowTo.SetActive(false);
    this.FailConnection.SetActive(false);
    this.MultiMenu.SetActive(false);
    this.SaveStatsHighscore();
    this.SaveStatsCoins();
    this.GetStats();
    this.getLeaderBoard();
  }

  private void Update()
  {
    this.SaveStatsHighscore();
    this.SaveStatsCoins();
    PlayFab_Stats.Coins = 0;
    this.GetStats();
    this.high.text = "Highscore: " + (object) this.highscore + " Meter";
    this.point.text = "Coins: " + (object) this.TotalCoins;
  }

  public void SaveStatsHighscore()
  {
    UpdatePlayerStatisticsRequest request = new UpdatePlayerStatisticsRequest();
    request.Statistics = new List<StatisticUpdate>();
    StatisticUpdate statisticUpdate = new StatisticUpdate()
    {
      StatisticName = "Highscore",
      Value = PlayFab_Stats.Meter
    };
    MonoBehaviour.print((object) PlayFab_Stats.Meter);
    request.Statistics.Add(statisticUpdate);
    PlayFabClientAPI.UpdatePlayerStatistics(request, new Action<UpdatePlayerStatisticsResult>(this.Success), new Action<PlayFabError>(this.Fail));
  }

  public void SaveStatsCoins()
  {
    UpdatePlayerStatisticsRequest request = new UpdatePlayerStatisticsRequest();
    request.Statistics = new List<StatisticUpdate>();
    StatisticUpdate statisticUpdate = new StatisticUpdate()
    {
      StatisticName = "Coins",
      Value = PlayFab_Stats.Coins
    };
    MonoBehaviour.print((object) PlayFab_Stats.Coins);
    request.Statistics.Add(statisticUpdate);
    PlayFabClientAPI.UpdatePlayerStatistics(request, new Action<UpdatePlayerStatisticsResult>(this.Success), new Action<PlayFabError>(this.Fail));
  }

  private void Fail(PlayFabError obj) => MonoBehaviour.print((object) "fail stats");

  private void Success(UpdatePlayerStatisticsResult obj) => MonoBehaviour.print((object) "success stats");

  public void GetStats() => PlayFabClientAPI.GetPlayerStatistics(new GetPlayerStatisticsRequest(), new Action<GetPlayerStatisticsResult>(this.SuccessSL), new Action<PlayFabError>(this.Fail));

  private void SuccessSL(GetPlayerStatisticsResult obj)
  {
    MonoBehaviour.print((object) "reload stats erfolgreich");
    foreach (StatisticValue statistic in obj.Statistics)
    {
      MonoBehaviour.print((object) (statistic.StatisticName + (object) statistic.Value));
      if (statistic.StatisticName == "Highscore")
        this.highscore = statistic.Value;
      if (statistic.StatisticName == "Coins")
        this.TotalCoins = statistic.Value;
    }
  }

  public void Bestenliste()
  {
    this.Menu.SetActive(false);
    this.HowTo.SetActive(false);
    this.list.SetActive(true);
  }

  public void Menu1()
  {
    this.HowTo.SetActive(false);
    this.Menu.SetActive(true);
    this.list.SetActive(false);
  }

  public void HowToPlay()
  {
    this.HowTo.SetActive(true);
    this.Menu.SetActive(false);
    this.list.SetActive(false);
  }

  public void getLeaderBoard() => PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest()
  {
    StartPosition = 0,
    StatisticName = "Highscore"
  }, new Action<GetLeaderboardResult>(this.SuccessB), new Action<PlayFabError>(this.FailF));

  private void FailF(PlayFabError obj) => MonoBehaviour.print((object) "fail leaderboard");

  private void SuccessB(GetLeaderboardResult obj)
  {
    foreach (PlayerLeaderboardEntry message in obj.Leaderboard)
    {
      MonoBehaviour.print((object) message);
      MonoBehaviour.print((object) (message.Position.ToString() + " ." + (object) message.StatValue + " " + message.DisplayName));
      if (message.Position == 0)
        this.t1.text = "1. " + message.DisplayName + " mit " + (object) message.StatValue + " Metern";
      if (message.Position == 1)
        this.t2.text = "2. " + message.DisplayName + " mit " + (object) message.StatValue + " Metern";
      if (message.Position == 2)
        this.t3.text = "3. " + message.DisplayName + " mit " + (object) message.StatValue + " Metern";
      if (message.Position == 3)
        this.t4.text = "4. " + message.DisplayName + " mit " + (object) message.StatValue + " Metern";
      if (message.Position == 4)
        this.t5.text = "5. " + message.DisplayName + " mit " + (object) message.StatValue + " Metern";
    }
  }
}
