using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    public GameObject[] PanelList;
    private GameObject _activePanel;
    private GameObject _prevPanel;
    public Slider MusicVolumeSlider;
    public Slider SfxVolumeSlider;
    public Text SfxValue;
    public Text MusicValue;

    void Start()
    {
        PanelList = GameObject.FindGameObjectsWithTag("Panel");
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
            if (_activePanel !=null && _activePanel.name == "PausePanel" && _activePanel.activeInHierarchy)
            {
                SwapActivePanel("");
            }
            else if(_activePanel == null)
            {
                foreach (var o in PanelList)
                {
                    if (o.name == "PausePanel")
                    {
                        _activePanel = o;
                        _activePanel.SetActive(true);
                    }
                }
            }
        }
    }

    public void MusicVolumeUpdate()
    {
        MusicValue.text = MusicVolumeSlider.value.ToString();
    }

    public void SfxVolumeUpdate()
    {
        SfxValue.text = SfxVolumeSlider.value.ToString();
    }
    
    public void PlayButtonTrigger()
    {
        SwapActivePanel("");
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
        SwapActivePanel("");
    }

    public void QuitButtonTrigger()
    {
        SwapActivePanel("MenuPanel");
    }
    
    void SwapActivePanel(string panelName)
    {
        CheckForActivePanel();
        if (panelName == "")
        {
            _prevPanel = _activePanel;
            _activePanel.SetActive(false);
            _activePanel = null;
        }
        else if (panelName == "Back")
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
                if (o.name == panelName)
                {
                    _activePanel.SetActive(false);
                    _prevPanel = _activePanel;
                    _activePanel = o;
                    _activePanel.SetActive(true);
                }
            }
        }
    }

    void CheckForActivePanel()
    {
        if (PanelList == null)
            Debug.LogError("No Panels Found");
        foreach (var o in PanelList)
        {
            if (_activePanel != o && o.activeInHierarchy)
                _activePanel = o;
        }
    }
}
