using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour
{
    public Bullet bulletPrefab; 
    public ObjectPool bulletPool;

    private void Awake()
    {
        bulletPool = new ObjectPool();

        Assert.IsNotNull(bulletPrefab, "El prefab de bala no está asignado");

        for (int i = 0; i < 5; i++) // Crea 5 instancias de la bala y agrégalas al pool
        {
            var instance = Instantiate(bulletPrefab);

            instance.gameObject.SetActive(false); // Desactivar la bala
            bulletPool._objects.Add(instance);
        }
    }

    // Método para obtener una bala del pool
    public Bullet GetBullet()
    {
        foreach (var bullet in bulletPool._objects)
        {
            if (!bullet.Active)
            {
                bulletPool.Get(bullet);
                return (Bullet)bullet;
            }
        }
        return null; // Si no hay balas disponibles en el pool
    }

    // Método para liberar una bala y devolverla al pool
    public void ReleaseBullet(Bullet bullet)
    {
        bulletPool.Release(bullet);
    }
}
