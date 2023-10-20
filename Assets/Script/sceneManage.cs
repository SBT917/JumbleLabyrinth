using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManage : MonoBehaviour
{
    private int count;
    [SerializeField] private GameObject tutorialCanvas;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            count++;
            tutorialCanvas.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.S)&&count>1)
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
