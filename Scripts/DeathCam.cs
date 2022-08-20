// Decompiled with JetBrains decompiler
// Type: DeathCam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DeathCam : MonoBehaviour
{
  public GameObject cameraHolder;
  private float verticalLookRotation;

  private void Start()
  {
  }

  private void Update() => this.Look();

  private void Look()
  {
    this.transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * 3f);
    this.verticalLookRotation += Input.GetAxisRaw("Mouse Y") * 3f;
    this.verticalLookRotation = Mathf.Clamp(this.verticalLookRotation, -90f, 90f);
    this.cameraHolder.transform.localEulerAngles = Vector3.left * this.verticalLookRotation;
  }
}
