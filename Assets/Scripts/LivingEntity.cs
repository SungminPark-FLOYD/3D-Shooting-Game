using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startingHealth;
    protected float health;
    protected bool dead;

    public event System.Action OnDeath; //�̺�Ʈ �ý���

    protected virtual void Start()
    {
            health = startingHealth;
    }
    public void TakeHit(float damage, RaycastHit hit)
    {
        health -= damage;

        if(health <= 0 && !dead)
        {
            Die();
        }
    }

    protected void Die()
    {
        dead = true;
        if (OnDeath != null)
        {
            OnDeath();
        }
        GameObject.Destroy(gameObject);
    }
}
