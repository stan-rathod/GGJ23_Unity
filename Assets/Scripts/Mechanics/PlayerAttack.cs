using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class PlayerAttack : MonoBehaviour
{
    internal Animator animator;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private float attackDamage = 35f;
    [SerializeField] private LayerMask enemyLayer;
    PlayerController playerController;

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Attack();
    }

    void Attack()
    {
        animator.SetTrigger("attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.TryGetComponent(out EnemyController enemyController))
            {
                enemyController.ApplyDamage(attackDamage, playerController);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if(attackPoint != null)
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
