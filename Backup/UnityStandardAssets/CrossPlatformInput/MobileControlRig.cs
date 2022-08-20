// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.CrossPlatformInput.MobileControlRig
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityStandardAssets.CrossPlatformInput
{
  [ExecuteInEditMode]
  public class MobileControlRig : MonoBehaviour
  {
    private void OnEnable() => this.CheckEnableControlRig();

    private void Start()
    {
      if (!((UnityEngine.Object) UnityEngine.Object.FindObjectOfType<EventSystem>() == (UnityEngine.Object) null))
        return;
      GameObject gameObject = new GameObject("EventSystem");
      gameObject.AddComponent<EventSystem>();
      gameObject.AddComponent<StandaloneInputModule>();
    }

    private void CheckEnableControlRig() => this.EnableControlRig(false);

    private void EnableControlRig(bool enabled)
    {
      try
      {
        foreach (Component component in this.transform)
          component.gameObject.SetActive(enabled);
      }
      catch (Exception ex)
      {
      }
    }
  }
}
