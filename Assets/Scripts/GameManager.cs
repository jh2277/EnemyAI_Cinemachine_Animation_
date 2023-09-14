using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int itemCount;
    public int totalCount;

    //텍스트 컴퍼넌트 만들기
    public TMP_Text currentText;
    public TMP_Text totalText;
    //스테이지
    public int stage;
    public bool finalStage;

    void Start() //첫 프레임에 실행
    {
        itemCount = 0; //알아서 초기화 되긴 함. 혹시 모르니.
    }

    private void Awake() //인스턴스 활성화 됬을 때 실행
    {
        totalText.text = "/ " + totalCount;
    }

    public void GetItem(int count)
    {
        currentText.text = count.ToString(); //현재 
    }
    // Update is called once per frame
    
    public void NextStage()
    {
        if (itemCount == totalCount)
        {
            if (!finalStage)
            {
               SceneManager.LoadScene(stage + 1);
            }
            else
            {
                ExitGame();
            }
        }
        else
        {
            SceneManager.LoadScene(stage);
        }
    }

    

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif

    }
}