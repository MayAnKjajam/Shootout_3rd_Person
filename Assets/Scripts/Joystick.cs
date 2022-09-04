using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour,IDragHandler,IPointerDownHandler,IPointerUpHandler
{

    public Image JoyStickBG,JoyStick;
    public Vector2 PosInput;
    public bool Move;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnDrag(PointerEventData eventData)
    {
       
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(JoyStickBG.rectTransform,eventData.position,eventData.pressEventCamera,out PosInput))
        {
            PosInput.x = PosInput.x / (JoyStickBG.rectTransform.sizeDelta.x);
            PosInput.y = PosInput.y / (JoyStickBG.rectTransform.sizeDelta.y);
            if (PosInput.magnitude > 1)
            {
                PosInput=PosInput.normalized;
            }
            JoyStick.rectTransform.anchoredPosition = new Vector2(PosInput.x * ((JoyStickBG.rectTransform.sizeDelta.x) / 4), PosInput.y * ((JoyStickBG.rectTransform.sizeDelta.y) / 4));
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
        Move = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PosInput = Vector2.zero;
        JoyStick.rectTransform.anchoredPosition = Vector2.zero;
        Move = false;
    }

    public float InputHorizontal()
    {
        if (PosInput.x != 0)
        {
            return PosInput.x;
        }
        else
        {
            return Input.GetAxis("Horizontal");
        }
    }

    public float InputVertical()
    {
        if (PosInput.y != 0)
        {
            return PosInput.y;
        }
        else
        {
            return Input.GetAxis("Vertical");
        }
    }
}
