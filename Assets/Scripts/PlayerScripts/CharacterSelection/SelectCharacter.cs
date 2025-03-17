using System;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using UnityEngine.UI;
using System.Threading;
using System.Collections;
using UnityEngine.PlayerLoop; // DOTween namespace

public class SelectCharacter : MonoBehaviour
{
    int selected;
    
    public event Action<int> OnSelect;
    SelectMove MoveScript;
    
    bool canSelect = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selected = 0;
        MoveScript = GetComponent<SelectMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveScript.enabled == true)
        {
            canSelect = !MoveScript.CheckIfSelected();
        }
        
        if (!canSelect) 
            return;
        
        GetInput();
        
        if (selected != 0)
            Notify();
    }

    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            selected = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            selected = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            selected = 3;
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            selected = 4;
    }

    void Notify()
    {
        OnSelect?.Invoke(selected);
        MoveScript.enabled = true;
        MoveScript.OnReset += ResetChoice;
        selected = 0;
        
        // Selection cooldown
        StartCoroutine(Cooldown());
    }

    private void ResetChoice()
    {
        MoveScript.OnReset -= ResetChoice;

        canSelect = true;
        OnSelect?.Invoke(0); // Resets all
    }

    IEnumerator Cooldown()
    {
        canSelect = false;
        yield return new WaitForSeconds(.2f);
        if (MoveScript.enabled == true)
        {
            if (!MoveScript.CheckIfSelected())
                canSelect = true;
        }
        else
            canSelect = true;
    }
}
