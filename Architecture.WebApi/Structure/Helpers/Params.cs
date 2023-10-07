using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Architecture.WebApi.Structure.Helpers;

public static class Params
{
    public static string GetRoute(Type typeParams, string prefix)
    {
        var translate = "";

        var properties = typeParams.GetProperties();
        var parameters = new List<string>();

        properties.ToList().ForEach(prop =>
        {
            var attr = prop.GetCustomAttributes<FromRouteAttribute>()?.FirstOrDefault();
            if (attr != null)
            {
                parameters.Add(string.Concat("{", attr.Name, "}"));
            }
        });

        translate = string.Join("/", parameters);

        return $"{prefix}/{translate}";
    }

    public static string GetRoute(Type typeParams)
    {
        var translate = "";

        var properties = typeParams.GetProperties();
        var parameters = new List<string>();

        properties.ToList().ForEach(prop =>
        {
            var attr = prop.GetCustomAttributes<FromRouteAttribute>()?.FirstOrDefault();
            if (attr != null)
            {
                parameters.Add(string.Concat("{", attr.Name, "}"));
            }
        });

        translate = string.Join("/", parameters);

        return $"{translate}";
    }
}
