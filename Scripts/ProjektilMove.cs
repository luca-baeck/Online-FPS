// Decompiled with JetBrains decompiler
// Type: ProjektilMove
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class ProjektilMove : MonoBehaviour
{
  public float speed;
  private float rangeB;
  private float Distance;
  private Vector3 StartPos;
  private Vector3 Target;

  private void Start() => this.StartPos = this.transform.position;

  private void Update()
  {
    Vector3 vector3 = this.Target - this.transform.position;
    MonoBehaviour.print((object) vector3);
    MonoBehaviour.print((object) Vector3.Distance(this.Target, this.transform.position));
    if (0.5 <= (double) Vector3.Distance(this.Target, this.transform.position))
      this.transform.rotation = Quaternion.LookRotation(vector3, Vector3.up);
    else
      Object.Destroy((Object) this.gameObject);
    this.transform.position += this.transform.forward * (this.speed * Time.deltaTime);
    this.Distance = Vector3.Distance(this.StartPos, this.transform.position);
    if ((double) this.Distance < (double) this.rangeB)
      return;
    Object.Destroy((Object) this.gameObject);
    MonoBehaviour.print((object) "RangeDes");
  }

  public void Range(float range, Vector3 target)
  {
    this.rangeB = range;
    this.Target = target;
  }

  private void OnTriggerEnter(Collider col)
  {
    if (col.CompareTag("Player") || col.CompareTag("Projektil"))
      return;
    MonoBehaviour.print((object) col.gameObject.name);
    Object.Destroy((Object) this.gameObject);
    MonoBehaviour.print((object) "TimeDes");
  }
}
