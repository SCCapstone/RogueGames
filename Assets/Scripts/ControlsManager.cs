using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsManager : MonoBehaviour
{

    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();



    // Start is called before the first frame update
    void Start()
    {
        LoadKeybinds();
    }

    void LoadKeybinds()
    {
        //theoretically, read in keyCodes and see what to bind it to. 
        keys.Add("Reset", KeyCode.R);
        keys.Add("ZoomOut", KeyCode.Minus);
        keys.Add("ZoomIn", KeyCode.Equals);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keys["Reset"])) {
            //end/delete any current effects that might persist
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        } 
        if (Input.GetKey(keys["ZoomOut"])) {
            Camera.main.orthographicSize = Mathf.Min(9.0f, Camera.main.orthographicSize + 0.1f); 
        }
        if (Input.GetKey(keys["ZoomIn"])) {
            Camera.main.orthographicSize = Mathf.Max(1.0f, Camera.main.orthographicSize - 0.1f);
        }
    }
}
