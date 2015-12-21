using UnityEngine;
using System.Collections;

public class PlayerInput : Bolt.EntityBehaviour<IPlayer>  {

    public Motor _motor;

    public override void Attached()
    {
        state.SetTransforms(state.Transform, transform);
    }

    public override void SimulateController()
    {
        var input = PlayerCommand.Create();

        if (_motor != null && _motor.RigidBody != null)
        { 

            input.Position = transform.position;
            input.RPM = _motor.RPM;
            input.Rotation = transform.eulerAngles.z;
            input.Velocity = _motor.RigidBody.velocity;
            input.AngularVelocity = _motor.RigidBody.angularVelocity;
        }
        if (Camera.main != null)
        {
            input.MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        } else
        {
            input.MousePosition = Vector2.zero;
        }

        entity.QueueInput(input);
    }

    public override void ExecuteCommand(Bolt.Command command, bool resetState)
    {
        if (_motor != null)
        {
            if (entity.isOwner && _motor.RigidBody != null)
            {
                PlayerCommand cmd = (PlayerCommand)command;

                transform.position = cmd.Input.Position;
                state.MousePosition = cmd.Input.MousePosition;
                state.Movement.RPM = cmd.Input.RPM;
                transform.rotation = Quaternion.Euler(0, 0, cmd.Input.Rotation);
                _motor.RigidBody.velocity = cmd.Input.Velocity;
                _motor.RigidBody.angularVelocity = cmd.Input.AngularVelocity;
            }
        } else
        {
            _motor = GetComponentInChildren<Motor>();
        }
    }
}
