using UnityEngine;

public class UI_AutoButton : MonoBehaviour
{
    //버튼을 클릭하면 Toggle 하고 싶음.
    // - 플레이어의 자동 이동
    // - 플레이어의 자동 공격
    private bool _autoMode = false;
    private PlayerAutoMove _playerAutoMove;

    private void Start()
    {
        _playerAutoMove = FindAnyObjectByType<PlayerAutoMove>();
    }

    public void AutoToggle()
    {
        _autoMode = !_autoMode;
        _playerAutoMove.SetAutoMove(_autoMode);
    }
}
