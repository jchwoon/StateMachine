using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadGameScene()
    {
        // "GameScene"으로 씬 전환
        SceneManager.LoadScene("SampleScene");
    }
}
