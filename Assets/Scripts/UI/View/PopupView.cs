using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XReal.XTown.UI
{
    public class PopupView : ObjectBase
    {
        public override void Init()
        {
            UIManager.UI.PrepareCanvas(gameObject, true);
        }

        public virtual void ClosePopup()
        {
            UIManager.UI.ClosePopup(this);
        }
    }
}