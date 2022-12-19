using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]//해당 컴포넌트 포함
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
        //이동
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);

        //바라보기
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition); //ScreenPointToRay 화면상에서 위치를 반환해주는 메소드 - 마우스위치를 가져왔다
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero); //마우스와 플레이어의 교차지점을 바라보게하는 준비
        float rayDistance;

        if(groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            //Debug.DrawLine(ray.origin,point,Color.red);
            controller.LookAt(point);
        }

        //무기입력
        if(Input.GetMouseButton(0))
        {
            gunController.Shoot();
        }
    }
}
