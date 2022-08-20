// Decompiled with JetBrains decompiler
// Type: Photon.Voice.Unity.Demos.BackgroundMusicController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Photon.Voice.Unity.Demos
{
  public class BackgroundMusicController : MonoBehaviour
  {
    [SerializeField]
    private Text volumeText;
    [SerializeField]
    private Slider volumeSlider;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private float initialVolume = 0.125f;

    private void Awake()
    {
      this.volumeSlider.minValue = 0.0f;
      this.volumeSlider.maxValue = 1f;
      this.volumeSlider.SetSingleOnValueChangedCallback(new UnityAction<float>(this.OnVolumeChanged));
      this.volumeSlider.value = this.initialVolume;
      this.OnVolumeChanged(this.initialVolume);
    }

    private void OnVolumeChanged(float newValue)
    {
      this.volumeText.text = string.Format("BG Volume: {0:0.###}", (object) newValue);
      this.audioSource.volume = newValue;
    }
  }
}
