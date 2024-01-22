using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubMenu : MonoBehaviour
{
    public void backToOptions()
    {
        SceneManager.LoadScene("Options");
    }
}
