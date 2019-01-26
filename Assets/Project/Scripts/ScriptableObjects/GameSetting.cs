using UnityEngine;

[CreateAssetMenu(fileName = "GameSetting", menuName = "CreateGameSetting", order = 51)]
public class GameSetting : ScriptableObject {
    [Header("StartConfig")]
    [Range(100, 1000)]
    public float startForce = 500;
    public Vector2 startVector;
    [Range(3, 7)]
    public float maxVelocityBall = 10;
    [Range(1, 5)]
    public float minVelocityBall = 3;
    public Vector3 StartPositionPlayer = new Vector3(0, -4.9f, 0);
    public Vector3 SpawnPositionBall = new Vector3(0, -4.6f, 0);
    [Header("VariableParameters")]
    public int PointForBasicBlock = 50;
    public int PointForRepeatedlyBlock = 100;
    public int BonusChancePrecentEllongation = 20;
    public Vector3 SizePlayer = new Vector3(1.5f, 0.2f, 0);
    public Vector3 SizePlayerOnElongation = new Vector3(3, 0.2f, 0);
    public int BonusChancePrecentInvulnerability = 5;
    public Vector3 SizeBall = new Vector3(0.4f, 0.4f, 0.4f);
    public Vector3 SizeBallOnInvulnerability = new Vector3(0.6f, 0.6f, 0.6f);
    public int BonusChancePrecentMultiplication = 10;
    public float TimeElongation = 5;
    public float TimeInvulnerability = 3;
}
