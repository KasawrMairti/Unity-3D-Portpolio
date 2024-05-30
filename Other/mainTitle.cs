using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainTitle : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private GameObject mainImage;
    [SerializeField] private GameObject titleText;
    [SerializeField] private GameObject mainTitleImage;
    [SerializeField] private TextMeshProUGUI _textStart;
    [SerializeField] private TextMeshProUGUI _textMANUAL;
    [SerializeField] private TextMeshProUGUI _textEXIT;

    [SerializeField] private GameObject o_manual;


    private static bool TestSystem = true;
    // Variable
    private bool b_firstEvent = TestSystem;
    private bool b_secondEvent = TestSystem;
    private bool b_thirdEvent = false;
    private bool b_btnEvent = false;
    private bool b_manualOpened = false;

    private enum BUTTON { START, MANUAL, EXIT }
    private BUTTON button = BUTTON.START;

    private void Start()
    {
        SoundManager.Instance.PlayBGM("Title");

        mainImage.transform.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f + mainImage.GetComponent<RectTransform>().sizeDelta.y);
        titleText.transform.position = new Vector2(Screen.width * 0.5f, -(titleText.GetComponent<RectTransform>().sizeDelta.y + 50.0f));
    }

    private void Update()
    {
        eventManage();

        if (b_btnEvent) btn_Change();
    }

    private void eventManage()
    {
        if (!b_firstEvent) firstEvent();
        if (!b_secondEvent) secondEvent();

        if (b_firstEvent && b_secondEvent && !b_thirdEvent)
        {
            b_thirdEvent = true;
            b_btnEvent = true;

            mainTitleImage.SetActive(true);
            _textStart.gameObject.SetActive(true);
            _textMANUAL.gameObject.SetActive(true);
            _textEXIT.gameObject.SetActive(true);
        }
    }

    private void firstEvent()
    {
        if (mainImage.transform.position.y > Screen.height * 0.5f)
        {
            mainImage.transform.Translate(0.0f, -1.0f * Time.deltaTime * 45.0f, 0.0f);
        }
        else
        {
            b_firstEvent = true;
            mainImage.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0.0f);
        }
    }

    private void secondEvent()
    {
        if (titleText.transform.position.y < (titleText.GetComponent<RectTransform>().sizeDelta.y - 500.0f))
            titleText.transform.Translate(0.0f, 1.0f * Time.deltaTime * 110.0f, 0.0f);
        else
        {
            b_secondEvent = true;
            titleText.transform.position = new Vector3(Screen.width * 0.5f, titleText.GetComponent<RectTransform>().sizeDelta.y, 0.0f);
        }
    }

    private void btn_Change()
    {
        BUTTON buttonIndex = button;

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (button == BUTTON.START) button = BUTTON.MANUAL;
            else if (button == BUTTON.MANUAL) button = BUTTON.EXIT;
            else if (button == BUTTON.EXIT) button = BUTTON.START;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (button == BUTTON.START) button = BUTTON.EXIT;
            else if (button == BUTTON.MANUAL) button = BUTTON.START;
            else if (button == BUTTON.EXIT) button = BUTTON.MANUAL;
        }

        if (buttonIndex != button)
        {
            switch (button)
            {
                case BUTTON.START:
                    _textStart.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
                    _textMANUAL.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    _textEXIT.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    break;
                case BUTTON.MANUAL:
                    _textStart.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    _textMANUAL.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
                    _textEXIT.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    break;
                case BUTTON.EXIT:
                    _textStart.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    _textMANUAL.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    _textEXIT.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
                    break;
                default:
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (button)
            {
                case BUTTON.START:
                    SoundManager.Instance.StopBGM();
                    SceneManager.LoadScene("Field");
                    break;
                case BUTTON.MANUAL:
                    if (!b_manualOpened)
                    {
                        o_manual.SetActive(true);
                        b_manualOpened = true;
                    }
                    else
                    {
                        o_manual.SetActive(false);
                        b_manualOpened = false;
                    }
                    break;
                case BUTTON.EXIT:
                    Application.Quit();
                    break;
                default:
                    break;
            }
        }
    }
}
