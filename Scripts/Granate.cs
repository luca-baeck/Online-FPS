// Decompiled with JetBrains decompiler
// Type: Granate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using Photon.Pun;
using System.IO;
using UnityEngine;

public class Granate : MonoBehaviour
{
  public float radius = 5f;
  public float delay = 3f;
  private bool hasExploded;
  private float countDown;
  private GameObject explosion;
  public float damage = 100f;
  public float explosionForce = 700f;
  private PhotonView PV;

  private void Start()
  {
    this.countDown = this.delay;
    this.PV = this.GetComponent<PhotonView>();
  }

  private void Update()
  {
    this.countDown -= Time.deltaTime;
    if ((double) this.countDown > 0.0 || this.hasExploded)
      return;
    this.Explode();
    this.hasExploded = true;
  }

  private void Explode()
  {
    if (!this.PV.IsMine)
      return;
    this.explosion = PhotonNetwork.Instantiate(Path.Combine("Photon", "BigExplosionEffect"), this.transform.position, this.transform.rotation);
    foreach (Collider collider in Physics.OverlapSphere(this.transform.position, this.radius))
    {
      collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(this.damage);
      Rigidbody component = collider.GetComponent<Rigidbody>();
      if ((Object) component != (Object) null)
        component.AddExplosionForce(this.explosionForce, this.transform.position, this.radius);
    }
    PhotonNetwork.Destroy(this.gameObject);
  }
}
