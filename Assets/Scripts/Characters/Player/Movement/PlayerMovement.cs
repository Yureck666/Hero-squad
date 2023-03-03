using HeroSquad.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

namespace HeroSquad.Characters.Player.Movement
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(FollowTarget))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Joystick joystick;
        [SerializeField] private bool invertJoystick;
        [SerializeField] private MovementSettings settings;

        private NavMeshAgent _navMeshAgent;
        private FollowTarget _followTarget;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _followTarget = GetComponent<FollowTarget>();
        }
    
        private void FixedUpdate()
        {
            var velocity = joystick.Direction.ToVector3Y() * settings.PlayerMoveSpeed;
            velocity *= (invertJoystick ? -1 : 1);
            _navMeshAgent.Move((velocity + Physics.gravity) * Time.fixedDeltaTime);

            _followTarget.SetInMove(velocity.magnitude > 0);
        }
    }
}
