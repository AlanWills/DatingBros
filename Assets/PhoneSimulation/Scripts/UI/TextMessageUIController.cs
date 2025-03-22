using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DatingBros.PhoneSimulation.UI
{
    public class TextMessageUIController : MonoBehaviour
    {
        [Serializable]
        public struct VisualSettings
        {
            public float xPivot;
            public Color backgroundColour;
        }
        
        #region Properties and Fields

        [SerializeField] private RectTransform root;
        [SerializeField] private Image background;
        [SerializeField] private TextMeshProUGUI text;

        #endregion

        public void Hookup(string message, VisualSettings visualSettings)
        {
            text.text = message;
            root.pivot = new  Vector2(visualSettings.xPivot, root.pivot.y); 
            background.color = visualSettings.backgroundColour;
        }
    }
}