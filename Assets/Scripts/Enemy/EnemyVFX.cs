using UnityEngine;

public class EnemyVFX : EntityVFX
{
    [Header("Counter Attack Window")]
    [SerializeField] private GameObject attackAlert;


    public void EnableAttackAlert(bool enable)
    {
        if(attackAlert == null)
            return;

        attackAlert.SetActive(enable);
    }
    
}
