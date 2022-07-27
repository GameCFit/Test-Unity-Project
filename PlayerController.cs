using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _speedRotation;
    [SerializeField] private int _damage;
    [SerializeField] private int _hp;
    [SerializeField] private GameObject _attackEffectGun;

    private Gun _gun;
    private Rigidbody _rigidbody;
    private Transform _gunTransform;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _gun = FindObjectOfType<Gun>();
        _gunTransform = FindObjectOfType<Gun>().transform;
    }
    private void FixedUpdate()
    {
        float horizonatal = Input.GetAxis("Horizontal") * _speedRotation;
        float vertical = Input.GetAxis("Vertical") * _speed;

        _rigidbody.transform.Translate(0, 0, vertical);
        _rigidbody.transform.Rotate(0, horizonatal, 0);
        if (Input.GetMouseButton(0))
        {
            _gun.Attack(_attackEffectGun, _gunTransform, _damage);
        }
        if(_hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        _hp -= damage;
    }
}
