// Decompiled with JetBrains decompiler
// Type: Photon.Voice.Unity.Demos.DemoVoiceUI.RemoteSpeakerUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Photon.Voice.Unity.Demos.DemoVoiceUI
{
  [RequireComponent(typeof (Speaker))]
  public class RemoteSpeakerUI : MonoBehaviour, IInRoomCallbacks
  {
    [SerializeField]
    private Text nameText;
    [SerializeField]
    protected Image remoteIsMuting;
    [SerializeField]
    private Image remoteIsTalking;
    [SerializeField]
    private InputField minDelaySoftInputField;
    [SerializeField]
    private InputField maxDelaySoftInputField;
    [SerializeField]
    private InputField maxDelayHardInputField;
    [SerializeField]
    private Text bufferLagText;
    protected Speaker speaker;
    protected VoiceConnection voiceConnection;

    protected virtual void Start()
    {
      this.speaker = this.GetComponent<Speaker>();
      this.minDelaySoftInputField.text = this.speaker.PlaybackDelayMinSoft.ToString();
      this.minDelaySoftInputField.SetSingleOnEndEditCallback(new UnityAction<string>(this.OnMinDelaySoftChanged));
      this.maxDelaySoftInputField.text = this.speaker.PlaybackDelayMaxSoft.ToString();
      this.maxDelaySoftInputField.SetSingleOnEndEditCallback(new UnityAction<string>(this.OnMaxDelaySoftChanged));
      this.maxDelayHardInputField.text = this.speaker.PlaybackDelayMaxHard.ToString();
      this.maxDelayHardInputField.SetSingleOnEndEditCallback(new UnityAction<string>(this.OnMaxDelayHardChanged));
      this.SetNickname();
      this.SetMutedState();
    }

    private void OnMinDelaySoftChanged(string newMinDelaySoftString)
    {
      int playbackDelayMaxSoft = this.speaker.PlaybackDelayMaxSoft;
      int playbackDelayMaxHard = this.speaker.PlaybackDelayMaxHard;
      int result;
      if (int.TryParse(newMinDelaySoftString, out result) && result >= 0 && result < playbackDelayMaxSoft)
        this.speaker.SetPlaybackDelaySettings(result, playbackDelayMaxSoft, playbackDelayMaxHard);
      else
        this.minDelaySoftInputField.text = this.speaker.PlaybackDelayMinSoft.ToString();
    }

    private void OnMaxDelaySoftChanged(string newMaxDelaySoftString)
    {
      int playbackDelayMinSoft = this.speaker.PlaybackDelayMinSoft;
      int playbackDelayMaxHard = this.speaker.PlaybackDelayMaxHard;
      int result;
      if (int.TryParse(newMaxDelaySoftString, out result) && playbackDelayMinSoft < result)
        this.speaker.SetPlaybackDelaySettings(playbackDelayMinSoft, result, playbackDelayMaxHard);
      else
        this.maxDelaySoftInputField.text = this.speaker.PlaybackDelayMaxSoft.ToString();
    }

    private void OnMaxDelayHardChanged(string newMaxDelayHardString)
    {
      int playbackDelayMinSoft = this.speaker.PlaybackDelayMinSoft;
      int playbackDelayMaxSoft = this.speaker.PlaybackDelayMaxSoft;
      int result;
      if (int.TryParse(newMaxDelayHardString, out result) && result >= playbackDelayMaxSoft)
        this.speaker.SetPlaybackDelaySettings(playbackDelayMinSoft, playbackDelayMaxSoft, result);
      else
        this.maxDelayHardInputField.text = this.speaker.PlaybackDelayMaxHard.ToString();
    }

    private void Update()
    {
      this.remoteIsTalking.enabled = this.speaker.IsPlaying;
      this.bufferLagText.text = "Buffer Lag: " + (object) this.speaker.Lag;
    }

    private void OnDestroy() => this.voiceConnection.Client.RemoveCallbackTarget((object) this);

    private void SetNickname()
    {
      string str = this.speaker.name;
      if (this.speaker.Actor != null)
      {
        str = this.speaker.Actor.NickName;
        if (string.IsNullOrEmpty(str))
          str = "user " + (object) this.speaker.Actor.ActorNumber;
      }
      this.nameText.text = str;
    }

    private void SetMutedState() => this.SetMutedState(this.speaker.Actor.IsMuted());

    protected virtual void SetMutedState(bool isMuted) => this.remoteIsMuting.enabled = isMuted;

    protected virtual void OnActorPropertiesChanged(Player targetPlayer, Hashtable changedProps)
    {
      if (targetPlayer.ActorNumber != this.speaker.Actor.ActorNumber)
        return;
      this.SetMutedState();
      this.SetNickname();
    }

    public virtual void Init(VoiceConnection vC)
    {
      this.voiceConnection = vC;
      this.voiceConnection.Client.AddCallbackTarget((object) this);
    }

    void IInRoomCallbacks.OnPlayerEnteredRoom(Player newPlayer)
    {
    }

    void IInRoomCallbacks.OnPlayerLeftRoom(Player otherPlayer)
    {
    }

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
  }
}
