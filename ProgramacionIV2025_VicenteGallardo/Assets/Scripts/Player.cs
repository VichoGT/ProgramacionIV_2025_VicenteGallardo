using UnityEngine;
using System.Collections.Generic;
using TMPro;
public class Player : MonoBehaviour
{
    [SerializeField] Movement movement;
    [SerializeField] Shooting shooting;
    [SerializeField] Bullet bullet;
   
    public List<StatInfo> currentStats = new List<StatInfo>();
    public string tankName;
    public int currentDmg;
    public int score;
    public TankSpriteModifier spriteModifier;


    [Header("NameTank")]
    public TextMeshProUGUI tankNameCurrent;
    [SerializeField] TMPro.TMP_InputField inputField;

    [Header("TankPieces")]
    public Color piece_lightColor;
    public TankPieceScriptable piece_Track;
    public TankPieceScriptable piece_Hull;
    public TankPieceScriptable piece_Tower;
    public TankPieceScriptable piece_Gun;
    public TankPieceScriptable piece_GunConnector;
    public TankPieceScriptable piece_Projectile;
    public ColorPicker colorpicker;

    [Header("TankStats")]
    public StatInfo stat_Spd;
    public StatInfo stat_RootSpd;
    public StatInfo stat_Attack;
    public StatInfo stat_Defense;
    public StatInfo stat_Life;
    public StatInfo stat_PowerShoot;

    private void Awake()
    {
        inputField.onValueChanged.AddListener(ChangeName);
    }

    private void Start()
    {
        
        UpdateControllerWithTankPieces();
        LoadData();
         
        
    }

    public void ChangeName(string val)
    {
        tankNameCurrent.text = val;
        tankName = val;
    }
    public void OnTankPieceChangeEvent(TankPieceScriptable tankPiece)
    {
        switch (tankPiece.pieceType)
        {
            case TankPieceType.Light:
                Debug.Log("si");
                break;
            case TankPieceType.Track:
                piece_Track = tankPiece;
                break;
            case TankPieceType.Hull:
                piece_Hull = tankPiece;
                break;
            case TankPieceType.Tower:
                piece_Tower = tankPiece;
                break;
            case TankPieceType.Gun:
                piece_Gun = tankPiece;
                break;
            case TankPieceType.GunConnector:
                piece_GunConnector= tankPiece;
                break;
            case TankPieceType.Projectile:
                piece_Projectile = tankPiece;
                break;
            default:
                break;
        }
        //Debug.Log("The piece modified is: " + tankPiece.pieceType);
        //Debug.Log("The id is: " + tankPiece.id);
        UpdateControllerWithTankPieces();
    }

    public void OnChangeColorTank(Color tankColor)
    {
        piece_lightColor = tankColor;

    }
    public void UpdateControllerWithTankPieces()
    {
        List<StatInfo> statinfo = new List<StatInfo>();


        foreach (var item in piece_Track.statInfo)
        {
           StatInfo currentStat = statinfo.Find(x => x.type == item.type);
            if (currentStat!= null)
            {
                currentStat.value += item.value;
            }
            else
            {
                StatInfo newInfo = new StatInfo();
                newInfo.type = item.type;
                newInfo.value = item.value;
                statinfo.Add(newInfo);
            }

        }
        foreach (var item in piece_Hull.statInfo)
        {
            StatInfo currentStat = statinfo.Find(x => x.type == item.type);
            if (currentStat != null)
            {
                currentStat.value += item.value;
            }
            else
            {
                StatInfo newInfo = new StatInfo();
                newInfo.type = item.type;
                newInfo.value = item.value;
                statinfo.Add(newInfo);
            }
        }
        foreach (var item in piece_Tower.statInfo)
        {
            StatInfo currentStat = statinfo.Find(x => x.type == item.type);
            if (currentStat != null)
            {
                currentStat.value += item.value;
            }
            else
            {
                StatInfo newInfo = new StatInfo();
                newInfo.type = item.type;
                newInfo.value = item.value;
                statinfo.Add(newInfo);
            }
        }
        foreach (var item in piece_Gun.statInfo)
        {
            StatInfo currentStat = statinfo.Find(x => x.type == item.type);
            if (currentStat != null)
            {
                currentStat.value += item.value;
            }
            else
            {
                StatInfo newInfo = new StatInfo();
                newInfo.type = item.type;
                newInfo.value = item.value;
                statinfo.Add(newInfo);
            }
        }
        foreach (var item in piece_GunConnector.statInfo)
        {
            StatInfo currentStat = statinfo.Find(x => x.type == item.type);
            if (currentStat != null)
            {
                currentStat.value += item.value;
            }
            else
            {
                StatInfo newInfo = new StatInfo();
                newInfo.type = item.type;
                newInfo.value = item.value;
                statinfo.Add(newInfo);
            }
        }
        foreach (var item in piece_Projectile.statInfo)
        {
            StatInfo currentStat = statinfo.Find(x => x.type == item.type);
            if (currentStat != null)
            {
                currentStat.value += item.value;
            }
            else
            {
                StatInfo newInfo = new StatInfo();
                newInfo.type = item.type;
                newInfo.value = item.value;
                statinfo.Add(newInfo);
            }
        }
        currentStats = statinfo;        
        UpdateControllerStats();
    }
    public void UpdateControllerStats()
    {
        foreach (var item in currentStats)
        {
            switch (item.type)
            {
                case StatType.Spd:
                    movement.spd = item.value;
                    break;
                case StatType.RootSpd:
                    movement.rotateSpd = item.value;
                    break;
                case StatType.Attack:
                    bullet.dmg = item.value;
                    break;
                case StatType.Defense:
                    break;
                case StatType.Life:
                    break;
                case StatType.PowerShoot:
                    shooting.powerShoot = item.value;
                    break;
                default:
                    break;
            }

        }
    }

