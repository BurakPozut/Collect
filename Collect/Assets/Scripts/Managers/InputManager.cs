using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{

    public static InputManager inst;
    [HideInInspector] public Vector2 dir;
    [HideInInspector] public Vector2 deltaDir;
    [HideInInspector] public bool isTouchDown;
    [HideInInspector] public bool isTouchUp;
    [HideInInspector] public bool isTouching;
    public float dirMaxMagnitude = float.PositiveInfinity;
    public float dirMultiplier = 10;

    private Vector2 dirOld;
    private const int NO_TOUCH = -1;
    private int touchId;
    private Vector2 joystickCenterPos;
    private bool touchControls;

    private void Awake() 
    {
        inst = this;
        touchId = NO_TOUCH;
        touchControls = Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android;
    }


    void Update()
    {
        GetTouchInput();
    }

    private void GetTouchInput()
    {
        int touchIdOld = touchId;

        if(touchControls)
        {
            foreach(Touch touch in Input.touches)
            {
                switch(touch.phase)
                {
                    case TouchPhase.Began:
                        if(touchId == NO_TOUCH && !IsPointerOverUIObject(touch.fingerId))
                        {
                            touchId = touch.fingerId;
                            joystickCenterPos = touch.position;
                        }
                    break;
                    case TouchPhase.Canceled:
                    case TouchPhase.Ended:
                        touchId = NO_TOUCH;
                    break;
                }
            }
        }
        else    // Unity Editor
        {
            if(Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
            {
                touchId = 0;
                joystickCenterPos = Input.mousePosition;
            }
            if(Input.GetMouseButtonUp(0))
            {
                touchId = NO_TOUCH;
            }
        }


        if(touchId != NO_TOUCH)
        {
            float muliplier = dirMultiplier / Screen.width;
            dir = (GetTouchPos(touchId) - joystickCenterPos) * muliplier;
            float m = dir.magnitude;
            if(m > dirMaxMagnitude) dir = dir * dirMaxMagnitude / m;
            deltaDir = dir - dirOld;
        }
        else
        {
            dir = Vector2.zero;
            if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                dir = dir.normalized * dirMaxMagnitude;
            }
            deltaDir = Vector2.zero;
        }

        isTouchDown = touchIdOld == NO_TOUCH && touchId != NO_TOUCH;
        isTouchUp = touchIdOld != NO_TOUCH && touchId == NO_TOUCH;
        isTouching = touchId != NO_TOUCH;
    }




    private Vector2 GetTouchPos(int touchId)
    {
        if(touchControls)
        {
            foreach(Touch touch in Input.touches)
            {
                if(touch.fingerId == touchId)   return touch.position;
            }
            return joystickCenterPos;
        }
        else    return Input.mousePosition;
    }

    public bool IsPointerOverUIObject(int touchId = 0)
    {
        if(touchControls)
        {
            if(EventSystem.current.IsPointerOverGameObject(touchId)) return true;
        }   
        else    
        {
            if(EventSystem.current.IsPointerOverGameObject()) return true;
        }

        return false;
    }

}    
