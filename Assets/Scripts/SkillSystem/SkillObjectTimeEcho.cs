using UnityEngine;

public class SkillObjectTimeEcho : SkillObjectBase
{
    [SerializeField] private GameObject onDeathVfx;
    [SerializeField] private LayerMask whatIsGround;
    private SkillTimeEcho echoManager;

    public void SetupEcho(SkillTimeEcho echoManager)
    {
        this.echoManager = echoManager;

        Invoke(nameof(HandleDeath), echoManager.getEchoDuration());
    }
    
    private void Update()
    {
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
        StopHorizontalMovement();
    }

    public void HandleDeath()
    {
        Instantiate(onDeathVfx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void StopHorizontalMovement()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, whatIsGround);

        if(hit.collider != null)
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }
}
