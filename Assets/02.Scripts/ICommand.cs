using UnityEngine;

public interface ICommand
{
    void Execute();
    //void Undo();
}

public interface IHoldCommand : ICommand
{
    float ExecutedTime { get; }
}
