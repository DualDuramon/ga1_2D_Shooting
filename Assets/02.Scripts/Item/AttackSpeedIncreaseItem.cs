using UnityEngine;

public class AttackSpeedIncreaseItem : Item
{
    [Header("Speed Adjust Amount")]
    [Tooltip("플레이어가 아이템을 먹으면 공격 쿨타임을 얼마나 조정할지 정하는 변수입니다. 공격 속도를 낮추려면 음수값을 넣으세요")]
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
