// Decompiled with JetBrains decompiler
// Type: Gun
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public abstract class Gun : Item
{
  public GameObject bulletImpactPrefab;
  public GameObject bulletImpactSplashPrefab;

  public abstract override void Use();
}
