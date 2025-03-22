using Unity.AI.Navigation;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class DoorOpener : MonoBehaviour
{
    public AudioSource src;
    public GeneralData generalData;
    private AudioClip _sfx;
    private Animator _animator;
    private BoxCollider[] _colliders;
    private NavMeshModifier _navMeshModifier;
    private int _openingAnimation;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _sfx = generalData.doorAudioClip;
        _animator = GetComponent<Animator>();
        _openingAnimation = Animator.StringToHash("isOpening");
        _colliders = GetComponentsInChildren<BoxCollider>();
        _navMeshModifier = GetComponent<NavMeshModifier>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OpenDoor()
    {
        //Rende il collider della porta un trigger
        src.clip = _sfx;
        src.Play();
        foreach (var boxCollider in _colliders)
            boxCollider.enabled = false;
        _animator.SetBool(_openingAnimation, true);
        _navMeshModifier.enabled = true;
        GeneralMethods.BakeMap();
    }
}
