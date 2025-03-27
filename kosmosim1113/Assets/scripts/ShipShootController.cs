using UnityEngine;

public class ShipShootController : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private Ray _ray;
    private RaycastHit _raycastHit;

    private GameObject _shootPoint;

    void Start()
    {
        _shootPoint = transform.GetChild(0).gameObject;
        _lineRenderer = GetComponentInParent<LineRenderer>();
        _lineRenderer.positionCount = 2;
        _lineRenderer.startWidth = 0.1f;
        _lineRenderer.endWidth = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            
            Vector3 targetPosition = LaserShoot();
            if (targetPosition != Vector3.zero)
            {
                _lineRenderer.enabled = true;
                _lineRenderer.SetPosition(0, _shootPoint.transform.position);
                _lineRenderer.SetPosition(1, targetPosition);
                _lineRenderer.material.color = Random.ColorHSV();
            }
        }
        else
        {
            _lineRenderer.enabled = false;
        }
    }

    private Vector3 LaserShoot()
    {
        _ray = new Ray(_shootPoint.transform.position, _shootPoint.transform.forward);
        Debug.DrawRay(_ray.origin, _ray.direction * 1000);

        if (Physics.Raycast(_ray, out _raycastHit))
        {
            return _raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
