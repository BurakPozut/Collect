using UnityEngine;
using System.Threading.Tasks;
using System.Collections.Generic;
using TMPro;



public class BasketController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI basketBallCount;
    [SerializeField] private GameObject ballParent;
    [SerializeField] private DifficultyLevel difficultyLevel;
    private List<Rigidbody> finishLineBallList;
    
    private int totalBallCount = 0;
    private int ballsInBasket = 0;
    private int ballsRequired = 0;

    private bool once = false;

    private void Start() 
    {
        finishLineBallList = new List<Rigidbody>();
        GetTotalBallCount();

        ballsRequired = (int)Mathf.Ceil(totalBallCount * ((float)difficultyLevel * 0.1f));  // I have to mul. with 0.1f cause enums dont allow for float,
        // so i just gave them integer values in the enum and turned them floats here.

        basketBallCount.SetText("0 / " + ballsRequired);
    }

    // We add our ball objects that made to the finish line so we can add force to them and meak them fall into the basket
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Ball") finishLineBallList.Add(other.gameObject.GetComponent<Rigidbody>());
    }

    // After the player exited the basket's trigger we switch the controls and add force to every ball in our list
    private async void OnTriggerExit(Collider other) 
    {
        if(other.tag == "Player" && !once)
        {
            once = true;
            EventManager.SwitchPlayerCanControl.Invoke();
            LaunchBalls();
            await WaitforSeconds(2);
            if(finishLineBallList.Count >= ballsRequired)   EventManager.BasketCheck?.Invoke();
            else    EventManager.SetLose?.Invoke();
        }   
    }

    private void LaunchBalls()
    {
        foreach(Rigidbody ball in finishLineBallList)
            ball.AddForce(Vector3.forward * 35,ForceMode.Impulse);
    }

    private void GetTotalBallCount() // Improve
    {
        foreach(Transform ballShape in ballParent.transform)
            totalBallCount += ballShape.GetComponent<BallShapes>().ballCount;
        //Debug.Log("total ball count :"+totalBallCount);
    }

    // this is actually just for UI
    private void OnCollisionEnter(Collision other) 
    {
        ballsInBasket++;
        basketBallCount.SetText(""+ ballsInBasket +" / "+ ballsRequired);
    }

    public async Task WaitforSeconds(float duration)
    {
        var end = Time.time + duration;
        while(Time.time < end)
            await Task.Yield();
    }
}

