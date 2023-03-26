// See https://aka.ms/new-console-template for more information
using avansDevOps;
using avansDevOps.Backlog;
using avansDevOps.Backlog.TasklStates__State_;
using avansDevOps.Git__Adapter_;
using avansDevOps.Users;


Console.WriteLine("Hello World!");
GitHandler hanlder = new GitHandler();
Console.WriteLine(hanlder.PullCode());

