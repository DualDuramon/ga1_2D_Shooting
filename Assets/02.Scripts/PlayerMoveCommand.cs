using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class PlayerMoveCommand : IHoldCommand
{
    private float _executedTime;
    private Vector2 _direction;
    private PlayerMove _recevier;

    public float ExecutedTime => _executedTime;

    public PlayerMoveCommand(PlayerMove receiver, Vector2 direction, float excutedTime)
    {
        _recevier = receiver;
        _direction = direction;
        _executedTime = excutedTime;

    }

    public void Execute()
    {
        _recevier.ExecutePlayerMove(_direction);
    }
}
