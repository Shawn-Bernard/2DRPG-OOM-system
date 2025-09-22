using _2DRPG_OOM_system;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public class Quest
{
    public enum QuestType
    {
        Kill,
        BeatLevel,
        None
    };

    public string title;
    public int goal;
    public int progress;
    private int money;
    public Color color;
    public QuestType questType;
    public bool IsComplete
    {
        get
        {
            if (progress == goal)
            {
                return true;
            }
            else
            {
                color = Color.Green;
                return false;
            }
        }
    }
    public Quest(string Title, int Goal, QuestType TypeOfQuest) 
    {
        title = Title;
        goal = Goal;
        questType = TypeOfQuest;
        //progress = 0;

        switch (TypeOfQuest)
        {
            case QuestType.Kill:
                money = 10;
                break;
            case QuestType.BeatLevel:
                money = 5;
                break;
        }
    }

    public Color QuestTextColor()
    {
        if (IsComplete)
        {
            color = Color.Green;
        }
        else
        {
            color = Color.White;
        }
        return color;
    }

    public void AddProgress()
    {
        progress++;
        if (progress > goal)
        {
            progress = goal;
        }
    }

    public bool QuestCheck(QuestType type)
    {
        if (progress == goal)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetReward()
    {
        return money;
    }

    public override string ToString()
    {
        string questDisplay = title + goal;
        //title += goal;
        return $"{title} [{progress}/{goal}]";
    }
}
