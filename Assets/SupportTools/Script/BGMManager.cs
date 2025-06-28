using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    //シーンに合わせて以下のテンプレートを使用
    /*
    [Header("")]
    [Tooltip("好きなBGMを設定してください")]
    [SerializeField]
    AudioClip _a;
    */

    //ミキサーが使いたければ以下を任意の数設定
    /*[Header("AudioMixer")]
    [SerializeField]
    AudioMixerGroup _mixer;*/

    [Header("Title")]
    [Tooltip("好きなBGMを設定してください")]
    [SerializeField]
    AudioClip _title;

    [Header("Info")]
    [Tooltip("好きなBGMを設定してください")]
    [SerializeField]
    AudioClip _info;

    [Header("Credit")]
    [Tooltip("好きなBGMを設定してください")]
    [SerializeField]
    AudioClip _credit;

    [Header("Select")]
    [Tooltip("好きなBGMを設定してください")]
    [SerializeField]
    AudioClip _select;

    [Header("InGame")]
    [Tooltip("好きなBGMを設定してください")]
    [SerializeField]
    AudioClip _inGame;

    static AudioSource _audioSource;
    private static BGMManager _instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_instance == null)//シングルトン処理
        {
            _instance = this;
            _audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
            //シーンが切り替わったとき（ロード完了時）にBGMが変更される
            SceneManager.sceneLoaded += BGMChange;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// シーンが切り替わったとき（ロード完了時）にBGMを変更する
    /// </summary>
    /// <param name="scene"> 変更後のシーンの名前</param>
    /// <param name="loadScene"> シーンの読み込みモード（SingleかAdditive）</param>
    void BGMChange(Scene scene, LoadSceneMode loadScene)
    {
        if (scene.name == "Title")//シーンの名前を評価に設定
        {
            ChangeBGM(_title,"Title");//流したいAudioClip（上で設定）とシーンの名前を引数に設定
        }
        else if (scene.name == "Info")
        {
            ChangeBGM(_info, "Info");
        }
        else if (scene.name == "Credit")
        {
            ChangeBGM(_credit, "Credit");
        }
        else if (scene.name == "Select")
        {
            ChangeBGM(_select, "Select");
        }
        else if (scene.name == "InGame")
        {
            ChangeBGM(_inGame, "InGame");
        }
    }

    /// <summary>
    /// シーンが切り替わったときにBGMが切り替わる
    /// </summary>
    /// <param name="clip"> 再生するクリップ</param>
    /// <param name="name"> 再生するシーンの名前</param>
    void ChangeBGM(AudioClip clip,string name)
    {
        if (_audioSource != null)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
            Debug.Log(name + "BGM");
        }
        else
        {
            Debug.LogWarning("AudioSourceが設定されていません");
            return;
        }
    }
}
