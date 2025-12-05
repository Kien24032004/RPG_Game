using UnityEngine;

public enum SkillUpgradeType
{
    None,

    // ---- Dash Tree ----
    Dash, // dash to avoid damage
    DashCloneOnStart, //create a clone when dash start
    DashCloneOnStartAndArrival, // create a clone when dash start and end
    DashShardOnStart, // create a shard when dash start
    DashShardOnStartAndArrival, // create a shard when dash start and end

    // ---- Shard Tree ----
    Shard, // The shard explodes when touched by an enemy or time goes up
    ShardMoveToEnemy, // shard will move towards nearest enemy
    ShardMulticast, // shard anility can have up to N charges. You can cast them all in a raw
    ShardTeleport, // you can swap place with the last shard tou created
    ShardTeleportHpRewind, // when you swap place with shard, yoour HP % is same as it was when you created shard

     // ---- Shard Tree ----
    SwordThrow, 
    SwordThrowSpin, 
    SwordThrowPierce,
    SwordThrowBounce,

    // ---- Time Echo ----
    TimeEcho,
    TimeEchoSingleAttack,
    TimeEchoMultiAttack,
    TimeEchoChanceToMultiply,
    TimeEchoHealWisp,
    TimeEchoCleanseWisp,
    TimeEchoCooldownWisp
}
