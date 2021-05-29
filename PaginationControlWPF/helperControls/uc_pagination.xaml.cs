using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using helperControls.ViewModel;
using helperControls.Models;

namespace helperControls
{
    /// <summary>
    /// Interaction logic for uc_pagination.xaml
    /// </summary>
    public partial class uc_pagination : UserControl
    {
        public uc_pagination()
        {
            InitializeComponent();
        }
        
        private void tbxDisplayitems_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyboardDevice.IsKeyDown(Key.Enter))
                {
                    ((VMPagination)this.DataContext).calculatePagination();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
