<?xml version="1.0" encoding="utf-8"?>
<GenerateProject xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" ProjectType="Schema" VisualStudioVersion="v90">
  <SchemaFile>.\Data.schema</SchemaFile>
  <SolutionFile>D:\Develop\Projects\Net\KnowledgeBase\KnowledgeBase.sln</SolutionFile>
  <OutputPath>D:\Tmp</OutputPath>
  <TemplatePath>.</TemplatePath>
  <LastView />
  <Properties>
    <Property Name="namespace" Value="KnowledgeBase" />
  </Properties>
  <Generations>
    <Generation TemplateFile="EntityValue.tcs" DestinationFile="{0}Value.cs" ObjectType="dfg" 
                DestinationDirectory="S:\Develop\Projects\Net\KnowledgeBase\DAL\Entities" SolutionPath="KnowledgeBase\DAL\Entities" />
    <Generation TemplateFile="Entity.tcs" DestinationFile="{0}.cs" ObjectType="dfg" 
                DestinationDirectory="S:\Develop\Projects\Net\KnowledgeBase\BussinesLayer\Entities" 
                SolutionPath="KnowledgeBase\DAL\Entities" />
    <Generation TemplateFile="Mssql.Factory.tcs" DestinationFile="{0}Factory.cs" ObjectType="dfg" 
                DestinationDirectory="S:\Develop\Projects\Net\KnowledgeBase\DAL\Factories" 
                SolutionPath="KnowledgeBase\DAL\Factories" />
    <Generation TemplateFile="SqlLite.Factory.tcs" DestinationFile="{0}Factory.Gen.cs" ObjectType="dfg"
                DestinationDirectory="D:\MY\Develop\Projects\Net\KnowledgeBase\KnowledgeBase.SqlLite.Dal\Factories"
                SolutionPath="KnowledgeBase\DAL\Factories" />    
    <Generation TemplateFile="Manager.tcs" DestinationFile="{0}Manager.cs" ObjectType="dfg" 
                DestinationDirectory="S:\Develop\Projects\Net\KnowledgeBase\BussinesLayer\Managers" SolutionPath="KnowledgeBase\BussinesLayer\Managers" />
    <Generation TemplateFile="ManagerTests.tcs" DestinationFile="{0}ManagerTests.cs" ObjectType="dfg" 
                DestinationDirectory="S:\Develop\Projects\Net\KnowledgeBase\BussinesLayerTests\Generated" SolutionPath="KnowledgeBase\BussinesLayerTests\Generated" />
    <Generation TemplateFile="DAO.tsql" DestinationFile="{0}SP.sql" ObjectType="dfg" 
                DestinationDirectory="S:\Develop\Projects\Net\KnowledgeBase\Database\Schema Objects\Stored Procedures" SolutionPath="KnowledgeBase\DataBase\Factories\Schema Objects\Stored Procedures" />
    <Generation TemplateFile="spGetBy.tsql" DestinationFile="Run.sql" ObjectType="dfg" 
                DestinationDirectory="S:\Develop\Projects\Net\KnowledgeBase\Database\Scripts" SolutionPath="" />
  </Generations>
  <BookMarks />
</GenerateProject>