using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _Instance { get; protected set; }

    [SerializeField]
    private int m_numberOfAudioSource;

    [SerializeField]
    private List<GameObject> m_audioSourceList;

    [SerializeField]
    private GameObject m_audioSourcePrefab;

    [SerializeField]
    private AudioClip m_hitSound;

    [SerializeField]
    private AudioClip m_explosionSound;

    [SerializeField]
    private AudioClip m_jumpSound;

    [SerializeField]
    private AudioClip m_landSound;

    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
        }
        else if (_Instance != this)
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        GenerateAudioSources();
    }

    private void GenerateAudioSources() 
    {
        for (int i = 0; i < m_numberOfAudioSource; i++) 
        {
            m_audioSourceList.Add(Instantiate(m_audioSourcePrefab, transform));
        }
    }

    public void PlayAudioClip(EAudio_Type audioType, Vector3 pos)
    {
        switch (audioType)
        {
            case EAudio_Type.Hit:
                PlayClip(m_hitSound, pos);
                break;
            case EAudio_Type.Explosion:
                PlayClip(m_explosionSound, pos);
                break;
            case EAudio_Type.Jump:
                PlayClip(m_jumpSound, pos);
                break;
            case EAudio_Type.Land:
                PlayClip(m_landSound, pos);
                break;
            case EAudio_Type.Count:
                break;
        }
    }

    private void PlayClip(AudioClip audioClip, Vector3 pos) 
    {
        for(int i = 0; i < m_audioSourceList.Count; i++) 
        {
            if (!m_audioSourceList[i].GetComponent<AudioSource>().isPlaying) 
            {
                Debug.Log("Audiosource: " + i + "  is playing");
                m_audioSourceList[i].transform.position = pos;
                m_audioSourceList[i].GetComponent<AudioSource>().PlayOneShot(audioClip);
                break;
            }
        }
    }
}

public enum EAudio_Type
{
    Hit,
    Explosion,
    Jump,
    Land,
    Count
}
