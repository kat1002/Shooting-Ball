using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    public SceneScripts sc;
    public bool hasMoved = false;

    // Start is called before the first frame update
    public virtual void Start()
    {
        sc = GameObject.FindGameObjectWithTag("PlayGround").GetComponent<SceneScripts>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (sc.RoundEnded && !hasMoved)
        {
            goDown();
            hasMoved = true;
        }

        if (!sc.RoundEnded) hasMoved = false;
    }
    public void goDown()
    {
        transform.position += new Vector3(0, -1, 0);
    }
}
