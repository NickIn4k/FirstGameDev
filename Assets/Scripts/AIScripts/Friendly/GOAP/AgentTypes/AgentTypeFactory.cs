using AIScripts.Friendly.GOAP.Capabilities;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace AIScripts.Friendly.GOAP.AgentTypes
{
    public class AgentTypeFactory : AgentTypeFactoryBase
    {
        public override IAgentTypeConfig Create()
        {
            var factory = new AgentTypeBuilder("NPCAgent");
            
            factory.AddCapability<MoveToCapabilityFactory>();
            factory.AddCapability<StationaryCapability>();
            factory.AddCapability<StayNearCcCapabilityFactory>();
            factory.AddCapability<InteractCapability>();

            return factory.Build();
        }
    }
}