// Decompiled with JetBrains decompiler
// Type: RoomManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using Photon.Pun;
using Photon.Realtime;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviourPunCallbacks
{
  public static RoomManager Instance;
  public GameObject Map1G;
  public GameObject Map2G;

  private void Awake()
  {
    if ((bool) (Object) RoomManager.Instance)
    {
      Object.Destroy((Object) this.gameObject);
    }
    else
    {
      Object.DontDestroyOnLoad((Object) this.gameObject);
      RoomManager.Instance = this;
    }
  }

  public override void OnEnable()
  {
    base.OnEnable();
    SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>(this.OnSceneLoaded);
  }

  public override void OnDisable()
  {
    base.OnDisable();
    SceneManager.sceneLoaded -= new UnityAction<Scene, LoadSceneMode>(this.OnSceneLoaded);
  }

  private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
  {
    if (scene.buildIndex != 5)
      return;
    Player[] playerList = PhotonNetwork.PlayerList;
    int num1 = 0;
    int num2 = 0;
    foreach (Player player in playerList)
    {
      object obj;
      if (player.CustomProperties.TryGetValue((object) "Map", out obj))
      {
        if (obj.ToString() == "1")
          ++num1;
        if (obj.ToString() == "2")
          ++num2;
      }
    }
    MonoBehaviour.print((object) (num1.ToString() + "Map1" + (object) num2 + "Map2"));
    if (num1 > num2)
      Object.Instantiate<GameObject>(this.Map1G, Vector3.zero, Quaternion.identity);
    else if (num2 > num1)
    {
      Object.Instantiate<GameObject>(this.Map2G, Vector3.zero, Quaternion.identity);
    }
    else
    {
      foreach (Player player in playerList)
      {
        object obj;
        if (player.IsMasterClient && player.CustomProperties.TryGetValue((object) "Map", out obj))
        {
          if (obj.ToString() == "1")
          {
            Object.Instantiate<GameObject>(this.Map1G, Vector3.zero, Quaternion.identity);
            ++num1;
          }
          if (obj.ToString() == "2")
          {
            Object.Instantiate<GameObject>(this.Map2G, Vector3.zero, Quaternion.identity);
            ++num2;
          }
        }
      }
      if (num1 == num2)
        Object.Instantiate<GameObject>(this.Map1G, Vector3.zero, Quaternion.identity);
    }
    this.Invoke("SpawnPlayerManager", 0.01f);
  }

  private void SpawnPlayerManager() => PhotonNetwork.Instantiate(Path.Combine("Photon", "PlayerManager"), Vector3.zero, Quaternion.identity);
}
