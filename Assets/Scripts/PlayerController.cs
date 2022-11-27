using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]//�ش� ������Ʈ ����
public class PlayerController : MonoBehaviour
{
    Rigidbody rigid;
    Vector3 velocity;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void LookAt(Vector3 lookPoint)
    {
        Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z); //y�� ����
        transform.LookAt(heightCorrectedPoint);
    }

    public void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + velocity * Time.fixedDeltaTime);
    }
}
