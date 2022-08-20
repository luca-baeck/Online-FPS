// Decompiled with JetBrains decompiler
// Type: ExitGames.Demos.DemoPunVoice.VoiceDemoUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.PUN;
using Photon.Voice.Unity;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ExitGames.Demos.DemoPunVoice
{
  public class VoiceDemoUI : MonoBehaviour
  {
    [SerializeField]
    private Text punState;
    [SerializeField]
    private Text voiceState;
    private PhotonVoiceNetwork punVoiceNetwork;
    private Canvas canvas;
    [SerializeField]
    private Button punSwitch;
    private Text punSwitchText;
    [SerializeField]
    private Button voiceSwitch;
    private Text voiceSwitchText;
    [SerializeField]
    private Button calibrateButton;
    private Text calibrateText;
    [SerializeField]
    private Text voiceDebugText;
    public Recorder recorder;
    [SerializeField]
    private GameObject inGameSettings;
    [SerializeField]
    private GameObject globalSettings;
    [SerializeField]
    private Text devicesInfoText;
    private GameObject debugGO;
    private bool debugMode;
    private float volumeBeforeMute;
    private DebugLevel previousDebugLevel;
    [SerializeField]
    private int calibrationMilliSeconds = 2000;

    public bool DebugMode
    {
      get => this.debugMode;
      set
      {
        this.debugMode = value;
        this.debugGO.SetActive(this.debugMode);
        this.voiceDebugText.text = string.Empty;
        if (this.debugMode)
        {
          this.previousDebugLevel = this.punVoiceNetwork.Client.LoadBalancingPeer.DebugOut;
          this.punVoiceNetwork.Client.LoadBalancingPeer.DebugOut = DebugLevel.ALL;
        }
        else
          this.punVoiceNetwork.Client.LoadBalancingPeer.DebugOut = this.previousDebugLevel;
        if (VoiceDemoUI.DebugToggled == null)
          return;
        VoiceDemoUI.DebugToggled(this.debugMode);
      }
    }

    public static event VoiceDemoUI.OnDebugToggle DebugToggled;

    private void Awake() => this.punVoiceNetwork = PhotonVoiceNetwork.Instance;

    private void OnEnable()
    {
      ChangePOV.CameraChanged += new ChangePOV.OnCameraChanged(this.OnCameraChanged);
      BetterToggle.ToggleValueChanged += new BetterToggle.OnToggle(this.BetterToggle_ToggleValueChanged);
      CharacterInstantiation.CharacterInstantiated += new CharacterInstantiation.OnCharacterInstantiated(this.CharacterInstantiation_CharacterInstantiated);
      this.punVoiceNetwork.Client.StateChanged += new Action<ClientState, ClientState>(this.VoiceClientStateChanged);
      PhotonNetwork.NetworkingClient.StateChanged += new Action<ClientState, ClientState>(this.PunClientStateChanged);
    }

    private void OnDisable()
    {
      ChangePOV.CameraChanged -= new ChangePOV.OnCameraChanged(this.OnCameraChanged);
      BetterToggle.ToggleValueChanged -= new BetterToggle.OnToggle(this.BetterToggle_ToggleValueChanged);
      CharacterInstantiation.CharacterInstantiated -= new CharacterInstantiation.OnCharacterInstantiated(this.CharacterInstantiation_CharacterInstantiated);
      this.punVoiceNetwork.Client.StateChanged -= new Action<ClientState, ClientState>(this.VoiceClientStateChanged);
      PhotonNetwork.NetworkingClient.StateChanged -= new Action<ClientState, ClientState>(this.PunClientStateChanged);
    }

    private void CharacterInstantiation_CharacterInstantiated(GameObject character)
    {
      if ((bool) (UnityEngine.Object) this.recorder)
        return;
      PhotonVoiceView component = character.GetComponent<PhotonVoiceView>();
      if (!component.IsRecorder)
        return;
      this.recorder = component.RecorderInUse;
    }

    private void InitToggles(Toggle[] toggles)
    {
      if (toggles == null)
        return;
      for (int index = 0; index < toggles.Length; ++index)
      {
        Toggle toggle = toggles[index];
        switch (toggle.name)
        {
          case "AutoConnectAndJoin":
            toggle.isOn = this.punVoiceNetwork.AutoConnectAndJoin;
            break;
          case "AutoLeaveAndDisconnect":
            toggle.isOn = this.punVoiceNetwork.AutoLeaveAndDisconnect;
            break;
          case "DebugEcho":
            toggle.isOn = (UnityEngine.Object) this.recorder != (UnityEngine.Object) null && this.recorder.DebugEchoMode;
            break;
          case "DebugVoice":
            toggle.isOn = this.DebugMode;
            break;
          case "Mute":
            toggle.isOn = (double) AudioListener.volume <= 1.0 / 1000.0;
            break;
          case "Transmit":
            toggle.isOn = (UnityEngine.Object) this.recorder != (UnityEngine.Object) null && this.recorder.TransmitEnabled;
            break;
          case "VoiceDetection":
            toggle.isOn = (UnityEngine.Object) this.recorder != (UnityEngine.Object) null && this.recorder.VoiceDetection;
            break;
        }
      }
    }

    private void BetterToggle_ToggleValueChanged(Toggle toggle)
    {
      string name = toggle.name;
      // ISSUE: reference to a compiler-generated method
      switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(name))
      {
        case 251438200:
          if (!(name == "AutoLeaveAndDisconnect"))
            break;
          this.punVoiceNetwork.AutoLeaveAndDisconnect = toggle.isOn;
          break;
        case 307911202:
          if (!(name == "DebugVoice"))
            break;
          this.DebugMode = toggle.isOn;
          break;
        case 836321993:
          if (!(name == "Transmit") || !(bool) (UnityEngine.Object) this.recorder)
            break;
          this.recorder.TransmitEnabled = toggle.isOn;
          break;
        case 1063296884:
          if (!(name == "Mute"))
            break;
          if (toggle.isOn)
          {
            this.volumeBeforeMute = AudioListener.volume;
            AudioListener.volume = 0.0f;
            break;
          }
          AudioListener.volume = this.volumeBeforeMute;
          this.volumeBeforeMute = 0.0f;
          break;
        case 2809822681:
          if (!(name == "DebugEcho") || !(bool) (UnityEngine.Object) this.recorder)
            break;
          this.recorder.DebugEchoMode = toggle.isOn;
          break;
        case 2997042041:
          if (!(name == "AutoConnectAndJoin"))
            break;
          this.punVoiceNetwork.AutoConnectAndJoin = toggle.isOn;
          break;
        case 3751824224:
          if (!(name == "VoiceDetection") || !(bool) (UnityEngine.Object) this.recorder)
            break;
          this.recorder.VoiceDetection = toggle.isOn;
          break;
      }
    }

    private void OnCameraChanged(Camera newCamera) => this.canvas.worldCamera = newCamera;

    private void Start()
    {
      this.canvas = this.GetComponentInChildren<Canvas>();
      if ((UnityEngine.Object) this.punSwitch != (UnityEngine.Object) null)
      {
        this.punSwitchText = this.punSwitch.GetComponentInChildren<Text>();
        this.punSwitch.onClick.AddListener(new UnityAction(this.PunSwitchOnClick));
      }
      if ((UnityEngine.Object) this.voiceSwitch != (UnityEngine.Object) null)
      {
        this.voiceSwitchText = this.voiceSwitch.GetComponentInChildren<Text>();
        this.voiceSwitch.onClick.AddListener(new UnityAction(this.VoiceSwitchOnClick));
      }
      if ((UnityEngine.Object) this.calibrateButton != (UnityEngine.Object) null)
      {
        this.calibrateButton.onClick.AddListener(new UnityAction(this.CalibrateButtonOnClick));
        this.calibrateText = this.calibrateButton.GetComponentInChildren<Text>();
      }
      if ((UnityEngine.Object) this.punState != (UnityEngine.Object) null)
        this.debugGO = this.punState.transform.parent.gameObject;
      this.volumeBeforeMute = AudioListener.volume;
      this.previousDebugLevel = this.punVoiceNetwork.Client.LoadBalancingPeer.DebugOut;
      if ((UnityEngine.Object) this.globalSettings != (UnityEngine.Object) null)
      {
        this.globalSettings.SetActive(true);
        this.InitToggles(this.globalSettings.GetComponentsInChildren<Toggle>());
      }
      if (!((UnityEngine.Object) this.devicesInfoText != (UnityEngine.Object) null))
        return;
      if (UnityMicrophone.devices == null || UnityMicrophone.devices.Length == 0)
      {
        this.devicesInfoText.enabled = true;
        this.devicesInfoText.color = Color.red;
        this.devicesInfoText.text = "No microphone device detected!";
      }
      else if (UnityMicrophone.devices.Length == 1)
      {
        this.devicesInfoText.text = string.Format("Mic.: {0}", (object) UnityMicrophone.devices[0]);
      }
      else
      {
        this.devicesInfoText.text = string.Format("Multi.Mic.Devices:\n0. {0} (active)\n", (object) UnityMicrophone.devices[0]);
        for (int index = 1; index < UnityMicrophone.devices.Length; ++index)
          this.devicesInfoText.text += string.Format("{0}. {1}\n", (object) index, (object) UnityMicrophone.devices[index]);
      }
    }

    private void PunSwitchOnClick()
    {
      switch (PhotonNetwork.NetworkClientState)
      {
        case ClientState.PeerCreated:
        case ClientState.Disconnected:
          PhotonNetwork.ConnectUsingSettings();
          break;
        case ClientState.Joined:
          PhotonNetwork.Disconnect();
          break;
      }
    }

    private void VoiceSwitchOnClick()
    {
      if (this.punVoiceNetwork.ClientState == ClientState.Joined)
      {
        this.punVoiceNetwork.Disconnect();
      }
      else
      {
        if (this.punVoiceNetwork.ClientState != ClientState.PeerCreated && this.punVoiceNetwork.ClientState != ClientState.Disconnected)
          return;
        this.punVoiceNetwork.ConnectAndJoinRoom();
      }
    }

    private void CalibrateButtonOnClick()
    {
      if (!(bool) (UnityEngine.Object) this.recorder || this.recorder.VoiceDetectorCalibrating)
        return;
      this.recorder.VoiceDetectorCalibrate(this.calibrationMilliSeconds);
    }

    private void Update()
    {
      if (!((UnityEngine.Object) this.recorder != (UnityEngine.Object) null) || this.recorder.LevelMeter == null)
        return;
      this.voiceDebugText.text = string.Format("Amp: avg. {0:0.000000}, peak {1:0.000000}", (object) this.recorder.LevelMeter.CurrentAvgAmp, (object) this.recorder.LevelMeter.CurrentPeakAmp);
    }

    private void PunClientStateChanged(ClientState fromState, ClientState toState)
    {
      this.punState.text = string.Format("PUN: {0}", (object) toState);
      switch (toState)
      {
        case ClientState.PeerCreated:
        case ClientState.Disconnected:
          this.punSwitch.interactable = true;
          this.punSwitchText.text = "PUN Connect";
          break;
        case ClientState.Joined:
          this.punSwitch.interactable = true;
          this.punSwitchText.text = "PUN Disconnect";
          break;
        default:
          this.punSwitch.interactable = false;
          this.punSwitchText.text = "PUN busy";
          break;
      }
      this.UpdateUiBasedOnVoiceState(this.punVoiceNetwork.ClientState);
    }

    private void VoiceClientStateChanged(ClientState fromState, ClientState toState) => this.UpdateUiBasedOnVoiceState(toState);

    private void UpdateUiBasedOnVoiceState(ClientState voiceClientState)
    {
      this.voiceState.text = string.Format("PhotonVoice: {0}", (object) voiceClientState);
      switch (voiceClientState)
      {
        case ClientState.PeerCreated:
        case ClientState.Disconnected:
          if (PhotonNetwork.InRoom)
          {
            this.voiceSwitch.interactable = true;
            this.voiceSwitchText.text = "Voice Connect";
            this.voiceDebugText.text = string.Empty;
          }
          else
          {
            this.voiceSwitch.interactable = false;
            this.voiceSwitchText.text = "Voice N/A";
            this.voiceDebugText.text = string.Empty;
          }
          this.calibrateButton.interactable = false;
          this.voiceSwitchText.text = "Voice Connect";
          this.calibrateText.text = "Unavailable";
          this.inGameSettings.SetActive(false);
          break;
        case ClientState.Joined:
          this.voiceSwitch.interactable = true;
          this.inGameSettings.SetActive(true);
          this.voiceSwitchText.text = "Voice Disconnect";
          this.InitToggles(this.inGameSettings.GetComponentsInChildren<Toggle>());
          if ((UnityEngine.Object) this.recorder != (UnityEngine.Object) null)
          {
            this.calibrateButton.interactable = !this.recorder.VoiceDetectorCalibrating;
            this.calibrateText.text = this.recorder.VoiceDetectorCalibrating ? "Calibrating" : string.Format("Calibrate ({0}s)", (object) (this.calibrationMilliSeconds / 1000));
            break;
          }
          this.calibrateButton.interactable = false;
          this.calibrateText.text = "Unavailable";
          break;
        default:
          this.voiceSwitch.interactable = false;
          this.voiceSwitchText.text = "Voice busy";
          break;
      }
    }

    public delegate void OnDebugToggle(bool debugMode);
  }
}
