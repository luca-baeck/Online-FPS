// Decompiled with JetBrains decompiler
// Type: UB.Simple2dWeatherEffects.Standard.D2FogsNoiseTexPE
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace UB.Simple2dWeatherEffects.Standard
{
  [ExecuteInEditMode]
  public class D2FogsNoiseTexPE : EffectBase
  {
    public Transform CamTransform;
    private Vector3 _firstPosition;
    private Vector3 _difference;
    public float CameraSpeedMultiplier = 1f;
    public Color Color = new Color(1f, 1f, 1f, 1f);
    public Texture2D Noise;
    public float Size = 1f;
    public float HorizontalSpeed = 0.2f;
    public float VerticalSpeed;
    [Range(0.0f, 5f)]
    public float Density = 2f;
    public Shader Shader;
    private Material _material;

    private void Awake() => this._firstPosition = this.CamTransform.position;

    private void Update() => this._difference = this.CamTransform.position - this._firstPosition;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
      if ((bool) (Object) this._material)
      {
        Object.DestroyImmediate((Object) this._material);
        this._material = (Material) null;
      }
      if ((bool) (Object) this.Shader)
      {
        this._material = new Material(this.Shader);
        this._material.hideFlags = HideFlags.HideAndDontSave;
        if (this._material.HasProperty("_Color"))
          this._material.SetColor("_Color", this.Color);
        if (this._material.HasProperty("_NoiseTex"))
          this._material.SetTexture("_NoiseTex", (Texture) this.Noise);
        if (this._material.HasProperty("_Size"))
          this._material.SetFloat("_Size", this.Size);
        if (this._material.HasProperty("_Speed"))
          this._material.SetFloat("_Speed", this.HorizontalSpeed);
        if (this._material.HasProperty("_VSpeed"))
          this._material.SetFloat("_VSpeed", this.VerticalSpeed);
        if (this._material.HasProperty("_Density"))
          this._material.SetFloat("_Density", this.Density);
        if (this._material.HasProperty("_CameraSpeedMultiplier"))
          this._material.SetFloat("_CameraSpeedMultiplier", this.CameraSpeedMultiplier);
        if (this._material.HasProperty("_UVChangeX"))
          this._material.SetFloat("_UVChangeX", this._difference.x);
        if (this._material.HasProperty("_UVChangeY"))
          this._material.SetFloat("_UVChangeY", this._difference.y);
      }
      if (!((Object) this.Shader != (Object) null) || !((Object) this._material != (Object) null))
        return;
      Graphics.Blit((Texture) source, destination, this._material);
    }
  }
}
