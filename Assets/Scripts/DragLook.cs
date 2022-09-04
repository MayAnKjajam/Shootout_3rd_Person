using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragLook : MonoBehaviour
{

   
    public Camera camera;
    public static bool obstacle;
    public GameObject Target;
    public Vector3 Offset;
    public float Distance = 2.3f;
    float YAngleMin = -80f;
    float YAngleMax = 50f;
    private float CurrentX = 0f;
    private float CurrentY = 0f;
    public float Speed = 2f;

    void Start()
    {
       

        Offset = transform.position - Target.transform.position;


    }
    private void Update()
    {
        if (IsPointerOverUIObject())
        {
            obstacle = true;
        }
        else
        {
            obstacle = false;
        }
       

        Debug.Log("obstacle " + obstacle);
        Debug.Log("TouchCount " + Input.touchCount);

        if (Input.touchCount > 0 && !obstacle)
        {
            if (Input.touchCount == 1)
            {
                CurrentX -= Input.touches[0].deltaPosition.y * Speed * Time.deltaTime;
                CurrentY += Input.touches[0].deltaPosition.x * Speed * Time.deltaTime;
                CurrentX = Mathf.Clamp(CurrentX, YAngleMin, YAngleMax);
            }
            else
            {
                CurrentX -= Input.touches[1].deltaPosition.y * Speed * Time.deltaTime;
                CurrentY += Input.touches[1].deltaPosition.x * Speed * Time.deltaTime;
                CurrentX = Mathf.Clamp(CurrentX, YAngleMin, YAngleMax);
            }
        }
    }
    private void LateUpdate()
    {
        Vector3 Dir = new Vector3(0, 0, -Distance);
        Quaternion rotation = Quaternion.Euler(CurrentX, CurrentY, 0);
        camera.transform.position = Target.transform.position + rotation * Dir;
        camera.transform.LookAt(Target.transform.position );
    }  

    public bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current)
        {
            position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
        };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    

}