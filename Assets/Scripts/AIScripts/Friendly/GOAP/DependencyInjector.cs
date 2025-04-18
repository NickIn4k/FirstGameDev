﻿using System;
using AIScripts.Friendly.GOAP.Data;
using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;
using Global_Variables;
using UnityEngine;

namespace AIScripts.Friendly.GOAP.Behaviours
{
    public class DependencyInjector : GoapConfigInitializerBase, IGoapInjector
    {
        //public ScriptableObject MoveToSO;
        public CCData CCData;
        public MapData MapData;
        public Vector3 moveToPosition;
        public Collider interactCollider;
        
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