using UnityEngine;


[System.Serializable]
public class BulletStats 
{
    // Stats
    [SerializeField, Min(0f), Tooltip("How much damage it will do to the enemy.")]         public int damage = 10;
    [SerializeField, Min(0f), Tooltip("How many bullets it will eject in one shot.")]      public int bulletsPerShot = 1;
    [SerializeField, Min(0f), Tooltip("How fast the Bullet is.")]                          public int bulletSpeed = 1;
    
    
    
    
    
    //[SerializeField] private Bullet bulletPrefab;
    //[SerializeField] RaycastBullet raycastBullet;
    //[SerializeField, Tooltip("The GameObject where bullets are gonna spawn from.")] private GameObject bulletSpawnPoint;

    // Getter 
    //public RaycastBullet BulletPrefab => raycastBullet;
    //public GameObject BulletSpawnPoint => bulletSpawnPoint;
}