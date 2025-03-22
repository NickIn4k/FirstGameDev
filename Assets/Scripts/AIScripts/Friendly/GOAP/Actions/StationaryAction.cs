﻿using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace AIScripts.Friendly.GOAP.Actions
{
    public class StationaryAction : GoapActionBase<CommonData>
    {
        public override void Start(IMonoAgent agent, CommonData data)
        {
        }
        
        public override IActionRunState Perform(IMonoAgent agent, CommonData data, IActionContext context)
        {
            return ActionRunState.Completed;
        }

        public override void End(IMonoAgent agent, CommonData data)
        {
        }
    }
}