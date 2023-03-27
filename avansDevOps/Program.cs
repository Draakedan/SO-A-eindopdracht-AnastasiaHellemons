// See https://aka.ms/new-console-template for more information
using avansDevOps;
using avansDevOps.Backlog;
using avansDevOps.Backlog.DiscussuionForum;
using avansDevOps.Backlog.TasklStates__State_;
using avansDevOps.DevelopmentPipeline__facade_;
using avansDevOps.Git__Adapter_;
using avansDevOps.Users;
using System.Threading;

var _sprint = new Sprint();


var _pipeline = new DevelopmentPipeline();

Console.WriteLine("Hello World!");

var developer = new Developer();
var user = new User("", "", "");
user.AddRole(developer);

var result = _sprint.CanEditSprint(user);

Console.WriteLine(result);

