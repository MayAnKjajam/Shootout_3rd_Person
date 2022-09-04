using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float speed = 50f;
    private float timeToDestroy=3f;
    public float BulletDamage = 25; 
    public Vector3 target { get; set; }
    public bool hit { get; set; }


    private void OnEnable()
    {
        Destroy(gameObject, timeToDestroy);
    }

   
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (!hit && Vector3.Distance(transform.position, target) < .01f)
        {
            Destroy(gameObject);
        }
    }

    [System.Obsolete]
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject,1f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AIEnemy")
        {
            //Debug.Log("Enemy Hit" + collision.transform.GetComponent<AIEnemy>().Health);
            other.transform.GetComponentInParent<AIEnemy>().Health -= BulletDamage;
            Destroy(gameObject);
        }

    }
}
