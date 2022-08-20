// Decompiled with JetBrains decompiler
// Type: SoundsForJoinAndLeave
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class SoundsForJoinAndLeave : MonoBehaviourPunCallbacks
{
  public AudioClip JoinClip;
  public AudioClip LeaveClip;
  private AudioSource source;

  public override void OnPlayerEnteredRoom(Player newPlayer)
  {
    if (!((Object) this.JoinClip != (Object) null))
      return;
    if ((Object) this.source == (Object) null)
      this.source = Object.FindObjectOfType<AudioSource>();
    this.source.PlayOneShot(this.JoinClip);
  }

  public override void OnPlayerLeftRoom(Player otherPlayer)
  {
    if (!((Object) this.LeaveClip != (Object) null))
      return;
    if ((Object) this.source == (Object) null)
      this.source = Object.FindObjectOfType<AudioSource>();
    this.source.PlayOneShot(this.LeaveClip);
  }
}
