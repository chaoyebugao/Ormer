namespace Ormer.ToolWindow.ToolWindow
{
    using Ormer.DatabaseFirst.MySql;
    using System;
    using System.Diagnostics.CodeAnalysis;
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

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                string.Format(System.Globalization.CultureInfo.CurrentUICulture, "Invoked '{0}'", this.ToString()),
                "MainToolWindow");
        }

        private void btn_gen_now_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var connectionString = "server=111.230.18.222;User Id=root;password=ehV3!rwfESEmbZh8;Database=cms;SslMode=None";
                var modelHelper = new MySqlModelConverter(connectionString);
                var list = modelHelper.GetModelClassStringList();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btn_setting_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}