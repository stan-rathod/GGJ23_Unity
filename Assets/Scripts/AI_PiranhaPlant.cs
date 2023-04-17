using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;
using DG.Tweening;

public class AI_PiranhaPlant : MonoBehaviour
{
    internal Animator animator;
    SpriteRenderer spriteRenderer;
    [SerializeField] private Transform shootTrans;
    [SerializeField] private Fireball fireball;

    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var p = other.gameObject.GetComponent<PlayerController>();
        if (p != null)
        {
            InvokeRepeating("LaunchProjectile", 0.0f, 2.0f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var p = other.gameObject.GetComponent<PlayerController>();
        if (p != null)
        {
            CancelInvoke();
        }
    }

    void LaunchProjectile()
    {
        animator.SetTrigger("attack");
        Instantiate(fireball, shootTrans.position, shootTrans.rotation);
    }

    public float maxHP = 100;
    public bool IsAlive => currentHP > 0;
    float currentHP = 100;

    public void ApplyDamage(float BaseDamage, PlayerController player)
    {
        currentHP = Mathf.Clamp(currentHP - BaseDamage, 0, maxHP);
        if (currentHP <= 0)
        {
            CancelInvoke();
            spriteRenderer.DOColor(Color.red, 0.2f);
            Invoke("DestroySelf", 0.4f);
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
