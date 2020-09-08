using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace sal7lyAdmin
{
    public class validateException
    {
        public static string[] validate(Exception ex)
        {
            List<string> errors = new List<string>();
            var sqlException = ex.InnerException as System.Data.SqlClient.SqlException;

            if (sqlException.Number == 2601 || sqlException.Number == 2627)
            {
                errors.Add("Cannot insert duplicate values.");
            }
            else
            {
                errors.Add("Error while saving data.");
            }
            return errors.ToArray();
        }
    }
}
