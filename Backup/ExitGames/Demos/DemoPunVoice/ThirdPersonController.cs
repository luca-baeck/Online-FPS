// Decompiled with JetBrains decompiler
// Type: ExitGames.Demos.DemoPunVoice.ThirdPersonController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace ExitGames.Demos.DemoPunVoice
{
  public class ThirdPersonController : BaseController
  {
    [SerializeField]
    private float movingTurnSpeed = 360f;

    protected override void Move(float h, float v)
    {
      this.rigidBody.velocity = v * this.speed * this.transform.forward;
      this.transform.rotation *= Quaternion.AngleAxis(this.movingTurnSpeed * h * Time.deltaTime, Vector3.up);
    }
  }
}
