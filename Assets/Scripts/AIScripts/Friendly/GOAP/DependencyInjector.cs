using System;
using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace AIScripts.Friendly.GOAP.Behaviours
{
    public class DependencyInjector : GoapConfigInitializerBase, IGoapInjector
    {
        ScriptableObject MoveToSO;
        public Vector3 moveToPosition;
        
        public override void InitConfig(IGoapConfig config)
        {
            config.GoapInjector = this;
        }

        public void Inject(IAction action)
        {
            if (action is IInjectable injectable)
                injectable.Inject(this);
        }

        public void Inject(IGoal goal)
        {
            if (goal is IInjectable injectable)
                injectable.Inject(this);
        }

        public void Inject(ISensor sensor)
        {
            if (sensor is IInjectable injectable)
                injectable.Inject(this);
        }
    }
}