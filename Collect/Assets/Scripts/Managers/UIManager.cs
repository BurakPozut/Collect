using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance{get; private set;}
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private TextMeshProUGUI levelText;
    private void Awake() 
    {
        Instance = this;    
    }
    
    private void OnEnable() 
    {
        EventManager.StartLevel += StartLevel;   
        EventManager.SetWin += GameWon;
        EventManager.SetLose += GameLost;
        EventManager.NextLevel += ClosePanles;
        EventManager.RestartLevel += ClosePanles;
        EventManager.SetLevelIndexUI += SetLevelIndex;
    }

    private void OnDisable() 
    {
        EventManager.StartLevel -= StartLevel;   
        EventManager.SetWin -= GameWon;
        EventManager.SetLose -= GameLost;
        EventManager.NextLevel -= ClosePanles;
        EventManager.RestartLevel -= ClosePanles;
        EventManager.SetLevelIndexUI -= SetLevelIndex;
    }

    private void StartLevel() =>    startPanel.SetActive(false);

    private void GameWon() =>   winPanel.SetActive(true);

    private void GameLost() =>  losePanel.SetActive(true);

    private void ClosePanles()
    {
        winPanel.SetActive(false);
        startPanel.SetActive(true);
        losePanel.SetActive(false);
    }

    private void SetLevelIndex(int index)
    {
        index++;
        levelText.SetText("Level " + index);
    }
}
