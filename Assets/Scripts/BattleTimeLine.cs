using UnityEngine;
using System.Collections.Generic;

public class BattleTimeLine
{
    private int currentTime;
    private int currentRound;
    private int maxStartTime;
    private PriorityQueue<IUsable, int> actionQueue;

    public BattleTimeLine()
    {
        currentTime = 0;
        currentRound = 1;
        maxStartTime = 255;
        actionQueue = new PriorityQueue<IUsable, int>();
    }

    public void AddAction(IBattleAction newAction)
    {
        actionQueue.Enqueue(newAction, currentTime + newAction.ExecuteDelay);
    }
}
