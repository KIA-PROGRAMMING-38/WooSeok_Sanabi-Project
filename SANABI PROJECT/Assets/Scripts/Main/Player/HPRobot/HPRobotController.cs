using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HPRobotController : MonoBehaviour
{
    public PlayerController playerController;
    public HPRobotStateMachine StateMachine { get; private set; }
    public Animator animator { get; private set; }
    public PlayerHealth playerHealth { get; private set; }

    #region States
    public HPRobotIdleState IdleState { get; private set; }
    public HPRobotDamagedState DamagedState { get; private set; }
    public HPRobotWaitForRecoveryState WaitForRecoveryState { get; private set; }
    public HPRobotRecovery RecoveryState { get; private set; }
    public HPRobotRecoveryIdleState RecoveryIdleState { get; private set; }
    public HPRobotTransitionToIdleState TransitionToIdleState { get; private set;}
    

    #endregion

    //public event Action<int> OnRecoveryAnimationDone;

    #region FollowPlayer
    [SerializeField] private Transform targetPos;
    [SerializeField] private float followSpeed = 0.2f;
    private Vector2 velocity = Vector2.zero; // reference speed
    #endregion

    #region Glow
    public Material material;
    private Color originalColor;
    private Color glowOffColor;
    private Color glowOnColor;
    private WaitForSeconds glowCooltime;
    [SerializeField] private float glowCoolTime = 0.25f;
    [SerializeField]private float multiplyFactor = 20f;
    #endregion

    #region Variables

    public bool IsPlayerDamaged { get; set; }

    #endregion
    private void Awake()
    {
        StateMachine = new HPRobotStateMachine();
        animator = GetComponent<Animator>();
        //playerHealth = GameManager.Instance.playerController.playerHealth;

        //playerHealth = playerController.GetComponentInParent<PlayerHealth>();
        //Debug.Log($"HPBar컨트롤러에서의 ID = {playerHealth.GetInstanceID()}");
    }
    private void OnEnable()
    {
        
    }

    private void Start()
    {
        //playerHealth = playerController.playerHealth; // 여기 한번 다시봐야함
        playerHealth = GameManager.Instance.playerController.playerHealth;
        glowCooltime = new WaitForSeconds(glowCoolTime);
        originalColor = material.color;

        glowOffColor = originalColor;
        glowOnColor = new Color(glowOffColor.r * multiplyFactor, glowOffColor.g * multiplyFactor, glowOffColor.b * multiplyFactor);
        StartCoroutine(StartGlowing());


        IdleState = new HPRobotIdleState(this, StateMachine, playerHealth, "idle");
        DamagedState = new HPRobotDamagedState(this, StateMachine, playerHealth, "damaged");
        WaitForRecoveryState = new HPRobotWaitForRecoveryState(this, StateMachine, playerHealth, "waitForRecovery");
        RecoveryState = new HPRobotRecovery(this, StateMachine, playerHealth, "recovery");
        RecoveryIdleState = new HPRobotRecoveryIdleState(this, StateMachine, playerHealth, "recoveryIdle");
        TransitionToIdleState = new HPRobotTransitionToIdleState(this, StateMachine, playerHealth, "transitionToIdle");
        

        StateMachine.Initialize(IdleState);
    }
    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }
    private void LateUpdate()
    {
        transform.position = Vector2.SmoothDamp(transform.position, targetPos.position, ref velocity, followSpeed);
        
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    private IEnumerator StartGlowing()
    {
        while (true)
        {
            material.color = glowOffColor;
            yield return glowCooltime;
            material.color = glowOnColor;
            yield return glowCooltime;
        }
    }

    private void OnDisable()
    {
        material.color = originalColor;
    }


    public void ChangeToWaitState()
    {
        StateMachine.ChangeState(WaitForRecoveryState);
    }

    public void ChangeToRecoveryIdleState()
    {
        StateMachine.ChangeState(RecoveryIdleState);
    }

    public void ChangeToIdleState()
    {
        StateMachine.ChangeState(IdleState);
    }

    public void RecoverHP()
    {
        playerHealth.RecoverHP();
        //OnRecoveryAnimationDone?.Invoke(playerHealth.GetCurrentHp());
    }
}
