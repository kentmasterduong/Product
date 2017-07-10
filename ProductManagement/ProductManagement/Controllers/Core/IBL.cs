using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Controllers.Core
{
    public interface IBL
    {


        int SearchData(IModel model, out System.Data.DataTable dtResult);

        int UpdateData(IModel updateModel);

        int InsertData(IModel insertModel);

        int DeleteData(IModel listModel);

    }
}
