using UnityEngine;

[RequireComponent (typeof(Rigidbody))]

public class EnemyController : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] private Transform _player;
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;
    [SerializeField] private float _searchDistance;
    [SerializeField] private float _safeDistance;
    [SerializeField] private float _HP;

    private float _distance;

    private float _targetDistance;

    private Vector3 _direction;

    private Ray newRay;

    private bool _isPlayer;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        if (_HP <= 0)
        {
            DestroyShip();
        }

        _distance = Vector3.Distance (transform.position, _player.position);

        _targetDistance = Vector3.Distance(transform.position, _target.position);

        if (_distance < _searchDistance)
        {
            _direction = _player.position - transform.position;
            _isPlayer = true;
        }
        else
        {
            _direction = _target.position - transform.position;
            _isPlayer = false;
        }
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_direction), 0.1f);

        if (_distance > _safeDistance && _isPlayer) 
        { 
            _rb.AddForce(transform.forward * _speed * Time.fixedDeltaTime);
        }

        if (_targetDistance > _safeDistance && !_isPlayer)
        {
            _rb.AddForce(transform.forward * _speed * Time.fixedDeltaTime);
        }
        
    }

    public float GetHP()
    {
        return _HP;
    }

    public void GetDamage(float damage)
    {
        _HP -= damage;
    }

    public void DestroyShip()
    {
        Destroy(gameObject);
    }
}
