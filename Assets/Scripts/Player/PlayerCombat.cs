using UnityEngine;

public class PlayerCombat : EntityCombat
{
    [Header("Counter Attack Details")]
    [SerializeField] private float counterRecovery = 0.1f;

    public bool CounterAttackPerformed()
    {
        bool hasPerformedCounter = false;

        foreach (var target in GetDetectedColliders())
        {
            ICounterable counterable = target.GetComponent<ICounterable>();

            if(counterable == null) // skip this target if it is not counterable, go to next target
                continue;

            if (counterable.CanbeCountered)
            {
                counterable.HandleCounter();
                hasPerformedCounter = true;
            }
        }

        return hasPerformedCounter;
    }

    public float GetCounterRecoveryDuration() => counterRecovery;
}
