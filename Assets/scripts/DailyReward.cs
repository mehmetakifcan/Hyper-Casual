using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyReward : MonoBehaviour
{
    public bool initialized;
    public long rewardGivingTimeTicks;
    public GameObject rewardMenu;
    public Text remaningTimeText;

    public void IntializedDailyReward()
    {
        if (PlayerPrefs.HasKey("lastDailyReward"))
        {
            rewardGivingTimeTicks = long.Parse(PlayerPrefs.GetString("lastDailyReward")) + 864000000000;
            long currentTime = System.DateTime.Now.Ticks;
            if (currentTime >= rewardGivingTimeTicks)
            {
                GiveReward();
            }

        }
        else
        {
            GiveReward();
        }



        initialized = true;
    }
    public void GiveReward()
    {
        LevelController.Current.GiveMoneyToPlayer(100);
        rewardMenu.SetActive(true);
        PlayerPrefs.SetString("lastDailyReward",System.DateTime.Now.Ticks.ToString());
        rewardGivingTimeTicks = long.Parse(PlayerPrefs.GetString("lastDailyReward")) + 864000000000;





    }

    // Update is called once per frame
    void Update()
    {
        if (initialized)
        {
            if (LevelController.Current.startMenu.activeInHierarchy)
            {
                long currentTime = System.DateTime.Now.Ticks;
                long remaningTime = rewardGivingTimeTicks - currentTime;
                if(remaningTime<=0)
                {
                    GiveReward();
                }
                else
                {
                    System.TimeSpan timespan = System.TimeSpan.FromTicks(remaningTime);
                    remaningTimeText.text = string.Format("{0}:{1}:{2}",timespan.Hours.ToString("D2") , timespan.Minutes.ToString("D2"), timespan.Seconds.ToString("D2"));

                }
            }

        }
        
    }

    public void TaptoReturnButton()
    {
        rewardMenu.SetActive(false);
    }
}
