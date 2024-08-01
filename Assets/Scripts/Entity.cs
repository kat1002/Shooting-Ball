using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    public bool hasMoved = false;

    // Start is called before the first frame update
    public virtual void Start()
    {
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (GameManager.Instance.RoundEnded && !hasMoved)
        {
            goDown();
            hasMoved = true;
        }

        if (!GameManager.Instance.RoundEnded) hasMoved = false;
    }
    public void goDown()
    {
        transform.position += new Vector3(0, -1, 0);
    }
}
