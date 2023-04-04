using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Die,
    Moving,
    Idle
}

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float   _moveSpeed  = 10f;

    Vector3 _destPos;
    PlayerState _state = PlayerState.Idle;

    private void Start()
    {
        Managers.Input.MouseAction  -= OnMouseClicked;
        Managers.Input.MouseAction  += OnMouseClicked;

        //Temp
        Managers.UI.ShowPopupUI<UI_Button>();

    }

    private void Update()
    {
        switch (_state)
        {
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case PlayerState.Idle:
                UpdateIdle();
                break;
        }
    }

    private void UpdateDie()
    {

    }

    private void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.0001f)
        {
            _state = PlayerState.Idle;
        }
        else
        {
            //_moveSpeed * Time.deltaTime이 dir.magnitude보다 커지면 목적지를 넘어갔다가 돌아오려고해서 마지막에 부들부들 거리는거다.
            float moveDist = Mathf.Clamp(_moveSpeed * Time.deltaTime, 0, dir.magnitude);

            transform.position += (dir.normalized * moveDist);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10f * Time.deltaTime);
        }

        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", _moveSpeed);
    }

    private void UpdateIdle()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", 0f);
    }

    private void OnMouseClicked(Define.MouseEvent evt)
    {
        if (_state == PlayerState.Die) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 10, Color.red, 1.0f);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall")))
        {
            _destPos    = hit.point + new Vector3(0, transform.position.y, 0);
            _state = PlayerState.Moving;
        }
    }
}
