﻿#region CPL License
/*
Nuclex Framework
Copyright (C) 2002-2010 Nuclex Development Labs

This library is free software; you can redistribute it and/or
modify it under the terms of the IBM Common Public License as
published by the IBM Corporation; either version 1.0 of the
License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
IBM Common Public License for more details.

You should have received a copy of the IBM Common Public
License along with this library
*/
#endregion

using System;
using System.Diagnostics;
using System.Reflection;

namespace Nuclex.Support.Plugins {

  /// <summary>Integration host for plugins</summary>
  /// <remarks>
  ///   This class is created by the party that is interested in loading plugins,
  ///   herein referred to as the "plugin user". The plugin host will monitor a
  ///   repository and react to any assembly being loaded into that repository by
  ///   iterating over all types (as in classes and structures) found in the
  ///   assembly and using the employer to do whatever the plugin user intends 
  ///   to do with the types found in that assembly
  /// </remarks>
  public class PluginHost {

    /// <summary>Initializes a plugin host using a new repository</summary>
    /// <param name="employer">Employer used assess and employ the plugin types</param>
    public PluginHost(Employer employer) :
      this(employer, new PluginRepository()) { }

    /// <summary>Initializes the plugin using an existing repository</summary>
    /// <param name="employer">Employer used assess and employ the plugin types</param>
    /// <param name="repository">Repository in which plugins will be stored</param>
    public PluginHost(Employer employer, PluginRepository repository) {
      this.employer = employer;
      this.repository = repository;

      foreach(Assembly assembly in this.repository.LoadedAssemblies) {
        employAssemblyTypes(assembly);
      }

      this.repository.AssemblyLoaded += new AssemblyLoadEventHandler(assemblyLoadHandler);
    }

    /// <summary>The repository containing all loaded plugins</summary>
    public PluginRepository Repository {
      get { return this.repository; }
    }

    /// <summary>The employer that is used by this plugin integration host</summary>
    public Employer Employer {
      get { return this.employer; }
    }

    /// <summary>Responds to a new plugin being loaded into the repository</summary>
    /// <param name="sender">Repository into which the assembly was loaded</param>
    /// <param name="arguments">Event arguments; contains the loaded assembly</param>
    private void assemblyLoadHandler(object sender, AssemblyLoadEventArgs arguments) {
      employAssemblyTypes(arguments.LoadedAssembly);
    }

    /// <summary>Employs all employable types in an assembly</summary>
    /// <param name="assembly">Assembly whose types to assess and to employ</param>
    private void employAssemblyTypes(Assembly assembly) {

      // Iterate all types contained in the assembly
      Type[] types = assembly.GetTypes();
      for(int index = 0; index < types.Length; ++index) {
        Type type = types[index];

        // We'll ignore abstract and non-public types
        if(!type.IsPublic || type.IsAbstract) {
          continue;
        }

        // Types that have been tagged with the [NoPlugin] attribute will be ignored
        object[] attributes = type.GetCustomAttributes(true);
        if(containsNoPluginAttribute(attributes)) {
          continue;
        }

        // Type seems to be acceptable, assess and possibly employ it
        try {
          if(this.employer.CanEmploy(type)) {
            this.employer.Employ(type);
          }
        }
        catch(Exception exception) {
          reportError("Could not employ " + type.ToString() + ": " + exception.Message);
        }
      }

    }

    /// <summary>
    ///   Determines whether the specifies list of attributes contains a NoPluginAttribute
    /// </summary>
    /// <param name="attributes">List of attributes to check</param>
    /// <returns>True if the list contained a NoPluginAttribute, false otherwise</returns>
    private static bool containsNoPluginAttribute(object[] attributes) {
      for(int index = 0; index < attributes.Length; ++index) {
        if(attributes[index] is NoPluginAttribute) {
          return true;
        }
      }
      return false;
    }

    /// <summary>Reports an error to the debugging console</summary>
    /// <param name="error">Error message that will be reported</param>
    private static void reportError(string error) {
#if !XBOX360 && !WINDOWS_PHONE
      Trace.WriteLine(error);
#endif
    }

    /// <summary>Employs and manages types in the loaded plugin assemblies</summary>
    private Employer employer;
    /// <summary>Repository containing all plugins loaded, shared with other hosts</summary>
    private PluginRepository repository;

  }

} // namespace Nuclex.Support.Plugins
