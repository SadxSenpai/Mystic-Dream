using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAnimationStrings", menuName = "ScriptableObjects/PlayerAnimationStrings", order = 1)]
public class PlayerAnimationStrings : ScriptableObject
{
    public string idle, walk, attack, hit, die;
    public string moveX, moveY;
}
