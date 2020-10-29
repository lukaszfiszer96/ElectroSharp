using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectroSharp.DA.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroSharp.DA
{
    public class DomainContext : DbContext
    {
        public DbSet<ElectronicElement> ElementsTable { get; set; }
        public DomainContext
            (string connectionString = "Data Source=LAPTOP-SIMKQKC3;Integrated Security=True;" +
            "                           Initial Catalog= ElectronicElementsDB;")
            : base(connectionString)
        {

        }
    }
}