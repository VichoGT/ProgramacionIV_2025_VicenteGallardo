using UnityEngine;
using System.Collections.Generic;
public class Player : MonoBehaviour
{
    [SerializeField] Movement movement;
    public List<StatInfo> currentStats = new List<StatInfo>();
    public Color piece_lightColor;
    public TankPieceScriptable piece_Track;
    public TankPieceScriptable piece_Hull;
    public TankPieceScriptable piece_Tower;
    public TankPieceScriptable piece_Gun;
    public TankPieceScriptable piece_GunConnector;
    public TankPieceScriptable piece_Projectile;

    private void Start()
    {
        UpdateControllerWithTankPieces();
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
                    break;
                case StatType.Defense:
                    break;
                case StatType.Life:
                    break;
                case StatType.BulletSpd:
                    break;
                default:
                    break;
            }

        }
    }
}




