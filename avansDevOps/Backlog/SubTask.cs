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
    public class SubTask : IBacklogItem
    {
        public string Name { get; private set; }
        public IState State { get; set; }
        public BackLogDiscussionForum Discussions { get; set; }
#pragma warning disable CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
        public User? Developer { get; set; }
#pragma warning restore CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).

        public SubTask(String name, StateCount stateCount)
        {
            this.Name = name;
            this.State = new ToDoState(stateCount);
            this.Discussions = new(new ToDoState(stateCount));
            this.Developer = new User("", "", "");
        }
        public bool AddDeveloper(User developer)
        {
            bool canAdd = this.Developer!.Name == "" && developer!.HasRole(typeof(Developer).ToString());

            if (canAdd)
                this.Developer = developer;
            return canAdd;
        }
        public void ChangeState(IState state)
        {
            this.State.ChangeStateCount(state.GetType().ToString());
            State = state;
        }
        public void RemoveDeveloper()
        {
            Developer = new User("", "", "");
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
            var canChange = state.GetType().ToString()! switch
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
            Console.WriteLine(Developer.Name);
            Console.WriteLine(Developer!.Name.Equals(user.Name));
            bool canChangeState = State.ChangeStateToDoing(user, state.MaxItems) && Developer!.Name.Equals(user.Name);
            if (canChangeState) ChangeState(state);
            return canChangeState;
        }

        private bool CanChangeStateToTodo(User user, IState state)
        {
            bool canChangeState = State.ChangeStateToToDo(user);

            if (canChangeState) ChangeState(state);
            return canChangeState;
        }

        private bool CanChangeStateToReadyForTesting(User user, IState state)
        {
            bool canChangeState = State.ChangeStateToReadyForTesting(user);

            if (canChangeState) ChangeState(state);
            return canChangeState;
        }

        private bool CanChangeStateToTesting(User user, IState state)
        {
            bool canChangeState = State.ChangeStateToTesting(user, state.MaxItems);

            if (canChangeState) ChangeState(state);
            return canChangeState;
        }

        private bool CanChangeStateToTested(User user, IState state)
        {
            bool canChangeState = State.ChangeStateToTested(user);

            if (canChangeState) ChangeState(state);
            return canChangeState;
        }

        private bool CanChangeStateToDone(User user, IState state)
        {
            bool canChangeState = State.ChangeStateToDone(user);

            if (canChangeState) ChangeState(state);
            return canChangeState;
        }
    }
}
