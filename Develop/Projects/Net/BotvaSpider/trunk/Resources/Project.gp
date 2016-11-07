<?xml version="1.0" encoding="utf-8"?>
<GenerateProject xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" ProjectType="Schema" VisualStudioVersion="v90">
  <SchemaFile>.\Data.schema</SchemaFile>
  <SolutionFile>D:\Develop\Projects\Net\KnowledgeBase\KnowledgeBase.sln</SolutionFile>
  <OutputPath>D:\Tmp</OutputPath>
  <TemplatePath>.</TemplatePath>
  <LastView />
  <Properties>
    <Property Name="namespace" Value="Site" />
  </Properties>
  <Generations>
    <Generation TemplateFile="EntityValue.tcs" DestinationFile="{0}Value.cs" ObjectType="dfg" 
                DestinationDirectory="S:\Develop\Projects\Net\BotvaSpider\trunk\Site.Core\Dal\Entities" SolutionPath="KnowledgeBase\DAL\Entities" />
    <Generation TemplateFile="Entity.tcs" DestinationFile="{0}.cs" ObjectType="dfg" 
                DestinationDirectory="S:\Develop\Projects\Net\BotvaSpider\trunk\Site.Core\Bl\Entities" SolutionPath="KnowledgeBase\DAL\Entities" />
    <Generation TemplateFile="Factory.tcs" DestinationFile="{0}Factory.cs" ObjectType="dfg" 
                DestinationDirectory="S:\Develop\Projects\Net\BotvaSpider\trunk\Site.Core\Dal\Factories" SolutionPath="KnowledgeBase\DAL\Factories" />
    <Generation TemplateFile="Manager.tcs" DestinationFile="{0}Manager.cs" ObjectType="dfg" 
                DestinationDirectory="S:\Develop\Projects\Net\BotvaSpider\trunk\Site.Core\Bl\Managers" SolutionPath="KnowledgeBase\BussinesLayer\Managers" />
   <Generation TemplateFile="DAO.tsql" DestinationFile="{0}SP.sql" ObjectType="dfg" 
                DestinationDirectory="S:\Develop\Projects\Net\BotvaSpider\trunk\Resources" SolutionPath="KnowledgeBase\DataBase\Factories\Schema Objects\Stored Procedures" />
    <!--Generation TemplateFile="ManagerTests.tcs" DestinationFile="{0}ManagerTests.cs" ObjectType="dfg" 
                DestinationDirectory="S:\Develop\Projects\Net\KnowledgeBase\BussinesLayerTests\Generated" SolutionPath="KnowledgeBase\BussinesLayerTests\Generated" />
  
    <Generation TemplateFile="spGetBy.tsql" DestinationFile="Run.sql" ObjectType="dfg" 
                DestinationDirectory="S:\Develop\Projects\Net\KnowledgeBase\Database\Scripts" SolutionPath="" /-->
  </Generations>
  <BookMarks />
</GenerateProject>