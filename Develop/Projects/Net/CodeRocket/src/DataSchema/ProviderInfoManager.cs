using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Savchin.Data.Schema
{
    //public class ProviderInfoManager
    //{
    //    private static ProviderInfoManager instance;
    //    private static readonly Hashtable ht = new Hashtable();
    //    private static readonly ProvidersInfo ds = new ProvidersInfo();

    //    /// <summary>
    //    /// Creates a new <see cref="ProviderInfoManager"/> instance.
    //    /// </summary>
    //    private ProviderInfoManager()
    //    {
    //        Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream("Adapdev.Data.Xml.ProviderInfo.xml");
    //        ds.ReadXml(s);
    //        this.LoadTypes();
    //        if (ht.Count == 0)
    //        {
    //            throw new Exception("DatabaseProviders contains no information.");
    //        }
    //    }

    //    /// <summary>
    //    /// Gets the instance.
    //    /// </summary>
    //    /// <returns></returns>
    //    public static ProviderInfoManager GetInstance()
    //    {
    //        if (instance == null)
    //        {
    //            instance = new ProviderInfoManager();
    //        }
    //        return instance;
    //    }

    //    /// <summary>
    //    /// Loads the types.
    //    /// </summary>
    //    private void LoadTypes()
    //    {
    //        foreach (ProvidersInfo.ProviderInfoRow dr in ds.ProviderInfo.Rows)
    //        {
    //            Hashtable tht = new Hashtable();
    //            ht.Add(dr.Name.Trim().ToLower(), tht);
    //        }

    //        foreach (ProvidersInfo.TypeRow tr in ds.Type.Rows)
    //        {
    //            Hashtable tht = (Hashtable)ht[tr.ProviderInfoRow.Name.Trim().ToLower()];
    //            tht.Add(tr.Name.ToLower(), tr);
    //        }
    //    }

    //    /// <summary>
    //    /// Gets the prefix by name
    //    /// </summary>
    //    /// <param name="name">Name.</param>
    //    /// <param name="typeName">Name of the type.</param>
    //    /// <returns></returns>
    //    public string GetPrefixByName(string name, string typeName)
    //    {
    //        return this.GetTypeRowByName(name, typeName).Prefix;
    //    }

    //    /// <summary>
    //    /// Gets the prefix by name
    //    /// </summary>
    //    /// <param name="ct">Ct.</param>
    //    /// <param name="typeName">Name of the type.</param>
    //    /// <returns></returns>
    //    public string GetPrefixByName(DbProviderType ct, string typeName)
    //    {
    //        return this.GetPrefixByName(ct.ToString(), typeName);
    //    }

    //    /// <summary>
    //    /// Gets the postfix by name
    //    /// </summary>
    //    /// <param name="name">Name.</param>
    //    /// <param name="typeName">Name of the type.</param>
    //    /// <returns></returns>
    //    public string GetPostfixByName(string name, string typeName)
    //    {
    //        return this.GetTypeRowByName(name, typeName).Postfix;
    //    }

    //    /// <summary>
    //    /// Gets the postfix by name
    //    /// </summary>
    //    /// <param name="ct">Ct.</param>
    //    /// <param name="typeName">Name of the type.</param>
    //    /// <returns></returns>
    //    public string GetPostfixByName(DbProviderType ct, string typeName)
    //    {
    //        return this.GetPostfixByName(ct.ToString(), typeName);
    //    }

    //    /// <summary>
    //    /// Gets the postfix by name
    //    /// </summary>
    //    /// <param name="name">Name.</param>
    //    /// <param name="typeName">Name of the type.</param>
    //    /// <returns></returns>
    //    public string GetObjectByName(string name, string typeName)
    //    {
    //        return this.GetTypeRowByName(name, typeName).Object;
    //    }

    //    /// <summary>
    //    /// Gets the object by name
    //    /// </summary>
    //    /// <param name="providerType">Provider type</param>
    //    /// <param name="typeName">Name of the type.</param>
    //    /// <returns></returns>
    //    public string GetObjectByName(DbProviderType providerType, string typeName)
    //    {
    //        return this.GetObjectByName(providerType.ToString(), typeName);
    //    }

    //    /// <summary>
    //    /// Gets the object by id.
    //    /// </summary>
    //    /// <param name="name">Name.</param>
    //    /// <param name="id">Id.</param>
    //    /// <returns></returns>
    //    public string GetObjectById(string name, int id)
    //    {
    //        ProviderInfo p = this.GetTypeRowById(name, id);
    //        if (p != null)
    //            return p.Object;
    //        else
    //            return "";
    //    }

    //    /// <summary>
    //    /// Gets the object by id.
    //    /// </summary>
    //    /// <param name="ct">Ct.</param>
    //    /// <param name="id">Id.</param>
    //    /// <returns></returns>
    //    public string GetObjectById(DbProviderType ct, int id)
    //    {
    //        return this.GetObjectById(ct.ToString(), id);
    //    }

    //    /// <summary>
    //    /// Gets the name by id.
    //    /// </summary>
    //    /// <param name="name">Name.</param>
    //    /// <param name="id">Id.</param>
    //    /// <returns></returns>
    //    public string GetNameById(string name, int id)
    //    {
    //        ProviderInfo p = this.GetTypeRowById(name, id);
    //        if (p != null)
    //            return p.Name;
    //        else
    //            return "";
    //    }

    //    /// <summary>
    //    /// Gets the name by id.
    //    /// </summary>
    //    /// <param name="providerType">Provider type.</param>
    //    /// <param name="id">Id.</param>
    //    /// <returns></returns>
    //    public string GetNameById(DbProviderType providerType, int id)
    //    {
    //        return this.GetNameById(providerType.ToString(), id);
    //    }

    //    /// <summary>
    //    /// Gets the default by id.
    //    /// </summary>
    //    /// <param name="name">Name.</param>
    //    /// <param name="id">Id.</param>
    //    /// <returns></returns>
    //    public string GetDefaultById(string name, int id)
    //    {
    //        ProviderInfo p = this.GetTypeRowById(name, id);
    //        if (p != null)
    //            return p.Default;
    //        else
    //            return "";
    //    }

    //    /// <summary>
    //    /// Gets the default by id.
    //    /// </summary>
    //    /// <param name="providerType">Provider type.</param>
    //    /// <param name="id">Id.</param>
    //    /// <returns></returns>
    //    public string GetDefaultById(DbProviderType providerType, int id)
    //    {
    //        return this.GetDefaultById(providerType.ToString(), id);
    //    }

    //    /// <summary>
    //    /// Gets the test default by id.
    //    /// </summary>
    //    /// <param name="name">Name.</param>
    //    /// <param name="id">Id.</param>
    //    /// <returns></returns>
    //    public string GetTestDefaultById(string name, int id)
    //    {
    //        ProviderInfo p = this.GetTypeRowById(name, id);
    //        if (p != null)
    //            return p.TestDefault;
    //        else
    //            return "";
    //    }

    //    /// <summary>
    //    /// Gets the test default by id.
    //    /// </summary>
    //    /// <param name="providerType">Provider type.</param>
    //    /// <param name="id">Id.</param>
    //    /// <returns></returns>
    //    public string GetTestDefaultById(DbProviderType providerType, int id)
    //    {
    //        return this.GetTestDefaultById(providerType.ToString(), id);
    //    }

    //    /// <summary>
    //    /// Gets the name of the id by.
    //    /// </summary>
    //    /// <param name="name">Name.</param>
    //    /// <param name="typeName">Name of the type.</param>
    //    /// <returns></returns>
    //    public int GetIdByName(string name, string typeName)
    //    {
    //        return Convert.ToInt32(this.GetTypeRowByName(name, typeName).Id);
    //    }

    //    /// <summary>
    //    /// Gets the name of the id by.
    //    /// </summary>
    //    /// <param name="providerType">Provider type.</param>
    //    /// <param name="typeName">Name of the type.</param>
    //    /// <returns></returns>
    //    public int GetIdByName(DbProviderType providerType, string typeName)
    //    {
    //        return this.GetIdByName(providerType.ToString(), typeName);
    //    }

    //    /// <summary>
    //    /// Gets the name of the type row by.
    //    /// </summary>
    //    /// <param name="name">Name.</param>
    //    /// <param name="typeName">Name of the type.</param>
    //    /// <returns></returns>
    //    protected ProviderInfo GetTypeRowByName(string name, string typeName)
    //    {
    //        string _name = name.Trim().ToLower();
    //        string _typeName = typeName.Trim().ToLower();

    //        if (ht.Contains(_name))
    //        {
    //            Hashtable tht = (Hashtable)ht[_name];
    //            if (tht.Contains(_typeName))
    //            {
    //                ProvidersInfo.TypeRow tr = (ProvidersInfo.TypeRow)tht[_typeName];
    //                return this.BuildProviderInfo(tr);
    //            }
    //        }
    //        else
    //        {
    //            throw new Exception(String.Format("TypeRow not found. name: {0}, typeName: {1}", name, typeName));
    //        }
    //        return null;
    //    }

    //    /// <summary>
    //    /// Gets the type row by id.
    //    /// </summary>
    //    /// <param name="name">Name.</param>
    //    /// <param name="id">Id.</param>
    //    /// <returns></returns>
    //    protected ProviderInfo GetTypeRowById(string name, int id)
    //    {
    //        string _name = name.Trim().ToLower();
    //        if (ht.Contains(_name))
    //        {
    //            Hashtable tht = (Hashtable)ht[_name];
    //            foreach (DictionaryEntry d in tht)
    //            {
    //                ProvidersInfo.TypeRow tr = (ProvidersInfo.TypeRow)d.Value;
    //                if (tr.Id.Equals(id.ToString())) return this.BuildProviderInfo(tr);
    //            }
    //        }
    //        else
    //        {
    //            throw new Exception(String.Format("TypeRow not found. name: {0}, id: {1}", name, id));
    //        }
    //        return null;
    //    }

    //    /// <summary>
    //    /// Builds the provider info.
    //    /// </summary>
    //    /// <param name="r">Row.</param>
    //    /// <returns></returns>
    //    protected ProviderInfo BuildProviderInfo(ProvidersInfo.TypeRow r)
    //    {
    //        ProviderInfo pr = new ProviderInfo();
    //        pr.Default = r.Default;
    //        pr.Id = r.Id;
    //        pr.Name = r.Name;
    //        pr.Object = r.Object;
    //        pr.Postfix = r.Postfix;
    //        pr.Prefix = r.Prefix;
    //        pr.TestDefault = r.TestDefault;

    //        return pr;
    //    }
    //}

}
