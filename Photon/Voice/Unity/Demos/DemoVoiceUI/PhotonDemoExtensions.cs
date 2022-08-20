// Decompiled with JetBrains decompiler
// Type: Photon.Voice.Unity.Demos.DemoVoiceUI.PhotonDemoExtensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using Photon.Realtime;

namespace Photon.Voice.Unity.Demos.DemoVoiceUI
{
  public static class PhotonDemoExtensions
  {
    internal const string IS_MUTED_PROPERTY_KEY = "mute";

    public static bool Mute(this Player player)
    {
      Player player1 = player;
      Hashtable propertiesToSet = new Hashtable(1);
      propertiesToSet.Add((object) "mute", (object) true);
      return player1.SetCustomProperties(propertiesToSet);
    }

    public static bool Unmute(this Player player)
    {
      Player player1 = player;
      Hashtable propertiesToSet = new Hashtable(1);
      propertiesToSet.Add((object) "mute", (object) false);
      return player1.SetCustomProperties(propertiesToSet);
    }

    public static bool IsMuted(this Player player)
    {
      object obj;
      return player.CustomProperties.TryGetValue((object) "mute", out obj) && (bool) obj;
    }
  }
}
