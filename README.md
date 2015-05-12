# CODEFramework.NLog
I was using [CODEFramework](http://codeframework.codeplex.com/) in a project, and I really like the [LoggingMediator](http://codeframework.codeplex.com/wikipage?title=Logging%20Framework). Particularly the fact that it displays the errors in Development service host when you're using that feature. However, I wasn't particularly happy with the built in output methods. I really like [NLog](http://nlog-project.org/), but I don't like having the GetCurrentClassLogger call scattered throughout my code.

There are several great solutions for the NLog problem - for instance [NLog.Interface](https://www.nuget.org/packages/NLog.Interface/) or [Common.Logging](https://www.nuget.org/packages/Common.Logging/). But, I wanted to use LoggingMediator with NLog.

So, here is the CODEFramework.NLog plugin.

You will need to reference NLog.Config in whichever assemblies you would *normally* reference NLog.Config. Aside from that, just reference this and CODEFramework.Core and you should be good to go.

Please let me know if you'd like to contribute!
