using Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL.Dao.Product;
using DTO.Product;

namespace BL.Product
{
    public class ItemBL : IBL
    {
        public int DeleteData(List<IDTO> listDto)
        {
            throw new NotImplementedException();
        }

        public int InsertData(IDTO insertDto)
        {
            int returnCode = ItemDAO.InsertData(insertDto as ItemDTO);
            return returnCode;
        }

        public int SearchData(string itemName, out DataTable dtResult)
        {
            int returnCode = ItemDAO.SearchData(itemName, out dtResult);
            return returnCode;
        }

        public int UpdateData(IDTO updateDto)
        {
            throw new NotImplementedException();
        }
    }
}
