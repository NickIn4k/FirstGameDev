using CrashKonijn.Agent.Runtime;
using CrashKonijn.Goap.Editor;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace AIScripts.Friendly.GOAP.Behaviours
{
    public class GoapSetBinder : MonoBehaviour
    {
        public GoapBehaviour goapBehaviour;

        private void Awake()
        {
            GoapActionProvider provider = GetComponent<GoapActionProvider>();
            provider.AgentType = goapBehaviour.GetAgentType("NPCAgent");
        }
    }
}