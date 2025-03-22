using System.Security.Cryptography;
using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace AIScripts.Friendly.GOAP.Actions
{
    public class StayNearCcAction : GoapActionBase<StayNearCcData>
    {
        ITarget OldTarget;
        
        public override void Start(IMonoAgent agent, StayNearCcData data)
        {
        }
        
        public override IActionRunState Perform(IMonoAgent agent, StayNearCcData data, IActionContext context)
        {
            /*if (OldTarget == null || data.Target != OldTarget) // New Target Found
            {
                OldTarget = data.Target;
                return ActionRunState.Completed; // Complete Action
            }*/

            if (agent.transform.position == data.Target.Position)
            {
                return ActionRunState.Completed;
            }
            
            return ActionRunState.Stop; // Evaluate
        }

        public override void End(IMonoAgent agent, StayNearCcData data)
        {
        }
    }
}