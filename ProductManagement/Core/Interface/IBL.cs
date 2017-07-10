using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface
{
    public interface IBL
    {


        int SearchData(string searchCondition, out System.Data.DataTable dtResult);

        int UpdateData(IDTO updateDto);

        int InsertData(IDTO insertDto);

        int DeleteData(List<IDTO> listDto);

    }
}
