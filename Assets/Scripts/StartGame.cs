using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartMenu()
    {
        //Ϊ�˼�����һ���������˽������0����Ϸ�������1
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
