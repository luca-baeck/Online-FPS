// Decompiled with JetBrains decompiler
// Type: ParticleCollision
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
  private List<ParticleCollisionEvent> m_CollisionEvents = new List<ParticleCollisionEvent>();
  private ParticleSystem m_ParticleSystem;

  private void Start() => this.m_ParticleSystem = this.GetComponent<ParticleSystem>();

  private void OnParticleCollision(GameObject other)
  {
    int collisionEvents = ParticlePhysicsExtensions.GetCollisionEvents(this.m_ParticleSystem, other, this.m_CollisionEvents);
    for (int index = 0; index < collisionEvents; ++index)
    {
      ExtinguishableFire component = this.m_CollisionEvents[index].colliderComponent.GetComponent<ExtinguishableFire>();
      if ((Object) component != (Object) null)
        component.Extinguish();
    }
  }
}
