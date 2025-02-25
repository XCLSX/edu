using System;
using System.Collections.Generic;
using UnityEngine;

namespace CommonTools
{
    public class TimerManager : MonoBehaviour
    {
        private readonly List<TimerTask> _activeTasks = new List<TimerTask>();  // 活跃的定时任务列表
        private readonly List<int> _tasksToRemove = new List<int>(); // 标记需要删除的任务ID
        private int _nextId;  // 用于生成唯一的任务ID

        // 定时任务类，包含回调方法、时间间隔、任务ID、是否为循环任务
        private class TimerTask
        {
            public readonly int ID;            // 定时任务的ID
            public readonly Action Callback;   // 定时任务的回调方法
            public readonly float Interval;    // 定时任务间隔时间
            public float LastExecutionTime; // 上次执行时间
            public readonly bool IsLooping;    // 是否为循环任务

            public TimerTask(int id, Action callback, float interval, bool isLooping)
            {
                this.ID = id;
                this.Callback = callback;
                this.Interval = interval;
                this.LastExecutionTime = Time.time;
                this.IsLooping = isLooping;
            }
        }

        // 每帧更新，检查所有任务是否需要执行
        private void Update()
        {
            foreach (var task in _activeTasks)
            {
                // 如果达到定时条件，执行回调并更新时间
                if (Time.time - task.LastExecutionTime >= task.Interval)
                {
                    task.Callback.Invoke();  // 执行定时任务回调

                    if (!task.IsLooping) 
                    {
                        // 如果不是循环任务，标记该任务需要被删除
                        _tasksToRemove.Add(task.ID);
                    }
                    else
                    {
                        task.LastExecutionTime = Time.time;  // 更新最后执行时间
                    }
                }
            }

            // 在遍历完成后，统一删除标记的任务
            RemoveMarkedTasks();
        }

        // 添加定时任务，返回任务ID
        public int AddTask(Action callback, float interval, bool isLooping = true)
        {
            int taskId = _nextId++;
            TimerTask newTask = new TimerTask(taskId, callback, interval, isLooping);
            _activeTasks.Add(newTask);
            return taskId;  // 返回任务ID
        }

        // 通过ID移除指定的定时任务
        public void RemoveTask(int taskId)
        {
            _activeTasks.RemoveAll(task => task.ID == taskId);
        }

        // 移除所有定时任务
        public void RemoveAllTasks()
        {
            _activeTasks.Clear();
        }

        // 根据标记的任务ID移除任务
        private void RemoveMarkedTasks()
        {
            foreach (int taskId in _tasksToRemove)
            {
                _activeTasks.RemoveAll(task => task.ID == taskId);
            }

            // 清空删除标记
            _tasksToRemove.Clear();
        }
    }
}
