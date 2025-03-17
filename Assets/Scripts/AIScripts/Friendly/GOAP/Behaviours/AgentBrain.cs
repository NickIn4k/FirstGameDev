using AIScripts.Friendly.GOAP.Actions;
using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Goap.Runtime;
using Settings.CharacterSelection.Moves;
using UnityEngine;

namespace AIScripts.Friendly.GOAP.Behaviours
{
    public class AgentBrain : MonoBehaviour
    {
        private AgentBehaviour agent;
        private GoapActionProvider provider;
        private GoapBehaviour goap;
        private SelectCharacter sc;
        
        private Move1 click;
        private Move2 backToCc;
        
        public int id;
        private bool canPerform;
        
        private void Awake()
        {
            this.goap = FindFirstObjectByType<GoapBehaviour>();
            this.agent = this.GetComponent<AgentBehaviour>();
            this.provider = this.GetComponent<GoapActionProvider>();
            this.sc = GeneralMethods.GetCC().GetComponent<SelectCharacter>();
            
            this.click = GeneralMethods.GetCC().GetComponent<Move1>();
            this.backToCc = GeneralMethods.GetCC().GetComponent<Move2>();
            
            // This only applies sto the code demo
            if (this.provider.AgentTypeBehaviour == null)
                this.provider.AgentType = this.goap.GetAgentType("NPCAgent");
        }

        private void Start()
        {
            provider.RequestGoal<StationaryGoal>();
        }

        private void ActionCompleteHandler(IAction action)
        {
            if (!(action is StationaryAction) && !(action is StayNearCcAction))
                provider.RequestGoal<StationaryGoal>();
        }

        private void OnEnable()
        {
            sc.OnSelect += UpdateCanPerform;
            click.OnUpdate += FollowTarget;
            backToCc.OnUpdate += GetBackToCc;
            agent.Events.OnActionComplete += ActionCompleteHandler;
        }

        private void OnDisable()
        {
            sc.OnSelect -= UpdateCanPerform;
            click.OnUpdate -= FollowTarget;
            backToCc.OnUpdate -= GetBackToCc;
            agent.Events.OnActionComplete -= ActionCompleteHandler;
        }
        
        private void UpdateCanPerform(int id)
        {
            if (id == this.id || id == 4)
            {
                canPerform = true;
            }
            else
                canPerform = false;
        }

        private void FollowTarget()
        {
            if (canPerform)
                provider.RequestGoal<MoveToGoal>();
        }
        
        private void GetBackToCc()
        {
            if (canPerform)
                provider.RequestGoal<StayNearCcGoal>();
        }
    }
}