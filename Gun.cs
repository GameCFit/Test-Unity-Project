using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float _startRecharge;
    private float _recharge;
    private void Awake()
    {
        _recharge = _startRecharge;
    }
    private void Update()
    {
        _recharge -= Time.deltaTime;
        Debug.DrawRay(transform.position, transform.forward * 10, Color.red);
    }
    public void Attack(GameObject attackEffectGun, Transform gunTransform, int damage)
    {
        if (_recharge <= 0)
        {
            Instantiate(attackEffectGun, gunTransform);
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Enemy")
                {
                    if (hit.collider.gameObject.GetComponent<Enemy>())
                    {
                        Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();
                        if (enemy.TakeOnDistance() == true)
                        {
                            enemy.TakeDamage(damage);
                        }
                    }
                    else if (hit.collider.gameObject.GetComponent<EnemyShooter>())
                    {
                        EnemyShooter enemy = hit.collider.gameObject.GetComponent<EnemyShooter>();
                        if (enemy.TakeOnDistance() == true)
                        {
                            enemy.TakeDamage(damage);
                        }
                    }
                }
            }
            _recharge = _startRecharge;
        }
    }
}
