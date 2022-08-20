// Decompiled with JetBrains decompiler
// Type: SaveStats
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveStats : MonoBehaviourPunCallbacks
{
  public static int rankPosition;
  public int Kills;
  public int Deaths;
  public int win;
  public GameObject Laden;
  public GameObject Scoreboard;
  public GameObject Error;
  public GameObject Report;
  public InputField name;
  public InputField grund;
  private bool winB;
  private bool winBD;
  private bool winBM;
  private bool killsB;
  private bool deathsB;
  private bool Currency;

  public override void OnDisconnected(DisconnectCause cause)
  {
    PlayFabClientAPI.ForgetAllCredentials();
    SceneManager.LoadScene("LogIn");
  }

  public void ReportA()
  {
    this.Report.SetActive(true);
    this.Scoreboard.SetActive(false);
  }

  public void ScoreBoardA()
  {
    this.Report.SetActive(false);
    this.Scoreboard.SetActive(true);
  }

  public void ReportAbschicken()
  {
    string problematicPlayerId = "";
    foreach (Player player in PhotonNetwork.PlayerList)
    {
      if (player.NickName == this.name.text)
      {
        problematicPlayerId = player.CustomProperties[(object) "Id"].ToString();
        this.ReportPlayer(problematicPlayerId, this.grund.text);
        break;
      }
    }
    if (!(problematicPlayerId == ""))
      return;
    this.ScoreBoardA();
  }

  public void ReportPlayer(string problematicPlayerId, string reason) => PlayFabClientAPI.ReportPlayer(new ReportPlayerClientRequest()
  {
    ReporteeId = problematicPlayerId,
    Comment = reason
  }, (Action<ReportPlayerClientResult>) (result => this.ScoreBoardA()), (Action<PlayFabError>) (error =>
  {
    Debug.Log((object) error.GenerateErrorReport());
    this.ScoreBoardA();
  }));

  private void Start()
  {
    this.Scoreboard.SetActive(true);
    this.Laden.SetActive(false);
    this.Error.SetActive(false);
    this.Report.SetActive(false);
    this.win = 0;
  }

  public void SaveAndLeave()
  {
    this.Scoreboard.SetActive(false);
    this.Laden.SetActive(true);
    this.Error.SetActive(false);
    this.Kills = (int) PhotonNetwork.LocalPlayer.CustomProperties[(object) "Kills"];
    this.Deaths = (int) PhotonNetwork.LocalPlayer.CustomProperties[(object) "Tode"];
    if (SaveStats.rankPosition == 1)
      this.win = 1;
    if (!this.winB)
      this.SaveStatsWin();
    else if (!this.winBD)
      this.SaveStatsWinD();
    else if (!this.winBM)
      this.SaveStatsWinM();
    else if (!this.killsB)
      this.SaveStatsKills();
    else if (!this.deathsB)
      this.SaveStatsTode();
    else if (!this.Currency)
      this.AddCurrency();
    else
      this.LeaveRoom();
  }

  public void AddCurrency()
  {
    int num = 50;
    if (SaveStats.rankPosition == 1)
      num += 50;
    if (SaveStats.rankPosition == 2)
      num += 25;
    if (SaveStats.rankPosition == 3)
      num += 10;
    PlayFabClientAPI.AddUserVirtualCurrency(new AddUserVirtualCurrencyRequest()
    {
      Amount = num,
      VirtualCurrency = "CC"
    }, new Action<ModifyUserVirtualCurrencyResult>(this.SuccessA), new Action<PlayFabError>(this.FailA));
  }

  public void SuccessA(ModifyUserVirtualCurrencyResult obj)
  {
    this.Currency = true;
    this.LeaveRoom();
  }

  public void FailA(PlayFabError obj)
  {
    MonoBehaviour.print((object) obj);
    MonoBehaviour.print((object) "fail currency");
    this.Scoreboard.SetActive(true);
    this.Laden.SetActive(false);
    this.Error.SetActive(true);
  }

  public void SaveStatsWin()
  {
    UpdatePlayerStatisticsRequest request = new UpdatePlayerStatisticsRequest()
    {
      Statistics = new List<StatisticUpdate>()
    };
    request.Statistics.Add(new StatisticUpdate()
    {
      StatisticName = "Siege",
      Value = this.win
    });
    PlayFabClientAPI.UpdatePlayerStatistics(request, new Action<UpdatePlayerStatisticsResult>(this.SuccessW), new Action<PlayFabError>(this.FailW));
  }

  private void FailW(PlayFabError obj)
  {
    MonoBehaviour.print((object) "fail stats");
    this.Scoreboard.SetActive(true);
    this.Laden.SetActive(false);
    this.Error.SetActive(true);
  }

  private void SuccessW(UpdatePlayerStatisticsResult obj)
  {
    this.winB = true;
    this.SaveStatsWinD();
    MonoBehaviour.print((object) "success stats");
  }

  public void SaveStatsWinD()
  {
    UpdatePlayerStatisticsRequest request = new UpdatePlayerStatisticsRequest()
    {
      Statistics = new List<StatisticUpdate>()
    };
    request.Statistics.Add(new StatisticUpdate()
    {
      StatisticName = "Siege_Day",
      Value = this.win
    });
    PlayFabClientAPI.UpdatePlayerStatistics(request, new Action<UpdatePlayerStatisticsResult>(this.SuccessWd), new Action<PlayFabError>(this.FailWd));
  }

  private void FailWd(PlayFabError obj)
  {
    MonoBehaviour.print((object) "fail stats");
    this.Scoreboard.SetActive(true);
    this.Laden.SetActive(false);
    this.Error.SetActive(true);
  }

  private void SuccessWd(UpdatePlayerStatisticsResult obj)
  {
    this.winBD = true;
    this.SaveStatsWinM();
    MonoBehaviour.print((object) "success stats");
  }

  public void SaveStatsWinM()
  {
    UpdatePlayerStatisticsRequest request = new UpdatePlayerStatisticsRequest()
    {
      Statistics = new List<StatisticUpdate>()
    };
    request.Statistics.Add(new StatisticUpdate()
    {
      StatisticName = "Siege_Month",
      Value = this.win
    });
    PlayFabClientAPI.UpdatePlayerStatistics(request, new Action<UpdatePlayerStatisticsResult>(this.SuccessWm), new Action<PlayFabError>(this.FailWm));
  }

  private void FailWm(PlayFabError obj)
  {
    MonoBehaviour.print((object) "fail stats");
    this.Scoreboard.SetActive(true);
    this.Laden.SetActive(false);
    this.Error.SetActive(true);
  }

  private void SuccessWm(UpdatePlayerStatisticsResult obj)
  {
    this.winBM = true;
    this.SaveStatsKills();
    MonoBehaviour.print((object) "success stats");
  }

  public void SaveStatsKills()
  {
    UpdatePlayerStatisticsRequest request = new UpdatePlayerStatisticsRequest()
    {
      Statistics = new List<StatisticUpdate>()
    };
    request.Statistics.Add(new StatisticUpdate()
    {
      StatisticName = "Kills",
      Value = this.Kills
    });
    PlayFabClientAPI.UpdatePlayerStatistics(request, new Action<UpdatePlayerStatisticsResult>(this.SuccessK), new Action<PlayFabError>(this.FailK));
  }

  private void FailK(PlayFabError obj)
  {
    MonoBehaviour.print((object) "fail stats");
    this.Scoreboard.SetActive(true);
    this.Laden.SetActive(false);
    this.Error.SetActive(true);
  }

  private void SuccessK(UpdatePlayerStatisticsResult obj)
  {
    this.killsB = true;
    this.SaveStatsTode();
    MonoBehaviour.print((object) "success stats");
  }

  public void SaveStatsTode()
  {
    UpdatePlayerStatisticsRequest request = new UpdatePlayerStatisticsRequest()
    {
      Statistics = new List<StatisticUpdate>()
    };
    request.Statistics.Add(new StatisticUpdate()
    {
      StatisticName = "Tode",
      Value = this.Deaths
    });
    PlayFabClientAPI.UpdatePlayerStatistics(request, new Action<UpdatePlayerStatisticsResult>(this.SuccessT), new Action<PlayFabError>(this.FailT));
  }

  private void FailT(PlayFabError obj)
  {
    MonoBehaviour.print((object) "fail stats");
    this.Scoreboard.SetActive(true);
    this.Laden.SetActive(false);
    this.Error.SetActive(true);
  }

  private void SuccessT(UpdatePlayerStatisticsResult obj)
  {
    MonoBehaviour.print((object) "success stats");
    this.deathsB = true;
    this.AddCurrency();
  }

  private void LeaveRoom() => PhotonNetwork.LeaveRoom();

  public override void OnLeftRoom()
  {
    OverviewEnable.kills = 0;
    SceneManager.LoadScene("Lobby");
  }
}
