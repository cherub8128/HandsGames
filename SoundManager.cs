// TODO SFX 딕셔너리화, 싱글턴 대신 정적 또는 중재자패턴으로
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip[] BGM_Clip;
    [SerializeField] AudioClip[] SFX_Clip;
    [SerializeField] int channel;
    private AudioSource BGM;
    private AudioSource[] SFX;


    public static SoundManager Instance { get; private set; } = null;
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
    private void Start()
    {
        SFX = new AudioSource[channel];
        for (int i =0; i<channel; i++) SFX[i] = gameObject.AddComponent<AudioSource>();
        BGM = gameObject.AddComponent<AudioSource>();
        BGM.loop = true;
        insertBGM(0);
        playBGM();
    }
    public void insertBGM(int Clip)
    {
        BGM.clip = BGM_Clip[Clip];
    }
    public void insertSFX(int Source, int Clip)
    {
        SFX[Source].clip = SFX_Clip[Clip];
    }
    public void playSFX(int Source)
    {
        SFX[Source].Play();
    }
    public void playBGM()
    {
        BGM.Play();
    }
    public void modifyBGM(float pitch)
    {
        BGM.pitch = pitch;
    }
    public void muteSound()
    {
        BGM.volume = 0f;
        foreach(AudioSource audio in SFX)
        {
            audio.volume = 0f;
        }
    }
    public void unmuteSound()
    {
        BGM.volume = 1f;
        foreach(AudioSource audio in SFX)
        {
            audio.volume = 1f;
        }
    }
}
