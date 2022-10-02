using UnityEngine;
using System;

public class EventManager
{
    #region LevelManagerEvents
    public static Action StartLevel;
    public static Action BasketCheck;
    public static Action NextLevel;
    public static Action RestartLevel;
    public static Action Setcamera;
    #endregion
    
    #region PlayerControlEvents
    public static Action SwitchPlayerCanControl;
    #endregion

    #region GameManager Events
    public static Action SetWin;
    public static Action SetLose;
    #endregion

    public static Action<int> SetLevelIndexUI;
    public static Action<int> DeactivateRoad;
}
