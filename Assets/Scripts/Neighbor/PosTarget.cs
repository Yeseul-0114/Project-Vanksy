using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PosTarget", menuName = "ScriptableObjects/PosTarget")]
public class PosTarget : ScriptableObject
{
    [SerializeField] private List<GameObject> _targetPos = new List<GameObject>();
    [SerializeField] private Transform _startPos;
    [SerializeField] private Transform _endPos;
    
    public List<GameObject> TargetPos => _targetPos;
    public Transform StartPos => _startPos;
    public Transform EndPos => _endPos;
}
