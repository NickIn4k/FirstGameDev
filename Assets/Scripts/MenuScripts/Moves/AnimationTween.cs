using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AnimationTween : MonoBehaviour
{
    public float 
        appearX,
        appearY,
        hideY,
        animationTime;
    
    private SelectCharacter selector;
    private int animating = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<RectTransform>().localPosition = new Vector3(appearX, hideY, 0);
        selector = GeneralMethods.GetCC().GetComponent<SelectCharacter>();
        selector.OnSelect += Appear;
        
        foreach (var obj in GetComponentsInChildren<Image>())
        {
            obj.color = new Color(obj.color.r, obj.color.g, obj.color.b, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        selector.OnSelect -= Appear;
    }

    void Appear(int id)
    {
        transform.LeanMoveLocal(new Vector2(appearX, appearY), animationTime);
        foreach (var obj in GetComponentsInChildren<Image>())
        {
            animating++;
            obj.DOFade(1f, animationTime).OnComplete(() =>
            {
                animating--;
                if (animating == 0)
                    Debug.Log("No animations playing!");
            });
        }
    }
    
    void Disappear(int id)
    {
        transform.LeanMoveLocal(new Vector2(appearX, hideY), animationTime);
    }
}
