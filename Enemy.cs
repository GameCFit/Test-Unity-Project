using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _hp;
    [SerializeField] private int _damage;
    [SerializeField] private float _distance;

    private bool _onDistance;
    private Transform _targetTransform;
    private Rigidbody _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _targetTransform = FindObjectOfType<PlayerController>().transform;
    }
    private void Update()
    {
        if (Vector3.Distance(_targetTransform.position, transform.position) < _distance)
        {
            _rigidbody.transform.LookAt(_targetTransform.position);
            _rigidbody.transform.Translate(0, 0, _speed);
            _onDistance = true;
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
    public bool TakeOnDistance()
    {
        return _onDistance;
    }
    public void TakeDamage(int damage)
    {
        _hp -= damage;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag =="Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(_damage);
        }
    }
}