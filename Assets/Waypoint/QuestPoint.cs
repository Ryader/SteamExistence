using UnityEngine;
using UnityEngine.UI;

public class QuestPoint : MonoBehaviour
{
    public Text QuestText;
    public GameObject[] Objectives;
    public int CurrentObjective = 0;

    #region Singleton
    public static QuestPoint Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            print("Several instances of quest managers found");
            return;
        }
        Instance = this;
    }
    #endregion

    private void Start()
    {
        Waypoint.Instance.UpdateWaypoint(Objectives[CurrentObjective].transform);
        QuestText.text = "Дойдите до точки, используя кнопки W A S D";
    }

    public void UpdateObjective()
    {
        CurrentObjective++;
        if (CurrentObjective < Objectives.Length)
        {
            Waypoint.Instance.enabled = true;
            Waypoint.Instance.UpdateWaypoint(Objectives[CurrentObjective].transform);
        }
        else
        {
            Waypoint.Instance.enabled = false;
        }

        switch (CurrentObjective)
        {
            case 0:
                QuestText.text = "Подойди к глашатаю используя кнопки 'W, A, S, D'";
                break;

            case 1:
                QuestText.text = "Отправляйся к Ди и возьми у нее квест.";
                break;

            case 2:
                QuestText.text = "Видишь оружие? Нажми кнопку 'I' чтоб открыть инвентарь и взять его.";
                break;
            case 3:
                QuestText.text = "Возвращайся к Ди";
                break;
            case 4:
                QuestText.text = "Отрпавляйся домой.";
                break;
        }
    }
}
