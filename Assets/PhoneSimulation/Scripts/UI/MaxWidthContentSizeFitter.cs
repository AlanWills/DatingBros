using Celeste.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace DatingBros.PhoneSimulation.UI
{
    [ExecuteAlways]
    public class MaxWidthContentSizeFitter : ContentSizeFitter
    {
        [SerializeField] private float maxWidth = 100;
        [SerializeField] private float maxHeight = 100;
        [SerializeField] private RectTransform rectTransform;

        protected override void OnValidate()
        {
            base.OnValidate();

            this.TryGet(ref rectTransform);
        }

        public override void SetLayoutHorizontal()
        {
            base.SetLayoutHorizontal();

            if (rectTransform.sizeDelta.x > maxWidth)
            {
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxWidth);
            }
        }

        /*public override void SetLayoutVertical()
        {
            if (rectTransform.sizeDelta.y > maxHeight)
            {
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, maxHeight);
            }

            //base.SetLayoutVertical();
        }*/
    }
}