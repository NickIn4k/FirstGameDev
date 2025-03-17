using System;
using UnityEngine;

namespace Settings.CharacterSelection.Moves
{
    public class Move2 : MonoBehaviour
    {
        public event Action OnUpdate;

        void OnEnable()
        {
            // Ehhh solo questo a dire il vero
            OnUpdate?.Invoke(); // Invoke
            this.enabled = false;
        }
    }
}