using System;
using System.Collections.Generic;
using NHibernate.Cfg;
using NHibernate.Validator.Constraints;

namespace TVSeriesTracker.Core
{
  [System.CodeDom.Compiler.GeneratedCode("NHibernateModelGenerator", "1.0.0.0")]
  public partial class Movie
  {
    public virtual long MovieId { get; set; }
    [NotNull]
    [Length(Max=50)]
    public virtual string ImdbId { get; set; }
    [NotNull]
    public virtual string Title { get; set; }
    [Length(Max=50)]
    public virtual string Season { get; set; }
    [Length(Max=50)]
    public virtual string Episode { get; set; }

    static partial void CustomizeMappingDocument(System.Xml.Linq.XDocument mappingDocument);

    internal static System.Xml.Linq.XDocument MappingXml
    {
      get
      {
        var mappingDocument = System.Xml.Linq.XDocument.Parse(@"<?xml version='1.0' encoding='utf-8' ?>
<hibernate-mapping xmlns='urn:nhibernate-mapping-2.2'
                   assembly='" + typeof(Movie).Assembly.GetName().Name + @"'
                   namespace='TVSeriesTracker.Core'
                   >
  <class name='Movie'
         table='`Movies`'
         >
    <id name='MovieId'
        column='`MovieId`'
        >
      <generator class='identity'>
      </generator>
    </id>
    <property name='ImdbId'
              column='`ImdbId`'
              />
    <property name='Title'
              column='`Title`'
              />
    <property name='Season'
              column='`Season`'
              />
    <property name='Episode'
              column='`Episode`'
              />
  </class>
</hibernate-mapping>");
        CustomizeMappingDocument(mappingDocument);
        return mappingDocument;
      }
    }
  }


  /// <summary>
  /// Provides a NHibernate configuration object containing mappings for this model.
  /// </summary>
  public static class ConfigurationHelper
  {
    /// <summary>
    /// Creates a NHibernate configuration object containing mappings for this model.
    /// </summary>
    /// <returns>A NHibernate configuration object containing mappings for this model.</returns>
    public static Configuration CreateConfiguration()
    {
      var configuration = new Configuration();
      configuration.Configure();
      ApplyConfiguration(configuration);
      return configuration;
    }

    /// <summary>
    /// Adds mappings for this model to a NHibernate configuration object.
    /// </summary>
    /// <param name="configuration">A NHibernate configuration object to which to add mappings for this model.</param>
    public static void ApplyConfiguration(Configuration configuration)
    {
      configuration.AddXml(ModelMappingXml.ToString());
      configuration.AddXml(Movie.MappingXml.ToString());
      configuration.AddAssembly(typeof(ConfigurationHelper).Assembly);
    }

    /// <summary>
    /// Provides global mappings not associated with a specific entity.
    /// </summary>
    public static System.Xml.Linq.XDocument ModelMappingXml
    {
      get
      {
        var mappingDocument = System.Xml.Linq.XDocument.Parse(@"<?xml version='1.0' encoding='utf-8' ?>
<hibernate-mapping xmlns='urn:nhibernate-mapping-2.2'
                   assembly='" + typeof(ConfigurationHelper).Assembly.GetName().Name + @"'
                   namespace='TVSeriesTracker.Core'
                   >
</hibernate-mapping>");
        return mappingDocument;
      }
    }
  }
}
