using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class UIBehaviour : MonoBehaviour
{
    #region Variables

    public Player_Data PlayerData;
    public GameObject MenuPanelObject;
    public GameObject PausePanelObject;
    public GameObject OptionsPanelObject;
    public GameObject HudPanelObject;
    public GameObject StartUpPanelObject;
    public GameObject[] HealthBar;
    public Slider MusicVolumeSlider;
    public Slider SfxVolumeSlider;
    public Text SfxValue;
    public Text MusicValue;
    public Text GemFragValue;
    public Text FullGemValue;
    public Sprite FullHeart;
    public Sprite EmptyHeart;
    public FloatVariable SavedMusicVolume;
    private int _curHealth;
    private GameObject _prevPanelObject;
    private GameObject _activePanelObject;
    private List<GameObject> _panels;

    #endregion
    void Awake()
    {
        MusicVolumeSlider.value = SavedMusicVolume.Value * 100;
        MusicValue.text = MusicVolumeSlider.value.ToString();
    }

    void Start()
    {
        _panels = new List<GameObject> { HudPanelObject, MenuPanelObject, OptionsPanelObject, PausePanelObject };
        _panels.ForEach(x => x.SetActive(false));
        _curHealth = HealthBar.Length;
        StartUpPanelObject.SetActive(true);
        _activePanelObject = MenuPanelObject;
        GemFragValue.text = PlayerData.gemFragments.ToString();
        FullGemValue.text = PlayerData.lifeGems.ToString();
    }
    void Update()
    {
        CheckForActivePanel();
        
            
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            if (OptionsPanelObject.activeSelf)
            {
                OptionsPanelObject.SetActive(false);
                HudPanelObject.SetActive(true);
            }

            else if (HudPanelObject.activeSelf)
            {
                HudPanelObject.SetActive(false);
                PausePanelObject.SetActive(true);
                Time.timeScale = 0;
            }
            else if (PausePanelObject.activeSelf)
            {
                PausePanelObject.SetActive(false);
                HudPanelObject.SetActive(true);
                Time.timeScale = 1;
            }
        }
    }

    #region SettingUpdates
    public void MusicVolumeUpdate()
    {
        MusicValue.text = MusicVolumeSlider.value.ToString();
        SavedMusicVolume.Value = MusicVolumeSlider.value * 1 / 100;
    }
    public void SfxVolumeUpdate()
    {
        SfxValue.text = SfxVolumeSlider.value.ToString();
    }
    #endregion
    #region MenuNavigation
    public void PlayButtonTrigger()
    {
        CheckForActivePanel();
        Time.timeScale = 1;
        _activePanelObject.SetActive(false);
        _prevPanelObject = _activePanelObject;
        HudPanelObject.SetActive(true);
    }

    public void OptionButtonTrigger()
    {
        CheckForActivePanel();
        _activePanelObject.SetActive(false);
        _prevPanelObject = _activePanelObject;
        OptionsPanelObject.SetActive(true);
    }

    public void BackButtonTrigger()
    {
        CheckForActivePanel();
        _activePanelObject.SetActive(false);
        _prevPanelObject.SetActive(true);
        _prevPanelObject = _activePanelObject;
    }

    public void ResumeButtonTrigger()
    {
        CheckForActivePanel();
        Time.timeScale = 1;
        _activePanelObject.SetActive(false);
        _prevPanelObject = _activePanelObject;
        HudPanelObject.SetActive(true);
    }

    public void QuitButtonTrigger()
    {
        CheckForActivePanel();
        Time.timeScale = 1;
        if (_activePanelObject == PausePanelObject)
        {
            _activePanelObject.SetActive(false);
            _prevPanelObject = _activePanelObject;
            MenuPanelObject.SetActive(true);
        }
        else if (_activePanelObject == MenuPanelObject)
        {
            Application.Quit();
        }
    }

    void CheckForActivePanel()
    {
        foreach (var panel in _panels)
        {
            if (panel.activeSelf)
                _activePanelObject = panel;
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

        if (int.Parse(FullGemValue.text) <= 0 || _curHealth > 0) return;
        foreach (var o in HealthBar)
        {
            o.GetComponent<Image>().sprite = FullHeart;
        }

        var decrementFullGem = int.Parse(FullGemValue.text) - 1;
        FullGemValue.text = decrementFullGem.ToString();
        _curHealth = 4;
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
    #region UpdatingGems
    public void GainGemFrag(Object[] args)
    {
        if (int.Parse(GemFragValue.text) >= 99)
        {
            GemFragValue.text = "0";
            IncrementGemText(FullGemValue);
        }
        else if (int.Parse(FullGemValue.text) >= 3)
            IncrementGemText(GemFragValue);
    }


    public void IncrementGemText(Text textToUpdate)
    {
        textToUpdate.text = (int.Parse(textToUpdate.text) + 1).ToString();
    }
    #endregion
}
