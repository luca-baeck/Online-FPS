// Decompiled with JetBrains decompiler
// Type: ExitGames.Demos.DemoPunVoice.BetterToggle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ExitGames.Demos.DemoPunVoice
{
  [RequireComponent(typeof (Toggle))]
  [DisallowMultipleComponent]
  public class BetterToggle : MonoBehaviour
  {
    private Toggle toggle;

    public static event BetterToggle.OnToggle ToggleValueChanged;

    private void Start()
    {
      this.toggle = this.GetComponent<Toggle>();
      this.toggle.onValueChanged.AddListener((UnityAction<bool>) (_param1 => this.OnToggleValueChanged()));
    }

    public void OnToggleValueChanged()
    {
      if (BetterToggle.ToggleValueChanged == null)
        return;
      BetterToggle.ToggleValueChanged(this.toggle);
    }

    public delegate void OnToggle(Toggle toggle);
  }
}
