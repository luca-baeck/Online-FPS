// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.CrossPlatformInput.InputAxisScrollbar
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace UnityStandardAssets.CrossPlatformInput
{
  public class InputAxisScrollbar : MonoBehaviour
  {
    public string axis;

    private void Update()
    {
    }

    public void HandleInput(float value) => CrossPlatformInputManager.SetAxis(this.axis, (float) ((double) value * 2.0 - 1.0));
  }
}
