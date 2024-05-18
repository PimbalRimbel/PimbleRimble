using UnityEngine;
using UnityEngine.Assertions;


public class BulletPool : MonoBehaviour
{
    public Bullet[] bulletPrefabs;
    public ObjectPool bulletPool;

    private void Awake()
    {
        bulletPool = new ObjectPool();

        Assert.IsTrue(bulletPrefabs.Length > 0, "Debe haber más de 1 prefab de bala");

        foreach (var prefab in bulletPrefabs)
        {
            var instance = Instantiate(prefab);
            instance.gameObject.SetActive(false);

            bulletPool._objects.Add(prefab);
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
