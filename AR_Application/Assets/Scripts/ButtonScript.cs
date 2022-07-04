using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Button btn;
    void Start()
    {
        btn.onClick.AddListener(BtnCallback);
    }

    void BtnCallback() {
        Debug.Log("tesssst");
        UIDebugText.WriteLog("testingggg button!!");
        // GameObject go = GameObject.Find("/underworld");
        var objects = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (var obj in objects)
        {
            if (obj.name == "underworld")
            {
                obj.SetActive(!obj.activeSelf);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
