using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIEvents : MonoBehaviour
{
    [SerializeField] GameObject[] Objects;
    [SerializeField] GameObject[] Panels;
    [SerializeField] Toggle PauseToggle;
    [SerializeField] GameObject EntireAreaButton, StartEntireAreaButton;
    [SerializeField] Collider2D PlayerArea;
    [SerializeField] private string GameName;
    private bool isPause = false;
    private bool isMute = false;
    public void LoadScene(int n)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(n, LoadSceneMode.Single);
    }
    public void popPanel(int n)
    {
        Panels[n].SetActive(true);
        EntireAreaButton.SetActive(true);
        PauseToggle.isOn = true;
        TouchEvents.isStateIn = true;
    }
    public void closePanel(int n)
    {
        Panels[n].SetActive(false);
        EntireAreaButton.SetActive(false);
        PauseToggle.isOn = false;
        TouchEvents.isStateIn = false;
    }
    public void anyTouchClosePanel()
    {
        foreach (GameObject obj in Panels) obj.SetActive(false);
        EntireAreaButton.SetActive(false);
        PauseToggle.isOn = false;
    }
    public void startAnyTouchClosePanel()
    {
        foreach (GameObject obj in Panels) obj.SetActive(false);
        StartEntireAreaButton.SetActive(false);
        PauseToggle.isOn = false;
        Time.timeScale = 1;
    }
    public void activeSwitch(int n)
    {
        Objects[n].SetActive(!Objects[n].activeSelf);
    }
    public void instantiateTempObject(int n)
    {
        var Instance = Instantiate(Objects[n]);
        Destroy(Instance, 1f);
    }
    public void pause()
    {
        if (!isPause)
        {
            isPause = true;
            Time.timeScale = 0;
        }
        else
        {
            isPause = false;
            Time.timeScale = 1;
        }
        
    }
    public void disable()
    {
        if(!isPause)
        {
            isPause = true;
            Time.timeScale = 0;
            PlayerArea.enabled = false;
        }
        else
        {
            isPause = false;
            Time.timeScale = 1;
            PlayerArea.enabled = true;
        }
    }
    public void playMusic()
    {
        if(!isMute)
        {
            isMute = true;
            SoundManager.Instance.muteSound();
        }
        else
        {
            isMute = false;
            SoundManager.Instance.unmuteSound();
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void ViewLeaderBoard()
    {
        PauseToggle.isOn = true;
    }
    public void Start()
    {
        if (PlayerPrefs.GetInt(GameName) < 2)
        {
            Panels[1].SetActive(true);
            EntireAreaButton.SetActive(true);
            PauseToggle.isOn = true;
            PlayerPrefs.SetInt(GameName, 2);
        }
    }
}
