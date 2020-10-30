using System;
using System.Collections.Generic;
using System.Text;

namespace ProductsProject.Infrastructure.Data
{
    public interface IDBInitializer
    {
        void Initialize(Context context);
    }
}
