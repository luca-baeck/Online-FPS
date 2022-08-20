// Decompiled with JetBrains decompiler
// Type: MouseLookHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[Serializable]
public class MouseLookHelper
{
  public float XSensitivity = 2f;
  public float YSensitivity = 2f;
  public bool clampVerticalRotation = true;
  public float MinimumX = -90f;
  public float MaximumX = 90f;
  public bool smooth;
  public float smoothTime = 5f;
  private Quaternion m_CharacterTargetRot;
  private Quaternion m_CameraTargetRot;

  public void Init(Transform character, Transform camera)
  {
    this.m_CharacterTargetRot = character.localRotation;
    this.m_CameraTargetRot = camera.localRotation;
  }

  public void LookRotation(Transform character, Transform camera)
  {
    float y = CrossPlatformInputManager.GetAxis("Mouse X") * this.XSensitivity;
    float num = CrossPlatformInputManager.GetAxis("Mouse Y") * this.YSensitivity;
    this.m_CharacterTargetRot *= Quaternion.Euler(0.0f, y, 0.0f);
    this.m_CameraTargetRot *= Quaternion.Euler(-num, 0.0f, 0.0f);
    if (this.clampVerticalRotation)
      this.m_CameraTargetRot = this.ClampRotationAroundXAxis(this.m_CameraTargetRot);
    if (this.smooth)
    {
      character.localRotation = Quaternion.Slerp(character.localRotation, this.m_CharacterTargetRot, this.smoothTime * Time.deltaTime);
      camera.localRotation = Quaternion.Slerp(camera.localRotation, this.m_CameraTargetRot, this.smoothTime * Time.deltaTime);
    }
    else
    {
      character.localRotation = this.m_CharacterTargetRot;
      camera.localRotation = this.m_CameraTargetRot;
    }
  }

  private Quaternion ClampRotationAroundXAxis(Quaternion q)
  {
    q.x /= q.w;
    q.y /= q.w;
    q.z /= q.w;
    q.w = 1f;
    float num = Mathf.Clamp(114.59156f * Mathf.Atan(q.x), this.MinimumX, this.MaximumX);
    q.x = Mathf.Tan((float) Math.PI / 360f * num);
    return q;
  }
}
