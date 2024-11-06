using UnityEngine;

public class RouteMover : MonoBehaviour
{
    [SerializeField] private Transform[] _routePoints;
    [SerializeField] private float _speed;

    private int _indexOfCurrentPoint;

    private void Update()
    {
        MoveToPoint();
    }

    private void MoveToPoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, _routePoints[_indexOfCurrentPoint].position, _speed * Time.deltaTime);

        if (transform.position == _routePoints[_indexOfCurrentPoint].position)
            SwitchToNextPoint();
    }

    public void SwitchToNextPoint() =>
        _indexOfCurrentPoint = ++_indexOfCurrentPoint % _routePoints.Length;
}
