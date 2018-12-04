using BLL;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjectConfiguration
{
    public class NinjectConfig : NinjectModule
    {
        public override void Load()
        {
            Bind<BLL.ICategory_Operations>().To<BLL.Category_Operations>();
            Bind<BLL.ISubcategory_Operations>().To<BLL.Subcategory_Operations>();
            Bind<BLL.IRealEstate_Operations>().To<BLL.RealEstate_Operations>();
            Bind<BLL.IUser_Operations>().To<BLL.User_Operations>();
            Bind<Category_Operations>().ToSelf();
            Bind<Subcategory_Operations>().ToSelf();
            Bind<RealEstate_Operations>().ToSelf();
            Bind<User_Operations>().ToSelf();
        }
    }
}
