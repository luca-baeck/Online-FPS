// Decompiled with JetBrains decompiler
// Type: ChangeColor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof (Renderer))]
[RequireComponent(typeof (PhotonView))]
public class ChangeColor : MonoBehaviour
{
  private PhotonView photonView;

  private void Start()
  {
    this.photonView = this.GetComponent<PhotonView>();
    if (!this.photonView.IsMine)
      return;
    Color color = Random.ColorHSV();
    this.photonView.RPC("ChangeColour", RpcTarget.AllBuffered, (object) new Vector3(color.r, color.g, color.b));
  }

  [PunRPC]
  private void ChangeColour(Vector3 randomColor) => this.GetComponent<Renderer>().material.SetColor("_Color", new Color(randomColor.x, randomColor.y, randomColor.z));
}
