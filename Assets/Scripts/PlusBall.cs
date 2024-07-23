using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusBall : Entity
{
    public override void Update()
    {
        base.Update();
        if (transform.position.y <= 1) Destroy(gameObject);
    }

    public void AddBall() { 
        sc.AddPlusBalls();
        Destroy(gameObject);
    }


}
