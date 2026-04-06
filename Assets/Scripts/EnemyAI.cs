using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;

    // Rangos
    public float detectionRange = 5f;
    public float attackRange = 1.2f;

    // Ataque
    public int damage = 10;
    public float attackCooldown = 1f;
    float nextAttackTime = 0f;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    void FixedUpdate()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            if (distance <= attackRange)
            {
                rb.velocity = Vector2.zero;
                TryAttack();
            }
            else
            {
                Vector2 direction = (player.position - transform.position).normalized;
                rb.velocity = direction * speed;

                Flip(direction.x);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void TryAttack()
    {
        if (Time.time >= nextAttackTime)
        {
            Health playerHealth = player.GetComponent<Health>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void Flip(float directionX)
    {
        if (directionX > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (directionX < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    // Visualización en escena
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}