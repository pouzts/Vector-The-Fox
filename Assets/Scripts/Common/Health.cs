using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] GameObject deathPrefab;
    [SerializeField] bool destroyOnDeath = true;
    [SerializeField] int maxHealth = 100;
    [SerializeField] bool destroyRoot = false;

    public int health { get; set; }
    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void Damage(int damage)
    {
        health -= damage;
        if (!isDead && health <= 0)
        {
            isDead = true;
            if (TryGetComponent<IDestroyable>(out IDestroyable destroyable))
            {
                destroyable.DestroySelf();
            }
            if(deathPrefab != null)
            {
                Instantiate(deathPrefab, transform.position, transform.rotation);
            }
            if(destroyOnDeath)
            {
                if (destroyRoot) Destroy(gameObject.transform.root.gameObject);
                else Destroy(gameObject);
            }
        }
    }
}
