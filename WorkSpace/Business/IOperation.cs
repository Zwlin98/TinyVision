using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkSpace.Business
{
    public interface IOperation
    {
        string GetDescription();

        IItem GetItem();
    }

    public interface IItem
    {

    }
}
