using Licenta.Entityes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.ViewModel
{
    public class HomePageViewModel
    {
        public DbSet<Hotels> Hotels { get; set; }
        
    }
}
