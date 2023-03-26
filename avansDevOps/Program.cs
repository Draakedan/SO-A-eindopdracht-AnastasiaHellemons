// See https://aka.ms/new-console-template for more information
using avansDevOps;
using avansDevOps.Backlog;
using avansDevOps.Backlog.DiscussuionForum;
using avansDevOps.Backlog.TasklStates__State_;
using avansDevOps.Git__Adapter_;
using avansDevOps.Users;
using System.Threading;

var _stateCount = new StateCount();
var _user = new User("", "", "");
_user.AddRole(new Developer());
var _response = new Response(new TestedState(_stateCount), "", _user, true);
var _thread = new DiscussionThread(new TestedState(_stateCount), "testTopic", _user, _response);




Console.WriteLine("Hello World!");
var result = _thread.CanDeleteResponse(_user, _response);
Console.WriteLine(result);

