// Decompiled with JetBrains decompiler
// Type: Photon.Voice.Unity.Demos.SidebarToggle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Photon.Voice.Unity.Demos
{
  public class SidebarToggle : MonoBehaviour
  {
    [SerializeField]
    private Button sidebarButton;
    [SerializeField]
    private RectTransform panelsHolder;
    private float sidebarWidth = 300f;
    private bool sidebarOpen = true;

    private void Awake()
    {
      this.sidebarButton.onClick.RemoveAllListeners();
      this.sidebarButton.onClick.AddListener(new UnityAction(this.ToggleSidebar));
      this.ToggleSidebar(this.sidebarOpen);
    }

    [ContextMenu("ToggleSidebar")]
    private void ToggleSidebar()
    {
      this.sidebarOpen = !this.sidebarOpen;
      this.ToggleSidebar(this.sidebarOpen);
    }

    private void ToggleSidebar(bool open)
    {
      if (!open)
        this.panelsHolder.SetPosX(0.0f);
      else
        this.panelsHolder.SetPosX(this.sidebarWidth);
    }
  }
}
