using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;

[UnsetVisualStudioEnvironmentVariables]
class Build : NukeBuild
{
    // Invoke: nuke --plan
    public static int Main() => Execute<Build>(x => x.Compile);

    Target Clean => _ => _
        .Before(Restore);

    Target Restore => _ => _;

    Target Compile => _ => _
        .DependsOn(Restore);

    Target Pack => _ => _
        .DependsOn(Compile);

    Target Test => _ => _
        .DependsOn(Compile);

    Target Publish => _ => _
        .DependsOn(Clean, Test, Pack);

    Target Announce => _ => _
        .TriggeredBy(Publish);
}
