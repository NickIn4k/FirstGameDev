using AIScripts.Friendly.GOAP.Actions;
using AIScripts.Friendly.GOAP.Sensors;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;

namespace AIScripts.Friendly.GOAP.Capabilities
{
    public class StationaryCapability : CapabilityFactoryBase
    {
        public override ICapabilityConfig Create()
        {
            var builder = new CapabilityBuilder("MoveToCapability");
            
            builder.AddGoal<StationaryGoal>()
                .AddCondition<IsStationary>(Comparison.GreaterThanOrEqual, 1)
                .SetBaseCost(2);

            builder.AddAction<StationaryAction>()
                .SetBaseCost(1)
                .AddEffect<IsStationary>(EffectType.Increase)
                .SetTarget<StationaryTarget>()
                .SetStoppingDistance(1);

            builder.AddTargetSensor<StationarySensor>()
                .SetTarget<StationaryTarget>();
            
            return builder.Build();
        }
    }
}