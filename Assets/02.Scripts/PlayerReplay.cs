using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReplay : MonoBehaviour
{
    private PlayerMove _playerMove;
    private Queue<ICommand> _commandQueue = new Queue<ICommand>();
    private Coroutine _replayCoroutine = null;
    private bool IsReplaying => _replayCoroutine != null;

    public Action OnReplayStart;
    public Action OnReplayEnd;

    private void Awake()
    {
        _playerMove = GetComponent<PlayerMove>();
    }

    private void OnDestroy()
    {
        if (IsReplaying)
        {
            OnReplayEnd.Invoke();
            StopCoroutine(_replayCoroutine);
            _replayCoroutine = null;
        }
        _commandQueue.Clear();
    }

    private void Update()
    {
        if (!IsReplaying)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _replayCoroutine = StartCoroutine(StartReplay());
            }
        }
    }

    public void AddMoveCommand(ICommand command)
    {
        _commandQueue.Enqueue(command);
    }

    private IEnumerator StartReplay()
    {
        OnReplayStart?.Invoke();
        _playerMove.ResetPlayerLocation();
        float timer = 0.0f;

        while (_commandQueue.Count != 0)
        {
            ICommand cmd = _commandQueue.Dequeue();

            if (cmd is IHoldCommand)
            {
                IHoldCommand holdCommand = cmd as IHoldCommand;
                while (timer <= holdCommand.ExecutedTime)
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
        _replayCoroutine = null;
    }
}