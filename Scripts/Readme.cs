// Decompiled with JetBrains decompiler
// Type: Readme
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class Readme : ScriptableObject
{
  public Texture2D icon;
  public float iconMaxWidth = 128f;
  public string title;
  public string titlesub;
  public Readme.Section[] sections;
  public bool loadedLayout;

  [Serializable]
  public class Section
  {
    public string heading;
    public string text;
    public string linkText;
    public string url;
  }
}
