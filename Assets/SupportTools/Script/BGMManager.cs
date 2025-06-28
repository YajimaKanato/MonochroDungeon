using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    //�V�[���ɍ��킹�Ĉȉ��̃e���v���[�g���g�p
    /*
    [Header("")]
    [Tooltip("�D����BGM��ݒ肵�Ă�������")]
    [SerializeField]
    AudioClip _a;
    */

    //�~�L�T�[���g��������Έȉ���C�ӂ̐��ݒ�
    /*[Header("AudioMixer")]
    [SerializeField]
    AudioMixerGroup _mixer;*/

    [Header("Title")]
    [Tooltip("�D����BGM��ݒ肵�Ă�������")]
    [SerializeField]
    AudioClip _title;

    [Header("Info")]
    [Tooltip("�D����BGM��ݒ肵�Ă�������")]
    [SerializeField]
    AudioClip _info;

    [Header("Credit")]
    [Tooltip("�D����BGM��ݒ肵�Ă�������")]
    [SerializeField]
    AudioClip _credit;

    [Header("Select")]
    [Tooltip("�D����BGM��ݒ肵�Ă�������")]
    [SerializeField]
    AudioClip _select;

    [Header("InGame")]
    [Tooltip("�D����BGM��ݒ肵�Ă�������")]
    [SerializeField]
    AudioClip _inGame;

    static AudioSource _audioSource;
    private static BGMManager _instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_instance == null)//�V���O���g������
        {
            _instance = this;
            _audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
            //�V�[�����؂�ւ�����Ƃ��i���[�h�������j��BGM���ύX�����
            SceneManager.sceneLoaded += BGMChange;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// �V�[�����؂�ւ�����Ƃ��i���[�h�������j��BGM��ύX����
    /// </summary>
    /// <param name="scene"> �ύX��̃V�[���̖��O</param>
    /// <param name="loadScene"> �V�[���̓ǂݍ��݃��[�h�iSingle��Additive�j</param>
    void BGMChange(Scene scene, LoadSceneMode loadScene)
    {
        if (scene.name == "Title")//�V�[���̖��O��]���ɐݒ�
        {
            ChangeBGM(_title,"Title");//��������AudioClip�i��Őݒ�j�ƃV�[���̖��O�������ɐݒ�
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
    /// �V�[�����؂�ւ�����Ƃ���BGM���؂�ւ��
    /// </summary>
    /// <param name="clip"> �Đ�����N���b�v</param>
    /// <param name="name"> �Đ�����V�[���̖��O</param>
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
            Debug.LogWarning("AudioSource���ݒ肳��Ă��܂���");
            return;
        }
    }
}
