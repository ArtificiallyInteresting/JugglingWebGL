using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


//Started from here, thanks Unity!
//https://github.com/Unity-Technologies/Q-GridWorld/blob/master/Assets/Scripts/InternalAgent.cs


public class qLearning
{
    public float[][] q_table;   // The matrix containing the values estimates.
    float learning_rate = 0.5f; // The rate at which to update the value estimates given a reward.
    int action = -1;
    float gamma = 0.99f; // Discount factor for calculating Q-target.
    float e = 1; // Initial epsilon value for random action selection.
    float eMin = 0.1f; // Lower bound of epsilon.
    int annealingSteps = 2000; // Number of steps to lower e to eMin.
    int lastState;

    public qLearning(int numStates, int numActions)
    {
        q_table = new float[numStates][];
        action = 0;
        for (int i = 0; i < numStates; i++)
        {
            q_table[i] = new float[numActions];
            for (int j = 0; j < numActions; j++)
            {
                q_table[i][j] = 0.0f;
            }
        }
    }

    /// <summary>
    /// Picks an action to take from its current state.
    /// </summary>
    /// <returns>The action choosen by the agent's policy</returns>
    public float[] GetAction()
    {
        action = q_table[lastState].ToList().IndexOf(q_table[lastState].Max());
        if (Random.Range(0f, 1f) < e) { action = Random.Range(0, 3); }
        if (e > eMin) { e = e - ((1f - eMin) / (float)annealingSteps); }
        GameObject.Find("ETxt").GetComponent<Text>().text = "Epsilon: " + e.ToString("F2");
        float currentQ = q_table[lastState][action];
        GameObject.Find("QTxt").GetComponent<Text>().text = "Current Q-value: " + currentQ.ToString("F2");
        return new float[1] { action };
    }

    /// <summary>
    /// Gets the values stored within the Q table.
    /// </summary>
    /// <returns>The average Q-values per state.</returns>
	public float[] GetValue()
    {
        float[] value_table = new float[q_table.Length];
        for (int i = 0; i < q_table.Length; i++)
        {
            value_table[i] = q_table[i].Average();
        }
        return value_table;
    }

    /// <summary>
    /// Updates the value estimate matrix given a new experience (state, action, reward).
    /// </summary>
    /// <param name="state">The environment state the experience happened in.</param>
    /// <param name="reward">The reward recieved by the agent from the environment for it's action.</param>
    /// <param name="done">Whether the episode has ended</param>
    public void SendState(List<float> state, float reward, bool done)
    {
        int nextState = Mathf.FloorToInt(state.First());
        if (action != -1)
        {
            if (done == true)
            {
                q_table[lastState][action] += learning_rate * (reward - q_table[lastState][action]);
            }
            else
            {
                q_table[lastState][action] += learning_rate * (reward + gamma * q_table[nextState].Max() - q_table[lastState][action]);
            }
        }
        lastState = nextState;
    }
}