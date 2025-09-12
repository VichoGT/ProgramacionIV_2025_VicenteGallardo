using UnityEngine;
using System.Collections.Generic;
public class Player : MonoBehaviour
{
    [SerializeField] Movement movement;
    [SerializeField] Shooting shooting;
    [SerializeField] Bullet bullet;
    public List<StatInfo> currentStats = new List<StatInfo>();
    public string playerName;
    public int currentDmg;
    public int score;
    public TankSpriteModifier spriteModifier;


    [Header("TankPieces")]
    public Color piece_lightColor;
    public TankPieceScriptable piece_Track;
    public TankPieceScriptable piece_Hull;
    public TankPieceScriptable piece_Tower;
    public TankPieceScriptable piece_Gun;
    public TankPieceScriptable piece_GunConnector;
    public TankPieceScriptable piece_Projectile;

    [Header("TankStats")]
    public StatInfo stat_Spd;
    public StatInfo stat_RootSpd;
    public StatInfo stat_Attack;
    public StatInfo stat_Defense;
    public StatInfo stat_Life;
    public StatInfo stat_BulletSpd;

    private void Start()
    {
        UpdateControllerWithTankPieces();
        LoadData();
    }

    public void OnTankPieceChangeEvent(TankPieceScriptable tankPiece)
    {
        switch (tankPiece.pieceType)
        {
            case TankPieceType.Light:
                Debug.Log("Nick Farmea Aura");
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
                case StatType.BulletSpd:
                    shooting.bulletSpd = item.value;
                    break;
                default:
                    break;
            }

        }
    }

    void LoadData()
    {
        LoadSaveSystem loadSave = new LoadSaveSystem();
        PlayerDataInfo playerData = loadSave.LoadPlayerInfo();



      playerName = playerData.playerName; 
      currentDmg = playerData.currentDmg; 
      score = playerData.score; 


      LoadResources loadResource = new LoadResources();


        piece_Track = loadResource.GetTankPieceScriptable(TankPieceType.Track, playerData.pieceNames[0]);
        piece_Hull = loadResource.GetTankPieceScriptable(TankPieceType.Hull, playerData.pieceNames[1]);
        piece_Tower = loadResource.GetTankPieceScriptable(TankPieceType.Tower, playerData.pieceNames[2]);
        piece_Gun = loadResource.GetTankPieceScriptable(TankPieceType.Gun, playerData.pieceNames[3]);
        piece_GunConnector = loadResource.GetTankPieceScriptable(TankPieceType.GunConnector, playerData.pieceNames[4]);
        piece_Projectile = loadResource.GetTankPieceScriptable(TankPieceType.Projectile, playerData.pieceNames[5]);


        spriteModifier.ChangeSprite(piece_Track.pieceType,piece_Track.pieceSprite);
        spriteModifier.ChangeSprite(piece_Hull.pieceType,piece_Hull.pieceSprite);
        spriteModifier.ChangeSprite(piece_Tower.pieceType,piece_Tower.pieceSprite);
        spriteModifier.ChangeSprite(piece_Gun.pieceType,piece_Gun.pieceSprite);
        spriteModifier.ChangeSprite(piece_GunConnector.pieceType,piece_GunConnector.pieceSprite);
        spriteModifier.ChangeSprite(piece_Projectile.pieceType,piece_Projectile.pieceSprite);


        UpdateControllerWithTankPieces();
    }
     
    public void SaveData()
    {
        PlayerDataInfo playerData = new PlayerDataInfo();

        playerData.playerName = playerName;
        playerData.currentDmg = currentDmg;
        playerData.score = score;

        playerData.pieceNames = new List<string>();
        playerData.pieceNames.Add(piece_Track.id);
        playerData.pieceNames.Add(piece_Hull.id);
        playerData.pieceNames.Add(piece_Tower.id);
        playerData.pieceNames.Add(piece_Gun.id);
        playerData.pieceNames.Add(piece_GunConnector.id);
        playerData.pieceNames.Add(piece_Projectile.id);


        LoadSaveSystem loadSave = new LoadSaveSystem();
        loadSave.SavePlayerInfo(playerData);


    }



}




