using System;
using AIScripts.Friendly.GOAP.Behaviours;
using AIScripts.Friendly.GOAP.Data;
using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Runtime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace AIScripts.Friendly.GOAP.Sensors
{
    public class StayNearCcSensor : LocalTargetSensorBase, IInjectable
    {
        CCData _ccData;
        Collider[] _collider;
        
        public override void Created()
        {
            _collider = new Collider[10];
        }

        public override void Update()
        {
            
        }

        public override ITarget Sense(IActionReceiver agent, IComponentReference references, ITarget existingTarget)
        {
            if (existingTarget == null || Physics.OverlapSphereNonAlloc(existingTarget.Position, 4f, _collider, GeneralVariables.PLAYER) < 1)
            {
                return new PositionTarget(GetRandomPlayerPosition()); // Player moved out of range
            }
            
            return existingTarget;
        }   

        public Vector3 GetRandomPlayerPosition()
        {
            Vector2 random = UnityEngine.Random.insideUnitCircle * 4;
            Vector3 pos = GeneralMethods.GetPlayer().transform.position + new Vector3(
                random.x,
                0,
                random.y
            );
            if (NavMesh.SamplePosition(pos, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
                return hit.position;
            return GetRandomPlayerPosition();
        }

        public void Inject(DependencyInjector injector)
        {
            _ccData = injector.CCData;
        }
    }
}