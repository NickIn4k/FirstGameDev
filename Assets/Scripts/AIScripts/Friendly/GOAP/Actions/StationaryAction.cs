using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace AIScripts.Friendly.GOAP.Actions
{
    public class StationaryAction : GoapActionBase<CommonData>
    {
        public override void Start(IMonoAgent agent, CommonData data)
        {
            data.Timer = UnityEngine.Random.Range(1, 2);
        }
        
        public override IActionRunState Perform(IMonoAgent agent, CommonData data, IActionContext context)
        {
            data.Timer -= context.DeltaTime;
            
            if (data.Timer > 0)
            {
                return ActionRunState.Continue;
            }
            
            return ActionRunState.Completed;
        }

        public override void End(IMonoAgent agent, CommonData data)
        {
        }
    }
}