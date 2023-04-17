using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class PlayerAttack : MonoBehaviour
{
    internal Animator animator;
    SpriteRenderer spriteRenderer;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Transform attackPoint2;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private float attackDamage = 35f;
    [SerializeField] private float attackRate = 2f;
    float attackTime = 0.0f;
    [SerializeField] private LayerMask enemyLayer;
    PlayerController playerController;

    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Time.time >= attackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                attackTime = Time.time + (1 / attackRate);
            }
        }
    }

    void Attack()
    {
        animator.SetTrigger("attack");

        Transform trans =  spriteRenderer.flipX ? attackPoint2 : attackPoint;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(trans.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.TryGetComponent(out EnemyController enemyController))
            {
                enemyController.ApplyDamage(attackDamage, playerController);
            }
            
            if (enemy.TryGetComponent(out AI_PiranhaPlant piranhaPlant))
            {
                piranhaPlant.ApplyDamage(attackDamage, playerController);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if(attackPoint != null)
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
