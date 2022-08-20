// Decompiled with JetBrains decompiler
// Type: Photon.Voice.Unity.Demos.DemoVoiceUI.MicrophoneDropdownFiller
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Photon.Voice.Unity.Demos.DemoVoiceUI
{
  public class MicrophoneDropdownFiller : MonoBehaviour
  {
    private List<MicRef> micOptions;
    [SerializeField]
    private Dropdown micDropdown;
    [SerializeField]
    private Recorder recorder;
    [SerializeField]
    [FormerlySerializedAs("RefreshButton")]
    private GameObject refreshButton;
    [SerializeField]
    [FormerlySerializedAs("ToggleButton")]
    private GameObject toggleButton;
    private Toggle photonToggle;

    private void Awake()
    {
      this.photonToggle = this.toggleButton.GetComponentInChildren<Toggle>();
      this.RefreshMicrophones();
    }

    private void SetupMicDropdown()
    {
      this.micDropdown.ClearOptions();
      this.micOptions = new List<MicRef>();
      List<string> options = new List<string>();
      for (int index = 0; index < Microphone.devices.Length; ++index)
      {
        string device = Microphone.devices[index];
        this.micOptions.Add(new MicRef(device));
        options.Add(string.Format("[Unity] {0}", (object) device));
      }
      if (Recorder.PhotonMicrophoneEnumerator.IsSupported)
      {
        for (int idx = 0; idx < Recorder.PhotonMicrophoneEnumerator.Count; ++idx)
        {
          string name = Recorder.PhotonMicrophoneEnumerator.NameAtIndex(idx);
          this.micOptions.Add(new MicRef(name, Recorder.PhotonMicrophoneEnumerator.IDAtIndex(idx)));
          options.Add(string.Format("[Photon] {0}", (object) name));
        }
      }
      this.micDropdown.AddOptions(options);
      this.micDropdown.onValueChanged.RemoveAllListeners();
      this.micDropdown.onValueChanged.AddListener((UnityAction<int>) (_param1 => this.MicDropdownValueChanged(this.micOptions[this.micDropdown.value])));
    }

    private void MicDropdownValueChanged(MicRef mic)
    {
      this.recorder.MicrophoneType = mic.MicType;
      switch (mic.MicType)
      {
        case Recorder.MicType.Unity:
          this.recorder.UnityMicrophoneDevice = mic.Name;
          break;
        case Recorder.MicType.Photon:
          this.recorder.PhotonMicrophoneDeviceId = mic.PhotonId;
          break;
      }
      if (!this.recorder.RequiresRestart)
        return;
      this.recorder.RestartRecording();
    }

    private void SetCurrentValue()
    {
      if (this.micOptions == null)
      {
        Debug.LogWarning((object) "micOptions list is null");
      }
      else
      {
        bool isSupported = Recorder.PhotonMicrophoneEnumerator.IsSupported;
        this.photonToggle.onValueChanged.RemoveAllListeners();
        this.photonToggle.isOn = this.recorder.MicrophoneType == Recorder.MicType.Photon;
        if (!isSupported)
          this.photonToggle.onValueChanged.AddListener(new UnityAction<bool>(this.PhotonMicToggled));
        this.micDropdown.gameObject.SetActive(isSupported || this.recorder.MicrophoneType == Recorder.MicType.Unity);
        this.toggleButton.SetActive(!isSupported);
        this.refreshButton.SetActive(isSupported || this.recorder.MicrophoneType == Recorder.MicType.Unity);
        for (int index = 0; index < this.micOptions.Count; ++index)
        {
          MicRef micOption = this.micOptions[index];
          if (this.recorder.MicrophoneType == micOption.MicType)
          {
            if (this.recorder.MicrophoneType == Recorder.MicType.Unity && Recorder.CompareUnityMicNames(micOption.Name, this.recorder.UnityMicrophoneDevice))
            {
              this.micDropdown.value = index;
              return;
            }
            if (this.recorder.MicrophoneType == Recorder.MicType.Photon && micOption.PhotonId == this.recorder.PhotonMicrophoneDeviceId)
            {
              this.micDropdown.value = index;
              return;
            }
          }
        }
        for (int index = 0; index < this.micOptions.Count; ++index)
        {
          MicRef micOption = this.micOptions[index];
          if (this.recorder.MicrophoneType == micOption.MicType)
          {
            if (this.recorder.MicrophoneType == Recorder.MicType.Unity)
            {
              this.micDropdown.value = index;
              this.recorder.UnityMicrophoneDevice = micOption.Name;
              break;
            }
            if (this.recorder.MicrophoneType == Recorder.MicType.Photon)
            {
              this.micDropdown.value = index;
              this.recorder.PhotonMicrophoneDeviceId = micOption.PhotonId;
              break;
            }
          }
        }
        if (!this.recorder.RequiresRestart)
          return;
        this.recorder.RestartRecording();
      }
    }

    public void PhotonMicToggled(bool on)
    {
      this.micDropdown.gameObject.SetActive(!on);
      this.refreshButton.SetActive(!on);
      this.recorder.MicrophoneType = !on ? Recorder.MicType.Unity : Recorder.MicType.Photon;
      if (!this.recorder.RequiresRestart)
        return;
      this.recorder.RestartRecording();
    }

    public void RefreshMicrophones()
    {
      Recorder.PhotonMicrophoneEnumerator.Refresh();
      this.SetupMicDropdown();
      this.SetCurrentValue();
    }

    private void PhotonVoiceCreated() => this.RefreshMicrophones();
  }
}
