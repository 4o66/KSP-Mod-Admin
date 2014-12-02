using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSPModAdmin.utils.TreeListViewEx
{
    class HitInfoEx
    {
        public Utils.CommonTools.Node Node { get; set; }

        public Utils.CommonTools.TreeListColumn Column { get; set; }

        public bool isCell { get; set; }

        public bool isRow { get; set; }

        public bool isColumn { get; set; }

        public bool isColumnHeader { get; set; }

        public bool isIndent { get; set; }

        public int RowIndex { get; set; }

        public bool isPlusMinus { get; set; }

        public bool isCheckBox { get; set; }
    }
}
