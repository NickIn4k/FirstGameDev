using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace AIScripts.Friendly.GOAP.Actions
{
    public class StayNearCcAction : GoapActionBase<StayNearCcData>
    {
        public override void Start(IMonoAgent agent, StayNearCcData data)
        {
            data.Colliders = new Collider[1];
        }
        
        public override IActionRunState Perform(IMonoAgent agent, StayNearCcData data, IActionContext context)
        {
            
            //if (Physics.OverlapSphereNonAlloc(agent.transform.position, 4f, data.Colliders, GeneralVariables.CHARACTER) <= 0)
                return ActionRunState.Completed;           
            
            //return ActionRunState.Continue;
        }

        public override void End(IMonoAgent agent, StayNearCcData data)
        {
        }
    }
}