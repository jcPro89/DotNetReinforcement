using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TareksAccount.Logic.Projects
{
    class ProjectsLogic
    {
        public static int AddProject(int pClientId, string pName, string pDescription)
        {
            return Data.Projects.ProjectData.AddProject(pClientId, pName, pDescription);
        }

        public static DataTable AllProjects(int pCompanyId)
        {
            return Data.Projects.ProjectData.AllProjects(pCompanyId);
        }
    }
}
