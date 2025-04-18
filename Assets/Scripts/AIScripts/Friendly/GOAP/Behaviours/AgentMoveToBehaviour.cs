﻿using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;
using UnityEngine.AI;

namespace AIScripts.Friendly.GOAP
{
    public class AgentMoveToBehaviour : MonoBehaviour
    {
        private NavMeshAgent NavMeshAgent;
        private Animator Animator;
        private AgentBehaviour AgentBehaviour;
        private ITarget CurrentTarget;
        [SerializeField] private float MinMoveDistance = 0.25f;
        
        private Vector3 LastPosition;

        private void Awake()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();
            Animator = GetComponentInChildren<Animator>();
            AgentBehaviour = GetComponent<AgentBehaviour>();
        }

        private void OnEnable()
        {
            AgentBehaviour.Events.OnTargetChanged += EventsOnTargetChanged;
        }
        
        private void OnDisable()
        {
            AgentBehaviour.Events.OnTargetChanged -= EventsOnTargetChanged;
        }

        private void EventsOnTargetOutOfRange(ITarget target)
        {
            Animator.SetBool(GeneralVariables.ISWALKINGFORWARD, false);
        }

        private void EventsOnTargetChanged(ITarget target, bool inrange)
        {
            CurrentTarget = target;
            LastPosition = CurrentTarget.Position;
            CheckPath();
        }

        private void Update()
        {
            if (CurrentTarget == null)
                return;
            if (MinMoveDistance > Vector3.Distance(CurrentTarget.Position, LastPosition))
            {
                LastPosition = CurrentTarget.Position;
                CheckPath();
            }
            
            Animator.SetBool(GeneralVariables.ISWALKINGFORWARD, NavMeshAgent.velocity.magnitude > MinMoveDistance);
        }

        private void CheckPath()
        {
            NavMeshAgent.SetDestination(CurrentTarget.Position);
            if (NavMeshAgent.pathStatus == NavMeshPathStatus.PathPartial)
            {
                Debug.Log("Invalid path!");
                NavMeshAgent.ResetPath();
                AgentBehaviour.CompleteAction();
            }
        }
    }
}