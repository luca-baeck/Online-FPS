// Decompiled with JetBrains decompiler
// Type: OverviewEnable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverviewEnable : MonoBehaviourPunCallbacks
{
  public static int kills;
  public bool active;
  public GameObject overview;

  private void Update()
  {
    Cursor.visible = this.active;
    Screen.lockCursor = !this.active;
    this.active = Input.GetKey(GetBinds.Playerlist) || Input.GetKey(KeyCode.Escape);
    this.overview.SetActive(this.active);
  }

  public void Verlassen() => PhotonNetwork.LeaveRoom();

  public override void OnLeftRoom()
  {
    OverviewEnable.kills = 0;
    SceneManager.LoadScene("Lobby");
  }
}
