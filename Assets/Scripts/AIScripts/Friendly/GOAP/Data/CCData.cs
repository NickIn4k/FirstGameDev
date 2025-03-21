using UnityEngine;

namespace AIScripts.Friendly.GOAP.Data
{
    [CreateAssetMenu(menuName = "AIScripts/Friendly/GOAP/CC Data", fileName = "CC Data", order = 1)]
    public class CCData : ScriptableObject
    {
        // Targets near player
        public float playerInRange = 4f;
    }
}