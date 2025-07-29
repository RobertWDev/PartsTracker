using System.Reflection;
using PartsTracker.Modules.Parts.Domain.Parts;
using PartsTracker.Modules.Parts.Infrastructure;

namespace PartsTracker.Modules.Parts.ArchitectureTests.Abstractions;

#pragma warning disable CA1515 // Consider making public types internal
public abstract class BaseTest
#pragma warning restore CA1515 // Consider making public types internal
{
    protected static readonly Assembly ApplicationAssembly = typeof(Parts.Application.AssemblyReference).Assembly;

    protected static readonly Assembly DomainAssembly = typeof(Part).Assembly;

    protected static readonly Assembly InfrastructureAssembly = typeof(PartsModule).Assembly;

    protected static readonly Assembly PresentationAssembly = typeof(Parts.Presentation.AssemblyReference).Assembly;
}
