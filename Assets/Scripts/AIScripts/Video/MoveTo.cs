using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{
    private NavMeshAgent NavMeshAgent;
            private Animator Animator;
            public Transform Target;
            [SerializeField] private float MinMoveDistance = 0.25f;
            
            private Vector3 LastPosition = Vector3.zero;
    
            private void Awake()
            {
                NavMeshAgent = GetComponent<NavMeshAgent>();
                Animator = GetComponentInChildren<Animator>();
            }

            private void OnEnable()
            {
                NavMeshAgent.SetDestination(Target.position);
            }
    
            private void Update()
            {
                Animator.SetBool(GeneralVariables.ISWALKINGFORWARD, NavMeshAgent.velocity.magnitude > MinMoveDistance);
            }
}
