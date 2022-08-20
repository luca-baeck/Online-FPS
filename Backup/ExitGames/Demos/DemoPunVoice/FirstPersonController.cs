// Decompiled with JetBrains decompiler
// Type: ExitGames.Demos.DemoPunVoice.FirstPersonController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace ExitGames.Demos.DemoPunVoice
{
  public class FirstPersonController : BaseController
  {
    [SerializeField]
    private MouseLookHelper mouseLook = new MouseLookHelper();
    private float oldYRotation;
    private Quaternion velRotation;

    public Vector3 Velocity => this.rigidBody.velocity;

    protected override void SetCamera()
    {
      base.SetCamera();
      this.mouseLook.Init(this.transform, this.camTrans);
    }

    protected override void Move(float h, float v)
    {
      Vector3 vector3 = this.camTrans.forward * v + this.camTrans.right * h;
      vector3.x *= this.speed;
      vector3.z *= this.speed;
      vector3.y = 0.0f;
      this.rigidBody.velocity = vector3;
    }

    private void Update() => this.RotateView();

    private void RotateView()
    {
      this.oldYRotation = this.transform.eulerAngles.y;
      this.mouseLook.LookRotation(this.transform, this.camTrans);
      this.velRotation = Quaternion.AngleAxis(this.transform.eulerAngles.y - this.oldYRotation, Vector3.up);
      this.rigidBody.velocity = this.velRotation * this.rigidBody.velocity;
    }
  }
}
