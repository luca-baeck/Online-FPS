// Decompiled with JetBrains decompiler
// Type: MusicManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class MusicManager : MonoBehaviour
{
  public AudioSource _intro;
  public AudioSource _Lobby;
  public AudioSource _PreGame;
  public AudioSource _Game;

  private void Awake()
  {
    this._intro.Stop();
    this._Lobby.Stop();
    this._Game.Stop();
    this._PreGame.Stop();
    Object.DontDestroyOnLoad((Object) this.transform.gameObject);
  }

  public void PlayIntro()
  {
    if (this._intro.isPlaying)
      return;
    this._intro.Play();
    this._PreGame.Stop();
    this._Game.Stop();
    this._Lobby.Stop();
  }

  public void PlayLobby()
  {
    if (this._Lobby.isPlaying)
      return;
    this._Lobby.Play();
    this._intro.Stop();
    this._PreGame.Stop();
    this._Game.Stop();
  }

  public void PlayPreGame()
  {
    if (this._PreGame.isPlaying)
      return;
    this._PreGame.Play();
    this._intro.Stop();
    this._Lobby.Stop();
    this._Game.Stop();
  }

  public void PlayGame()
  {
    if (this._Game.isPlaying)
      return;
    this._Game.Play();
    this._intro.Stop();
    this._Lobby.Stop();
    this._PreGame.Stop();
  }
}
