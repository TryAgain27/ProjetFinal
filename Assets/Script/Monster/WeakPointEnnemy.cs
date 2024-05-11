using UnityEngine;

public class WeakPointEnnemy : Monster
{
    public Monster original;

    protected override void FixedUpdate()
    {
        rb.velocity = Vector3.zero;

    }

    public override void getHit(float value)
    {
        original.getHit(value * 5);
    }
}
