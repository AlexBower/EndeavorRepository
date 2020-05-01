using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayer : MovingObject
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    virtual public void TakeTurn()
    {
        
    }

    override protected void MoveSuccessfull()
    {
        base.MoveSuccessfull();
    }

    private void MoveLeft()
    {
        Move(-1, 0, out RaycastHit2D hit);
    }
}
