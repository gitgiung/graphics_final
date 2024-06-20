using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startbtn : MonoBehaviour
{
    [SerializeField] Button btnStart;
    // Start is called before the first frame update
    void Start()
    {
        btnStart.onClick.AddListener(()=>{
            SceneManager.LoadScene("Exam06");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
