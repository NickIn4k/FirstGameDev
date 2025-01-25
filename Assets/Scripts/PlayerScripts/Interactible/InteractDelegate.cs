using UnityEngine;
using System;

[CreateAssetMenu(fileName = "InteractDelegate", menuName = "Scriptable Objects/InteractDelegate")]

public class InteractArgs : EventArgs
{
    public Transform HitTransform { get; set; } // non obbligatoriamente
}
