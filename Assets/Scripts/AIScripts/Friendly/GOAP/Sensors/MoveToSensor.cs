using AIScripts.Friendly.GOAP.Behaviours;
using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Runtime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace AIScripts.Friendly.GOAP.Sensors
{
    public class MoveToSensor : LocalTargetSensorBase, IInjectable
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
            return new PositionTarget(injector.moveToPosition);
        }

        public void Inject(DependencyInjector injector)
        {
            this.injector = injector;
        }
    }
}