// Decompiled with JetBrains decompiler
// Type: Mapvote
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine.UI;

public class Mapvote : MonoBehaviourPunCallbacks
{
  public Text info;

  private void Start() => this.info.text = "";

  public void Map1()
  {
    Hashtable propertiesToSet = new Hashtable();
    propertiesToSet.Add((object) "Map", (object) "1");
    PhotonNetwork.LocalPlayer.SetCustomProperties(propertiesToSet);
    this.info.text = "Sie haben für Map1 gestimmt...";
  }

  public void Map2()
  {
    Hashtable propertiesToSet = new Hashtable();
    propertiesToSet.Add((object) "Map", (object) "2");
    PhotonNetwork.LocalPlayer.SetCustomProperties(propertiesToSet);
    this.info.text = "Sie haben für Map2 gestimmt...";
  }
}
