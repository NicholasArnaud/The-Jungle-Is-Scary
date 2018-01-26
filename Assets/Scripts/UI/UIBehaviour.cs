using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    #region Variables
    public GameObject[] PanelList;
    public GameObject[] HealthBar;
    public Slider MusicVolumeSlider;
    public Slider SfxVolumeSlider;
    public Text SfxValue;
    public Text MusicValue;
    public Sprite FullHeart;
    public Sprite EmptyHeart;
    private GameObject _activePanel;
    private GameObject _prevPanel;
    private int _curHealth;
    #endregion

    void Start()
    {
        PanelList = GameObject.FindGameObjectsWithTag("Panel");
        HealthBar = GameObject.FindGameObjectsWithTag("Health");
        _curHealth = HealthBar.Length;
        foreach (var o in PanelList)
        {
            o.SetActive(false);
            if (o.name == "MenuPanel")
            {
                _activePanel = o;
                _activePanel.SetActive(true);
            }
        }
    }

    void Update()
    {
        CheckForActivePanel();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_activePanel.name == "HudPanel")
            {
                SwapActivePanel("PausePanel");
            }
            else if (_activePanel.name == "PausePanel")
            {
                SwapActivePanel("HudPanel");
            }
        }
    }

    #region SettingUpdates
    public void MusicVolumeUpdate()
    {
        MusicValue.text = MusicVolumeSlider.value.ToString();
    }

    public void SfxVolumeUpdate()
    {
        SfxValue.text = SfxVolumeSlider.value.ToString();
    }
    #endregion
    #region MenuNavigation
    public void PlayButtonTrigger()
    {
        SwapActivePanel("HudPanel");
    }

    public void OptionButtonTrigger()
    {
        SwapActivePanel("OptionsPanel");
    }

    public void BackButtonTrigger()
    {
        CheckForActivePanel();
        SwapActivePanel("Back");
    }

    public void ResumeButtonTrigger()
    {
        SwapActivePanel("HudPanel");
    }

    public void QuitButtonTrigger()
    {
        SwapActivePanel("MenuPanel");
    }

    void SwapActivePanel(string panelName)
    {
        if (panelName == "Back")
        {
            if (_activePanel != null) _activePanel.SetActive(false);
            _activePanel = _prevPanel;
            _activePanel.SetActive(true);
            _prevPanel = _activePanel;
        }
        else
        {
            foreach (var o in PanelList)
            {
                if (o.name != panelName) continue;
                _activePanel.SetActive(false);
                _prevPanel = _activePanel;
                _activePanel = o;
                _activePanel.SetActive(true);
            }
        }
    }

    void CheckForActivePanel()
    {
        if (PanelList == null)
            Debug.LogError("No Panels Found");
        else
            foreach (var o in PanelList)
            {
                if (_activePanel != o && o.activeInHierarchy)
                    _activePanel = o;
            }
    }
    #endregion
    #region UpdatingHealth
    public void DamageHealthTrigger()
    {
        if (_curHealth > 0)
        {
            _curHealth--;
            HealthBar[_curHealth].GetComponent<Image>().sprite = EmptyHeart;
        }
    }

    public void HealedHealthTrigger()
    {
        if (_curHealth > 0 && _curHealth < HealthBar.Length)
        {
            _curHealth++;
            HealthBar[_curHealth - 1].GetComponent<Image>().sprite = FullHeart;
        }
    }
    #endregion
}
