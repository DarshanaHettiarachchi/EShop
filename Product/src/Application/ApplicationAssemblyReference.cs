using System.Reflection;

namespace Application;

#pragma warning disable CA1052 // Static holder types should be Static or NotInheritable
#pragma warning disable S1118 // Utility classes should not have public constructors
public class ApplicationAssemblyReference
#pragma warning restore S1118 // Utility classes should not have public constructors
#pragma warning restore CA1052 // Static holder types should be Static or NotInheritable
{
    internal static readonly Assembly Assembly = typeof(ApplicationAssemblyReference).Assembly;
}
