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
    //public static Color hoverColor = new Color(255f, 0f, 178f); // «÷ «Œ≈©
    //public static float shakeIntensity = 0.05f;
    public static GameManager Instance { get; private set; }
    public PlayerController playerController;
    public GrabController grabController;
    public PlayerArmController armController;
    public TurretSpawner turretSpawner;
    public PlayerData playerData;
    public WireDashIconController wireDashIconController;
    private void Awake()
    {
        Instance = this;
    }

}
