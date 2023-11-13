using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public int heart;
    [SerializeField] private GameObject[] heartImage;


    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public AudioSource exitAudio;
    public CanvasGroup caughtBackgorundImageCanvasGroup;
    public AudioSource caughtAudio;
    public CanvasGroup overBackgroundImageCanvasGroup;

    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    bool m_IsPlayerOver;
    float m_Timer;
    bool m_HasAudioPlayed;

    private void Awake()
    {
        DontDestroyOnLoad(heartImage[0]);
    }


    private void Update()
    {
        if (m_IsPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup,false,exitAudio);
        }
        else if(m_IsPlayerCaught)
        {
            EndLevel(caughtBackgorundImageCanvasGroup, true,caughtAudio);
        }
        //else if(m_IsPlayerOver)
        //{
        //    EndLevel(overBackgroundImageCanvasGroup, false, caughtAudio);
        //}
    }

    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if(!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }

        m_Timer += Time.deltaTime;
        imageCanvasGroup.alpha = m_Timer / fadeDuration;
        if (m_Timer > fadeDuration + displayImageDuration)
        {

            if (doRestart)
            {
                //player.transform.position = new Vector3(-9.8f, 0, -3.2f);
                SceneManager.LoadScene(0);
            }
            else
            {
                Application.Quit();
            }
        }
    }

    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            m_IsPlayerAtExit = true;
        }
    }

    private void ReduceHeart()
    {
        heartImage[heart-1].SetActive(false);
        heart--;

        if(heart == 0)
        {
            m_IsPlayerOver = true;
        }
    }
    

}
