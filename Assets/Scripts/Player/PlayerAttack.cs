using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 2f;
    public float attackRadius = 0.5f;
    public int damage = 20;
    public float attackCooldown = 0.5f;

    public Transform attackPoint;
    public LayerMask enemyLayer;

    private float lastAttackTime;
    private PlayerInputActions inputActions;

    void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Attack.performed += ctx => TryAttack();
    }

    void OnEnable() => inputActions.Enable();
    void OnDisable() => inputActions.Disable();

    void TryAttack()
    {
        if (Time.time < lastAttackTime + attackCooldown) return;

        Attack();
        lastAttackTime = Time.time;
    }

    void Attack()
    {
        Vector3 origin = attackPoint.position;

        Collider[] hits = Physics.OverlapSphere(
            origin,
            attackRadius,
            enemyLayer
        );

        foreach (Collider hit in hits)
        {
            Health enemy = hit.GetComponent<Health>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}