using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBehaviour", menuName = "ScriptableObjects/PlayerBehaviour_SO", order = 1)]
public class PlayerBehaviour : ScriptableObject
{
    public float minXClamp;
    public float maxXClamp;
    public float forwardSpeed;
    public float horizontalSpeed;
    [Range(0f, .99f)]
    public float horizontalDamping;
}
