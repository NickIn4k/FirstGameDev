using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Runtime;
using Unity.Mathematics;
using UnityEngine;

namespace AIScripts.Friendly.GOAP.Actions
{
    public class MoveToAction : GoapActionBase<MoveToData>
    {
        public override void Start(IMonoAgent agent, MoveToData data)
        {
            data.Tolerance = 0.4f;
        }
        
        public override IActionRunState Perform(IMonoAgent agent, MoveToData data, IActionContext context)
        {
            if (Vector3.Distance(agent.transform.position, data.Target.Position) > data.Tolerance)
            {
                return ActionRunState.Continue;
            }
            
            return ActionRunState.Completed;
        }

        public override void End(IMonoAgent agent, MoveToData data)
        {
        }
    }
}