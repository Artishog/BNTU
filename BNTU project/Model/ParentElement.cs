using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNTU_project
{
    public abstract class ParentElement
    {
        public abstract object getByName(String name);
        public abstract void setByName(String name, object value);

        protected List<String> _inputPropertyList;
        protected List<String> _outputPropertyList;

        public List<String> inputPropertyList
        {
            get { return _inputPropertyList; }
        }

        public List<String> outputPropertyList
        {
            get { return _outputPropertyList; }
        }

        public ParentElement ShallowCopy()
        {
            return (ParentElement)this.MemberwiseClone();
        }

    }
}
