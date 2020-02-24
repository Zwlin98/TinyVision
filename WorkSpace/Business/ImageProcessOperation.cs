using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkSpace.Business
{
    public class ImageProcessOperation:IOperation
    {
        private string _description;
        private MMat _imageMat;

        public ImageProcessOperation(string description,MMat imageMat)
        {
            _description = description;
            _imageMat = imageMat;
        }
        
        public string GetDescription()
        {
            return _description;
        }

        public IItem GetItem()
        {
            return _imageMat;
        }

    }
}
