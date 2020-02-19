using System.Linq;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private Checkpoint[] checkpoints;

    private void Start()
    {
        checkpoints = GetComponentsInChildren<Checkpoint>(); 
    }

    public Checkpoint GetLastCheckpointThatWasPassed() {
        return checkpoints.Last(t => t.Passed); 
    }
}


// checkpoints.Last (t => t.Passed) will go through each checkpoint in the array
// find the last one that was passed, and return it.
// The order of checkpoints depends on order of children in inspector