using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeStats : MonoBehaviour
{
    public TextMeshProUGUI sunflower, jasmine, santan, dandelion, rose, tulips, portulaca;

    int sunflowerIsDone1, sunflowerIsDone2, sunflowerIsDone3, sunflowerIsDone4;
    int jasmineIsDone1, jasmineIsDone2, jasmineIsDone3;
    int santanIsDone1, santanIsDone2, santanIsDone3;
    int dandelionIsDone1, dandelionIsDone2;
    int roseIsDone1, roseIsDone2;
    int tulipsIsDone1, tulipsIsDone2;
    int portulacaIsDone1, portulacaIsDone2;

    private void Update()
    {
        sunflowerIsDone1 = PlayerPrefs.GetInt("Sunflower Upgrade" + 1, 0);
        sunflowerIsDone2 = PlayerPrefs.GetInt("Sunflower Upgrade" + 2, 0);
        sunflowerIsDone3 = PlayerPrefs.GetInt("Sunflower Upgrade" + 3, 0);
        sunflowerIsDone4 = PlayerPrefs.GetInt("Sunflower Upgrade" + 4, 0);
        if(sunflowerIsDone1 == 1)
        {
            sunflower.text = "+10%";
        }
        if(sunflowerIsDone2 == 1)
        {
            sunflower.text = "+30%";
        }
        if(sunflowerIsDone3 == 1)
        {
            sunflower.text = "+80%";
        }
        if(sunflowerIsDone4 == 1)
        {
            sunflower.text = "+180%";
        }

        jasmineIsDone1 = PlayerPrefs.GetInt("Jasmine Upgrade" + 1, 0);
        jasmineIsDone2 = PlayerPrefs.GetInt("Jasmine Upgrade" + 2, 0);
        jasmineIsDone3 = PlayerPrefs.GetInt("Jasmine Upgrade" + 3, 0);
        if(jasmineIsDone1 == 1)
        {
            jasmine.text = "+10%";
        }
        if (jasmineIsDone2 == 1)
        {
            jasmine.text = "+60%";
        }
        if (jasmineIsDone3 == 1)
        {
            jasmine.text = "+160%";
        }

        santanIsDone1 = PlayerPrefs.GetInt("Santan Upgrade" + 1, 0);
        santanIsDone2 = PlayerPrefs.GetInt("Santan Upgrade" + 2, 0);
        santanIsDone3 = PlayerPrefs.GetInt("Santan Upgrade" + 3, 0);
        if(santanIsDone1 == 1)
        {
            santan.text = "+10%";
        }
        if (santanIsDone2 == 1)
        {
            santan.text = "+60%";
        }
        if (santanIsDone3 == 1)
        {
            santan.text = "+160%";
        }

        dandelionIsDone1 = PlayerPrefs.GetInt("Dandelion Upgrade" + 1, 0);
        dandelionIsDone2 = PlayerPrefs.GetInt("Dandelion Upgrade" + 2, 0);
        if(dandelionIsDone1 == 1)
        {
            dandelion.text = "+50%";
        }
        if (dandelionIsDone2 == 1)
        {
            dandelion.text = "+150%";
        }

        roseIsDone1 = PlayerPrefs.GetInt("Rose Upgrade" + 1, 0);
        roseIsDone2 = PlayerPrefs.GetInt("Rose Upgrade" + 2, 0);
        if(roseIsDone1 == 1)
        {
            rose.text = "15 sec";
        }
        if(roseIsDone2 == 1)
        {
            rose.text = "5 sec";
        }

        tulipsIsDone1 = PlayerPrefs.GetInt("Tulips Upgrade" + 1, 0);
        tulipsIsDone2 = PlayerPrefs.GetInt("Tulips Upgrade" + 2, 0);
        if(tulipsIsDone1 == 1)
        {
            tulips.text = "+50%";
        }
        if(tulipsIsDone2 == 1)
        {
            tulips.text = "+150%";
        }

        portulacaIsDone1 = PlayerPrefs.GetInt("Portulaca Upgrade" + 1, 0);
        portulacaIsDone2 = PlayerPrefs.GetInt("Portulaca Upgrade" + 2, 0);
        if(portulacaIsDone1 == 1)
        {
            portulaca.text = "30 sec";
        }
        if(portulacaIsDone2 == 1)
        {
            portulaca.text = "15 sec";
        }
    }
    
}
