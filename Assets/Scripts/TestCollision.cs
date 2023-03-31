using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"{collision.gameObject.name}");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
    }

    private void Update()
    {
        // Local <-> World <-> ViewPort <-> Screen (화면)

        //Debug.Log(Input.mousePosition); // Screen
        //Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition)); // ViewPort

        if (Input.GetMouseButtonDown(0))
        {
            //Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            //Vector3 dir = mousePos - Camera.main.transform.position;
            //dir = dir.normalized;

            //RaycastHit hit;
            //if (Physics.Raycast(Camera.main.transform.position, dir, out hit))
            //{
            //    Debug.Log($"Hit Name : {hit.collider.gameObject.name}");
            //}

            // Ray를 사용하면 위의 코드가 이렇게 줄어든다.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(Camera.main.transform.position, ray.direction * 10, Color.red, 1.0f);

            LayerMask layer = LayerMask.GetMask("Monster");
            // 윗줄 아랫줄 선택
            int mask = (1 << 8) | (1 << 9); // Wall도 체크 가능

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0f, mask))
            {
                
                Debug.Log($"Hit Name : {hit.collider.gameObject.tag}");
            }
                
        }

        
    }
}
