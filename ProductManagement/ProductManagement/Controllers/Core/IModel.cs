using System;

namespace ProductManagement.Controllers.Core
{
    public abstract class IModel
    {
        public int page_count { get; set; }

        public int page { get; set; } = 1;
    }
}
