using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{


    [SerializeField] Transform shootPos;
    [SerializeField] BallController ball;
    [SerializeField] GameObject spawner;
    [SerializeField] private float power;
    [SerializeField] private bool isGameEnded;
    private int numOfBalls;
    private int plusBalls;
    private int count;
    private int hasDes;
    private int thisRound;
    private int prevRound;

    Timer ballTimer;
    Spawner spawn;
    Vector3 throwVector;
    Vector3 startPoint;
    Vector3 endPoint;

    Vector3 s_pos;

    public TrajectoryLine tl;
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // Find the GameManager in the scene
                _instance = FindObjectOfType<GameManager>();

                // If no GameManager is found, create a new one
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("GameManager");
                    _instance = singletonObject.AddComponent<GameManager>();

                    // Optionally, mark the GameManager to persist between scenes
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        // If another instance of GameManager exists, destroy it
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        InitNewGame();
    }

    #region Fields


    public bool isGameEnd { 
        get { return isGameEnded; }
        set { isGameEnded = value; }
    }

    public int Des { 
        get { return hasDes; }
        set { hasDes = value; }
    }

    public int NumOfBalls
    {
        get { return numOfBalls; }
        set { numOfBalls = value; }
    }
    
    public int Round { 
        get { return thisRound; }
    }

    public int PlusBalls {
        get { return plusBalls; }
        set { plusBalls = value; }
    }

    public bool RoundEnded
    {
        get { return !(prevRound == thisRound); }
    }

    #endregion


    void Start()
    {
        spawn = spawner.GetComponent<Spawner>();
        ballTimer = gameObject.AddComponent<Timer>();
        tl = shootPos.GetComponent<TrajectoryLine>();
        ballTimer.Duration = 0.2f;
        thisRound = 1;
    }

    void Update()
    {
        switch (RoundEnded) {
            case true:
                if (!PauseMenu.isGamePaused &&!isGameEnd) DragAndShoot();
                break;

            case false:
                if (count >= NumOfBalls && Des == NumOfBalls)
                {
                    ++thisRound;
                    count = 0;
                    Des = 0;
                    ballTimer.Stop();
                    AddBall();
                    spawn.Initialize();
                }

                if (ballTimer.Finished && count < NumOfBalls && Des < NumOfBalls)
                {
                    ++count;
                    ThrowBall();
                    ballTimer.Run();
                }

                break;
        }
    }

    #region Methods

    private void InitNewGame() {
        isGameEnded = false;
        s_pos = Vector3.zero;
        numOfBalls = 1;
        plusBalls = 0;
        count = 0;
        hasDes = 0;
        thisRound = 0;
        prevRound = 0;

        ObjectPool.ClearAllPools();
        SetupPool();
    }
    private void SetupPool() {
        ObjectPool.SetupPool(ball, 10, "Ball");
    }

    void CalculateThrowVector(Vector3 startPos)
    {
        Vector3 dragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 distance = (startPos.y == 0)? dragStartPos - shootPos.transform.position : startPos - shootPos.transform.position;
        throwVector = distance.normalized;
    }

    void DragAndShoot() {

        if (Input.GetMouseButtonDown(0))
        {
            startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPoint.z = 15;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 currentPoints = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPoints.z = 15;
            s_pos = (shootPos.transform.position + (startPoint - currentPoints));
            tl.RenderLine(s_pos, shootPos.transform.position);
        }

        if (Input.GetMouseButtonUp(0))
        {
            endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPoint.z = 15;
            tl.EndLine();


            ++count;
            ++prevRound;
            CalculateThrowVector(s_pos);
            ThrowBall();
            ballTimer.Run();
        }
    }

    void ThrowBall()
    {
        //Debug.Log("Shooted");
        var b = ObjectPool.DequeueObject<BallController>("Ball");
        b.gameObject.transform.position = shootPos.position;
        b.gameObject.SetActive(true);
        b.GetComponent<Rigidbody2D>().AddForce(new Vector2(throwVector.x, throwVector.y).normalized * power, ForceMode2D.Impulse);
    }

    public void AddBall() {
        NumOfBalls += PlusBalls;
        PlusBalls = 0;
    }

    public void AddPlusBalls() {
        PlusBalls++;
    }
    #endregion
}
