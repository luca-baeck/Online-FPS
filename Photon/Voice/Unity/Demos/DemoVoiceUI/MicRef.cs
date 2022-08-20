// Decompiled with JetBrains decompiler
// Type: Photon.Voice.Unity.Demos.DemoVoiceUI.MicRef
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

namespace Photon.Voice.Unity.Demos.DemoVoiceUI
{
  public struct MicRef
  {
    public Recorder.MicType MicType;
    public string Name;
    public int PhotonId;

    public MicRef(string name, int id)
    {
      this.MicType = Recorder.MicType.Photon;
      this.Name = name;
      this.PhotonId = id;
    }

    public MicRef(string name)
    {
      this.MicType = Recorder.MicType.Unity;
      this.Name = name;
      this.PhotonId = -1;
    }

    public override string ToString() => string.Format("Mic reference: {0}", (object) this.Name);
  }
}
