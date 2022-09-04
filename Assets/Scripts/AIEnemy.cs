using UnityEngine.AI;
using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    public GameObject Player;
    public NavMeshAgent AIEnemyAgent;

    public float AttackDamage;
    public float health = 100f;

    public float Timer = 0f;
    public float WanderTime = 5f;
    public float Health
    {
        get { return health; }
        set
        {
            if (value < 0)
                value = 0;
            health = value;
            if (health <= 0)
            {
                AIEnemyAgent.speed = 0;
                transform.GetComponent<Animator>().SetBool("Dead", true);
                Object.Destroy(gameObject, 5f);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        AIEnemyAgent = transform.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float DistanceFromPlayer = Vector3.Distance(Player.transform.position, AIEnemyAgent.transform.position);
        if (health > 0 && Mathf.Abs(DistanceFromPlayer) < 5f && Player.GetComponent<PlayerController>().Health > 0)
        {
            AIEnemyAgent.SetDestination(Player.transform.position);
            transform.LookAt(Player.transform);
        }
        else
        {
            Timer += Time.deltaTime;

            if (Timer >= WanderTime)
            {
                Vector3 newPos = RandomNavSphere(transform.position, WanderTime, -1);
                AIEnemyAgent.SetDestination(newPos);
                Timer = 0;
            }
        }

    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (health > 0 && other.gameObject.tag == "Player")
        {
            AttackAnim(true);
            Debug.Log("Inside Trigger");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (health > 0 && other.gameObject.tag == "Player")
        {
            Player.GetComponent<PlayerController>().Health -= (AttackDamage * Time.deltaTime);
            if (Player.GetComponent<PlayerController>().Health <= 0)
            {
                AttackAnim(false);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            AttackAnim(false);
        }
    }

    public void AttackAnim(bool value)
    {
        Animator anim = transform.GetComponent<Animator>();
        anim.SetBool("Attack", value);
    }
}
