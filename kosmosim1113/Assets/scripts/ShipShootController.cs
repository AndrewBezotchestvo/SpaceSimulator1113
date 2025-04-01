using UnityEngine;

public class ShipShootController : MonoBehaviour
{
    private LineRenderer _lineRenderer1;
    private LineRenderer _lineRenderer2;

    private Ray _ray;
    private RaycastHit _raycastHit;

    private GameObject _shootPoint1;
    private GameObject _shootPoint2;

    void Start()
    {
        _shootPoint1 = transform.GetChild(0).gameObject;
        _shootPoint2 = transform.GetChild(1).gameObject;

        _lineRenderer1 = _shootPoint1.GetComponent<LineRenderer>();
        _lineRenderer2 = _shootPoint2.GetComponent<LineRenderer>();

        _lineRenderer1.positionCount = 2;
        _lineRenderer1.startWidth = 0.1f;
        _lineRenderer1.endWidth = 0.1f;

        _lineRenderer2.positionCount = 2;
        _lineRenderer2.startWidth = 0.1f;
        _lineRenderer2.endWidth = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {

            Vector3 targetPosition = LaserShoot();
            if (targetPosition != Vector3.zero)
            {
                _lineRenderer1.enabled = true;
                _lineRenderer1.SetPosition(0, _shootPoint1.transform.position);
                _lineRenderer1.SetPosition(1, targetPosition);

                _lineRenderer2.enabled = true;
                _lineRenderer2.SetPosition(0, _shootPoint2.transform.position);
                _lineRenderer2.SetPosition(1, targetPosition);
            }
        }
        else
        {
            _lineRenderer1.enabled = false;
            _lineRenderer2.enabled = false;
        }
    }

    private Vector3 LaserShoot()
    {
        _ray = new Ray(_shootPoint1.transform.position, _shootPoint1.transform.forward);
        Debug.DrawRay(_ray.origin, _ray.direction * 1000);

        if (Physics.Raycast(_ray, out _raycastHit))
        {
            return _raycastHit.point;
        }
        else
        {
            return transform.position + transform.forward * 100;
        }
    }
}
