using UnityEngine;
using UnityEngine.Audio;

public class SEManager : MonoBehaviour
{
    //SEごとに以下のテンプレートを使用
    /*[Header("")]
    [SerializeField]
    AudioClip a;*/

    [Header("AudioMixer")]
    [SerializeField]
    AudioMixerGroup mixer;

    AudioSource[] audioSource;
    private static SEManager instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = new AudioSource[20];
            for (int i = 0; i < audioSource.Length; i++)
            {
                audioSource[i] = gameObject.AddComponent<AudioSource>();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    /// <summary>
    /// 引数のAudioSourceにミキサーを設定
    /// </summary>
    /// <param name="source"></param>
    void SetMixer(AudioSource source)
    {
        source.outputAudioMixerGroup = mixer;
    }

    /// <summary>
    /// 未使用のAudioSourceを取得
    /// </summary>
    /// <returns></returns>
    AudioSource GetUnusedAudioSource()
    {
        Debug.Log("getUnused");
        foreach (var audiosource in audioSource)
        {
            if (audiosource.isPlaying == false)
            {
                return audiosource;
            }
        }
        return null;
    }

    
}
