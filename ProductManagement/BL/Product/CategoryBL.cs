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
    public class CategoryBL : IBL
    {
        public int DeleteData(List<IDTO> listDto)
        {
            throw new NotImplementedException();
        }

        public int InsertData(IDTO insertDto)
        {
            throw new NotImplementedException();
        }

        public int SearchData(string searchCondition, out DataTable dtResult)
        {
            throw new NotImplementedException();
        }

        public int SearchData(IDTO searchDto, out DataTable dtResult)
        {
            int returnCode = CategoryDAO.SearchData(searchDto as CategoryDTO, out dtResult);
            return returnCode;
        }

        public int UpdateData(IDTO updateDto)
        {
            throw new NotImplementedException();
        }
    }
}
