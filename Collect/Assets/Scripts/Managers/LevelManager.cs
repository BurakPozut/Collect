using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance {get; private set;}
    [SerializeField] private int levelIndex;
    [SerializeField] private List<GameObject> LevelPrefabs;
    [HideInInspector] public Transform currentPlayer;   // We need this transform so cinemachine can accsess the which player is active
    private List<LevelData> Levels;
    
    private int currentBasket = 1;

    GameObject levelPrefab;

    private void Awake()    
    {
        Instance = this;

        levelIndex = SaveSystem.LoadData().levelIndex;
        GetLevels();
        GetLevelData();
    } 
    private void Start() => EventManager.SetLevelIndexUI?.Invoke(levelIndex);

    private void OnEnable() 
    {
        EventManager.BasketCheck += LevelFinishedCheck;
        EventManager.NextLevel += NextLevel;
        EventManager.RestartLevel += RestartLevel;
    }

    private void OnDisable() 
    {
        EventManager.BasketCheck -= LevelFinishedCheck;    
        EventManager.NextLevel -= NextLevel;
        EventManager.RestartLevel -= RestartLevel;
    }

    private async void LevelFinishedCheck()
    {
        if(currentBasket == Levels[levelIndex].basketCount) EventManager.SetWin?.Invoke();
        else
        {
            EventManager.SwitchPlayerCanControl.Invoke();
            await WaitforSeconds();
            EventManager.DeactivateRoad.Invoke(currentBasket-1);
            currentBasket++;
        } 
    }

    private void GetLevels()
    {
        Levels = new List<LevelData>();

        foreach(Transform level in this.transform)
        {
            Levels.Add(level.gameObject.GetComponent<LevelData>());
        }
        //Debug.Log("Level count: " + Levels.Count);
    }

    private void GetLevelData()
    {
        currentPlayer = Levels[levelIndex].player;
        Levels[levelIndex].gameObject.SetActive(true);
    }

    private void NextLevel()
    {
        if(levelPrefab != null) Destroy(levelPrefab);

        if(levelIndex + 1 >= Levels.Count)    // If all levels are over we open a random level
        {
            CreateLevelPrefab(true);
           
            return;
        }
        Levels[levelIndex].gameObject.SetActive(false);
        levelIndex++;
        Levels[levelIndex].gameObject.SetActive(true);
        currentBasket = 1;
        GetLevelData();
        EventManager.Setcamera?.Invoke();
        EventManager.SetLevelIndexUI?.Invoke(levelIndex);
        SaveSystem.SaveData(levelIndex);
    }

    // I didn't use a seperate scene for levels because it is not optimal specially in mobile devices
    // So I did this method where you edit and play your levels in the same scene
    // But this method has bring me some troubles about restarting the level
    
    private void RestartLevel()
    {
        currentBasket = 1;
        CreateLevelPrefab(false);
    }

    private async Task WaitforSeconds()
    {
        var end = Time.time + 3.7f;
        while(Time.time < end)  await Task.Yield();
    }

    private void CreateLevelPrefab(bool isRandom)
    {
        Levels[levelIndex].gameObject.SetActive(false);
        if(levelPrefab != null) Destroy(levelPrefab);
        if(isRandom)
        {
            int random = Random.Range(0,Levels.Count-1);
            levelPrefab = Instantiate(LevelPrefabs[random],Vector3.zero,Quaternion.identity);
            EventManager.SetLevelIndexUI?.Invoke(random);
        }
        else
        {
            levelPrefab = Instantiate(LevelPrefabs[levelIndex],Vector3.zero,Quaternion.identity);
            EventManager.SetLevelIndexUI?.Invoke(levelIndex);
        }
        levelPrefab.SetActive(true);
        currentPlayer = levelPrefab.GetComponentInChildren<HorizontalMovement>().gameObject.transform;
        EventManager.Setcamera?.Invoke();
    }
    
    #region Editor
    public void SaveFromEditor()
    {
        SaveSystem.SaveData(levelIndex);
        Debug.Log("Data saved");
    } 
    public void GetLEvelIndex()
    {
        levelIndex = SaveSystem.LoadData().levelIndex;
    }
    #endregion
}
