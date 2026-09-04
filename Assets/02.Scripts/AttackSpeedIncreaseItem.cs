using UnityEngine;

public class AttackSpeedIncreaseItem : Item
{
    [Header("Speed Adjusted Amount")]
    [SerializeField] private float _adjustAmount = 10f;

    protected override void ApplyEffect(PlayerStatus player)
    {
        if (player == null)
        {
            Debug.LogWarning($"{transform.name} : player doesn't Exist!");
            return;
        }
        player.AdjustFireDuration(_adjustAmount);
    }
}
