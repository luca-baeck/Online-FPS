// Decompiled with JetBrains decompiler
// Type: Splash1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class Splash1 : MonoBehaviour
{
  private Rigidbody rb;
  public float spread;
  public float spreadUp;

  private void Start()
  {
    this.rb = this.GetComponent<Rigidbody>();
    this.rb.AddForce(new Vector3(Random.Range(-1f * this.spread, this.spread), Random.Range(-1f * this.spread * this.spreadUp, this.spread * this.spreadUp), Random.Range(-1f * this.spread, this.spread)), ForceMode.Impulse);
  }
}
