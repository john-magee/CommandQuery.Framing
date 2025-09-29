using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommandQuery.Framing;

internal class Scanner
{
    private readonly List<Assembly> _asm = [];
    private readonly List<Type> _doNotInclude = [];

    public Scanner ScanAssemblyWithType<T>()
    {
        _asm.Add(typeof(T).Assembly);

        return this;
    }

    public Scanner DoNotIncludeType<T>()
    {
        _doNotInclude.Add(typeof(T));

        return this;
    }

    public void Scan(IServiceCollection serviceCollection)
    {
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            builder.SetMinimumLevel(LogLevel.Information);
        });

        var logger = loggerFactory.CreateLogger<AssemblyConventionScanner>();

        new AssemblyConventionScanner(logger)
            .Assemblies([.. _asm])
            .Do(foundInterface =>
                {
                    var implInterface = foundInterface.GetTypeInfo().ImplementedInterfaces.ToList();

                    implInterface.Add(foundInterface);

                    if (_doNotInclude.Count != 0 && !implInterface.Any(x => _doNotInclude.Any(i => i == x)))
                    {
                        foreach (var type in implInterface)
                        {
                            serviceCollection.AddTransient(type, foundInterface);
                        }
                    }
                })
            .Execute();
    }
}