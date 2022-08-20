// Decompiled with JetBrains decompiler
// Type: GunInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[CreateAssetMenu(menuName = "FPS/NewGun")]
public class GunInfo : ItemInfo
{
  public float damage;
  public float timeBetweenShooting;
  public float spread;
  public float range;
  public float reloadTime;
  public float timeBetweenShots;
  public int magazineSize;
  public int BulletsPerTab;
  public bool allowButtonHold;
  public int bulletsLeft;
  public int bulletsShot;
  public bool shooting;
  public bool readyToShoot;
  public bool reloading;
}
