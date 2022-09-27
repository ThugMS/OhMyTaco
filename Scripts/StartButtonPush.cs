using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonPush : MonoBehaviour
{
    public void PushButton()
    {
        SceneManager.LoadScene("Fishing");
    }
}
