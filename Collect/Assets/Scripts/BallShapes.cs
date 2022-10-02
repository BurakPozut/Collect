using UnityEngine;
using System.Collections.Generic;

public class BallShapes : MonoBehaviour
{
    [SerializeField] private BallShapeTypes shapeTypes;
    [HideInInspector] public int ballCount;

    private List<Rigidbody> ballList;

    private void Awake() 
    {
        ballCount = (int)shapeTypes;
        ballList = new List<Rigidbody>();

        foreach(Transform ball in this.transform)
        {
            ballList.Add(ball.GetComponent<Rigidbody>());
        }
    }
    


    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "BallActivate")   ActivateRB();
    }
    public void ActivateRB() 
    {
        this.GetComponent<BoxCollider>().enabled = false;
        foreach(Rigidbody ball in ballList) ball.isKinematic = false;
    } 

}
