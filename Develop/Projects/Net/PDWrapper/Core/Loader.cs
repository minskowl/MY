using System;
using System.Collections;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;

namespace PDWrapper
{
    public class Loader
    {
        AppDomain appDomain;
        RemoteLoader remoteLoader;

        public IApplication LoadAppliction(string filePath, object instance)
        {
            AppDomainSetup setup = new AppDomainSetup();
            setup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
            setup.ShadowCopyDirectories = Path.GetDirectoryName(filePath);
            setup.ShadowCopyFiles = "true";            
            //setup.PrivateBinPath = 
            //	AppDomain.CurrentDomain.BaseDirectory;
            //setup.CachePath
            setup.ApplicationName = "Graph";



            appDomain = AppDomain.CreateDomain("PdDomain", null, setup);

            SetAppDomainPolicy();

            remoteLoader = (RemoteLoader)appDomain.CreateInstanceAndUnwrap(
                        "PDWrapper",
                        "PDWrapper.RemoteLoader");

            return remoteLoader.LoadAppliction(Path.GetFileName(filePath), instance);
        }

        public void Unload()
        {
            AppDomain.Unload(appDomain);
            appDomain = null;
        }
        void SetAppDomainPolicy()
        {
            // Create an AppDomain policy level.
            PolicyLevel pLevel = PolicyLevel.CreateAppDomainLevel();
            // The root code group of the policy level combines all
            // permissions of its children.
            UnionCodeGroup rootCodeGroup;
            PermissionSet ps = new PermissionSet(PermissionState.None);
            ps.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
            rootCodeGroup = new UnionCodeGroup(new AllMembershipCondition(),
                new PolicyStatement(ps, PolicyStatementAttribute.Nothing));

            NamedPermissionSet localIntranet = FindNamedPermissionSet("LocalIntranet");
            // The following code limits all code on this machine to local intranet permissions
            // when running in this application domain.
            UnionCodeGroup virtualIntranet = new UnionCodeGroup(
                new ZoneMembershipCondition(SecurityZone.MyComputer),
                new PolicyStatement(localIntranet,
                PolicyStatementAttribute.Nothing));
            virtualIntranet.Name = "Virtual Intranet";
            // Add the code groups to the policy level.
            rootCodeGroup.AddChild(virtualIntranet);
            pLevel.RootCodeGroup = rootCodeGroup;
            appDomain.SetAppDomainPolicy(pLevel);
        }

        private NamedPermissionSet FindNamedPermissionSet(string name)
        {
            IEnumerator policyEnumerator = SecurityManager.PolicyHierarchy();

            while (policyEnumerator.MoveNext())
            {
                PolicyLevel currentLevel = (PolicyLevel)policyEnumerator.Current;

                if (currentLevel.Label == "Machine")
                {
                    IList namedPermissions = currentLevel.NamedPermissionSets;
                    IEnumerator namedPermission = namedPermissions.GetEnumerator();

                    while (namedPermission.MoveNext())
                    {
                        if (((NamedPermissionSet)namedPermission.Current).Name == name)
                        {
                            return ((NamedPermissionSet)namedPermission.Current);
                        }
                    }
                }
            }
            return null;
        }

        

    }
}
