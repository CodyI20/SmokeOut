using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskStep : MonoBehaviour
{
    private bool isFinished = false;

    protected void FinishTaskStep()
    {
<<<<<<< Updated upstream
        isFinished = true;
        /// Todo - advance the task forward now that they have finished this step

        Destroy(this.gameObject);
=======
        if (!isFinished)
        {
            isFinished = true;
            /// Todo - advance the task forward now that they have finished this step

            Destroy(this.gameObject);
        }
    
>>>>>>> Stashed changes
    }
}
