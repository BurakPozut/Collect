using UnityEngine;

public class ForwardMovement : MonoBehaviour
{
    [SerializeField] private PlayerBehaviour playerBehaviour;
    [SerializeField] private bool canControl;
    private bool isMoveStraight = true;


    private void OnEnable() 
    {
        EventManager.StartLevel += SwitchCanControl;
        EventManager.SwitchPlayerCanControl += SwitchCanControl;
    }
    private void OnDisable() 
    {
        EventManager.StartLevel -= SwitchCanControl;
        EventManager.SwitchPlayerCanControl -= SwitchCanControl;
    }

    void Update()
    {
        if(!canControl) return;
        if(isMoveStraight)  transform.position += new Vector3(0f, 0f, playerBehaviour.forwardSpeed * Time.deltaTime);
    }

    private void SwitchCanControl()
    {
        canControl = !canControl;
    }

    
}
