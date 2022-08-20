// Decompiled with JetBrains decompiler
// Type: ProximityVoiceTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using Photon.Pun;
using Photon.Voice.PUN;
using Photon.Voice.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Collider))]
[RequireComponent(typeof (Rigidbody))]
public class ProximityVoiceTrigger : VoiceComponent
{
  private List<byte> groupsToAdd = new List<byte>();
  private List<byte> groupsToRemove = new List<byte>();
  [SerializeField]
  private byte[] subscribedGroups;
  private PhotonVoiceView photonVoiceView;
  private PhotonView photonView;

  public byte TargetInterestGroup => (UnityEngine.Object) this.photonView != (UnityEngine.Object) null ? (byte) this.photonView.OwnerActorNr : (byte) 0;

  protected override void Awake()
  {
    this.photonVoiceView = this.GetComponentInParent<PhotonVoiceView>();
    this.photonView = this.GetComponentInParent<PhotonView>();
    this.GetComponent<Collider>().isTrigger = true;
    this.IsLocalCheck();
  }

  private void ToggleTransmission()
  {
    if (!((UnityEngine.Object) this.photonVoiceView.RecorderInUse != (UnityEngine.Object) null))
      return;
    byte targetInterestGroup = this.TargetInterestGroup;
    if ((int) this.photonVoiceView.RecorderInUse.InterestGroup != (int) targetInterestGroup)
    {
      if (this.Logger.IsInfoEnabled)
        this.Logger.LogInfo("Setting RecorderInUse's InterestGroup to {0}", new object[1]
        {
          (object) targetInterestGroup
        });
      this.photonVoiceView.RecorderInUse.InterestGroup = targetInterestGroup;
    }
    bool flag = this.subscribedGroups != null && this.subscribedGroups.Length != 0;
    if (this.photonVoiceView.RecorderInUse.TransmitEnabled != flag)
    {
      if (this.Logger.IsInfoEnabled)
        this.Logger.LogInfo("Setting RecorderInUse's TransmitEnabled to {0}", new object[1]
        {
          (object) flag
        });
      this.photonVoiceView.RecorderInUse.TransmitEnabled = flag;
    }
    this.photonVoiceView.RecorderInUse.IsRecording = true;
  }

  private void OnTriggerEnter(Collider other)
  {
    if (!this.IsLocalCheck())
      return;
    ProximityVoiceTrigger component = other.GetComponent<ProximityVoiceTrigger>();
    if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
      return;
    byte targetInterestGroup = component.TargetInterestGroup;
    if (this.Logger.IsDebugEnabled)
      this.Logger.LogDebug("OnTriggerEnter {0}", new object[1]
      {
        (object) targetInterestGroup
      });
    if ((int) targetInterestGroup == (int) this.TargetInterestGroup || targetInterestGroup == (byte) 0 || this.groupsToAdd.Contains(targetInterestGroup))
      return;
    this.groupsToAdd.Add(targetInterestGroup);
  }

  private void OnTriggerExit(Collider other)
  {
    if (!this.IsLocalCheck())
      return;
    ProximityVoiceTrigger component = other.GetComponent<ProximityVoiceTrigger>();
    if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
      return;
    byte targetInterestGroup = component.TargetInterestGroup;
    if (this.Logger.IsDebugEnabled)
      this.Logger.LogDebug("OnTriggerExit {0}", new object[1]
      {
        (object) targetInterestGroup
      });
    if ((int) targetInterestGroup == (int) this.TargetInterestGroup || targetInterestGroup == (byte) 0)
      return;
    if (this.groupsToAdd.Contains(targetInterestGroup))
      this.groupsToAdd.Remove(targetInterestGroup);
    if (this.groupsToRemove.Contains(targetInterestGroup))
      return;
    this.groupsToRemove.Add(targetInterestGroup);
  }

  private void Update()
  {
    if (!PhotonVoiceNetwork.Instance.Client.InRoom)
    {
      this.subscribedGroups = (byte[]) null;
    }
    else
    {
      if (!this.IsLocalCheck())
        return;
      if (this.groupsToAdd.Count > 0 || this.groupsToRemove.Count > 0)
      {
        byte[] groupsToAdd = (byte[]) null;
        byte[] groupsToRemove = (byte[]) null;
        if (this.groupsToAdd.Count > 0)
          groupsToAdd = this.groupsToAdd.ToArray();
        if (this.groupsToRemove.Count > 0)
          groupsToRemove = this.groupsToRemove.ToArray();
        if (this.Logger.IsInfoEnabled)
          this.Logger.LogInfo("client of actor number {0} trying to change groups, to_be_removed#:{1} to_be_added#={2}", new object[3]
          {
            (object) this.TargetInterestGroup,
            (object) this.groupsToRemove.Count,
            (object) this.groupsToAdd.Count
          });
        if (PhotonVoiceNetwork.Instance.Client.OpChangeGroups(groupsToRemove, groupsToAdd))
        {
          if (this.subscribedGroups != null)
          {
            List<byte> byteList = new List<byte>();
            for (int index = 0; index < this.subscribedGroups.Length; ++index)
              byteList.Add(this.subscribedGroups[index]);
            for (int index = 0; index < this.groupsToRemove.Count; ++index)
            {
              if (byteList.Contains(this.groupsToRemove[index]))
                byteList.Remove(this.groupsToRemove[index]);
            }
            for (int index = 0; index < this.groupsToAdd.Count; ++index)
            {
              if (!byteList.Contains(this.groupsToAdd[index]))
                byteList.Add(this.groupsToAdd[index]);
            }
            this.subscribedGroups = byteList.ToArray();
          }
          else
            this.subscribedGroups = groupsToAdd;
          this.groupsToAdd.Clear();
          this.groupsToRemove.Clear();
        }
        else if (this.Logger.IsErrorEnabled)
          this.Logger.LogError("Error changing groups", Array.Empty<object>());
      }
      this.ToggleTransmission();
    }
  }

  private bool IsLocalCheck()
  {
    if (this.photonVoiceView.IsPhotonViewReady)
    {
      if (this.photonView.IsMine)
        return true;
      if (this.enabled)
      {
        if (this.Logger.IsInfoEnabled)
          this.Logger.LogInfo("Disabling ProximityVoiceTrigger as does not belong to local player, actor number {0}", new object[1]
          {
            (object) this.TargetInterestGroup
          });
        this.enabled = false;
      }
    }
    return false;
  }
}
