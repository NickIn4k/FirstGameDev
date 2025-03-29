using AIScripts.Friendly.GOAP;
using AIScripts.Friendly.GOAP.Actions;
using AIScripts.Friendly.GOAP.Behaviours;
using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace AIScripts.Friendly.GOAP.Sensors
{
    public class InteractSensor : LocalTargetSensorBase, IInjectable
    {
        private DependencyInjector injector;
        
        public override void Created()
        {
        }

        public override void Update()
        {
        }

        public override ITarget Sense(IActionReceiver agent, IComponentReference references, ITarget existingTarget)
        {
            return new PositionTarget(GetInteractPosition());
        }

        private Vector3 GetInteractPosition()
        {
            // Get transform's front
            Vector3 front = injector.interactCollider.transform.position + injector.interactCollider.transform.up * 2f;
            front.y = injector.MapData.ground.y;
            Debug.Log("Front " + front);
            return front;
        }

        public void Inject(DependencyInjector injector)
        {
            this.injector = injector;
        }
    }
}