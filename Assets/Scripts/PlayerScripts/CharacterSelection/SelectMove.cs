using System;
using AIScripts.Friendly.GOAP;
using JetBrains.Annotations;
using Settings.CharacterSelection.Moves;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectMove : MonoBehaviour
{
    private Inputs inputs;
    private InputAction move1;
    private InputAction move2;
    private InputAction move3;

    private bool hasSelected = false;

    public event Action OnReset;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnEnable()
    {
        
        inputs = new Inputs();
        
        // --- Moves ---
        move1 = inputs.Gameplay.FirstMove;
        move1.Enable();
        move1.performed += EnableMove1;
        
        move2 = inputs.Gameplay.SecondMove;
        move2.Enable();
        move2.performed += EnableMove2;
        
        move3 = inputs.Gameplay.ThirdMove;
        move3.Enable();
        move3.performed += EnableMove3;
    }
    
    void DisableAllMoves()
    {
        hasSelected = true;
        
        move1.performed -= EnableMove1;
        move1.Disable();
        
        move2.performed -= EnableMove2;
        move2.Disable();
        
        move3.performed -= EnableMove3;
        move3.Disable();
    }

    void OnDisable()
    {
        GetComponent<Move1>().OnUpdate -= Reset;
        GetComponent<Move2>().OnUpdate -= Reset;
        GetComponent<Move3>().OnUpdate -= Reset;
    }

    private void Setup()
    {
        DisableAllMoves();
    }
    
    private void EnableMove1(InputAction.CallbackContext obj)
    {
        Setup();
        
        GetComponent<Move1>().OnUpdate += Reset;
        GetComponent<Move1>().enabled = true;
    }
    
    private void EnableMove2(InputAction.CallbackContext obj)
    {
        Setup();
        
        GetComponent<Move2>().OnUpdate += Reset;
        GetComponent<Move2>().enabled = true;
    }
    
    private void EnableMove3(InputAction.CallbackContext obj)
    {
        Setup();
        
        GetComponent<Move3>().OnUpdate += Reset;
        GetComponent<Move3>().enabled = true;
    }

    private void Reset()
    {
        // Return to pre choose state
        hasSelected = false;
        OnReset?.Invoke();
        this.enabled = false;
    }
    
    private void Reset(bool b)
    {
        // Return to pre choose state
        hasSelected = false;
        OnReset?.Invoke();
        this.enabled = false;
    }

    public bool CheckIfSelected()
    {
        return hasSelected;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
