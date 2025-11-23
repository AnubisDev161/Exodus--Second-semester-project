using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public static PlayerInputController Instance { get; private set; }
    [SerializeField] private InputActionAsset inputActionAsset;
    public static Action<bool> inTutorial;

    // Player Map Action
    private InputActionMap playerMap;
 
    
    // All Actions
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction lookAction;
    private InputAction shootAction;
    private InputAction sprintAction;
    private InputAction reloadAction;
    private InputAction interact;
    private InputAction switchWorld;
    private InputAction openPauseMenu;

    // Getter
    public InputAction MoveAction => moveAction;
    public InputAction JumpAction => jumpAction;
    public InputAction LookAction => lookAction;
    public InputAction ShootAction => shootAction;
    public InputAction SprintAction => sprintAction;
    public InputAction ReloadAction => reloadAction;
    public InputAction Interact => interact;
    public InputAction SwitchWorld => switchWorld;
    
    public InputAction OpenPauseMenu => openPauseMenu;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        InitializeInput();

        Debug.Log("jump action enabled" + jumpAction.enabled);
    

        // player map needs to be disabled after a short elay, for some reaseon the player map is enabled again by something else after being disabled
        //StartCoroutine(DisablePlayerActionMapAfterDelay());
    }
    
    private void OnEnable()
    {
        playerMap.Enable();
        TutorialHandler.enablePlayerInputMap += OnEnablePlayerInputMap;
        
    }

    private void OnDisable()
    {
        playerMap.Disable();
        TutorialHandler.enablePlayerInputMap -= OnEnablePlayerInputMap;
    }

    private void InitializeInput()
    {
        playerMap = inputActionAsset.FindActionMap("Player");
        moveAction = playerMap.FindAction("Move");
        jumpAction = playerMap.FindAction("Jump");
        lookAction = playerMap.FindAction("Look");
        shootAction = playerMap.FindAction("Shoot");
        sprintAction = playerMap.FindAction("Sprint");
        reloadAction = playerMap.FindAction("Reload");
        interact = playerMap.FindAction("Interact");
        switchWorld = playerMap.FindAction("SwitchWorld");
        openPauseMenu = playerMap.FindAction("Open_Pause_Menu");
    }

    public IEnumerator DisablePlayerActionMapAfterDelay()
    {
        yield return new WaitForSeconds(0.05f);
        playerMap.Disable();
    }

    public void OnEnablePlayerInputMap(bool enable)
    {
        if(enable)
        {
            playerMap.Enable();
            inTutorial?.Invoke(false);
        }
        else
        {
            playerMap.Disable();
            inTutorial?.Invoke(true);
            StartCoroutine(DisablePlayerActionMapAfterDelay());
        }
    }
}
