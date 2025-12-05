using UnityEngine;

public class SkillTimeEcho : SkillBase
{
    [SerializeField] private GameObject timeEchoPrefab;
    [SerializeField] private float timeEchoDuration;

    public float getEchoDuration()
    {
        return timeEchoDuration;
    }

    public override void TryUseSkill()
    {
        if(CanUseSkill() == false)
            return;

        CreateTimeEcho();
    }

    public void CreateTimeEcho()
    {
        GameObject timeEcho = Instantiate(timeEchoPrefab, transform.position, Quaternion.identity);
        timeEcho.GetComponent<SkillObjectTimeEcho>().SetupEcho(this);
    }    
}
