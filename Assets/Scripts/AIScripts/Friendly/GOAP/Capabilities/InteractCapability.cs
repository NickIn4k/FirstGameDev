using AIScripts.Friendly.GOAP.Actions;
using AIScripts.Friendly.GOAP.Sensors;
using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;
using NUnit.Framework;

namespace AIScripts.Friendly.GOAP.Capabilities
{
    public class InteractCapability : CapabilityFactoryBase
    {
        public override ICapabilityConfig Create()
        {
            var builder = new CapabilityBuilder("InteractCapability");
            
            builder.AddGoal<InteractGoal>()
                .AddCondition<ShouldInteract>(Comparison.GreaterThanOrEqual, 1)
                .SetBaseCost(2);

            builder.AddAction<InteractAction>()
                .AddEffect<ShouldInteract>(EffectType.Increase)
                .SetTarget<InteractTarget>()
                .SetRequiresTarget(false)
                .SetStoppingDistance(1);

            builder.AddTargetSensor<InteractSensor>().SetTarget<InteractTarget>();
            
            return builder.Build();
        }
    }
}