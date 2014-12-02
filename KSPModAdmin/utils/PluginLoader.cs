using KSPModAdmin.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace KSPModAdmin.Utils
{
    public interface IKSPMAPlugin
    {
        // TODO: Interface definition.

        ucBase GetUserControl();
    }

    public static class PluginLoader
    {
        public static List<T> LoadPlugins<T>(string path)
        {
            if (!Directory.Exists(path))
                return new List<T>();

            string[] dllFileNames = Directory.GetFiles(path, "*.dll");

            List<Assembly> assemblies = new List<Assembly>(dllFileNames.Length);
            foreach (string dllFile in dllFileNames)
            {
                AssemblyName assemblyName = AssemblyName.GetAssemblyName(dllFile);
                Assembly assembly = Assembly.Load(assemblyName);
                assemblies.Add(assembly);
            }

            Type pluginType = typeof(T);
            List<Type> pluginTypes = new List<Type>();
            foreach (Assembly assembly in assemblies)
            {
                if (assembly == null) 
                    continue;

                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    if (type.IsInterface || type.IsAbstract)
                        continue;
                        
                    if (type.GetInterface(pluginType.FullName) != null)
                        pluginTypes.Add(type);
                }
            }

            List<T> plugins = new List<T>(pluginTypes.Count);
            plugins.AddRange(pluginTypes.Select(type => (T)Activator.CreateInstance(type)));

            return plugins;
        }
    }
}
