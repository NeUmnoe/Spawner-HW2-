using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Animator))]

sealed class Entity : MonoBehaviour
{
    [SerializeField] private float _timerBeforeDeath = 1;

    private const string DieAnimation = "Die";

    private Movement _movement;
    private Animator _animator;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _movement.OnReachedDestination += HandleDeath;
    }

    private void OnDestroy()
    {
        _movement.OnReachedDestination -= HandleDeath;
    }

    public void Initialize(Vector3 direction)
    {
        _movement.SetSirection(direction);
    }

    private void HandleDeath()
    {
        _animator.SetTrigger(DieAnimation);
        Destroy(gameObject, _timerBeforeDeath);
    }
}