using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMChanger : MonoBehaviour
{
    GameObject bgmManager;
    BGMManager bgm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bgmManager = GameObject.FindGameObjectWithTag("BGMManager");
        if (bgmManager != null)
        {
            bgm = bgmManager.GetComponent<BGMManager>();
        }
        BGMChange();
    }

    void BGMChange()
    {
        if (bgm != null)
        {
            if (SceneManager.GetActiveScene().name == "Title")
            {
                bgm.TitleBGM();
            }
            else if (SceneManager.GetActiveScene().name == "Info")
            {
                bgm.InfoBGM();
            }
            else if (SceneManager.GetActiveScene().name == "Credit")
            {
                bgm.CreditBGM();
            }
            else if (SceneManager.GetActiveScene().name == "Select")
            {
                bgm.SelectBGM();
            }
            else
            {
                bgm.InGameBGM();
            }
        }
    }
}
