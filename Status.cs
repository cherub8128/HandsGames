using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] Sprite[] Hearts;
    [SerializeField] Image Heart1;
    [SerializeField] Image Heart2;
    [SerializeField] Image Heart3;
    [SerializeField] Text ComboText;
    [SerializeField] Text BestComboText;
    [SerializeField] Text ScoreText;
    [SerializeField] Text BestScoreText;
    [SerializeField] string gameName;

    public int Score { get; private set; }
    private int bestScore;
    private int Life = 12;
    public int combo { get; private set; } = 0;
    private int bestCombo = 0;
     
    

    //singleton
    public static Status Instance { get; private set; } = null;
    private void Awake()
    {
        if (Instance== null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void setScore(int i)
    {
        Score += i;
        ScoreText.text = Score.ToString();
        if (Score > bestScore)
        {
            bestScore = Score;
            PlayerPrefs.SetInt($"Game{gameName} Score", bestScore);
            BestScoreText.text = bestScore.ToString();
        }
    }
    public void setCombo(bool isCombo)
    {
        if (isCombo) combo++;
        else combo = 1;
        if (combo > bestCombo) 
        {
            bestCombo = combo;
            PlayerPrefs.SetInt($"Game{gameName} Combo", bestCombo);
            BestComboText.text = bestCombo.ToString();
        }
        ComboText.text = combo.ToString();
        
    }
    public void dealingLife(int i)
    {
        Life = Life > 12 ? 12 : Life + i;
        if (Life < 0) Life = 0;
        Heart1.sprite = Hearts[Life > 3 ? 4 : Life];
        Heart2.sprite = Hearts[Life > 7 ? 4 : (Life - 4 > 0 ? Life - 4 : 0)];
        Heart3.sprite = Hearts[Life < 9 ? 0 : (Life>11? 4 : Life%4)];
        if (Life < 1) gameOver();
    }
    public void gameOver()
    {
        GameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    void Start()
    {
        bestCombo = PlayerPrefs.GetInt($"Game{gameName} Best Score");
        bestScore = PlayerPrefs.GetInt($"Game{gameName} Best Combo");
        BestComboText.text = bestCombo.ToString();
        BestScoreText.text = bestScore.ToString();
    }
}
