using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCShop.Models
{
    [Table("products")]
    public class product
    {
        [Key]
        public int f_product_id { get; set; }
        public string f_catnbr { get; set; }
        public string f_sku_code { get; set; }
    }

    public class ProductContext : DbContext
    {
        public DbSet<product> products { get; set; }
    }
}