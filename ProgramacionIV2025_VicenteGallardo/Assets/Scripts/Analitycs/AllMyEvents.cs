
using Unity.Services.Analytics;

//se cammbia el nombre de la clase al mismo que nuestro evento 
public class MyFirstCustomEvent : Event
{

    // al constructor le ponemos el mismo tipo que la clase
    public MyFirstCustomEvent() : base("myFirstCustomEvent") // base("siempre va el nombre del evento igual que el dashboard")
    {
    }


    // aca abajo vamos a poner las mismas variables que nuestro evento en el dashboard
    
    public float mFCE_LindoFloat 
    {
         set 
         { 
            SetParameter("mFCE_LindoFloat",value);
         } 
    }
}
public class MySecondCustomEvent : Event
{

    // al constructor le ponemos el mismo tipo que la clase
    public MySecondCustomEvent() : base("mySecondCustomEvent") // base("siempre va el nombre del evento igual que el dashboard")
    {
    }


    // aca abajo vamos a poner las mismas variables que nuestro evento en el dashboard

    public int mSCE_LindoInt
    {
        set
        {
            SetParameter("mSCE_LindoInt", value);
        }
    }   
    public bool mSCE_LindoBool
    {
        set
        {
            SetParameter("mSCE_LindoBool", value);
        }
    }  public string mSCE_LindoString
    {
        set
        {
            SetParameter("mSCE_LindoString", value);
        }
    }    
}

public class PlayerDieEvent : Event
{

    // al constructor le ponemos el mismo tipo que la clase
    public PlayerDieEvent() : base("PlayerDieEvent") // base("siempre va el nombre del evento igual que el dashboard")
    {
    }


    // aca abajo vamos a poner las mismas variables que nuestro evento en el dashboard

    public string PD_Reason
    {
        set
        {
            SetParameter("PD_Reason", value);
        }
    }
    public float PD_Time
    {
        set
        {
            SetParameter("PD_Time", value);
        }
    }
    public float PD_PosX
    {
        set
        {
            SetParameter("PD_PosX", value);
        }
    }
    public float PD_PosY
    {
        set
        {
            SetParameter("PD_PosY", value);
        }
    }
    public float PD_PosZ
    {
        set
        {
            SetParameter("PD_PosZ", value);
        }
    }
}

