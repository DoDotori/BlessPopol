using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sc_Loading_Scene: MonoBehaviour {

    public static string m_strNextScene;
    private UISprite m_spProgressBar;

    void Awake()
    {
        m_spProgressBar = GameObject.Find("ProgressBar").GetComponent<UISprite>();
        m_spProgressBar.fillAmount = 0.0f;
        StartCoroutine(Loading());
    }

    public static void Loading(string _SceneName)
    {
        m_strNextScene = _SceneName;
        SceneManager.LoadScene("Loading_Scene");
    }

    IEnumerator Loading()
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(m_strNextScene);
        op.allowSceneActivation = false;

        float timer = 0.0f;

        while (!op.isDone)
        {
            yield return null;

            timer += Time.deltaTime;

            if (op.progress >= 0.9f)
            {
                m_spProgressBar.fillAmount = Mathf.Lerp(m_spProgressBar.fillAmount, 1f, timer);

                if (m_spProgressBar.fillAmount == 1.0f)
                    op.allowSceneActivation = true;
            }
            else
            {
                m_spProgressBar.fillAmount = Mathf.Lerp(m_spProgressBar.fillAmount, op.progress, timer);
                if (m_spProgressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
        }
    }
}
