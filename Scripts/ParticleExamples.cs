// Decompiled with JetBrains decompiler
// Type: ParticleExamples
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

[Serializable]
public class ParticleExamples
{
  public string title;
  [TextArea]
  public string description;
  public bool isWeaponEffect;
  public GameObject particleSystemGO;
  public Vector3 particlePosition;
  public Vector3 particleRotation;
}
