// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.CrossPlatformInput.ButtonHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace UnityStandardAssets.CrossPlatformInput
{
  public class ButtonHandler : MonoBehaviour
  {
    public string Name;

    private void OnEnable()
    {
    }

    public void SetDownState() => CrossPlatformInputManager.SetButtonDown(this.Name);

    public void SetUpState() => CrossPlatformInputManager.SetButtonUp(this.Name);

    public void SetAxisPositiveState() => CrossPlatformInputManager.SetAxisPositive(this.Name);

    public void SetAxisNeutralState() => CrossPlatformInputManager.SetAxisZero(this.Name);

    public void SetAxisNegativeState() => CrossPlatformInputManager.SetAxisNegative(this.Name);

    public void Update()
    {
    }
  }
}
