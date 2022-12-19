using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]//�ش� ������Ʈ ����
public class Player : LivingEntity
{
    public float moveSpeed = 5f;

    Camera viewCamera;
    PlayerController controller;
    GunController gunController;
    protected override void Start()
    {
        base.Start();
        controller = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();
        viewCamera = Camera.main;
    }

    
    void Update()
    {
        //�̵�
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);

        //�ٶ󺸱�
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition); //ScreenPointToRay ȭ��󿡼� ��ġ�� ��ȯ���ִ� �޼ҵ� - ���콺��ġ�� �����Դ�
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero); //���콺�� �÷��̾��� ���������� �ٶ󺸰��ϴ� �غ�
        float rayDistance;

        if(groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            //Debug.DrawLine(ray.origin,point,Color.red);
            controller.LookAt(point);
        }

        //�����Է�
        if(Input.GetMouseButton(0))
        {
            gunController.Shoot();
        }
    }
}
