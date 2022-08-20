// Decompiled with JetBrains decompiler
// Type: PlayerGroundCheck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
  private PlayerController player;

  private void Awake() => this.player = this.GetComponentInParent<PlayerController>();

  private void OnTriggerEnter(Collider col)
  {
    if ((Object) col.gameObject == (Object) this.player.gameObject)
      return;
    this.player.SetGroundedState(true);
  }

  private void OnTriggerExit(Collider col)
  {
    if ((Object) col.gameObject == (Object) this.player.gameObject)
      return;
    this.player.SetGroundedState(false);
  }

  private void OnTriggerStay(Collider col)
  {
    if ((Object) col.gameObject == (Object) this.player.gameObject)
      return;
    this.player.SetGroundedState(true);
  }

  private void OnCollisionEnter(Collision col)
  {
    if ((Object) col.gameObject == (Object) this.player.gameObject)
      return;
    this.player.SetGroundedState(true);
  }

  private void OnCollisionExit(Collision col)
  {
    if ((Object) col.gameObject == (Object) this.player.gameObject)
      return;
    this.player.SetGroundedState(false);
  }

  private void OnCollisionStay(Collision col)
  {
    if ((Object) col.gameObject == (Object) this.player.gameObject)
      return;
    this.player.SetGroundedState(true);
  }
}
