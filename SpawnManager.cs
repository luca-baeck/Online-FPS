// Decompiled with JetBrains decompiler
// Type: SpawnManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SpawnManager : MonoBehaviour
{
  public static SpawnManager Instance;
  private Spawnpoint[] spawnpoints;

  private void Awake()
  {
    SpawnManager.Instance = this;
    this.spawnpoints = this.GetComponentsInChildren<Spawnpoint>();
  }

  public Transform GetSpawnpoint() => this.spawnpoints[Random.Range(0, this.spawnpoints.Length)].transform;
}
