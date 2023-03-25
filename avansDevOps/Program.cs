// See https://aka.ms/new-console-template for more information
using avansDevOps;
using avansDevOps.Backlog;
using avansDevOps.Backlog.TasklStates__State_;
using avansDevOps.Users;


Console.WriteLine("Hello World!");

var _stateCount = new StateCount();
var _backlog = new BacklogItem(1, "", _stateCount);
var _subtask = new SubTask("", _stateCount);
var _subtask2 = new SubTask("", _stateCount);
var _subtask3 = new SubTask("", _stateCount);
var _state = new DoingState(_stateCount);

_backlog.AddSubtask(_subtask);
_backlog.AddSubtask(_subtask2);
_backlog.AddSubtask(_subtask3);

var _sprint = new Sprint();
_sprint.AddBacklogItem(_subtask);
_sprint.AddBacklogItem(_subtask2);
_sprint.AddBacklogItem(_subtask3);
_sprint.AddBacklogItem(_backlog);
var role = new Developer();
var _user = new User("", "i", "");
var user2 = new User("", "", "");
_user.AddRole(role);
user2.AddRole(role);

_backlog.AddDeveloper(_user);
_subtask.AddDeveloper(_user);
_subtask2.AddDeveloper(_user);
_subtask3.AddDeveloper(_user);

_sprint.AddDeveloper(_user);
_sprint.AddDeveloper(user2);

var result = _subtask.CanChangeState(_user, new DoingState(_stateCount));
Console.WriteLine(result);

