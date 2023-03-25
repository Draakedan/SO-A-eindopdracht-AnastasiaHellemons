using avansDevOps.Backlog.DiscussuionForum;
using avansDevOps.Backlog.TasklStates__State_;
using avansDevOps.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.Backlog
{
    public class BacklogItem : IBacklogItem
    {
        private int PriorityNo { get; set; }
        public string Name { get; set; }
        public IState State { get; set; }
        public BackLogDiscussionForum Discussions { get; set; }
        private List<SubTask> SubTasks { get; set; }

#pragma warning disable CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
        public User? Developer { get; set; }
#pragma warning restore CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).

        public BacklogItem(int priorityNo, string name, StateCount stateCount)
        {
            PriorityNo = priorityNo;
            Name = name;
            this.State = new ToDoState(stateCount);
            this.Discussions = new(this.State);
            this.SubTasks = new();
            this.Developer = new User("", "", "");
        }
        public bool CanAddSubtasks(User user, SubTask task) 
        {
            bool canAdd = user.HasRole(typeof(Developer).ToString()) ||
                user.HasRole(typeof(ScrumMaster).ToString());
            if (canAdd) 
                AddSubtask(task);
            return canAdd;
        }

        public string GetSubtasks() 
        {
            string tasks = "";
            foreach (SubTask s in SubTasks)
                tasks += s.Name + "\n";
            return tasks;
        }

        public void AddSubtask(SubTask name)
        {
            SubTasks.Add(name);
        }

        public void RemoveSubtask(SubTask name)
        {
            SubTasks.Remove(name);
        }

        public void EditPriority(int priorityNo)
        {
            this.PriorityNo = priorityNo;
        }

        public void ChangeState(IState state)
        {
            this.State.ChangeStateCount(state.GetType().ToString());
            this.State = state;
        }

        public bool AddDeveloper(User developer)
        {
            bool canAdd = developer == null && developer!.HasRole(typeof(Developer).ToString());

            if (canAdd)
                this.Developer = developer;
            return canAdd;
        }

        public void RemoveDeveloper()
        {
            Developer = null;
        }

        public bool CanAddUser(User Adder, User developer)
        {
            bool canAdd = (Adder.HasRole(typeof(Developer).ToString()) ||
                Adder.HasRole(typeof(ScrumMaster).ToString()));
            if (canAdd)
                this.AddDeveloper(developer);

            return canAdd;
        }

        public bool CanChangeState(User user, IState state)
        {
            var canChange = state.GetType().ToString() switch
            {
                "avansDevOps.Backlog.TasklStates__State_.ToDoState" => CanChangeStateToTodo(user, state),
                "avansDevOps.Backlog.TasklStates__State_.DoingState" => CanChangeStateToDoing(user, state),
                "avansDevOps.Backlog.TasklStates__State_.ReadyForTestingState" => CanChangeStateToReadyForTesting(user, state),
                "avansDevOps.Backlog.TasklStates__State_.TestingState" => CanChangeStateToTesting(user, state),
                "avansDevOps.Backlog.TasklStates__State_.TestedState" => CanChangeStateToTested(user, state),
                "avansDevOps.Backlog.TasklStates__State_.DoneState" => CanChangeStateToDone(user, state),
                _ => CanChangeToCustom(user, state),
            };
            return canChange;
        }

        private bool CanChangeToCustom(User user, IState state)
        {
            CustomState customState = (CustomState)state;
            return this.State.ChangeStateToCustom(user, customState);
        }

        private bool CanChangeStateToDoing(User user, IState state)
        {
            bool canChangeState = false;
            bool subtaskInToDo = false;
            bool subtaskInDone = false;
            if (this.State.ChangeStateToDoing(user, state.MaxItems) && Developer!.Name.Equals(user.Name))
            {
                if (SubTasks.Count > 0)
                {
                    foreach (SubTask task in SubTasks)
                    {
                        if (task.State.GetType() == typeof(ToDoState))
                            subtaskInToDo = true;
                        else if (task.State.GetType() == typeof(DoneState))
                            subtaskInDone = true;
                    }

                    canChangeState = (!subtaskInToDo && subtaskInDone);
                }
                else
                    canChangeState = true;
            }
            if (canChangeState) ChangeState(state);
            return canChangeState;
        }

        private bool CanChangeStateToTodo(User user, IState state)
        {
            bool canChangeState = false;
            bool subtaskInToDo = false;
            {
                if (this.State.ChangeStateToToDo(user))
                {
                    if (SubTasks.Count > 0)
                    {
                        foreach (SubTask task in SubTasks)
                        {
                            if (task.State.GetType() == typeof(ToDoState))
                                subtaskInToDo = true;
                        }
                        canChangeState = (subtaskInToDo);
                    }
                    else
                        canChangeState = true;
                }
            }
            if (canChangeState) ChangeState(state);
            return canChangeState;
        }

        private bool CanChangeStateToReadyForTesting(User user, IState state)
        {
            bool canChangeState = false;
            bool subtaskInReadyForTesting = false;
            bool subtaskInDoing = false;
            bool subtaskInToDo = false;
            {
                if (this.State.ChangeStateToReadyForTesting(user))
                {
                    if (SubTasks.Count > 0)
                    {
                        foreach (SubTask task in SubTasks)
                        {
                            if (task.State.GetType() == typeof(ToDoState))
                                subtaskInToDo = true;
                            else if (task.State.GetType() == typeof(DoingState))
                                subtaskInDoing = true;
                            else if (task.State.GetType() == typeof(ReadyForTestingState))
                                subtaskInReadyForTesting = true;
                        }
                        canChangeState = (subtaskInReadyForTesting && (!subtaskInToDo && !subtaskInDoing));
                    }
                    else
                        canChangeState = true;
                }
            }
            if (canChangeState) ChangeState(state);
            return canChangeState;
        }

        private bool CanChangeStateToTesting(User user, IState state)
        {
            bool canChangeState = false;
            bool subtaskInTesting = false;
            bool subtaskInReadyForTesting = false;
            bool subtaskInDoing = false;
            bool subtaskInToDo = false;
            if (this.State.ChangeStateToTesting(user, state.MaxItems))
            {
                if (SubTasks.Count > 0)
                {
                    foreach (SubTask task in SubTasks)
                    {
                        if (task.State.GetType() == typeof(ToDoState))
                            subtaskInToDo = true;
                        else if (task.State.GetType() == typeof(DoingState))
                            subtaskInDoing = true;
                        else if (task.State.GetType() == typeof(ReadyForTestingState))
                            subtaskInReadyForTesting = true;
                        else if (task.State.GetType() == typeof(TestingState))
                            subtaskInTesting = true;
                    }
                    canChangeState = (subtaskInTesting && (!subtaskInToDo && !subtaskInDoing && !subtaskInReadyForTesting));
                    Console.WriteLine(canChangeState);
                }
                else
                    canChangeState = true;
            }
            if (canChangeState) ChangeState(state);
            return canChangeState;
        }

        private bool CanChangeStateToTested(User user, IState state)
        {
            bool canChangeState = false;
            bool subtaskInTested = false;
            bool subtaskInTesting = false;
            bool subtaskInReadyForTesting = false;
            bool subtaskInDoing = false;
            bool subtaskInToDo = false;
            if (this.State.ChangeStateToTested(user))
            {
                if (SubTasks.Count > 0)
                {
                    foreach (SubTask task in SubTasks)
                    {
                        if (task.State.GetType() == typeof(ToDoState))
                            subtaskInToDo = true;
                        else if (task.State.GetType() == typeof(DoingState))
                            subtaskInDoing = true;
                        else if (task.State.GetType() == typeof(ReadyForTestingState))
                            subtaskInReadyForTesting = true;
                        else if (task.State.GetType() == typeof(TestingState))
                            subtaskInTesting = true;
                        else if (task.State.GetType() == typeof(TestedState))
                            subtaskInTested = true;
                    }
                    canChangeState = (subtaskInTested && (!subtaskInToDo && !subtaskInDoing && !subtaskInReadyForTesting && !subtaskInTesting));
                }
                else
                    canChangeState = true;
            }
            if (canChangeState) ChangeState(state);
            return canChangeState;
        }

        private bool CanChangeStateToDone(User user, IState state)
        {
            bool canChangeState = false;
            bool subtaskInDone = false;
            bool subtaskInTested = false;
            bool subtaskInTesting = false;
            bool subtaskInReadyForTesting = false;
            bool subtaskInDoing = false;
            bool subtaskInToDo = false;
            if (this.State.ChangeStateToDone(user))
            {
                if (SubTasks.Count > 0)
                {
                    foreach (SubTask task in SubTasks)
                    {
                        if (task.State.GetType() == typeof(ToDoState))
                            subtaskInToDo = true;
                        else if (task.State.GetType() == typeof(DoingState))
                            subtaskInDoing = true;
                        else if (task.State.GetType() == typeof(ReadyForTestingState))
                            subtaskInReadyForTesting = true;
                        else if (task.State.GetType() == typeof(TestingState))
                            subtaskInTesting = true;
                        else if (task.State.GetType() == typeof(TestedState))
                            subtaskInTested = true;
                        else if (task.State.GetType() == typeof(DoneState))
                            subtaskInDone = true;
                    }
                    canChangeState = (subtaskInDone && (!subtaskInToDo && !subtaskInDoing && !subtaskInReadyForTesting && !subtaskInTesting && !subtaskInTested));
                }
                else
                    canChangeState = true;
            }
            if (canChangeState) ChangeState(state);
            return canChangeState;
        }
    }
}
