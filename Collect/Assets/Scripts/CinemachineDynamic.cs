using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CinemachineDynamic : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private void Awake() 
    {
        Camera.main.gameObject.TryGetComponent<CinemachineBrain>(out var brain);
        if(brain == null)   brain = Camera.main.gameObject.AddComponent<CinemachineBrain>();
        
        cinemachineVirtualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();
    }

    private void OnEnable() 
    {
        EventManager.Setcamera += SetFollow;
    }

    private void OnDisable() 
    {
        EventManager.Setcamera -= SetFollow;
    }

    private void Start() => SetFollow();

    private void SetFollow()
    {
        LevelManager level = LevelManager.Instance;
        cinemachineVirtualCamera.Follow = level.currentPlayer;    
    }
}
