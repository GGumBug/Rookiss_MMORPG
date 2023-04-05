using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum PlayerState
{
    Die,
    Moving,
    Idle,
    Skill
}

public class PlayerController : MonoBehaviour
{
    Texture2D _attackIcon;
    Texture2D _handIcon;

    PlayerStat _stat;

    Vector3 _destPos;
    PlayerState _state = PlayerState.Idle;

    enum CursorType
    {
        None,
        Attack,
        Hand
    }

    CursorType _curcorType = CursorType.None;

    private void Start()
    {
        _stat = gameObject.GetComponent<PlayerStat>();
        _attackIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Attack");
        _handIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Hand");

        Managers.Input.MouseAction  -= OnMouseEnent;
        Managers.Input.MouseAction  += OnMouseEnent;
    }

    private void Update()
    {
        UpdateMouseCursor();

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

    private void UpdateMouseCursor()
    {
        if (Input.GetMouseButton(0))
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, _mask))
        {
            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
            {
                if(_curcorType != CursorType.Attack)
                {
                    // new Vector2(tex.width / 5, 0) 는 이미지와 마우스 커서의 보정값 (0,0)은 왼쪽 상단 모서리를 뜻한다.
                    Cursor.SetCursor(_attackIcon, new Vector2(_attackIcon.width / 5, 0), CursorMode.Auto);
                    _curcorType = CursorType.Attack;
                }
            }
            else
            {
                if (_curcorType != CursorType.Hand)
                {
                    Cursor.SetCursor(_handIcon, new Vector2(_handIcon.width / 3, 0), CursorMode.Auto);
                    _curcorType = CursorType.Hand;
                }
                
            }
        }
    }

    private void UpdateDie()
    {

    }

    private void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            _state = PlayerState.Idle;
        }
        else
        {
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
            //_moveSpeed * Time.deltaTime이 dir.magnitude보다 커지면 목적지를 넘어갔다가 돌아오려고해서 마지막에 부들부들 거리는거다.
            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);

            nma.Move(dir.normalized * moveDist); // Move(방향벡터)

            Debug.DrawRay(transform.position, dir.normalized, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1, LayerMask.GetMask("Block")))
            {
                if (!Input.GetMouseButton(0))
                    _state = PlayerState.Idle;
                return;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10f * Time.deltaTime);
        }

        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", _stat.MoveSpeed);
    }

    private void UpdateIdle()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", 0f);
    }

    int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

    GameObject _lockTarget;

    private void OnMouseEnent(Define.MouseEvent evt)
    {
        if (_state == PlayerState.Die) return;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(Camera.main.transform.position, ray.direction * 10, Color.red, 1.0f);
        bool rayCastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);

        switch (evt)
        {
            case Define.MouseEvent.PointerDown:
            {
                if (rayCastHit)
                {
                    _destPos = hit.point;
                    _state = PlayerState.Moving;

                    if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                        _lockTarget = hit.collider.gameObject;
                    else
                        _lockTarget = null;
                }    
            }
                break;
            case Define.MouseEvent.Press:
            {
                if (_lockTarget != null)
                    _destPos = _lockTarget.transform.position;
                else if (rayCastHit)
                        _destPos = hit.point;
            }
                break;
            case Define.MouseEvent.PointerUp:
                _lockTarget = null;
                break;
        }
    }
}
