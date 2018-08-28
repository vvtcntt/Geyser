using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Geyser.Models;
using Geyser.Models;
namespace Geyser.Models
{

    public class clsManufactures
    {
        public static GeyserContext db = new GeyserContext();

        public static int idManu(int id)
        {
            var listConnect = db.TblConnectManuProduct.Where(p => p.IdCate == id).Select(p => p.IdManu).Take(1).ToList();
            int idManu = int.Parse(listConnect[0].Value.ToString());

            return idManu;
        }
    }
}