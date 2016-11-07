using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Savchin.CodeGenration
{


    [Serializable()]
    public class GenerateProject
    {

        [XmlArrayItem(ElementName = "Generation", Type = typeof(Generation))]
        [XmlArray()]
        public List<Generation> Generations;

        [XmlArrayItem(ElementName = "Property", Type = typeof(ProjectProperty))]
        [XmlArray()]
        public List<ProjectProperty> Properties;

        public String PDModelFile=string.Empty;
        public String SolutionFile=string.Empty;
        public String TemplatePath = string.Empty;
        public String OutputPath = string.Empty;
        
        public static GenerateProject Initialize(String fileName)
        {
            GenerateProject result;
            try
            {
                result = Load(fileName);
            }

            catch
            {
                result = new GenerateProject();
                result.Generations = new List<Generation>();
            }

            return result;
        }

        public static GenerateProject Load(String fileName)
        {
            StreamReader fs;
            XmlSerializer ConSerializer = new XmlSerializer(typeof(GenerateProject));
            fs = new StreamReader(fileName);
            GenerateProject s = ((GenerateProject)(ConSerializer.Deserialize(fs)));
            fs.Close();
            if (s == null)
            {
                s = new GenerateProject();
                s.Generations = new List<Generation>();
                s.Properties = new List<ProjectProperty>();
            }
            return s;
        }

        public void Save(String fileName)
        {
            try
            {
                StreamWriter myWriter;
                XmlSerializer OptSerializer = new XmlSerializer(typeof(GenerateProject));
                myWriter = new StreamWriter(fileName);
                OptSerializer.Serialize(myWriter, this);
                myWriter.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    [Serializable()]
    public class ProjectProperty
    {

        [XmlAttributeAttribute()]
        public String Name;

        [XmlAttributeAttribute()]
        public String Value;

        ProjectProperty()
        {
        }

        ProjectProperty(String name, String value)
        {
            this.Name = name;
            this.Value = value;
        }
    }

    [Serializable()]
    public class Generation
    {

        [XmlAttributeAttribute()]
        public String TemplateFile;

        [XmlAttributeAttribute()]
        public String DestinationFile;

        [XmlAttributeAttribute()]
        public Boolean toOutput;

        [XmlAttributeAttribute()]
        public String DestinationDirectory;

        [XmlAttributeAttribute()]
        public String SolutionPath;

        Generation()
        {
        }

        Generation(String templateFile, String destinationFile)
        {
            this.TemplateFile = templateFile;
            this.DestinationFile = destinationFile;
        }
    }

}
