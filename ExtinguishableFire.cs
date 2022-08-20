// Decompiled with JetBrains decompiler
// Type: ExtinguishableFire
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

public class ExtinguishableFire : MonoBehaviour
{
  public ParticleSystem fireParticleSystem;
  public ParticleSystem smokeParticleSystem;
  protected bool m_isExtinguished;
  private const float m_FireStartingTime = 2f;

  private void Start()
  {
    this.m_isExtinguished = true;
    this.smokeParticleSystem.Stop();
    this.fireParticleSystem.Stop();
    this.StartCoroutine(this.StartingFire());
  }

  public void Extinguish()
  {
    if (this.m_isExtinguished)
      return;
    this.m_isExtinguished = true;
    this.StartCoroutine(this.Extinguishing());
  }

  private IEnumerator Extinguishing()
  {
    ExtinguishableFire extinguishableFire = this;
    extinguishableFire.fireParticleSystem.Stop();
    extinguishableFire.smokeParticleSystem.time = 0.0f;
    extinguishableFire.smokeParticleSystem.Play();
    for (float elapsedTime = 0.0f; (double) elapsedTime < 2.0; elapsedTime += Time.deltaTime)
    {
      float num = Mathf.Max(0.0f, (float) (1.0 - (double) elapsedTime / 2.0));
      extinguishableFire.fireParticleSystem.transform.localScale = Vector3.one * num;
      yield return (object) null;
    }
    yield return (object) new WaitForSeconds(2f);
    extinguishableFire.smokeParticleSystem.Stop();
    extinguishableFire.fireParticleSystem.transform.localScale = Vector3.one;
    yield return (object) new WaitForSeconds(4f);
    extinguishableFire.StartCoroutine(extinguishableFire.StartingFire());
  }

  private IEnumerator StartingFire()
  {
    this.smokeParticleSystem.Stop();
    this.fireParticleSystem.time = 0.0f;
    this.fireParticleSystem.Play();
    for (float elapsedTime = 0.0f; (double) elapsedTime < 2.0; elapsedTime += Time.deltaTime)
    {
      this.fireParticleSystem.transform.localScale = Vector3.one * Mathf.Min(1f, elapsedTime / 2f);
      yield return (object) null;
    }
    this.fireParticleSystem.transform.localScale = Vector3.one;
    this.m_isExtinguished = false;
  }
}
