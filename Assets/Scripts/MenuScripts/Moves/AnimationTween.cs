using System.Collections;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AnimationTween : MonoBehaviour
{
    public float 
        appearX,
        appearY,
        appearYFirst,
        hideY,
        animationTime;

    private bool isOn = false;
    private SelectCharacter selector;
    private int animating = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<RectTransform>().localPosition = new Vector3(appearX, hideY, 0);
        selector = GeneralMethods.GetCC().GetComponent<SelectCharacter>();
        selector.OnSelect += ChangeState;
        
        foreach (var obj in GetComponentsInChildren<Image>())
        {
            obj.color = new Color(obj.color.r, obj.color.g, obj.color.b, 0f);
        }

        foreach (var obj in GetComponentsInChildren<TextMeshProUGUI>())
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
        selector.OnSelect -= ChangeState;
    }

    void ChangeState(int id)
    {
        //Debug.Log(id);
        if (id == 1)
        {
            Appear(appearYFirst);
        }
        else if (id != 0)
        {
            Appear(appearY);
        }
        else
        {
            Disappear();
        }
    }

    void Appear(float finalY)
    {
        isOn = true;
        transform.LeanMoveLocal(new Vector2(appearX, finalY), animationTime);
        foreach (var obj in GetComponentsInChildren<Image>())
        {
            animating++;
            obj.DOFade(1f, animationTime).OnComplete(() =>
            {
                animating--;
                if (animating == 0 && isOn)
                    foreach (var txt in GetComponentsInChildren<TextMeshProUGUI>())
                    {
                        txt.DOFade(1f, animationTime);
                    }
            });
        }
    }
    
    void Disappear()
    {
        isOn = false;
        transform.LeanMoveLocal(new Vector2(appearX, hideY), animationTime);
        foreach (var obj in GetComponentsInChildren<Image>())
            obj.DOFade(0f, animationTime);
        foreach (var txt in GetComponentsInChildren<TextMeshProUGUI>())
            txt.DOFade(0f, animationTime);
    }
}
