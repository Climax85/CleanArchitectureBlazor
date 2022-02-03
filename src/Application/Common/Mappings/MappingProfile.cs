using System.Reflection;
using Application.Shared.Common.Mappings;
using AutoMapper;

namespace CleanArchitecture.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ApplyMappingsFromAssembly(new[] { Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(IMapFrom<>))! });
    }

    private void ApplyMappingsFromAssembly(Assembly[] assemblys)
    {
        foreach (Assembly assembly in assemblys)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping")
                                 ?? type.GetInterface("IMapFrom`1")!.GetMethod("Mapping");

                methodInfo?.Invoke(instance, new object[] { this });

            }
        }
    }
}
