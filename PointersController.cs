// Decompiled with JetBrains decompiler
// Type: PointersController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using Photon.Voice.PUN;
using UnityEngine;

[RequireComponent(typeof (PhotonVoiceView))]
public class PointersController : MonoBehaviour
{
  [SerializeField]
  private GameObject pointerDown;
  [SerializeField]
  private GameObject pointerUp;
  private PhotonVoiceView photonVoiceView;

  private void Awake()
  {
    this.photonVoiceView = this.GetComponent<PhotonVoiceView>();
    this.SetActiveSafe(this.pointerUp, false);
    this.SetActiveSafe(this.pointerDown, false);
  }

  private void Update()
  {
    this.SetActiveSafe(this.pointerDown, this.photonVoiceView.IsSpeaking);
    this.SetActiveSafe(this.pointerUp, this.photonVoiceView.IsRecording);
  }

  private void SetActiveSafe(GameObject go, bool active)
  {
    if (!((Object) go != (Object) null) || go.activeSelf == active)
      return;
    go.SetActive(active);
  }
}
