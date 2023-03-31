using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed    = 10f;

    bool    _moveToDest         = false;
    Vector3 _destPos;

    private void Start()
    {
        Managers.Input.KeyAction    -= OnKeyBord;
        Managers.Input.KeyAction    += OnKeyBord;
        Managers.Input.MouseAction  -= OnMouseClicked;
        Managers.Input.MouseAction  += OnMouseClicked;
    }

    private void Update()
    {
        if (_moveToDest)
        {
            Vector3 dir = _destPos - transform.position;
            if (dir.magnitude < 0.0001f)
            {
                _moveToDest         = false;
            }
            else
            {
                //_moveSpeed * Time.deltaTime이 dir.magnitude보다 커지면 목적지를 넘어갔다가 돌아오려고해서 마지막에 부들부들 거리는거다.
                float moveDist      = Mathf.Clamp(_moveSpeed * Time.deltaTime, 0, dir.magnitude);

                transform.position  += (dir.normalized * moveDist);
                transform.rotation  = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10f * Time.deltaTime);
            }
        }
    }

    private void OnKeyBord()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation  = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
            transform.position  += (Vector3.forward * _moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation  = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            transform.position  += (Vector3.left * _moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation  = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            transform.position  += (Vector3.right * _moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation  = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
            transform.position  += (Vector3.back * _moveSpeed * Time.deltaTime);
        }

        _moveToDest             = false;
    }

    private void OnMouseClicked(Define.MouseEvent evt)
    {
        if (evt != Define.MouseEvent.Click)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 10, Color.red, 1.0f);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall")))
        {
            _destPos    = hit.point + new Vector3(0, transform.position.y, 0);
            _moveToDest = true;
        }
    }
}
