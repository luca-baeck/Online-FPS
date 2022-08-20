// Decompiled with JetBrains decompiler
// Type: EndSceneRank
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using Photon.Pun;
using Photon.Realtime;
using System.IO;
using UnityEngine;

public class EndSceneRank : MonoBehaviourPunCallbacks
{
  public GameObject playerStats;
  public GameObject Laden;
  public GameObject ScoreBoard;
  public GameObject Button;
  public GameObject ButtonR;
  public Transform content;
  private Player[] Players;
  private int p;

  private void Start()
  {
    PhotonNetwork.Instantiate(Path.Combine("Photon", "VoiceChat"), Vector3.zero, Quaternion.identity);
    Screen.lockCursor = false;
    Cursor.visible = true;
    PhotonNetwork.LocalPlayer.SetCustomProperties(TimerRoom.Overview);
    this.Laden.SetActive(true);
    this.ScoreBoard.SetActive(false);
    this.Button.SetActive(false);
    this.ButtonR.SetActive(false);
    this.Invoke("StartRank", 3f);
  }

  private void StartRank()
  {
    this.Button.SetActive(true);
    this.ButtonR.SetActive(true);
    this.Laden.SetActive(false);
    this.ScoreBoard.SetActive(true);
    this.Players = PhotonNetwork.PlayerList;
    for (int index = 0; index < this.Players.Length; ++index)
    {
      PlayerStatsItem component = Object.Instantiate<GameObject>(this.playerStats, this.content).GetComponent<PlayerStatsItem>();
      this.GetHighestKills();
      MonoBehaviour.print((object) (this.p.ToString() + "Index"));
      MonoBehaviour.print((object) this.Players[this.p].NickName.ToString());
      Player player = this.Players[this.p];
      int rank = index + 1;
      int maxPlayers = this.Players.Length + 1;
      component.SetUp(player, rank, maxPlayers);
      this.Players[this.p] = (Player) null;
    }
  }

  private void GetHighestKills()
  {
    Player player = this.Players[0];
    int index1 = 0;
    int num = index1;
    while (player == null)
    {
      player = this.Players[index1];
      num = index1;
      ++index1;
    }
    for (int index2 = 1; index2 < this.Players.Length; ++index2)
    {
      if (this.Players[index2] != null)
      {
        MonoBehaviour.print((object) this.Players[index2].CustomProperties[(object) "Kills"].ToString());
        if ((int) this.Players[index2].CustomProperties[(object) "Kills"] > (int) player.CustomProperties[(object) "Kills"])
        {
          player = this.Players[index2];
          num = index2;
        }
        else if ((int) this.Players[index2].CustomProperties[(object) "Kills"] == (int) player.CustomProperties[(object) "Kills"])
        {
          if ((int) this.Players[index2].CustomProperties[(object) "Tode"] < (int) player.CustomProperties[(object) "Tode"])
          {
            player = this.Players[index2];
            num = index2;
          }
          else if ((int) this.Players[index2].CustomProperties[(object) "Tode"] == (int) player.CustomProperties[(object) "Tode"])
          {
            if ((double) this.Players[index2].CustomProperties[(object) "KD"] > (double) player.CustomProperties[(object) "KD"])
            {
              player = this.Players[index2];
              num = index2;
            }
            else if ((double) this.Players[index2].CustomProperties[(object) "KD"] == (double) player.CustomProperties[(object) "KD"] && (double) (float) this.Players[index2].CustomProperties[(object) "Random"] > (double) (float) player.CustomProperties[(object) "Random"])
            {
              player = this.Players[index2];
              num = index2;
            }
          }
        }
      }
    }
    this.p = num;
  }
}
