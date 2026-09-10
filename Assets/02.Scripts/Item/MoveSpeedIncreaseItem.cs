using UnityEngine;

public class MoveSpeedIncreaseItem : Item
{
    [Header("Speed Adjust Amount")]
    [SerializeField] private float _adjustAmount = 10f;

    protected override void ApplyEffect(PlayerStatus player)
    {
        if (player == null)
        {
            Debug.LogWarning($"{transform.name} : player doesn't Exist!");
            return;
        }
        player.AdjustMoveSpeed(_adjustAmount);
    }
}
