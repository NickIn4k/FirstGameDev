using AIScripts.Friendly.GOAP.Actions;
using AIScripts.Friendly.GOAP.Sensors;
using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;
using Unity.VisualScripting;
using Comparison = CrashKonijn.Goap.Core.Comparison;

namespace AIScripts.Friendly.GOAP.Capabilities
{
    public class MoveToCapabilityFactory : CapabilityFactoryBase
    {
        public override ICapabilityConfig Create()
        {
            var builder = new CapabilityBuilder("MoveToCapability");
            
            builder.AddGoal<MoveToGoal>()
                .AddCondition<ShouldMove>(Comparison.GreaterThanOrEqual, 1)
                .SetBaseCost(2);

            builder.AddAction<MoveToAction>()
                .SetBaseCost(1)
                .SetMoveMode(ActionMoveMode.MoveBeforePerforming)
                .AddEffect<ShouldMove>(EffectType.Increase) // Should not move after
                .SetTarget<MoveToTarget>()
                .SetStoppingDistance(1);

            builder.AddTargetSensor<MoveToSensor>().SetTarget<MoveToTarget>();
            
            return builder.Build();
        }
    }
}