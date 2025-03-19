using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartMenu()
    {
        //为了加入下一场景，开端界面放在0，游戏界面放在1
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
