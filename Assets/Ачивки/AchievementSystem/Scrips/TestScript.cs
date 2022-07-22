using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{ 

    void Start()
    {
        AchievementSystem.use.OnAchievement += TestOnAchievement;
    }

    void TestOnAchievement(int id, string title, string description)
    {
        Debug.Log(this + " открыта ачивка > идентификатор - [" + id + "] :: заголовок - [" + title + "] :: описание - [" + description + "]");
    }

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Space) && !AchievementSystem.isActive)
        {
            AchievementSystem.use.ShowAchievementList(true);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && AchievementSystem.isActive)
        {
            AchievementSystem.use.ShowAchievementList(false);
        }
    }

    // получение ачивки "десять кликов"
    public void Achievement_1()
    {
        AchievementSystem.use.AdjustAchievement(0, 1);
    }

    // получение ачивки "слайдер"
    public void Achievement_2(Slider slider)
    {
        if (slider.value == slider.maxValue) AchievementSystem.use.AdjustAchievement(1, 1);

    }

    // получение ачивки "клик"
    public void Achievement_3()
    {
        AchievementSystem.use.AdjustAchievement(2, 1);

    }

    public void Save()
    {
        AchievementSystem.use.Save();
    }
}
