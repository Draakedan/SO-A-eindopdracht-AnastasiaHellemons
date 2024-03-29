﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.Users
{
    public class Developer : IRole
    {
        public string Name { get; }
        private readonly Dictionary<string, int> EffortPoints;

        public Developer()
        {
            Name = "Developer";
            EffortPoints = new();
        }

        public bool CanAssingPoints(User user, string project, int points) 
        {
            bool isScumMaster = false;
            foreach(IRole role in user.Roles)
                if (role.GetType() == typeof(ScrumMaster))
                    isScumMaster = true;
            if (isScumMaster)
                AssignPoints(project, points);
            return isScumMaster;
        }

        public void AssignPoints(string project, int points)
        {
            if (!ProjectExists(project))
                MakeNewProject(project);

            EffortPoints[project] += points;
        }

        public bool CanRemovePoints(User user, string project, int points) 
        {
            bool isScumMaster = false;
            foreach (IRole role in user.Roles)
                if (role.GetType() == typeof(ScrumMaster))
                    isScumMaster = true;
            if (isScumMaster)
                RemovePoints(project, points);
            return isScumMaster;
        }

        public void RemovePoints(string project, int points)
        {
            if (!ProjectExists(project))
                MakeNewProject(project);

            EffortPoints[project] -= points;
        }

        public int ViewPoints(string project)
        {
            if (!ProjectExists(project))
                return -1;

            return EffortPoints[project];
        }

        public void ResetPoints(string project)
        {
            if (!ProjectExists(project))
                MakeNewProject(project);

            EffortPoints[project] = 0;
        }

        private bool ProjectExists(string project)
        {
            return EffortPoints.ContainsKey(project);
        }

        private void MakeNewProject(string project)
        {
            EffortPoints.Add(project, 0);
        }

        private void DeleteProject(string project)
        {
            EffortPoints.Remove(project);
        }

        public void JoinProject(string name)
        {
            Console.WriteLine($"Joined {name} as {Name}!");
            MakeNewProject(name);
        }

        public void LeaveProject(string name)
        {
            Console.WriteLine($"Left {name} as {Name}!");
            DeleteProject(name);
        }
    }
}
