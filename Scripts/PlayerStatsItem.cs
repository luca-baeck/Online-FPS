// Decompiled with JetBrains decompiler
// Type: PlayerStatsItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsItem : MonoBehaviourPunCallbacks
{
  public Text Platzierung;
  public Text Name;
  public Text Deaths;
  public Text Kills;
  public Text KD;
  public GameObject me;
  public Text Spruch;

  public void SetUp(Player myplayer, int rank, int maxPlayers)
  {
    if (myplayer == null)
      MonoBehaviour.print((object) "null");
    if (myplayer == PhotonNetwork.LocalPlayer)
    {
      this.me.SetActive(true);
      SaveStats.rankPosition = rank;
      this.Spruch = GameObject.Find("PlatzierungsSpruch").GetComponent<Text>();
      if (maxPlayers == 6)
      {
        switch (rank)
        {
          case 1:
            this.Spruch.text = "#1 Du hast gewonnen!";
            break;
          case 2:
            this.Spruch.text = "#2 Knapp daneben ist auch vorbei!";
            break;
          case 3:
            this.Spruch.text = "#3 Hauptsache auf dem Podest";
            break;
          case 4:
            this.Spruch.text = "#4 Schade, das kannst du besser!";
            break;
          case 5:
            this.Spruch.text = "#5 Die letzten werden die ersten sein!";
            break;
          case 6:
            this.Spruch.text = "#" + (object) rank + " Looser, streng dich mehr an!";
            break;
          default:
            MonoBehaviour.print((object) "Error");
            break;
        }
      }
      if (maxPlayers == 5)
      {
        switch (rank)
        {
          case 1:
            this.Spruch.text = "#1 Du hast gewonnen!";
            break;
          case 2:
            this.Spruch.text = "#2 Knapp daneben ist auch vorbei!";
            break;
          case 3:
            this.Spruch.text = "#3 Hauptsache auf dem Podest";
            break;
          case 4:
            this.Spruch.text = "#4 Schade, das kannst du besser!";
            break;
          case 5:
            this.Spruch.text = "#" + (object) rank + " Looser, streng dich mehr an!";
            break;
          default:
            MonoBehaviour.print((object) "Error");
            break;
        }
      }
      if (maxPlayers == 4)
      {
        switch (rank)
        {
          case 1:
            this.Spruch.text = "#1 Du hast gewonnen!";
            break;
          case 2:
            this.Spruch.text = "#2 Knapp daneben ist auch vorbei!";
            break;
          case 3:
            this.Spruch.text = "#3 Hauptsache auf dem Podest";
            break;
          case 4:
            this.Spruch.text = "#" + (object) rank + " Looser, streng dich mehr an!";
            break;
          default:
            MonoBehaviour.print((object) "Error");
            break;
        }
      }
      if (maxPlayers == 3)
      {
        switch (rank)
        {
          case 1:
            this.Spruch.text = "#1 Du hast gewonnen!";
            break;
          case 2:
            this.Spruch.text = "#2 Knapp daneben ist auch vorbei!";
            break;
          case 3:
            this.Spruch.text = "#" + (object) rank + " Looser, streng dich mehr an!";
            break;
          default:
            MonoBehaviour.print((object) "Error");
            break;
        }
      }
      if (maxPlayers == 2)
      {
        switch (rank)
        {
          case 1:
            this.Spruch.text = "#1 Du hast gewonnen!";
            break;
          case 2:
            this.Spruch.text = "#" + (object) rank + " Looser, streng dich mehr an!";
            break;
          default:
            MonoBehaviour.print((object) "Error");
            break;
        }
      }
      if (maxPlayers == 1)
      {
        if (rank == 1)
          this.Spruch.text = "#1 Du hast gewonnen!";
        else
          MonoBehaviour.print((object) "Error");
      }
    }
    else
      this.me.SetActive(false);
    this.Platzierung.text = rank.ToString() + ".";
    this.Name.text = myplayer.NickName.ToString();
    object obj;
    if (myplayer.CustomProperties.TryGetValue((object) "Kills", out obj))
      this.Kills.text = ((int) obj).ToString();
    if (myplayer.CustomProperties.ContainsKey((object) "itemIndex"))
      MonoBehaviour.print((object) "jes");
    else
      MonoBehaviour.print((object) "no");
    this.Deaths.text = myplayer.CustomProperties[(object) "Tode"].ToString();
    this.KD.text = (Mathf.Round((float) double.Parse(myplayer.CustomProperties[(object) "KD"].ToString()) * 10f) / 10f).ToString();
  }
}
