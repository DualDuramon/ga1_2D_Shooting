using UnityEngine;

public class ChaseEnemy : RushEnemy
{
    protected override void Update()
    {
        base.Update();
        CalculateMoveDirection(_targetPlayer.transform);
    }
}