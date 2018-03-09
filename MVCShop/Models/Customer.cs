using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCShop.Models
{
    [Table("customer_info")]
    public class customer
    {
        [Key]
        public int customer_id { get; set; }
        public string customer_name { get; set; }
        public string customer_type { get; set; }
        public string province{ get; set; }
        public string city { get; set; }
        public bool is_KA { get; set; }
    }

    [Table("prefecture")]
    public class address
    {
        [Key]
        public int id { get; set; }
        public string province { get; set; }
        public string city { get; set; }
    }
    public class CustomerContext : DbContext
    {
        public DbSet<customer> customers { get; set; }
        public DbSet<address> c_address { get; set; }
    }
}