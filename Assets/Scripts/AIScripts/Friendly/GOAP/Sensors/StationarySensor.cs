using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Runtime;

namespace AIScripts.Friendly.GOAP.Sensors
{
    public class StationarySensor : LocalTargetSensorBase
    {
        public override void Created()
        {
        }

        public override void Update()
        {
        }

        public override ITarget Sense(IActionReceiver agent, IComponentReference references, ITarget existingTarget)
        {
            return new PositionTarget(agent.Transform.position);
        }
    }
}