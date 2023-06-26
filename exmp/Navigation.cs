using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace exmp
{
    public static class ProjectManager
    {
        public static Frame MainFrame { get; set; }
        public static DateTime nowdate { get; } = DateTime.Now.Date;
    }
}
