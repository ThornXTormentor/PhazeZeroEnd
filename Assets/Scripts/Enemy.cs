using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 15f;

    private Animator enemyAnimator;

    private void Awake()
    {
        enemyAnimator = this.GetComponent<Animator>();
    }

    public void Damage(float dam)
    {
        health -= dam;
        if(health <= 0f)
        {
            enemyAnimator.SetBool("Dead", true);
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
