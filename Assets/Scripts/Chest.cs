using UnityEngine;

public class Chest : MonoBehaviour , IDamgable
{
    private Rigidbody2D rb => GetComponentInChildren<Rigidbody2D>();
    private Animator anim => GetComponentInChildren<Animator>();
    private EntityVFX vfx => GetComponent<EntityVFX>();

    [Header("Open Details")]
    [SerializeField] private Vector2 knockback;

    public void TakeDamage(float damage, Transform damageDealer)
    {
        vfx.PlayOnDamageVfx();
        anim.SetBool("chestOpen", true);
        rb.linearVelocity = knockback;
        rb.angularVelocity = Random.Range(-200f, 200f);

    }

    
}
