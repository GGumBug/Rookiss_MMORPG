using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform           _player = null;
    [SerializeField]
    private Define.CameraMode   _mode = Define.CameraMode.QuarterView;
    [SerializeField]
    private Vector3             _delta = new Vector3(0f, 6f, -5f);

    public void SetPlayer(Transform player) { _player = player; }

    private void LateUpdate()
    {
        if (_mode == Define.CameraMode.QuarterView)
        {
            if(!_player.gameObject.IsValid())
            {
                return;
            }
            RaycastHit hit;
            if (Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, 1 << (int)Define.Layer.Block))
            {
                float dis           = (hit.point - _player.position).magnitude * 0.8f;
                // 카메라 위치 = 시작지점 + 방향 + 거리;
                transform.position  = _player.position + _delta.normalized * dis;
                transform.LookAt(_player);
            }
            else
            {
                transform.position  = _player.position + _delta;
                transform.LookAt(_player);
            }
        }
    }

    public void SetQuarterView(Vector3 delta)
    {
        _mode   = Define.CameraMode.QuarterView;
        _delta  = delta;
    }
}
