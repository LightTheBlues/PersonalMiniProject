using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ExperienceBar : MonoBehaviour
{

    public Slider slider;
    public Image fill;
    public Text experienceText;


    public void NextLevel(int experience)
    {
        slider.maxValue = experience;
        UpdateExperienceText();
    }

    public void SetExperience(int CurrentExperience)
    {
        slider.value = CurrentExperience;
        UpdateExperienceText();
    }

    private void UpdateExperienceText()
    {
        if (experienceText != null)
        {
            int CurrentExperience = Mathf.RoundToInt(slider.value);
            int experience = Mathf.RoundToInt(slider.maxValue);
            experienceText.text = CurrentExperience + " / " + experience;
        }
    }
}
