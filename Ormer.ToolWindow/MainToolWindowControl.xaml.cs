//------------------------------------------------------------------------------
// <copyright file="MainToolWindowControl.xaml.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Ormer.ToolWindow
{
    using EnvDTE;
    using Helper;
    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Shell.Interop;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for MainToolWindowControl.
    /// </summary>
    public partial class MainToolWindowControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainToolWindowControl"/> class.
        /// </summary>
        public MainToolWindowControl()
        {
            this.InitializeComponent();
        }

        private void btn_gen_now_Click(object sender, RoutedEventArgs e)
        {



            try
            {

                //EnvDTE.Project project = ProjectHelper.GetProject(@"ConsoleApplication2\ConsoleApplication2.csproj");
                //ProjectItem pi = ProjectHelper.AddFolderToProject(project, "MotherModelTest");

                //string filePath = @"D:\Learning\ConsoleApplication1\ConsoleApplication2\MotherModelTest\FileTest.cs";

                //if (!File.Exists(filePath))
                //{
                //    var fs = File.Create(filePath);
                //    fs.Dispose();

                //    File.WriteAllLines(filePath, new string[] { "test1", "test2" });
                //}

                ////添加完毕
                //var pi_newFile = ProjectHelper.AddFileToProjectItem(pi, filePath);

                //ProjectHelper.AddFolderToProjectItem(pi, "asfgh");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            

        }

        private void btn_setting_Click(object sender, RoutedEventArgs e)
        {
            var r1 = ProjectHelper.GetProjectList();

            SettingForm sf = new SettingForm();
            sf.ShowDialog();

        }
    }
}