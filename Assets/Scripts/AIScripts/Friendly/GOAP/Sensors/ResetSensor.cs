using AIScripts.Friendly.GOAP.Actions;
using CrashKonijn.Goap.Runtime;

namespace AIScripts.Friendly.GOAP.Sensors
{
    public class ResetSensor : MultiSensorBase
    {
        public ResetSensor()
        {
            this.AddLocalWorldSensor<HasMoved>((agent, references) =>
            {
                // Get a cached reference to the DataBehaviour on the agent
                var data = references.GetCachedComponent<MoveToInteractData>();
                data.HasMoved = false;
                
                return data.HasMoved;
            });
            
            this.AddLocalWorldSensor<ShouldInteract>((agent, references) =>
            {
                // Get a cached reference to the DataBehaviour on the agent
                var data = references.GetCachedComponent<MoveToInteractData>();
                data.ShouldInteract = false;

                return data.ShouldInteract;
            });
            
            this.AddLocalWorldSensor<ShouldMove>((agent, references) =>
            {
                // Get a cached reference to the DataBehaviour on the agent
                var data = references.GetCachedComponent<MoveToData>();
                data.ShouldMove = false;

                return data.ShouldMove;
            });
        }
        
        public override void Created()
        {
        }

        public override void Update()
        {
        }
    }
}