using UnityEngine;

public class PlayerFireCommand : ICommand
{
    private PlayerFire _recevier;

    public PlayerFireCommand(PlayerFire receiver)
    {
        _recevier = receiver;
    }

    public void Execute()
    {
        _recevier.ExecuteFire();
    }
}
