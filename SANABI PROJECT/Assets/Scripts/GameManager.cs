using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneNumber
{
    Main,
    Settings,
    Exit,
    Boss
}
public class GameManager : MonoBehaviour
{
    //public static Color idleColor = Color.white;
    //public static Color hoverColor = new Color(255f, 0f, 178f); // �� ��ũ
    //public static float shakeIntensity = 0.05f;
    public static GameManager Instance { get; private set; }
    public PlayerController playerController;
    public GrabController grabController;
    public PlayerArmController armController;
    private void Awake()
    {
        Instance = this;
    }

}
