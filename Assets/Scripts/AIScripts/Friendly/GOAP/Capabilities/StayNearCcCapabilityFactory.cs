using AIScripts.Friendly.GOAP.Actions;
using AIScripts.Friendly.GOAP.Sensors;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;

namespace AIScripts.Friendly.GOAP.Capabilities
{
    public class StayNearCcCapabilityFactory : CapabilityFactoryBase
    {
        public override ICapabilityConfig Create()
        {
            var builder = new CapabilityBuilder("MoveToCapability");
            
            builder.AddGoal<StayNearCcGoal>()
                .AddCondition<StayNearCc>(Comparison.GreaterThanOrEqual, 1)
                .SetBaseCost(2);

            builder.AddAction<StayNearCcAction>()
                .SetBaseCost(1)
                .AddEffect<StayNearCc>(EffectType.Increase)
                .SetTarget<StayNearCcTarget>()
                .SetStoppingDistance(1);

            builder.AddTargetSensor<StayNearCcSensor>()
                .SetTarget<StayNearCcTarget>();
            
            return builder.Build();
        }
    }
}