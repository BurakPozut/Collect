using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    [SerializeField] private PlayerBehaviour playerBehaviour;
    private float xGoal;

    [SerializeField] private bool canControl;

    private void OnEnable() 
    {
        EventManager.StartLevel += SwitchCanControl; 
        EventManager.SwitchPlayerCanControl += SwitchCanControl;
        //EventManager.SetWin += SwitchCanControl;
    }

    private void OnDisable() 
    {
        EventManager.StartLevel -= SwitchCanControl;
        EventManager.SwitchPlayerCanControl -= SwitchCanControl;
        //EventManager.SetWin -= SwitchCanControl;     
    }

    void Start()
    {
        xGoal = transform.position.x;
    }

    void Update()
    {
        if(!canControl) return;

        xGoal += InputManager.inst.deltaDir.x * playerBehaviour.horizontalSpeed;
        xGoal = Mathf.Clamp(xGoal, playerBehaviour.minXClamp, playerBehaviour.maxXClamp);

        if(playerBehaviour.horizontalDamping < float.Epsilon)
            transform.position = new Vector3(xGoal, transform.position.y, transform.position.z);
        else
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, xGoal,(1 - playerBehaviour.horizontalDamping) * Time.deltaTime * 30), transform.position.y, transform.position.z);
    }

    private void SwitchCanControl()
	{
		canControl = !canControl;
		xGoal = transform.position.x;
	}
}
