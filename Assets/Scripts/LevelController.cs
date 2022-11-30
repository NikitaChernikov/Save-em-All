using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelController : MonoBehaviour
{
    [SerializeField] private bool isMenu;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject loseUI;
    [SerializeField] private Text scoreTextLoseUI;
    [SerializeField] private GameObject[] ui;
    [SerializeField] private Text bestScoreText;
    [SerializeField] private Text moneyText;
    [SerializeField] private GameObject loadingCanvas;
    [SerializeField] private GameObject loadingText;
    [SerializeField] private GameObject textToContinue;
    [SerializeField] private Slider bar;
    [SerializeField] private GameObject pauseUI;

    private bool isSound = true;
    private byte score = 0;

    private void Start()
    {
        Time.timeScale = 1;
        if (isMenu)
        {
            if (PlayerPrefs.HasKey("BestScore"))
            {
                bestScoreText.text = "BEST SCORE: " + PlayerPrefs.GetInt("BestScore");
            }
            else
            {
                bestScoreText.text = "BEST SCORE: 0";
            }
            if (PlayerPrefs.HasKey("Money"))
            {
                moneyText.text = "MONEY: " + PlayerPrefs.GetInt("Money");
            }
            else
            {
                moneyText.text = "MONEY: 0";
            }
        }
    }

    public void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void OpenStore()
    {
        ui[0].SetActive(false);
        ui[1].SetActive(true);
    }

    public void OpenMainMenu()
    {
        ui[1].SetActive(false);
        ui[0].SetActive(true);
    }

    public void StartGame()
    {
        loadingCanvas.SetActive(true);
        StartCoroutine(WaitScene(1));
    }

    public void ChangeScore()
    {
        score += 1;
        scoreText.text = score.ToString();
    }

    public void RestartGame()
    {
        loadingCanvas.SetActive(true);
        StartCoroutine(WaitScene(SceneManager.GetActiveScene().buildIndex));
    }

    public void MainMenu()
    {
        loadingCanvas.SetActive(true);
        StartCoroutine(WaitScene(0));
    }

    public void GameLost()
    {
        if (PlayerPrefs.HasKey("BestScore"))
        {
            if (score > PlayerPrefs.GetInt("BestScore"))
            {
                PlayerPrefs.SetInt("BestScore", score);
            }
        }
        else
        {
            PlayerPrefs.SetInt("BestScore", score);
        }
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + score);
        loseUI.SetActive(true);
        scoreTextLoseUI.text = score.ToString();
    }

    IEnumerator WaitScene(int index)
    {
        // ��������� ����� �� �������� �������
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(index);
        // ��������� ��������� ����� �����, ���� ���� ��� ������������
        loadScene.allowSceneActivation = false;
        while (!loadScene.isDone)
        {
            //������ �������� = �������� �������� ����� �����
            bar.value = loadScene.progress;
            //���� ����� ��������� �� 90%
            if (loadScene.progress >= .9f)
            {
                loadingText.SetActive(false);
                //�������� �������
                textToContinue.SetActive(true);
                //� ���� ����� ������ ����� ������
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    //������� ������ �� ��������� ����� �����
                    loadScene.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }

}
