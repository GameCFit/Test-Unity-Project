using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _hp;
    [SerializeField] private int _damage;
    [SerializeField] private float _distance;
    [SerializeField] private float _startRecharge;
    [SerializeField] private float _retreatDistance;
    [SerializeField] private GameObject _effectAttackGun;
    [SerializeField] private Transform _pointAttack;
    [SerializeField] private float _attackDistance;

    private bool _onDistance;
    private Transform _targetTransform;
    private Rigidbody _rigidbody;
    private float _recharge;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _targetTransform = FindObjectOfType<PlayerController>().transform;
        _recharge = _startRecharge;
    }
    private void Update()
    {
        _recharge -= Time.deltaTime;
        if (Vector3.Distance(_targetTransform.position, transform.position) < _distance)
        {
            _rigidbody.transform.LookAt(_targetTransform.position);
            if (Vector3.Distance(transform.position, _targetTransform.position) < _retreatDistance)
            {
                _rigidbody.transform.Translate(0, 0, -_speed);
            }
            else
            {
                _rigidbody.transform.Translate(0, 0, _speed);
            }
            _onDistance = true;
            Attack();
        }
        else
        {
            _onDistance = false;
        }
        if (_hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void Attack()
    {
        if(_recharge <= 0)
        {
            _recharge = _startRecharge;
            Instantiate(_effectAttackGun, _pointAttack);
            Ray ray = new Ray(_pointAttack.position, _pointAttack.forward);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    Transform targetTransform = hit.collider.gameObject.GetComponent<PlayerController>().transform;
                    if (Vector3.Distance(transform.position, targetTransform.position) < _attackDistance)
                    {
                        PlayerController player = hit.collider.gameObject.GetComponent<PlayerController>();
                        player.TakeDamage(_damage);
                    }
                }
            }
        }
    }
    public bool TakeOnDistance()
    {
        return _onDistance;
    }
    public void TakeDamage(int damage)
    {
        _hp -= damage;
    }
}
