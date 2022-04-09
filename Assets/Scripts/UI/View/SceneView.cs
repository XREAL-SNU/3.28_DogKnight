using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XReal.XTown.UI
{
    public class SceneView : ObjectBase
    {
        public override void Init()
        {
            UIManager.UI.PrepareCanvas(gameObject, false);
        }
    }
}