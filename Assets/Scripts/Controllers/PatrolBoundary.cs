using System;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

public class PatrolBoundary : MonoBehaviour {
    public delegate void HitPatrolBoundaryEvent();
    public HitPatrolBoundaryEvent onHitPatrolBoundary;

    [Header("References")]
    [SerializeField]
    private Transform start;
    [SerializeField] private Transform end;

    
    private void OnDrawGizmos() {
        if (!start || start == null) return;
        if (!end || end == null) return;

        Handles.DrawWireDisc(start.position, start.forward, 0.5f);
        Handles.DrawWireDisc(end.position, end.forward, 0.5f);
        Handles.DrawDottedLine(start.position, end.position, 5f);
    }
}