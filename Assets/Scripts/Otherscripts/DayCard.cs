using UnityEngine;

public class DayCard : MonoBehaviour
{
    public void Day()
    {
        GameManager.Instance.DayCard();
    }

    public void DayCardFinnished()
    {
        GameManager.Instance.DayCardFinnished();
    }
}
