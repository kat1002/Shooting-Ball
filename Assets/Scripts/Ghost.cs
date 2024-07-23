using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Entity
{
    // Start is called before the first frame update
    public override void Start()
    {
        Destroy(gameObject);
    }
}
