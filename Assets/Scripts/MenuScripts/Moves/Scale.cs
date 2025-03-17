using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterSelector
{
    public class Scale : MonoBehaviour
    {
        public float targetScaleFactor = 1.5f;  // The scale factor we want to reach
        public float duration = .3f;             // Duration of the tween

        float initialWidth, initialHeight;
        float targetWidth, targetHeight;
        
        private SelectCharacter selector;
        private bool active = false;
        public int id;
        
        void Start()
        {
            selector = GeneralMethods.GetCC().GetComponent<SelectCharacter>();
            selector.OnSelect += CheckAndScale;
            
            initialHeight = GetComponentsInChildren<RectTransform>()[1].rect.height;
            initialWidth = GetComponentsInChildren<RectTransform>()[1].rect.width;
            targetWidth = initialWidth * targetScaleFactor;
            targetHeight = initialHeight * targetScaleFactor;
        }

        void OnDestroy()
        {
            selector.OnSelect -= CheckAndScale;
        }

        private void CheckAndScale(int id)
        {
            var rect1 = GetComponentsInChildren<RectTransform>()[1];
            var rect2 = GetComponentsInChildren<RectTransform>()[2];

            if (id == this.id && !active)
            {
                active = true;
                
                // Tween both width and height proportionally
                rect1.DOSizeDelta(new Vector2(targetHeight, targetWidth), duration, false).SetEase(Ease.InOutQuad);
                rect2.DOSizeDelta(new Vector2(targetHeight, targetWidth), duration, false).SetEase(Ease.InOutQuad);

                // Phase alpha
                GetComponentsInChildren<Image>()[0].CrossFadeAlpha(1f, duration, false);
                GetComponentsInChildren<Image>()[1].CrossFadeAlpha(0f, duration, false);
            }
            else if (id != this.id && active)
            {
                active = false;
                
                rect1.DOSizeDelta(new Vector2(initialHeight, initialWidth), duration, false).SetEase(Ease.InOutQuad);
                rect2.DOSizeDelta(new Vector2(initialHeight, initialWidth), duration, false).SetEase(Ease.InOutQuad);

                GetComponentsInChildren<Image>()[0].CrossFadeAlpha(0f, duration, false);
                GetComponentsInChildren<Image>()[1].CrossFadeAlpha(1f, duration, false);
            }
        }
    }
}