using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class PlayerReplay : MonoBehaviour
{
    private PlayerMove _playerMove;
    private Queue<ICommand> commandQueue = new Queue<ICommand>();

    private Coroutine Replaycoroutine = null;
    private bool IsReplaying => Replaycoroutine != null;

    public Action OnReplayStart;
    public Action OnReplayEnd;


    private void Awake()
    {
        _playerMove = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        if(!IsReplaying)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Replaycoroutine = StartCoroutine(StartReplay());
            }
        }
    }

    public void AddMoveCommand(ICommand command)
    {
        commandQueue.Enqueue(command);
    }

    private IEnumerator StartReplay()
    {
        OnReplayStart?.Invoke();
        _playerMove.ResetPlayerLocation();
        float timer = 0.0f;

        while(commandQueue.Count != 0)
        {
            ICommand cmd = commandQueue.Dequeue();

            if(cmd is IHoldCommand)
            {
                IHoldCommand holdCommand = cmd as IHoldCommand;
                while(timer <= holdCommand.ExecutedTime)
                {
                    cmd.Execute();
                    timer += Time.deltaTime;
                    yield return null;
                }
            }
            else
            {
                cmd.Execute();
            } 
                
            timer = 0.0f;
        }

        Debug.Log("리플레이 종료");
        _playerMove.ResetPlayerLocation();
        yield return null;

        OnReplayEnd?.Invoke();
        Replaycoroutine = null;
    }
}
