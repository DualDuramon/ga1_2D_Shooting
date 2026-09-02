using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class PlayerReplay : MonoBehaviour
{
    private PlayerMove playerMove;
    private Queue<ICommand> commandQueue = new Queue<ICommand>();

    private Coroutine Replaycoroutine = null;
    private bool isReplaying => Replaycoroutine != null;

    public Action OnReplayStart;
    public Action OnReplayEnd;


    private bool IsReadingInput = false;
    private float _startInputTime = 0.0f;

    private void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        if(!isReplaying)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Replaycoroutine = StartCoroutine(StartReplay());
            }
        }

        if(!IsReadingInput)
        {
            IsReadingInput = true;
        }
        else
        {

        }
    }

    public void AddMoveCommand(ICommand command)
    {
        commandQueue.Enqueue(command);
    }

    private IEnumerator StartReplay()
    {
        OnReplayStart?.Invoke();
        playerMove.ResetPlayerLocation();
        float timer = 0.0f;

        Debug.Log("replay Start");
        while(commandQueue.Count != 0)
        {
            ICommand cmd = commandQueue.Dequeue();

            while(timer <= cmd.ExecutedTime)
            {
                cmd.Execute();
                timer += Time.deltaTime;
                yield return null;
            }

            timer = 0.0f;
        }
        Debug.Log("replay End");
        OnReplayEnd?.Invoke();

    }
}
