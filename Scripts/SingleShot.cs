// Decompiled with JetBrains decompiler
// Type: SingleShot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using Photon.Pun;
using UnityEngine;

public class SingleShot : Gun
{
  [SerializeField]
  private Camera cam;
  public static string WeaponName;
  public static float leftBullets;
  public static float maxBullets;
  public GameObject damageText;
  public float timeD;
  public bool reloading;
  public Reload reload;
  public GameObject MuzzleFlashPartikleSystem;
  public GameObject Hitmaker;
  public GameObject MyPlayer;
  private bool aiming;
  private PhotonView PV;
  public GameObject itemHolder;
  private Vector3 direction;
  public GameObject muzzleFlash;
  public GameObject attackPoint;
  public GameObject Projektil;
  public GameObject FadenkreuzUI;
  private Animator animItem;
  private bool person;

  private void Awake()
  {
    this.PV = this.GetComponent<PhotonView>();
    this.reload = GameObject.Find("Reload").GetComponent<Reload>();
    this.reload.Reset();
    ((GunInfo) this.itemInfo).bulletsLeft = ((GunInfo) this.itemInfo).magazineSize;
    ((GunInfo) this.itemInfo).readyToShoot = true;
    this.itemHolder = this.transform.parent.gameObject;
    this.animItem = this.itemHolder.GetComponent<Animator>();
    this.FadenkreuzUI = GameObject.Find("Fadenkreuz");
  }

  public override void Use()
  {
    MonoBehaviour.print((object) this.itemInfo.itemName);
    SingleShot.WeaponName = this.itemInfo.itemName;
    SingleShot.leftBullets = (float) ((GunInfo) this.itemInfo).bulletsLeft;
    SingleShot.maxBullets = (float) ((GunInfo) this.itemInfo).magazineSize;
    this.animItem.SetBool("Aiming", this.aiming);
    if (((GunInfo) this.itemInfo).allowButtonHold)
      ((GunInfo) this.itemInfo).shooting = Input.GetKey(GetBinds.Schießen);
    else
      ((GunInfo) this.itemInfo).shooting = Input.GetKeyDown(GetBinds.Schießen);
    if (Input.GetKeyDown(GetBinds.Nachladen) && ((GunInfo) this.itemInfo).bulletsLeft < ((GunInfo) this.itemInfo).magazineSize && !this.reloading)
      this.Reload1();
    if (((GunInfo) this.itemInfo).readyToShoot && ((GunInfo) this.itemInfo).shooting && !this.reloading && ((GunInfo) this.itemInfo).bulletsLeft > 0)
    {
      ((GunInfo) this.itemInfo).bulletsShot = ((GunInfo) this.itemInfo).BulletsPerTab;
      this.Shoot();
    }
    if (Input.GetKey(GetBinds.Zielen))
    {
      MonoBehaviour.print((object) "aiming");
      this.FadenkreuzUI.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
      this.aiming = true;
    }
    else
    {
      this.FadenkreuzUI.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
      this.aiming = false;
    }
  }

  private void Reload1()
  {
    this.reload.ReloadAnim(((GunInfo) this.itemInfo).reloadTime);
    this.reloading = true;
    this.Invoke("ReloadFinished", ((GunInfo) this.itemInfo).reloadTime);
  }

  private void ReloadFinished()
  {
    ((GunInfo) this.itemInfo).bulletsLeft = ((GunInfo) this.itemInfo).magazineSize;
    this.reloading = false;
  }

  private void Shoot()
  {
    ((GunInfo) this.itemInfo).readyToShoot = false;
    float spread = ((GunInfo) this.itemInfo).spread;
    if (!this.aiming)
      spread *= 1.75f;
    if (Input.GetKey(KeyCode.LeftShift))
      spread *= 1.75f;
    bool flag = false;
    Vector3 direction = this.cam.transform.forward + new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), Random.Range(-spread, spread));
    RaycastHit hitInfo;
    if (Physics.Raycast(this.cam.transform.position, direction, out hitInfo, ((GunInfo) this.itemInfo).range))
    {
      MonoBehaviour.print((object) "hit");
      if (hitInfo.collider.gameObject.GetComponent<IDamageable>() != null && (Object) hitInfo.collider.gameObject != (Object) this.MyPlayer)
      {
        this.person = true;
        GameObject gameObject1 = Object.Instantiate<GameObject>(this.Hitmaker, this.FadenkreuzUI.transform);
        GameObject gameObject2 = Object.Instantiate<GameObject>(this.damageText, this.FadenkreuzUI.transform);
        hitInfo.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(((GunInfo) this.itemInfo).damage);
        Object.Destroy((Object) gameObject1, 0.125f);
        gameObject2.GetComponent<DamageText>().Setup(((GunInfo) this.itemInfo).damage, this.timeD);
      }
      flag = true;
    }
    this.PV.RPC("RPC_Shoot", RpcTarget.All, (object) hitInfo.point, (object) hitInfo.normal, (object) flag, (object) ((GunInfo) this.itemInfo).range, (object) direction, (object) this.cam.transform.position, (object) this.cam.transform.rotation, (object) this.person);
    this.person = false;
    --((GunInfo) this.itemInfo).bulletsLeft;
    --((GunInfo) this.itemInfo).bulletsShot;
    this.Invoke("ResetShot", ((GunInfo) this.itemInfo).timeBetweenShooting);
    if (((GunInfo) this.itemInfo).bulletsShot <= 0 || ((GunInfo) this.itemInfo).bulletsLeft <= 0)
      return;
    this.Invoke(nameof (Shoot), ((GunInfo) this.itemInfo).timeBetweenShots);
  }

  private void ResetShot() => ((GunInfo) this.itemInfo).readyToShoot = true;

  [PunRPC]
  private void RPC_Shoot(
    Vector3 hitPosition,
    Vector3 hitNormal,
    bool hit,
    float range,
    Vector3 direction,
    Vector3 Start,
    Quaternion rotation,
    bool person1)
  {
    if (hit)
    {
      Collider[] colliderArray = Physics.OverlapSphere(hitPosition, 0.3f);
      if (colliderArray.Length != 0)
      {
        if (person1)
        {
          Object.Destroy((Object) Object.Instantiate<GameObject>(this.bulletImpactSplashPrefab, hitPosition + hitNormal * (1f / 1000f), Quaternion.identity), 3f);
          this.person = false;
        }
        else
          Object.Destroy((Object) Object.Instantiate<GameObject>(this.muzzleFlash, hitPosition + hitNormal * (1f / 1000f), Quaternion.identity), 1f);
        GameObject gameObject = Object.Instantiate<GameObject>(this.bulletImpactPrefab, hitPosition + hitNormal * (1f / 1000f), Quaternion.LookRotation(hitNormal, Vector3.up) * this.bulletImpactPrefab.transform.rotation);
        Object.Destroy((Object) gameObject, 10f);
        gameObject.transform.SetParent(colliderArray[0].transform);
      }
    }
    else
      hitPosition = Start + direction * range;
    GameObject gameObject1 = Object.Instantiate<GameObject>(this.MuzzleFlashPartikleSystem, this.attackPoint.transform.position, rotation);
    gameObject1.transform.SetParent(this.attackPoint.transform);
    Object.Destroy((Object) gameObject1, 2f);
    GameObject gameObject2 = Object.Instantiate<GameObject>(this.Projektil, this.attackPoint.transform.position, rotation);
    gameObject2.GetComponent<ProjektilMove>().Range(range, hitPosition);
    Object.Destroy((Object) gameObject2, 10f);
  }
}
