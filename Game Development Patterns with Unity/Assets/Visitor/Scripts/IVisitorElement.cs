using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter.Visitor
{
    public interface IVisitorElement
    {
        void Accept(IVisitor visitor);
    }
}
