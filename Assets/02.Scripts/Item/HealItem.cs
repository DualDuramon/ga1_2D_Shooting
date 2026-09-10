using UnityEngine;

public class HealItem : Item
{
    [Header("Heal Amount")]
    [SerializeField] private float _healAmount = 10f;

    protected override void ApplyEffect(PlayerStatus player)
    {
        if (player == null)
        {
            Debug.LogWarning($"{transform.name} : player doesn't Exist!");
            return;
        }
        player.Heal(_healAmount);
    }
}
