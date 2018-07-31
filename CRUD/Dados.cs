using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD
{
    public class Dados
    {
        public static string StringDeConexao
        {
            get
            {
                return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\AndreTrust\Anhanguera\Trabalhos\C\CRUD\agenda.mdf;Integrated Security=True;Connect Timeout=30";
            }
        }
    }
}
