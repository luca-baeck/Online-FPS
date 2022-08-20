// Decompiled with JetBrains decompiler
// Type: ExplosionDestroy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using Photon.Pun;
using UnityEngine;

public class ExplosionDestroy : MonoBehaviour
{
  private PhotonView PV;
  public float timer = 3.5f;

  private void Start() => this.PV = this.GetComponent<PhotonView>();

  private void Update()
  {
    this.timer -= Time.deltaTime;
    if ((double) this.timer > 0.0 || !this.PV.IsMine)
      return;
    PhotonNetwork.Destroy(this.gameObject);
  }
}
