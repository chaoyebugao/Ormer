using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ormer.ToolWindow.Helper
{
    class ProjectHelper
    {
        /// <summary>
        /// Get solution in type Solution3
        /// </summary>
        /// <returns></returns>
        public static EnvDTE90.Solution3 GetSolution3()
        {
            EnvDTE.DTE dte = (EnvDTE.DTE)Package.GetGlobalService(typeof(EnvDTE.DTE));
            if (dte == null)
            {
                return null;
            }
            EnvDTE90.Solution3 sln3 = (dte.Solution as EnvDTE90.Solution3);
            return sln3;
        }

        public static IEnumerable<Project> GetProjectList()
        {
            EnvDTE90.Solution3 sln3 = GetSolution3();
            var r1 = sln3.Projects;
            IList<Project> ps = new List<Project>();
            foreach (var p in sln3.Projects)
            {
                ps.Add(p as Project);
            }
            return ps;
        }

        public static Project GetProject(string uniqName)
        {
            var ps = GetProjectList();
            var p = ps.FirstOrDefault(m => m.UniqueName == uniqName);
            return p;
        }

        public static ProjectItem AddFolderToProject(Project targetProject, string folderName)
        {
            if (targetProject == null)
            {
                throw new ArgumentNullException("targetProject");
            }
            if (string.IsNullOrEmpty(folderName))
            {
                throw new ArgumentNullException("folderName");
            }
            ProjectItem pi = null;

            var projectPath = targetProject.FullName.Substring(0, targetProject.FullName.LastIndexOf(Path.DirectorySeparatorChar));
            var folderPath = Path.Combine(projectPath, folderName);

            //Check the project contain the folder or not.
            foreach (ProjectItem p in targetProject.ProjectItems)
            {

                if (p.Name == folderName)
                {
                    if (!IOHelper.IsFolder(folderPath))
                    {
                        throw new Exception($"Fail to add the folder to \"{projectPath}\". Item \"{folderPath}\" exists and it is not a folder.");
                    }
                    pi = p;
                    break;
                }
            }

            if (pi != null)
            {
                //Contained, return
                return pi;
            }

            //Not Contained, check the disk is exists.
            var isExists = Directory.Exists(folderPath);
            if (isExists)
            {
                pi = targetProject.ProjectItems.AddFromDirectory(folderPath);
            }
            else
            {
                pi = targetProject.ProjectItems.AddFolder(folderName);
            }

            return pi;
        }

        public static ProjectItem AddFolderToProjectItem(ProjectItem targetProjectItem, string folderName)
        {
            if (targetProjectItem == null)
            {
                throw new ArgumentNullException("targetProjectItem");
            }
            if (string.IsNullOrEmpty(folderName))
            {
                throw new ArgumentNullException("folderName");
            }
            ProjectItem pi = null;
            
            var itemPath = targetProjectItem.Properties.Item("FullPath").Value;
            var itemPathStr = itemPath.ToString();
            var folderPath = Path.Combine(itemPathStr, folderName);

            //Check the project contain the folder or not.
            foreach (ProjectItem p in targetProjectItem.ProjectItems)
            {

                if (p.Name == folderName)
                {
                    if (!IOHelper.IsFolder(folderPath))
                    {
                        throw new Exception($"Fail to add the folder to \"{itemPath}\". Item \"{folderPath}\" exists and it is not a folder.");
                    }
                    pi = p;
                    break;
                }
            }

            if (pi != null)
            {
                //Contained, return
                return pi;
            }

            //Not Contained, check the disk is exists.
            var isExists = Directory.Exists(folderPath);
            if (isExists)
            {
                pi = targetProjectItem.ProjectItems.AddFromDirectory(folderPath);
            }
            else
            {
                pi = targetProjectItem.ProjectItems.AddFolder(folderName);
            }

            return pi;
        }

        public static ProjectItem AddFileToProjectItem(ProjectItem targetProjectItem, string filePath)
        {
            ProjectItem pi = targetProjectItem.ProjectItems.AddFromFile(filePath);

            return pi;
        }
    }
}
