using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] arr;
    private int[] obj = {0, 90, 180, 270, 360};
    

    public void Start()
    {
        Initialize();
    }

    public void Update()
    {
        
    }

    public void Initialize() {
        Vector2 pos = transform.position;
        for (int i = -3; i < 4; ++i) {
            pos.x = transform.position.x + i;
            Spawn(Random.Range(0, arr.Length-1), pos);
        }
    }
    public void Spawn(int index, Vector2 pos) {
        Instantiate(arr[index], pos, Quaternion.identity);

        /*
        if (index % 4 == 0) Instantiate(plusBall, pos, Quaternion.identity);
        else if (index % 2 == 0) Instantiate(obs, pos, Quaternion.identity);
        else if (index % 3 == 0) Instantiate(triObs, pos, Quaternion.Euler(180,0,0));
        */
    }
}
