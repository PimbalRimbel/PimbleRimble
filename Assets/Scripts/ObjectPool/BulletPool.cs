using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using System;

public class BulletPool : MonoBehaviour
{
    public Bullet bulletPrefab; 
    public ObjectPool<Bullet> bulletPool;

    private void Awake()
    {
        bulletPool = new ObjectPool<Bullet>(5, bulletPrefab);

        Assert.IsNotNull(bulletPrefab, "El prefab de bala no est� asignado");
    }

    // M�todo para obtener una bala del pool
    public Bullet GetBullet()
    {
        var bullet = bulletPool.Get();
        ((Bullet)bullet).balaColisionada += balaColisionada;
        return (Bullet)bullet;
    }

    // M�todo para liberar una bala y devolverla al pool
    public void ReleaseBullet(Bullet bullet)
    {
        bulletPool.Release(bullet);
    }

    public void balaColisionada(object sender, EventArgs evenargs)
    {
        ((Bullet)sender).balaColisionada -= balaColisionada;
        ReleaseBullet((Bullet)sender);
    }
}
