using UnityEngine;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(NavMeshAgent))]

sealed class Movement : MonoBehaviour
{
    [SerializeField] private float _maxPathDistance = 10;

    private NavMeshAgent _navigationAgent;
    private Vector3 _startPosition;

    public event Action OnReachedDestination;

    private void Awake()
    {
        _navigationAgent = GetComponent<NavMeshAgent>();
        _startPosition = transform.position;
    }

    public void SetSirection(Vector3 direction)
    {
        Vector3 targetPosition = transform.position + direction.normalized * _maxPathDistance;

        _navigationAgent.SetDestination(targetPosition);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _startPosition) >= _maxPathDistance)
        {
            OnReachedDestination?.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        if (_navigationAgent != null && _navigationAgent.path != null)
        {
            var path = _navigationAgent.path;
            Vector3 previousCorner = transform.position;

            foreach (var corner in path.corners)
            {
                Gizmos.DrawLine(previousCorner, corner);
                previousCorner = corner;
            }
        }
    }
}