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

    //�ؽ�Ʈ ���۳�Ʈ �����
    public TMP_Text currentText;
    public TMP_Text totalText;
    //��������
    public int stage;
    public bool finalStage;

    void Start() //ù �����ӿ� ����
    {
        itemCount = 0; //�˾Ƽ� �ʱ�ȭ �Ǳ� ��. Ȥ�� �𸣴�.
    }

    private void Awake() //�ν��Ͻ� Ȱ��ȭ ���� �� ����
    {
        totalText.text = "/ " + totalCount;
    }

    public void GetItem(int count)
    {
        currentText.text = count.ToString(); //���� 
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