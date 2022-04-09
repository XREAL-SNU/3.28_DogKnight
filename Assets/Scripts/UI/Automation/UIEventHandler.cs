#nullable enable

using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace XReal.XTown.UI
{
    public class UIEventHandler:
        MonoBehaviour,
        IDragHandler,
        IPointerEnterHandler,
        IPointerClickHandler,
        IPointerExitHandler
    {
        public Action<PointerEventData>? OnDragHandler = null;
        public Action<PointerEventData>? OnPointerEnterHandler = null;
        public Action<PointerEventData>? OnPointerClickHandler = null;
        public Action<PointerEventData>? OnPointerExitHandler = null;

        public void OnDrag(PointerEventData eventData)
        {
            OnDragHandler?.Invoke(eventData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnPointerEnterHandler?.Invoke(eventData);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnPointerClickHandler?.Invoke(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnPointerExitHandler?.Invoke(eventData);
        }
    }
}