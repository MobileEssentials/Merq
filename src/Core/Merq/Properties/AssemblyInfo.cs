﻿using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: AssemblyVersion("1.0.0")]
[assembly: AssemblyFileVersion(ThisAssembly.Git.BaseVersion.Major + "." + ThisAssembly.Git.BaseVersion.Minor + "." + ThisAssembly.Git.BaseVersion.Patch)]
[assembly: AssemblyInformationalVersion(ThisAssembly.Git.BaseVersion.Major + "." + ThisAssembly.Git.BaseVersion.Minor + "." + ThisAssembly.Git.BaseVersion.Patch + "-" + ThisAssembly.Git.Branch + "+" + ThisAssembly.Git.Commit)]

[assembly: InternalsVisibleTo ("Merq.Core.Tests, PublicKey=00240000048000009400000006020000002400005253413100040000010001009d69078301b6c4881e95cd924d5e355a4d24ba3d28fb571e00124706538eef959eb371fbb9bd2776fbe7d228178df51fbd4a849aff37161190f3254c77167d16e42c2be32c817537b67b874b66be01a4b6d1c780ff052c8f7f52cad6751288911d450cf443ed4f40b266332f6f25204df23df9a23d38e5fe19f6372a636c7da1")]