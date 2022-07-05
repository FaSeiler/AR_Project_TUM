using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPrefsManager : MonoBehaviour
{
    public void UnlockLevelById(int id)
    {
        PlayerPrefs.SetInt(id.ToString(), 1);
    }

    public int GetNrOfUnlockedLevels()
    {
        int nr = 0;
        nr += PlayerPrefs.GetInt(0.ToString(), 0);
        nr += PlayerPrefs.GetInt(1.ToString(), 0);
        nr += PlayerPrefs.GetInt(2.ToString(), 0);
        nr += PlayerPrefs.GetInt(3.ToString(), 0);
        nr += PlayerPrefs.GetInt(4.ToString(), 0);

        return nr;
    }
}
