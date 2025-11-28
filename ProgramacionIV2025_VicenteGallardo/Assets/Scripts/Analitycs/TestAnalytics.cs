using UnityEngine;

public class TestAnalytics : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AnalyticsManager.Instance.SaveMyFirstCustomEvent(0.5f);
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AnalyticsManager.Instance.SaveMySecondCustomEvent(1,true,"Hola");
            
        }
    }
}  
    

