using UnityEngine;

public class ButtonController : MonoBehaviour
{

    [SerializeField] private ButtonTypes buttonTypes;

    private bool once;

    void Update()
    {
        //Debug.Log(InputManager.inst.isTouching);
        //Debug.Log("once : " + once);
        if(InputManager.inst.isTouching && once == false && buttonTypes == ButtonTypes.StartLevel)
        {
            once = true;
            ButtonClicked();
        }
    }

    public void ButtonClicked()
    {
        switch(buttonTypes)
        {
            case ButtonTypes.StartLevel:
                EventManager.StartLevel?.Invoke();
                once = false;
                break;
            case ButtonTypes.NextLevel:
                EventManager.NextLevel?.Invoke();
            break;
            case ButtonTypes.RestartLevel:
                EventManager.RestartLevel?.Invoke();
            break;
            
        }
    }
}
