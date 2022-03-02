using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //�ܵ�Ԥ����
    public GameObject pipe;
    //���ɹܵ���λ��
    public Transform minYPosObj;
    public Transform maxYPosObj;
    public Transform genXPosObj;
    private float minPipesY;
    private float maxPipesY;
    //���ɹܵ���Ƶ��
    private float genPipesRate = 3.0f;
    //�ܵ���ʼ���ĵ㣬�����Ұ֮��
    private Vector2 initPos = new Vector2(-20.0f, 0f);
    //���б������ɵĹܵ�
    private List<GameObject> pipes = new List<GameObject>();
    //��ʱ��
    private float timer = 0;
    //�ܹ��Ĺܵ���
    private const int pipesTotalCount = 5;
    //��ǰ���б���±�
    public int currIndex = 0;
    //ͨ���Ĺܵ��ĸ���
    public int passPipeCount = 0;

    //��Ϸ����UI
    public GameObject gameOverTip;
    //����UI
    public Text countText;
    //׼��UI
    public GameObject readyTip;

    /// <summary>
    /// ö����Ϸ��״̬
    /// </summary>
    public enum GameState {
        READY, START, PAUSE, OVER,
    }

    [HideInInspector] public GameState gameState = GameState.OVER;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        minPipesY = minYPosObj.position.y;
        maxPipesY = maxYPosObj.position.y;
        InitPipes();
        gameOverTip.SetActive(false);
        gameState = GameState.READY;
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.READY:
                GameReady();
                break;
            case GameState.START:
                GameStart();
                break;
            case GameState.PAUSE:
                GamePause();
                break;
            case GameState.OVER:
                ReStart();
                break;
        }
    }

    /// <summary>
    /// ��Ϸ׼��
    /// </summary>
    private void GameReady()
    {
        readyTip.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            readyTip.SetActive(false);
            gameState = GameState.START;
        }
    }

    /// <summary>
    /// ��Ϸ��ʼ
    /// </summary>
    private void GameStart()
    {

        if(GameState.OVER != gameState && timer <= 0)
        {
            timer = genPipesRate;
            UpdatePipesPos();
            currIndex = (currIndex + 1) % pipesTotalCount;
        }
        if(Input.GetMouseButton(1))
        {
            gameState = GameState.PAUSE;
        }
        timer -= Time.deltaTime;
    }

    /// <summary>
    /// ��Ϸ��ͣ
    /// </summary>
    private void GamePause()
    {
        readyTip.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            readyTip.SetActive(false);
            gameState = GameState.START;
        }    
    }
     
    /// <summary>
    /// ��Ϸ����
    /// </summary>
    public void GameOver()
    {
        if (GameState.OVER == gameState)
            return;
        gameState = GameState.OVER;
        gameOverTip.SetActive(true);
    }

    /// <summary>
    /// ͨ���ܵ�
    /// </summary>
    public void PassPipe()
    {
        passPipeCount++;
        countText.text = "Count : " + passPipeCount.ToString();
    }

    /// <summary>
    /// ������Ϸ
    /// </summary>
    private void ReStart()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// ��ʼ���ܵ��б�
    /// </summary>
    private void InitPipes()
    {
        for(int i = 0; i < pipesTotalCount; i++)
        {
            GameObject newPipe = Instantiate(pipe, initPos, Quaternion.identity);
            pipes.Add(newPipe);
        }
    }

    /// <summary>
    /// ���¹ܵ�λ��
    /// </summary>
    private void UpdatePipesPos()
    {
        float yPos = Random.Range(minPipesY, maxPipesY);
        pipes[currIndex].transform.position = new Vector2(genXPosObj.position.x, yPos);
    }

}
