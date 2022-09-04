using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController Player;
    public Joystick joystick;
    public Vector3 Velocity;
    public float Speed;
    private float health = 100f;
    public Transform Cam;
    public float gravity=-1f;
    public GameObject BulletPrefab;
    [SerializeField]
    private Transform BarellTransform;
    [SerializeField]
    private Transform BulletParent;


   

    public float Health
    {
        get { return health; }
        set { if (value < 0)
                value = 0;
            health = value;
            if (health <= 0)
            {
                transform.GetComponent<Animator>().enabled = true;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Cam = Camera.main.transform;
        Player = transform.GetComponent<CharacterController>();
        joystick = FindObjectOfType<Joystick>();
        transform.GetComponent<Animator>().enabled = false;
    }

    private void OnEnable()
    {
        CanvasController.FireShot += ShootGun; 
    }
   
    private void OnDisable()
    {
        CanvasController.FireShot -= ShootGun;
    }
    // Update is called once per frame
    void Update()
    {

        {
            Velocity.y += -gravity * Time.deltaTime;
            Player.Move(Velocity * Time.deltaTime);
        }

        float angleToLook = Mathf.Atan2(joystick.PosInput.x, joystick.PosInput.y) * Mathf.Rad2Deg + Cam.eulerAngles.y;
        Player.transform.rotation = Quaternion.Euler(0, angleToLook, 0);
        Vector3 movdir = Quaternion.Euler(0, angleToLook, 0) * Vector3.forward;
        if (joystick.Move)
        {
            Player.Move(movdir.normalized * Speed * Time.deltaTime);
        }
    }

    private void ShootGun()
    {
        RaycastHit hit;
        GameObject Bullet = GameObject.Instantiate(BulletPrefab, BarellTransform.position, Quaternion.identity, BulletParent);
        BulletController bulletController = Bullet.GetComponent<BulletController>();
        if (Physics.Raycast(Cam.position, Cam.forward, out hit, Mathf.Infinity))
        {
            bulletController.target = hit.point;
            bulletController.hit = true;
        }
        else
        {
            bulletController.target = Cam.position + Cam.forward * 25f;
            bulletController.hit = true;
        }
    }

  
}
