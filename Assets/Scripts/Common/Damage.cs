using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] int damage = 0;
    [SerializeField] bool oneTime = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!oneTime) return;

        if(other.gameObject.TryGetComponent<Health>(out Health health))
        {
            health.Damage(damage);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (oneTime) return;

        if (other.gameObject.TryGetComponent<Health>(out Health health))
        {
            health.Damage((int)(damage * Time.deltaTime));
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!oneTime) return;

        if (other.gameObject.TryGetComponent<Health>(out Health health))
        {
            health.Damage(damage);
            print("me attak :)");
        }
    }
}
