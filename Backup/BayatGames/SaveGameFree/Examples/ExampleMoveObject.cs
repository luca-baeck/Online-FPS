// Decompiled with JetBrains decompiler
// Type: BayatGames.SaveGameFree.Examples.ExampleMoveObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace BayatGames.SaveGameFree.Examples
{
  public class ExampleMoveObject : MonoBehaviour
  {
    private void Update()
    {
      Vector3 position = this.transform.position;
      position.x += Input.GetAxis("Horizontal");
      position.y += Input.GetAxis("Vertical");
      this.transform.position = position;
    }
  }
}
