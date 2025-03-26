using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace AIScripts.Friendly.GOAP.Actions
{
    public class InteractAction : GoapActionBase<InteractAction.Data>
    {
        public override void Start(IMonoAgent agent, Data data)
        {
        }
        
        public override IActionRunState Perform(IMonoAgent agent, Data data, IActionContext context)
        {
            return ActionRunState.Completed;
        }

        public override void End(IMonoAgent agent, Data data)
        {
        }
        
        public class Data : IActionData
        {
            public ITarget Target { get; set; }
            
            // When using the GetComponent attribute, the system will automatically inject the reference
            [GetComponent]
            public MoveToInteractData InteractData { get; set; }
        }
    }
}