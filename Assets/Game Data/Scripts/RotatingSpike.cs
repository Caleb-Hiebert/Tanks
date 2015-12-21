using UnityEngine;
using System.Collections;

public class RotatingSpike : Bolt.EntityBehaviour<IPositionState> {

    public float rotateSpeed;

    public override void Attached()
    {
        state.SetTransforms(state.Transform, transform);
    }

    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
}
