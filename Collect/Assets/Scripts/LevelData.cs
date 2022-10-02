using UnityEngine;
using System.Collections.Generic;

public class LevelData : MonoBehaviour
{
    public Transform player;
    public List<GameObject> roads;
    public int basketCount;
    private void OnEnable() 
    {
        EventManager.DeactivateRoad += DeactivateRoad;
    }

    private void OnDisable() 
    {
        EventManager.DeactivateRoad -= DeactivateRoad;    
    }

    private void DeactivateRoad(int roadIndex)
    {
        roads[roadIndex].SetActive(false);
    }
}
