using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage = 10;
    [SerializeField] private int bulletSpeed = 10;
    Rigidbody rigidbody;
    [SerializeField] float selfDestructionTime = 3;
    float currentSelfDestructionTime = 0;
    GameObject hitException;
    [SerializeField] LayerMask selfDestructionLayer;
    public void AddHitException(GameObject parentObject)
    {
        hitException = parentObject;
    }
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    public int BulletSpeed
    {
        get { return bulletSpeed; }
        set { bulletSpeed = value; }
    }
    
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IHitable>(out IHitable hitableComponent) && other.gameObject != hitException)
        {
            other.GetComponent<IHitable>().OnHit(Damage, rigidbody.linearVelocity);
            Destroy(gameObject);
        }




    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * bulletSpeed * Time.fixedDeltaTime;

        if (currentSelfDestructionTime < selfDestructionTime)
        {
            currentSelfDestructionTime += Time.deltaTime;

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
