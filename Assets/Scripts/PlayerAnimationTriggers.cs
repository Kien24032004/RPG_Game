using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    // This method is called in the animation event
    private void CurrentStateTrigger()
    {
        player.CallAnimationTrigger();
    }
}
