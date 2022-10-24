using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    #region PublicMethod
    void Awake()
    {
        GetComponents();
        SetDefaults();
    }

    public void PlayBgm(Bgm bgm)
    {
        m_audioSource.clip = m_bgmClips[(int)bgm];
        m_audioSource.Play();
    }

    public void StopBgm()
    {
        if (m_audioSource.isPlaying)
            m_audioSource.Stop();
    }

    public void PlaySfx(Sfx sfx, float volumeScale = 1f)
    {
        m_audioSource.PlayOneShot(m_sfxClips[(int)sfx], volumeScale);
    }
    #endregion

    #region PublicVariable
    public static SoundManager instance = null;

    public enum Bgm
    {
        MAIN,
    }

    public enum Sfx
    {

    }
    #endregion

    #region PrivateVariable
    [SerializeField]
    AudioClip[] m_bgmClips;
    [SerializeField]
    AudioClip[] m_sfxClips;

    AudioSource m_audioSource;
    #endregion

    #region PrivateMethod
    void GetComponents()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    void SetDefaults()
    {
        instance = this;
    }
    #endregion
}
