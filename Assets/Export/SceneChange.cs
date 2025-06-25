using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    [Header("Fade")]
    [SerializeField]
    Image _fade;

    public void ChangeScene(string name)
    {
        if (_fade != null)
        {
            _fade.gameObject.SetActive(true);
            StartCoroutine(FadeStayCoroutine(name));
        }
        else
        {
            SceneManager.LoadScene(name);
        }
    }

    IEnumerator FadeStayCoroutine(string name)
    {
        yield return new WaitForSeconds(1.0f);//フェードインアウトに合わせる
        SceneManager.LoadScene(name);
    }
}
