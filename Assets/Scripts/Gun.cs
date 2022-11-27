using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform muzzle; //�߻�ü �ν��Ͻ�ȭ
    public Bullet bullet;
    public float msBetweenShots = 100; //����ӵ�
    public float muzzleVelocity = 35; //�߻�ü �ӵ�

    float nextShotTime;

    public void Shoot()
    {

        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots / 1000; //�и��ʸ� �ʷ� �ٲ�
            Bullet newBullet = Instantiate(bullet, muzzle.position, muzzle.rotation) as Bullet;
            newBullet.SetSpeed(muzzleVelocity);
        }
    }
}
