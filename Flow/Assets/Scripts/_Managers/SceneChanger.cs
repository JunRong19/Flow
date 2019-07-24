using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void LoadFarmlandScene() {
        SceneManager.LoadScene("FarmLand");
    }

    public void LoadTimerScene() {
        SceneManager.LoadScene("Timer");
    }
}
