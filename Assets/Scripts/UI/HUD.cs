using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ballText;
    [SerializeField] TextMeshProUGUI roundText;
    [SerializeField] GameObject scene;

    const string RoundPrefix = "Round: ";
    const string BallPrefix = "Ball: x";

    // Start is called before the first frame update
    void Start()
    {

        ballText.text = BallPrefix + GameManager.Instance.NumOfBalls;
        roundText.text = RoundPrefix + GameManager.Instance.Round;
    }


    void Update()
    {
        ballText.text = BallPrefix + GameManager.Instance.NumOfBalls;
        roundText.text = RoundPrefix + GameManager.Instance.Round;
    }
}
