using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    /*
     *  <�̴ϰ��� ���� ����>
     *  MiniGameManager Awake Method (���� 1ȸ)
     *  MiniGame Awake Method (���� 1ȸ)
     *      -> MiniGame�� �ڽĵ鿡���� Awake (���� 1ȸ, GetComponent, new�� �Ҵ� �۾�)
     *  MiniGame SettingBeforeStartGame Method
     *      -> MiniGame�� �ڽĵ鿡�� SetDefaults (���� ������ ������ �⺻ ���õ�, �ʱ�ȭ)
     *  MiniGameManager CountDown Coroutine
     *  MiniGame StartMiniGame Method
     *  
     *  ~~GamePlay~~
     *  
     *  MiniGame EndMiniGame Method
     *  MiniGameManager ShowButtons Method -> �׽�Ʈ�� ��ư ������
     */

    #region PublicMethod
    void Awake()
    {
        instance = this;

        SetDefaults();
    }
    public void MiniGameChooseEvent() // ��ư Ŭ��, �̴ϰ��� ���� �̺�Ʈ, ���ΰ��� ȭ�鿡�� Ŭ���ؼ� ų ����
    {
        SetDefaults();

        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;

        int miniGameIndex = int.Parse(clickedButton.gameObject.name);

        StartCoroutine(StartCountDown(miniGameIndex));
    }

    //for test
    public void ShowButtons()
    {
        testCanvas.gameObject.SetActive(true);
    }

    #endregion

    #region PublicVariable
    public static MiniGameManager instance = null;
    #endregion

    #region PrivateVariable
    [SerializeField] MiniGame[] m_miniGames;
    [SerializeField] Image m_countDownImage;
    [SerializeField] Sprite[] m_countDownSprite;

    [Header("For Test")]
    [SerializeField] Canvas testCanvas;
    #endregion

    #region PrivateMethod
    void SetDefaults()
    {
        m_countDownImage.gameObject.SetActive(false);
    }
    IEnumerator StartCountDown(int _miniGameIdx)
    {
        //for test
        testCanvas.gameObject.SetActive(false);

        MiniGame selectedMiniGame = m_miniGames[_miniGameIdx].GetComponent<MiniGame>();

        // �ε� �ʿ�?
        selectedMiniGame.gameObject.SetActive(true);

        selectedMiniGame.SettingBeforeStartGame();

        yield return new WaitForSeconds(1.5f);

        m_countDownImage.gameObject.SetActive(true);

        for (int i = 0; i < 4; i++)
        {
            m_countDownImage.sprite = m_countDownSprite[i];

            yield return new WaitForSeconds(1);
        }

        m_countDownImage.gameObject.SetActive(false);
        selectedMiniGame.StartMiniGame();
    }
    #endregion
}
