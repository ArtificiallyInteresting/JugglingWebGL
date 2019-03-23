using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


//Started from here, thanks Unity!
//https://github.com/Unity-Technologies/Q-GridWorld/blob/master/Assets/Scripts/InternalAgent.cs


public class qLearning
{
    //public float[][] q_table;   // The matrix containing the values estimates.
    public Dictionary<string, Dictionary<int, float>> q_table;   // The matrix containing the values estimates.
    float learning_rate = 0.5f; // The rate at which to update the value estimates given a reward.
    int action = -1;
    float gamma = 0.99f; // Discount factor for calculating Q-target.
    float e = 1; // Initial epsilon value for random action selection.
    float eMin = 0.01f; // Lower bound of epsilon.
    int annealingSteps = 100000; // Number of steps to lower e to eMin.
    string lastState;
    int numActions;
    int strength;
    int cacheHits;
    int cacheTotal;
    private Dictionary<int, List<Vector2>> actions;

    public qLearning(int strength)
    {
        this.strength = strength;
        q_table = new Dictionary<string, Dictionary<int, float>>();
        this.createActions();
        this.numActions = this.actions.Keys.Count;
        this.cacheHits = 1;
        this.cacheTotal = 0;
        //action = 0;
        //for (int i = 0; i < numStates; i++)
        //{
        //    q_table[i] = new float[numActions];
        //    for (int j = 0; j < numActions; j++)
        //    {
        //        q_table[i][j] = 0.0f;
        //    }
        //}
    }

    /// <summary>
    /// Picks an action to take from its current state.
    /// </summary>
    /// <returns>The action choosen by the agent's policy</returns>
    public List<Vector2> GetAction()
    {
        //checkAndCreateState(lastState);
        if (q_table[lastState].Values.Distinct().Count() == 1 || Random.Range(0f, 1f) < e)
        {
            //No action is better than any other. Ignore the "max" and choose randomly.
            action = Random.Range(0, numActions);
        } else
        {
            action = q_table[lastState].Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
        }
        //if (Random.Range(0f, 1f) < e) { action = Random.Range(0, numActions); }
        if (e > eMin) { e = e - ((1f - eMin) / (float)annealingSteps); }
        //GameObject.Find("ETxt").GetComponent<Text>().text = "Epsilon: " + e.ToString("F2");
        float currentQ = q_table[lastState][action];
        //GameObject.Find("QTxt").GetComponent<Text>().text = "Current Q-value: " + currentQ.ToString("F2");
        return actions[action];
    }

    /// <summary>
    /// Gets the values stored within the Q table.
    /// </summary>
    /// <returns>The average Q-values per state.</returns>
	//public float[] GetValue()
 //   {
 //       float[] value_table = new float[q_table.Length];
 //       for (int i = 0; i < q_table.Length; i++)
 //       {
 //           value_table[i] = q_table[i].Average();
 //       }
 //       return value_table;
 //   }

    /// <summary>
    /// Updates the value estimate matrix given a new experience (state, action, reward).
    /// </summary>
    /// <param name="state">The environment state the experience happened in.</param>
    /// <param name="reward">The reward recieved by the agent from the environment for it's action.</param>
    /// <param name="done">Whether the episode has ended</param>
    public void SendState(string state, float reward, bool done)
    {
        //Debug.Log(state);
        checkAndCreateState(state);
        //int nextState = Mathf.FloorToInt(state.First());
        if (action != -1)
        {
            if (done == true)
            {
                q_table[lastState][action] += learning_rate * (reward - q_table[lastState][action]);
            }
            else
            {
                q_table[lastState][action] += learning_rate * (reward + gamma * q_table[state].Values.Max() - q_table[lastState][action]);
            }
        }
        lastState = state;
    }

    public Vector2 intToMotionAction(int action)
    {
        switch (action)
        {
            case 0:
                return Vector2.up * strength;
            case 1:
                return Vector2.left * strength;
            case 2:
                return Vector2.down * strength;
            case 3:
                return Vector2.right * strength;
            case 4:
            default:
                return Vector2.zero;
        }

    }

    public void createActions()
    {
        this.actions = new Dictionary<int, List<Vector2>>();
        List<Vector2> moves = new List<Vector2>();
        moves.Add(Vector2.up * strength);
        moves.Add(Vector2.left * strength);
        moves.Add(Vector2.right * strength);
        moves.Add(Vector2.down * strength);
        moves.Add(Vector2.zero);
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                actions[i * 5 + j] = new List<Vector2>() { moves[i], moves[j] };
            }
        }

    }

    public void checkAndCreateState(string state)
    {

        bool debug = true;
        if (debug && this.cacheTotal % 1000 == 0)
        {
            Debug.Log("Cache rate: " + (this.cacheHits / (float)(this.cacheTotal)) + " Epsilon: " + e);
        }
        this.cacheTotal += 1;
        if (q_table.ContainsKey(state))
        {
            this.cacheHits += 1;
            return;
        }
        else
        {
            q_table[state] = new Dictionary<int, float>();
            for(int action = 0; action < this.numActions; action++)
            {
                q_table[state][action] = 0f;
            }
        }
        
    }
}