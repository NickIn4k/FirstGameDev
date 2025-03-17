using System;
using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;
using UnityEngine.AI;

namespace AIScripts.Friendly.GOAP.Sensors
{
    public class StayNearCcSensor : LocalTargetSensorBase
    {
        public override void Created()
        {
        }

        public override void Update()
        {
            
        }

        public override ITarget Sense(IActionReceiver agent, IComponentReference references, ITarget existingTarget)
        {
            return new PositionTarget(GetRandomPlayerPosition());
        }

        public Vector3 GetRandomPlayerPosition()
        {
            Vector2 random = UnityEngine.Random.insideUnitCircle * 4;
            Vector3 pos = GeneralMethods.GetPlayer().transform.position  + new Vector3(
                random.x,
                0,
                random.y
            );
            if (NavMesh.SamplePosition(pos, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
                return hit.position;
            return GetRandomPlayerPosition();
        }
    }
}