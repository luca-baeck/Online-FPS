// Decompiled with JetBrains decompiler
// Type: ChangeName
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof (PhotonView))]
public class ChangeName : MonoBehaviour
{
  private void Start() => this.name = string.Format("ActorNumber {0}", (object) this.GetComponent<PhotonView>().OwnerActorNr);
}
