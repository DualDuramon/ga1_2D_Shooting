using UnityEngine;

public class PlayerCommandInvoker : MonoBehaviour
{
    private PlayerReplay _replay;
    private PlayerMove _cachedMove;
    private PlayerFire _cachedFire;

    private float _startInputTime = 0f;

    public bool CanReadInput = true;

    private Vector2 prevMoveVector = Vector2.zero;

    private void Awake()
    {
        _replay = GetComponent<PlayerReplay>();
        _cachedMove = GetComponent<PlayerMove>();
        _cachedFire = GetComponent<PlayerFire>();
    }

    private void OnEnable()
    {
        _replay.OnReplayStart += ToggleCanReadInput;
        _replay.OnReplayEnd += OnReplayEnd;
    }

    private void OnDisable()
    {
        _replay.OnReplayStart -= ToggleCanReadInput;
        _replay.OnReplayEnd -= OnReplayEnd;
    }

    private void Update()
    {
        if (!CanReadInput) return;
        ReadInputCommand();
    }

    private void ReadInputCommand()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2 dir = new Vector2(h, v);

        if (dir != prevMoveVector)
        {
            Debug.Log("입력 삽입..");
            _replay.AddMoveCommand(new PlayerMoveCommand(_cachedMove, prevMoveVector, Time.time - _startInputTime));

            _startInputTime = Time.time;
        }

        prevMoveVector = dir;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("입력 삽입..");
            _replay.AddMoveCommand(new PlayerFireCommand(_cachedFire));
        }
    }

    private void ToggleCanReadInput()
    {
        CanReadInput = false;
    }

    private void OnReplayEnd()
    {
        CanReadInput = true;

        _startInputTime = Time.time;
        prevMoveVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}