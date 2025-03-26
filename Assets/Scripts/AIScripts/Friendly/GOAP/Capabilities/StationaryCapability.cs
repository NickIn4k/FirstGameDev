using AIScripts.Friendly.GOAP.Actions;
using AIScripts.Friendly.GOAP.Sensors;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;
using NUnit.Framework;
using Unity.VisualScripting;
using Comparison = CrashKonijn.Goap.Core.Comparison;

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
                .SetBaseCost(2)
                .AddEffect<IsStationary>(EffectType.Increase)
                .AddEffect<ShouldInteract>(EffectType.Decrease)
                .AddEffect<ShouldMove>(EffectType.Decrease)
                .AddEffect<HasMoved>(EffectType.Decrease)
                .SetTarget<StationaryTarget>()
                .SetStoppingDistance(1);

            builder.AddTargetSensor<StationarySensor>()
                .SetTarget<StationaryTarget>();

            //builder.AddMultiSensor<ResetSensor>();
            
            return builder.Build();
        }
    }
}