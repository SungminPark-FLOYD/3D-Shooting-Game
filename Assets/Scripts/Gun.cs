using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform muzzle; //발사체 인스턴스화
    public Bullet bullet;
    public float msBetweenShots = 100; //연사속도
    public float muzzleVelocity = 35; //발사체 속도

    float nextShotTime;

    public void Shoot()
    {

        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots / 1000; //밀리초를 초로 바꿈
            Bullet newBullet = Instantiate(bullet, muzzle.position, muzzle.rotation) as Bullet;
            newBullet.SetSpeed(muzzleVelocity);
        }
    }
}
