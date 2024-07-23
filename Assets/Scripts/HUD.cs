using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ballText;
    [SerializeField] TextMeshProUGUI roundText;
    [SerializeField] GameObject scene;

    SceneScripts sc;

    const string RoundPrefix = "Round: ";
    const string BallPrefix = "Ball: x";

    // Start is called before the first frame update
    void Start()
    {
        sc = scene.GetComponent<SceneScripts>();

        ballText.text = BallPrefix + sc.NumOfBalls;
        roundText.text = RoundPrefix + sc.Round;
    }


    void Update()
    {
        ballText.text = BallPrefix + sc.NumOfBalls;
        roundText.text = RoundPrefix + sc.Round;
    }
}
