using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HPBarController : MonoBehaviour
{
    public PlayerController playerController;
    public HPBarStateMachine StateMachine { get; private set; }
    public Animator animator { get; private set; }

    //public PlayerHealth playerHealth;

    public PlayerHealth playerHealth { get; private set; }
    #region States
    public HPBarIdleState IdleState { get; private set; }
    public HPBarDamagedState DamagedState { get; private set; }
    public HPBarWaitForRecoveryState WaitForRecoveryState { get; private set; }
    public HPBarRecovery RecoveryState { get; private set; }
    public HPBarRecoveryIdleState RecoveryIdleState { get; private set; }
    public HPBarTransitionToIdleState TransitionToIdleState { get; private set;}

    #endregion

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
        StateMachine = new HPBarStateMachine();
        animator = GetComponent<Animator>();
        
        //playerHealth = playerController.GetComponentInParent<PlayerHealth>();
        //Debug.Log($"HPBar컨트롤러에서의 ID = {playerHealth.GetInstanceID()}");
    }
    private void OnEnable()
    {
        
    }

    private void Start()
    {
        playerHealth = playerController.playerHealth; // 여기 한번 다시봐야함
        //playerHealth = playerController.playerHealth; 
        glowCooltime = new WaitForSeconds(glowCoolTime);
        originalColor = material.color;

        glowOffColor = originalColor;
        glowOnColor = new Color(glowOffColor.r * multiplyFactor, glowOffColor.g * multiplyFactor, glowOffColor.b * multiplyFactor);
        StartCoroutine(StartGlowing());


        IdleState = new HPBarIdleState(this, StateMachine, playerHealth, "idle");
        DamagedState = new HPBarDamagedState(this, StateMachine, playerHealth, "damaged");
        WaitForRecoveryState = new HPBarWaitForRecoveryState(this, StateMachine, playerHealth, "waitForRecovery");
        RecoveryState = new HPBarRecovery(this, StateMachine, playerHealth, "recovery");
        RecoveryIdleState = new HPBarRecoveryIdleState(this, StateMachine, playerHealth, "recoveryIdle");
        TransitionToIdleState = new HPBarTransitionToIdleState(this, StateMachine, playerHealth, "transitionToIdle");

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
}
