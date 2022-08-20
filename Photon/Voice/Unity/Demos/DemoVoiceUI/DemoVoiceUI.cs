// Decompiled with JetBrains decompiler
// Type: Photon.Voice.Unity.Demos.DemoVoiceUI.DemoVoiceUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Voice.Unity.UtilityScripts;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Photon.Voice.Unity.Demos.DemoVoiceUI
{
  [RequireComponent(typeof (VoiceConnection), typeof (ConnectAndJoin))]
  public class DemoVoiceUI : MonoBehaviour, IInRoomCallbacks, IMatchmakingCallbacks
  {
    [SerializeField]
    private Text connectionStatusText;
    [SerializeField]
    private Text serverStatusText;
    [SerializeField]
    private Text roomStatusText;
    [SerializeField]
    private Text inputWarningText;
    [SerializeField]
    private Text rttText;
    [SerializeField]
    private Text rttVariationText;
    [SerializeField]
    private Text packetLossWarningText;
    [SerializeField]
    private InputField localNicknameText;
    [SerializeField]
    private Toggle debugEchoToggle;
    [SerializeField]
    private Toggle reliableTransmissionToggle;
    [SerializeField]
    private Toggle encryptionToggle;
    [SerializeField]
    private GameObject webRtcDspGameObject;
    [SerializeField]
    private Toggle aecToggle;
    [SerializeField]
    private Toggle aecHighPassToggle;
    [SerializeField]
    private InputField reverseStreamDelayInputField;
    [SerializeField]
    private Toggle noiseSuppressionToggle;
    [SerializeField]
    private Toggle agcToggle;
    [SerializeField]
    private Slider agcCompressionGainSlider;
    [SerializeField]
    private Toggle vadToggle;
    [SerializeField]
    private Toggle muteToggle;
    [SerializeField]
    private Toggle streamAudioClipToggle;
    [SerializeField]
    private Toggle audioToneToggle;
    [SerializeField]
    private Toggle dspToggle;
    [SerializeField]
    private Toggle highPassToggle;
    [SerializeField]
    private Toggle photonVadToggle;
    [SerializeField]
    private GameObject microphoneSetupGameObject;
    [SerializeField]
    private bool defaultTransmitEnabled;
    [SerializeField]
    private int screenWidth = 800;
    [SerializeField]
    private int screenHeight = 600;
    [SerializeField]
    private bool fullScreen;
    [SerializeField]
    private InputField roomNameInputField;
    [SerializeField]
    private InputField globalMinDelaySoftInputField;
    [SerializeField]
    private InputField globalMaxDelaySoftInputField;
    [SerializeField]
    private InputField globalMaxDelayHardInputField;
    [SerializeField]
    private int rttYellowThreshold = 100;
    [SerializeField]
    private int rttRedThreshold = 160;
    [SerializeField]
    private int rttVariationYellowThreshold = 25;
    [SerializeField]
    private int rttVariationRedThreshold = 50;
    private GameObject compressionGainGameObject;
    private Text compressionGainText;
    private GameObject aecOptionsGameObject;
    public Transform RemoteVoicesPanel;
    protected VoiceConnection voiceConnection;
    private WebRtcAudioDsp voiceAudioPreprocessor;
    private ConnectAndJoin connectAndJoin;
    private readonly Color warningColor = new Color(0.9f, 0.5f, 0.0f, 1f);
    private readonly Color okColor = new Color(0.0f, 0.6f, 0.2f, 1f);
    private readonly Color redColor = new Color(1f, 0.0f, 0.0f, 1f);
    private readonly Color defaultColor = new Color(0.0f, 0.0f, 0.0f, 1f);

    private void Awake()
    {
      Screen.SetResolution(this.screenWidth, this.screenHeight, this.fullScreen);
      this.connectAndJoin = this.GetComponent<ConnectAndJoin>();
      this.voiceConnection = this.GetComponent<VoiceConnection>();
      this.voiceAudioPreprocessor = this.voiceConnection.PrimaryRecorder.GetComponent<WebRtcAudioDsp>();
      this.compressionGainGameObject = this.agcCompressionGainSlider.transform.parent.gameObject;
      this.compressionGainText = this.compressionGainGameObject.GetComponentInChildren<Text>();
      this.aecOptionsGameObject = this.aecHighPassToggle.transform.parent.gameObject;
      this.SetDefaults();
      this.InitUiCallbacks();
      this.InitUiValues();
      this.GetSavedNickname();
    }

    protected virtual void SetDefaults() => this.muteToggle.isOn = !this.defaultTransmitEnabled;

    private void OnEnable()
    {
      this.voiceConnection.SpeakerLinked += new Action<Speaker>(this.OnSpeakerCreated);
      this.voiceConnection.Client.AddCallbackTarget((object) this);
    }

    private void OnDisable()
    {
      this.voiceConnection.SpeakerLinked -= new Action<Speaker>(this.OnSpeakerCreated);
      this.voiceConnection.Client.RemoveCallbackTarget((object) this);
    }

    private void GetSavedNickname()
    {
      string str = PlayerPrefs.GetString("vNick");
      if (string.IsNullOrEmpty(str))
        return;
      this.localNicknameText.text = str;
      this.voiceConnection.Client.NickName = str;
    }

    protected virtual void OnSpeakerCreated(Speaker speaker)
    {
      speaker.gameObject.transform.SetParent(this.RemoteVoicesPanel, false);
      speaker.GetComponent<RemoteSpeakerUI>().Init(this.voiceConnection);
      speaker.OnRemoteVoiceRemoveAction += new Action<Speaker>(this.OnRemoteVoiceRemove);
    }

    private void OnRemoteVoiceRemove(Speaker speaker)
    {
      if (!((UnityEngine.Object) speaker != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) speaker.gameObject);
    }

    private void ToggleMute(bool isOn)
    {
      this.muteToggle.targetGraphic.enabled = !isOn;
      if (isOn)
        this.voiceConnection.Client.LocalPlayer.Mute();
      else
        this.voiceConnection.Client.LocalPlayer.Unmute();
    }

    protected virtual void ToggleIsRecording(bool isRecording) => this.voiceConnection.PrimaryRecorder.IsRecording = isRecording;

    private void ToggleDebugEcho(bool isOn) => this.voiceConnection.PrimaryRecorder.DebugEchoMode = isOn;

    private void ToggleReliable(bool isOn) => this.voiceConnection.PrimaryRecorder.ReliableMode = isOn;

    private void ToggleEncryption(bool isOn) => this.voiceConnection.PrimaryRecorder.Encrypt = isOn;

    private void ToggleAEC(bool isOn)
    {
      this.voiceAudioPreprocessor.AEC = isOn;
      this.aecOptionsGameObject.SetActive(isOn);
    }

    private void ToggleNoiseSuppression(bool isOn) => this.voiceAudioPreprocessor.NoiseSuppression = isOn;

    private void ToggleAGC(bool isOn)
    {
      this.voiceAudioPreprocessor.AGC = isOn;
      this.compressionGainGameObject.SetActive(isOn);
    }

    private void ToggleVAD(bool isOn) => this.voiceAudioPreprocessor.VAD = isOn;

    private void ToggleHighPass(bool isOn) => this.voiceAudioPreprocessor.HighPass = isOn;

    private void ToggleDsp(bool isOn)
    {
      this.voiceAudioPreprocessor.Bypass = !isOn;
      this.voiceAudioPreprocessor.enabled = isOn;
      this.webRtcDspGameObject.SetActive(isOn);
    }

    private void ToggleAudioClipStreaming(bool isOn)
    {
      this.microphoneSetupGameObject.SetActive(!isOn && !this.audioToneToggle.isOn);
      if (isOn)
      {
        this.audioToneToggle.SetValue(false);
        this.voiceConnection.PrimaryRecorder.SourceType = Recorder.InputSourceType.AudioClip;
      }
      else if (!this.audioToneToggle.isOn)
        this.voiceConnection.PrimaryRecorder.SourceType = Recorder.InputSourceType.Microphone;
      if (!this.voiceConnection.PrimaryRecorder.RequiresRestart)
        return;
      this.voiceConnection.PrimaryRecorder.RestartRecording();
    }

    private void ToggleAudioToneFactory(bool isOn)
    {
      this.microphoneSetupGameObject.SetActive(!isOn && !this.streamAudioClipToggle.isOn);
      if (isOn)
      {
        this.streamAudioClipToggle.SetValue(false);
        this.dspToggle.isOn = false;
        this.voiceConnection.PrimaryRecorder.InputFactory = (Func<IAudioDesc>) (() => (IAudioDesc) new AudioUtil.ToneAudioReader<float>());
        this.voiceConnection.PrimaryRecorder.SourceType = Recorder.InputSourceType.Factory;
      }
      else if (!this.streamAudioClipToggle.isOn)
        this.voiceConnection.PrimaryRecorder.SourceType = Recorder.InputSourceType.Microphone;
      if (!this.voiceConnection.PrimaryRecorder.RequiresRestart)
        return;
      this.voiceConnection.PrimaryRecorder.RestartRecording();
    }

    private void TogglePhotonVAD(bool isOn) => this.voiceConnection.PrimaryRecorder.VoiceDetection = isOn;

    private void ToggleAecHighPass(bool isOn) => this.voiceAudioPreprocessor.AecHighPass = isOn;

    private void OnAgcCompressionGainChanged(float agcCompressionGain)
    {
      this.voiceAudioPreprocessor.AgcCompressionGain = (int) agcCompressionGain;
      this.compressionGainText.text = "Compression Gain: " + (object) agcCompressionGain;
    }

    private void OnGlobalPlaybackDelayMinSoftChanged(string newMinDelaySoftString)
    {
      int playbackDelayMaxSoft = this.voiceConnection.GlobalPlaybackDelayMaxSoft;
      int playbackDelayMaxHard = this.voiceConnection.GlobalPlaybackDelayMaxHard;
      int result;
      if (int.TryParse(newMinDelaySoftString, out result) && result >= 0 && result < playbackDelayMaxSoft)
        this.voiceConnection.SetGlobalPlaybackDelaySettings(result, playbackDelayMaxSoft, playbackDelayMaxHard);
      else
        this.globalMinDelaySoftInputField.text = this.voiceConnection.GlobalPlaybackDelayMinSoft.ToString();
    }

    private void OnGlobalPlaybackDelayMaxSoftChanged(string newMaxDelaySoftString)
    {
      int playbackDelayMinSoft = this.voiceConnection.GlobalPlaybackDelayMinSoft;
      int playbackDelayMaxHard = this.voiceConnection.GlobalPlaybackDelayMaxHard;
      int result;
      if (int.TryParse(newMaxDelaySoftString, out result) && result > playbackDelayMinSoft)
        this.voiceConnection.SetGlobalPlaybackDelaySettings(playbackDelayMinSoft, result, playbackDelayMaxHard);
      else
        this.globalMaxDelaySoftInputField.text = this.voiceConnection.GlobalPlaybackDelayMaxSoft.ToString();
    }

    private void OnGlobalPlaybackDelayMaxHardChanged(string newMaxDelayHardString)
    {
      int playbackDelayMinSoft = this.voiceConnection.GlobalPlaybackDelayMinSoft;
      int playbackDelayMaxSoft = this.voiceConnection.GlobalPlaybackDelayMaxSoft;
      int result;
      if (int.TryParse(newMaxDelayHardString, out result) && result >= playbackDelayMaxSoft)
        this.voiceConnection.SetGlobalPlaybackDelaySettings(playbackDelayMinSoft, playbackDelayMaxSoft, result);
      else
        this.globalMaxDelayHardInputField.text = this.voiceConnection.GlobalPlaybackDelayMaxHard.ToString();
    }

    private void OnReverseStreamDelayChanged(string newReverseStreamString)
    {
      int result;
      if (int.TryParse(newReverseStreamString, out result) && result > 0)
        this.voiceAudioPreprocessor.ReverseStreamDelayMs = result;
      else
        this.reverseStreamDelayInputField.text = this.voiceAudioPreprocessor.ReverseStreamDelayMs.ToString();
    }

    private void UpdateSyncedNickname(string nickname)
    {
      nickname = nickname.Trim();
      if (string.IsNullOrEmpty(nickname))
        return;
      this.voiceConnection.Client.LocalPlayer.NickName = nickname;
      PlayerPrefs.SetString("vNick", nickname);
    }

    private void JoinOrCreateRoom(string roomName)
    {
      if (string.IsNullOrEmpty(roomName))
      {
        this.connectAndJoin.RoomName = string.Empty;
        this.connectAndJoin.RandomRoom = true;
      }
      else
      {
        this.connectAndJoin.RoomName = roomName.Trim();
        this.connectAndJoin.RandomRoom = false;
      }
      if (this.voiceConnection.Client.InRoom)
      {
        this.voiceConnection.Client.OpLeaveRoom(false);
      }
      else
      {
        if (this.voiceConnection.Client.IsConnected)
          return;
        this.voiceConnection.ConnectUsingSettings();
      }
    }

    protected virtual void Update()
    {
      this.connectionStatusText.text = this.voiceConnection.Client.State.ToString();
      this.serverStatusText.text = string.Format("{0}/{1}", (object) this.voiceConnection.Client.CloudRegion, (object) this.voiceConnection.Client.CurrentServerAddress);
      if (this.voiceConnection.PrimaryRecorder.IsCurrentlyTransmitting)
      {
        float currentAvgAmp = this.voiceConnection.PrimaryRecorder.LevelMeter.CurrentAvgAmp;
        if ((double) currentAvgAmp > 1.0)
          currentAvgAmp /= 32768f;
        if ((double) currentAvgAmp > 0.1)
        {
          this.inputWarningText.text = "Input too loud!";
          this.inputWarningText.color = this.warningColor;
        }
        else
        {
          this.inputWarningText.text = string.Empty;
          this.ResetTextColor(this.inputWarningText);
        }
      }
      if ((double) this.voiceConnection.FramesReceivedPerSecond > 0.0)
      {
        this.packetLossWarningText.text = string.Format("{0:0.##}% Packet Loss", (object) this.voiceConnection.FramesLostPercent);
        this.packetLossWarningText.color = (double) this.voiceConnection.FramesLostPercent > 1.0 ? this.warningColor : this.okColor;
      }
      else
      {
        this.packetLossWarningText.text = string.Empty;
        this.ResetTextColor(this.packetLossWarningText);
      }
      this.rttText.text = "RTT:" + (object) this.voiceConnection.Client.LoadBalancingPeer.RoundTripTime;
      this.SetTextColor(this.voiceConnection.Client.LoadBalancingPeer.RoundTripTime, this.rttText, this.rttYellowThreshold, this.rttRedThreshold);
      this.rttVariationText.text = "VAR:" + (object) this.voiceConnection.Client.LoadBalancingPeer.RoundTripTimeVariance;
      this.SetTextColor(this.voiceConnection.Client.LoadBalancingPeer.RoundTripTimeVariance, this.rttVariationText, this.rttVariationYellowThreshold, this.rttVariationRedThreshold);
    }

    private void SetTextColor(int textValue, Text text, int yellowThreshold, int redThreshold)
    {
      if (textValue > redThreshold)
        text.color = this.redColor;
      else if (textValue > yellowThreshold)
        text.color = this.warningColor;
      else
        text.color = this.okColor;
    }

    private void ResetTextColor(Text text) => text.color = this.defaultColor;

    private void InitUiCallbacks()
    {
      this.muteToggle.SetSingleOnValueChangedCallback(new UnityAction<bool>(this.ToggleMute));
      this.debugEchoToggle.SetSingleOnValueChangedCallback(new UnityAction<bool>(this.ToggleDebugEcho));
      this.vadToggle.SetSingleOnValueChangedCallback(new UnityAction<bool>(this.ToggleVAD));
      this.aecToggle.SetSingleOnValueChangedCallback(new UnityAction<bool>(this.ToggleAEC));
      this.agcToggle.SetSingleOnValueChangedCallback(new UnityAction<bool>(this.ToggleAGC));
      this.debugEchoToggle.SetSingleOnValueChangedCallback(new UnityAction<bool>(this.ToggleDebugEcho));
      this.dspToggle.SetSingleOnValueChangedCallback(new UnityAction<bool>(this.ToggleDsp));
      this.highPassToggle.SetSingleOnValueChangedCallback(new UnityAction<bool>(this.ToggleHighPass));
      this.encryptionToggle.SetSingleOnValueChangedCallback(new UnityAction<bool>(this.ToggleEncryption));
      this.reliableTransmissionToggle.SetSingleOnValueChangedCallback(new UnityAction<bool>(this.ToggleReliable));
      this.streamAudioClipToggle.SetSingleOnValueChangedCallback(new UnityAction<bool>(this.ToggleAudioClipStreaming));
      this.photonVadToggle.SetSingleOnValueChangedCallback(new UnityAction<bool>(this.TogglePhotonVAD));
      this.aecHighPassToggle.SetSingleOnValueChangedCallback(new UnityAction<bool>(this.ToggleAecHighPass));
      this.noiseSuppressionToggle.SetSingleOnValueChangedCallback(new UnityAction<bool>(this.ToggleNoiseSuppression));
      this.audioToneToggle.SetSingleOnValueChangedCallback(new UnityAction<bool>(this.ToggleAudioToneFactory));
      this.agcCompressionGainSlider.SetSingleOnValueChangedCallback(new UnityAction<float>(this.OnAgcCompressionGainChanged));
      this.localNicknameText.SetSingleOnEndEditCallback(new UnityAction<string>(this.UpdateSyncedNickname));
      this.roomNameInputField.SetSingleOnEndEditCallback(new UnityAction<string>(this.JoinOrCreateRoom));
      this.globalMinDelaySoftInputField.SetSingleOnEndEditCallback(new UnityAction<string>(this.OnGlobalPlaybackDelayMinSoftChanged));
      this.globalMaxDelaySoftInputField.SetSingleOnEndEditCallback(new UnityAction<string>(this.OnGlobalPlaybackDelayMaxSoftChanged));
      this.globalMaxDelayHardInputField.SetSingleOnEndEditCallback(new UnityAction<string>(this.OnGlobalPlaybackDelayMaxHardChanged));
      this.reverseStreamDelayInputField.SetSingleOnEndEditCallback(new UnityAction<string>(this.OnReverseStreamDelayChanged));
    }

    private void InitUiValues()
    {
      this.muteToggle.SetValue(this.voiceConnection.Client.LocalPlayer.IsMuted());
      this.debugEchoToggle.SetValue(this.voiceConnection.PrimaryRecorder.DebugEchoMode);
      this.reliableTransmissionToggle.SetValue(this.voiceConnection.PrimaryRecorder.ReliableMode);
      this.encryptionToggle.SetValue(this.voiceConnection.PrimaryRecorder.Encrypt);
      this.streamAudioClipToggle.SetValue(this.voiceConnection.PrimaryRecorder.SourceType == Recorder.InputSourceType.AudioClip);
      this.audioToneToggle.SetValue(this.voiceConnection.PrimaryRecorder.SourceType == Recorder.InputSourceType.Factory);
      this.microphoneSetupGameObject.SetActive(!this.streamAudioClipToggle.isOn && !this.audioToneToggle.isOn);
      InputField delaySoftInputField1 = this.globalMinDelaySoftInputField;
      int num = this.voiceConnection.GlobalPlaybackDelayMinSoft;
      string v1 = num.ToString();
      delaySoftInputField1.SetValue(v1);
      InputField delaySoftInputField2 = this.globalMaxDelaySoftInputField;
      num = this.voiceConnection.GlobalPlaybackDelayMaxSoft;
      string v2 = num.ToString();
      delaySoftInputField2.SetValue(v2);
      InputField delayHardInputField = this.globalMaxDelayHardInputField;
      num = this.voiceConnection.GlobalPlaybackDelayMaxHard;
      string v3 = num.ToString();
      delayHardInputField.SetValue(v3);
      if ((UnityEngine.Object) this.webRtcDspGameObject != (UnityEngine.Object) null)
      {
        if ((UnityEngine.Object) this.voiceAudioPreprocessor == (UnityEngine.Object) null)
        {
          this.webRtcDspGameObject.SetActive(false);
          this.dspToggle.gameObject.SetActive(false);
        }
        else
        {
          this.dspToggle.gameObject.SetActive(true);
          this.dspToggle.SetValue(!this.voiceAudioPreprocessor.Bypass && this.voiceAudioPreprocessor.enabled);
          this.webRtcDspGameObject.SetActive(this.dspToggle.isOn);
          this.aecToggle.SetValue(this.voiceAudioPreprocessor.AEC);
          this.aecHighPassToggle.SetValue(this.voiceAudioPreprocessor.AecHighPass);
          InputField streamDelayInputField = this.reverseStreamDelayInputField;
          num = this.voiceAudioPreprocessor.ReverseStreamDelayMs;
          string str = num.ToString();
          streamDelayInputField.text = str;
          this.aecOptionsGameObject.SetActive(this.voiceAudioPreprocessor.AEC);
          this.noiseSuppressionToggle.isOn = this.voiceAudioPreprocessor.NoiseSuppression;
          this.agcToggle.SetValue(this.voiceAudioPreprocessor.AGC);
          this.agcCompressionGainSlider.SetValue((float) this.voiceAudioPreprocessor.AgcCompressionGain);
          this.compressionGainGameObject.SetActive(this.voiceAudioPreprocessor.AGC);
          this.vadToggle.SetValue(this.voiceAudioPreprocessor.VAD);
          this.highPassToggle.SetValue(this.voiceAudioPreprocessor.HighPass);
        }
      }
      else
        this.dspToggle.gameObject.SetActive(false);
    }

    private void SetRoomDebugText()
    {
      string empty = string.Empty;
      if (this.voiceConnection.Client.InRoom)
      {
        foreach (Player player in this.voiceConnection.Client.CurrentRoom.Players.Values)
          empty += player.ToStringFull();
        this.roomStatusText.text = string.Format("{0} {1}", (object) this.voiceConnection.Client.CurrentRoom.Name, (object) empty);
      }
      else
        this.roomStatusText.text = string.Empty;
      this.roomStatusText.text = this.voiceConnection.Client.CurrentRoom == null ? string.Empty : string.Format("{0} {1}", (object) this.voiceConnection.Client.CurrentRoom.Name, (object) empty);
    }

    protected virtual void OnActorPropertiesChanged(Player targetPlayer, Hashtable changedProps)
    {
      if (targetPlayer.IsLocal)
      {
        bool isOn = targetPlayer.IsMuted();
        this.voiceConnection.PrimaryRecorder.TransmitEnabled = !isOn;
        this.muteToggle.SetValue(isOn);
      }
      this.SetRoomDebugText();
    }

    void IInRoomCallbacks.OnPlayerEnteredRoom(Player newPlayer) => this.SetRoomDebugText();

    void IInRoomCallbacks.OnPlayerLeftRoom(Player otherPlayer) => this.SetRoomDebugText();

    void IInRoomCallbacks.OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
    }

    void IInRoomCallbacks.OnPlayerPropertiesUpdate(
      Player targetPlayer,
      Hashtable changedProps)
    {
      this.OnActorPropertiesChanged(targetPlayer, changedProps);
    }

    void IInRoomCallbacks.OnMasterClientSwitched(Player newMasterClient)
    {
    }

    void IMatchmakingCallbacks.OnFriendListUpdate(List<FriendInfo> friendList)
    {
    }

    void IMatchmakingCallbacks.OnCreatedRoom()
    {
    }

    void IMatchmakingCallbacks.OnCreateRoomFailed(
      short returnCode,
      string message)
    {
    }

    void IMatchmakingCallbacks.OnJoinedRoom() => this.SetRoomDebugText();

    void IMatchmakingCallbacks.OnJoinRoomFailed(
      short returnCode,
      string message)
    {
    }

    void IMatchmakingCallbacks.OnJoinRandomFailed(
      short returnCode,
      string message)
    {
    }

    void IMatchmakingCallbacks.OnLeftRoom()
    {
      if (ConnectionHandler.AppQuits)
        return;
      this.SetRoomDebugText();
      this.SetDefaults();
    }
  }
}
