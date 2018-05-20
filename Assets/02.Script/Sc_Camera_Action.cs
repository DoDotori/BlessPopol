using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Camera_Action : MonoBehaviour {
    public Camera m_camTopView;
	 
    public void UserGrid_Magnify()
    {
        StartCoroutine(Camera_Magnify());
    }

    IEnumerator Camera_Magnify()
    {
        Vector3 camera_position = m_camTopView.transform.position;
        Vector3 target_position = new Vector3(-60, 150, 0);
        float camera_size = m_camTopView.orthographicSize;
        float target_size = 60;
        float time = 0;
        bool isCamera_action = true;
        while(isCamera_action)
        {
            if(time >= 1.0f)
            {
                isCamera_action = false;
            }
            m_camTopView.transform.position = Vector3.Lerp(camera_position, target_position, time);
            m_camTopView.orthographicSize = Mathf.Lerp(camera_size, target_size,time);

            time += 0.04f;
            yield return new WaitForSeconds(0.015f);
        }
    }

}