    public void LoadData()
    {
        LoadSaveSystem loadSave = new LoadSaveSystem();
        PlayerDataInfo playerData = loadSave.LoadPlayerInfo();



       ChangeName(playerData.playerName);
       currentDmg = playerData.currentDmg; 
       score = playerData.score; 
      


        LoadResources loadResource = new LoadResources();


        piece_Track = loadResource.GetTankPieceScriptable(TankPieceType.Track, playerData.pieceNames[0]);
        piece_Hull = loadResource.GetTankPieceScriptable(TankPieceType.Hull, playerData.pieceNames[1]);
        piece_Tower = loadResource.GetTankPieceScriptable(TankPieceType.Tower, playerData.pieceNames[2]);
        piece_Gun = loadResource.GetTankPieceScriptable(TankPieceType.Gun, playerData.pieceNames[3]);
        piece_GunConnector = loadResource.GetTankPieceScriptable(TankPieceType.GunConnector, playerData.pieceNames[4]);
        piece_Projectile = loadResource.GetTankPieceScriptable(TankPieceType.Projectile, playerData.pieceNames[5]);

        piece_lightColor = playerData.colorTank;



        spriteModifier.ChangeSprite(piece_Track.pieceType,piece_Track.pieceSprite);
        spriteModifier.ChangeSprite(piece_Hull.pieceType,piece_Hull.pieceSprite);
        spriteModifier.ChangeSprite(piece_Tower.pieceType,piece_Tower.pieceSprite);
        spriteModifier.ChangeSprite(piece_Gun.pieceType,piece_Gun.pieceSprite);
        spriteModifier.ChangeSprite(piece_GunConnector.pieceType,piece_GunConnector.pieceSprite);
        spriteModifier.ChangeSprite(piece_Projectile.pieceType,piece_Projectile.pieceSprite);
        spriteModifier.ChangeLightColor(piece_lightColor);
       


        UpdateControllerWithTankPieces();
    }
     
    public void SaveData()
    {
        PlayerDataInfo playerData = new PlayerDataInfo();

        playerData.playerName = tankName;
        playerData.currentDmg = currentDmg;
        playerData.score = score;

        playerData.pieceNames = new List<string>();
        playerData.pieceNames.Add(piece_Track.id);
        playerData.pieceNames.Add(piece_Hull.id);
        playerData.pieceNames.Add(piece_Tower.id);
        playerData.pieceNames.Add(piece_Gun.id);
        playerData.pieceNames.Add(piece_GunConnector.id);
        playerData.pieceNames.Add(piece_Projectile.id);


        playerData.colorTank = piece_lightColor;

        LoadSaveSystem loadSave = new LoadSaveSystem();
        loadSave.SavePlayerInfo(playerData);


    }



}




