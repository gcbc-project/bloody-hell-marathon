using System;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerState : IState
{
    protected PlayerStateMachine _stateMachine;
    protected PlayerMovementHandler _movementHandler;
    protected PlayerAnimeHandler _animeHandler;
    protected PlayerInputHandler _inputHandler;

    protected PlayerState(PlayerStateMachine stateMachine)
    {
        _stateMachine = stateMachine;

        _movementHandler = new PlayerMovementHandler(_stateMachine);
        _animeHandler = new PlayerAnimeHandler(_stateMachine);
        _inputHandler = new PlayerInputHandler(_stateMachine);
    }

    public virtual void Enter()
    {
        _inputHandler.OnGrapPerformedEvent += OnGrapPerformed;
        _inputHandler.AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        _inputHandler.OnGrapPerformedEvent -= OnGrapPerformed;
        _inputHandler.RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void PhysicsUpdate()
    {
        _movementHandler.Movement(_stateMachine.MovementDir);
    }

    public virtual void Update() { }
    private void ReadMovementInput()
    {
        _stateMachine.MovementDir = _stateMachine.Player.Input.PlayerActions.Move.ReadValue<Vector2>();
    }

    private void OnGrapPerformed(InputAction.CallbackContext context)
    {
        _stateMachine.ChangeState(_stateMachine.HoldState);
    }

}
