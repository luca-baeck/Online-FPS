// Decompiled with JetBrains decompiler
// Type: UB.Simple2dWeatherEffects.Standard.EffectBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CC46971B-8B2A-4ACE-8A98-06E5F2B589E2
// Assembly location: C:\Luca\Unity\CryOfWar_Win\CryOfWar_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace UB.Simple2dWeatherEffects.Standard
{
  public class EffectBase : MonoBehaviour
  {
    public static Dictionary<string, RenderTexture> AlreadyRendered = new Dictionary<string, RenderTexture>();
    private static bool _insiderendering = false;

    public static bool InsideRendering
    {
      get => EffectBase._insiderendering;
      set => EffectBase._insiderendering = value;
    }
  }
}
