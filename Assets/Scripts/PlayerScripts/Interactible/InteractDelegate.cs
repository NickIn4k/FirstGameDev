using UnityEngine;
using System;
public class InteractArgs : EventArgs
{
    public Transform HitTransform { get; set; } // non obbligatoriamente
}
